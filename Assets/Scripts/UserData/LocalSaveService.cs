using UnityEngine;

public class LocalSaveService : ISaveService
{
    // 세이브 키.
    private const string kSAVE_KEY = "SAVE";

    public void Save(string json)
    {
        PlayerPrefs.SetString(kSAVE_KEY, json);
        PlayerPrefs.Save();
    }

    public string Load()
    {
        if (PlayerPrefs.HasKey(kSAVE_KEY) == false)
        {
            Debug.LogError($"{kSAVE_KEY} 데이터가 존재하지 않음");
                        
            return null;
        }
        
        return PlayerPrefs.GetString(kSAVE_KEY);
    }    
}
