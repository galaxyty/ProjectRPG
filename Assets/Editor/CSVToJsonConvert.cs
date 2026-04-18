using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;

public class CSVToJsonConvert : EditorWindow
{
    string csvFolderPath = "Data/CSV";
    string jsonFolderPath = "Data/JSON";

    [MenuItem("Tools/CSV Folder To JSON")]
    static void Open()
    {
        GetWindow<CSVToJsonConvert>("CSV Folder To JSON");
    }

    void OnGUI()
    {
        GUILayout.Label("CSV Folder → JSON Converter", EditorStyles.boldLabel);

        csvFolderPath = EditorGUILayout.TextField("CSV Folder", csvFolderPath);
        jsonFolderPath = EditorGUILayout.TextField("JSON Folder", jsonFolderPath);

        if (GUILayout.Button("Convert All CSV"))
        {
            ConvertAll();
        }
    }

    void ConvertAll()
    {
        if (!Directory.Exists(csvFolderPath))
        {
            Debug.LogError("CSV 폴더 없음");
            return;
        }

        if (!Directory.Exists(jsonFolderPath))
        {
            Directory.CreateDirectory(jsonFolderPath);
        }

        string[] files = Directory.GetFiles(csvFolderPath, "*.csv");

        foreach (string file in files)
        {
            ConvertFile(file);
        }

        AssetDatabase.Refresh();
        Debug.Log("전체 CSV → JSON 변환 완료!");
    }

    void ConvertFile(string path)
    {
        string[] lines = File.ReadAllLines(path);

        if (lines.Length < 3)
        {
            Debug.LogWarning($"스킵 (형식 오류): {path}");
            return;
        }

        string[] headers = lines[0].Split(',');
        string[] types = lines[1].Split(',');

        List<Dictionary<string, object>> result = new();

        for (int i = 2; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Split(',');

            Dictionary<string, object> row = new();
            Dictionary<string, List<object>> arrayMap = new();

            for (int j = 0; j < headers.Length; j++)
            {
                string key = headers[j];
                string type = types[j];
                string value = values[j];

                object parsed = ParseValue(value, type);

                // 배열 처리 (_0, _1)
                var split = key.Split('_');

                // 마지막이 _숫자인지 확인.
                if (split.Length >= 2 && int.TryParse(split[split.Length - 1], out _))
                {
                    // 배열로 나눔.
                    string baseKey = "";

                    // 뒷 부분 숫자빼고 이름 다시 조합.
                    for (int x = 0; x < split.Length - 1; x++)
                    {
                        if (x >= 1)
                        {
                            baseKey += "_";
                        }

                        baseKey += split[x];
                    }

                    if (arrayMap.ContainsKey(baseKey) == false)
                    {
                        arrayMap[baseKey] = new List<object>();
                    }

                    arrayMap[baseKey].Add(parsed);
                }
                else
                {
                    row[key] = parsed;
                }
            }

            foreach (var arr in arrayMap)
                row[arr.Key] = arr.Value;

            result.Add(row);
        }

        string fileName = Path.GetFileNameWithoutExtension(path);
        string jsonPath = Path.Combine(jsonFolderPath, fileName + ".json");

        string json = JsonConvert.SerializeObject(result, Formatting.Indented);

        File.WriteAllText(jsonPath, json);

        Debug.Log($"변환 완료: {fileName}");
    }

    object ParseValue(string value, string type)
    {
        switch (type)
        {
            case "int":
                return int.TryParse(value, out var i) ? i : 0;

            case "float":
                return float.TryParse(value, out var f) ? f : 0f;

            case "bool":
                return bool.TryParse(value, out var b) && b;

            default:
                return value;
        }
    }
}
