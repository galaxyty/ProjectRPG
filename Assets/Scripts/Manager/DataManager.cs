using UnityEngine;

// TODO :: 재로그인 시 데이터들 이벤트 구독한거 취소 안해준게 문제 될 수 있으니
// 추후 이 문제 고려해볼 것
public class DataManager : BaseObjectSingleton<DataManager>
{
    // 스탯 데이터.
    public StatData StatData = new();

    // 재화 데이터.
    public CurrencyData CurrencyData = new();

    // 세이브 데이터.
    private SaveData _saveData = new();

    // 세이브 키.
    private const string kSAVE_KEY = "SAVE";

    /// <summary>
    /// 데이터 저장
    /// </summary>
    public void Save()
    {
        SaveStat();         // 스탯 저장.
        SaveCurrency();     // 재화 저장.

        // C# -> Json으로 직렬화 시켜 로컬 저장.
        var json = JsonUtility.ToJson(_saveData);
        PlayerPrefs.SetString(kSAVE_KEY, json);
        PlayerPrefs.Save();

        Debug.Log($"데이터 저장 완료 : {json}");
    }

    /// <summary>
    /// 데이터 불러오기
    /// </summary>
    public void Load()
    {
        if (PlayerPrefs.HasKey(kSAVE_KEY) == false)
        {
            Debug.LogError($"{kSAVE_KEY} 데이터가 존재하지 않음");
            return;
        }

        // Json -> C#으로 역직렬화 시키고 C# 코드로 쓸 수 있게 함.
        var json = PlayerPrefs.GetString(kSAVE_KEY);
        var data = JsonUtility.FromJson<SaveData>(json);

        LoadStat(data);         // 스탯 로드.
        LoadCurrency(data);     // 재화 로드.

        Debug.Log($"데이터 로드 완료");
    }

    #region Data Save Functions

    // 스탯 저장.
    private void SaveStat()
    {
        _saveData.HP = StatData.HP.Value;
    }

    // 재화 저장.
    private void SaveCurrency()
    {
        _saveData.Gold = CurrencyData.Gold.Value;
    }

    #endregion

    #region Data Load Functions

    // 스탯 로드.
    private void LoadStat(SaveData data)
    {
        StatData.HP.Value = data.HP;
    }

    // 재화 로드.
    private void LoadCurrency(SaveData data)
    {
        CurrencyData.Gold.Value = data.Gold;
    }

    #endregion
}
