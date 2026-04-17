using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class DataManager : BaseObjectSingleton<DataManager>
{
    // 스탯 데이터.
    public StatUserData StatUserData { get; private set; } = new();

    // 재화 데이터.
    public CurrencyUserData CurrencyUserData { get; private set; } = new();

    // 세이브 데이터.
    private SaveData _saveData = new();

    // 세이브 키.
    private const string kSAVE_KEY = "SAVE";

    // 로컬 자동 저장 시간 주기 (초).
    private const int kSAVE_COUNT = 5;

    /// <summary>
    /// 저장 가능한 상태.
    /// </summary>
    public bool IsDirty = false;

    /// <summary>
    /// 데이터 저장
    /// </summary>
    public void Save()
    {
        // 저장 여부 가능한 상태인지 확인.
        if (IsDirty == false)
        {
            return;
        }

        // 함수를 추가하여 데이터 저장을 구현할 것.
        SaveStat();         // 스탯 저장.
        SaveCurrency();     // 재화 저장.

        // C# -> Json으로 직렬화 시켜 로컬 저장.
        var json = JsonConvert.SerializeObject(_saveData);

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

            // 1레벨 데이터로 셋팅.
            StatUserData.InitFirstData();
            return;
        }

        // Json -> C#으로 역직렬화 시키고 C# 코드로 쓸 수 있게 함.
        var json = PlayerPrefs.GetString(kSAVE_KEY);
        var data = JsonConvert.DeserializeObject<SaveData>(json);

        // 함수를 추가하여 데이터 로드를 구현할 것.
        LoadStat(data);         // 스탯 로드.
        LoadCurrency(data);     // 재화 로드.

        Debug.Log($"데이터 로드 완료");
    }

    /// <summary>
    /// 초기화.
    /// </summary>
    public override UniTask InitializationAsync()
    {
        StatUserData.Initialization();
        CurrencyUserData.Initialization();

        // 자동 저장 초기화 루프 동작.
        AutoSaveLoop().Forget();

        return UniTask.CompletedTask;
    }

    /// <summary>
    /// 정리.
    /// </summary>
    public override UniTask DisposeAsync()
    {
        StatUserData.Dispose();
        CurrencyUserData.Dispose();

        return UniTask.CompletedTask;
    }

    // 일정 시간마다 로컬 자동 저장.    
    private async UniTask AutoSaveLoop()
    {
        while (true)
        {
            await UniTask.Delay(kSAVE_COUNT * 1000);
            OnAutoSave();
        }
    }

    // 자동 저장 함수.
    private void OnAutoSave()
    {
        if (IsDirty == false)
        {
            return;
        }

        Save();
    }

    #region Data Save Functions

    // 스탯 저장.
    private void SaveStat()
    {
        _saveData.StatSaveData.LEVEL = StatUserData.Level.CurrentValue;
        _saveData.StatSaveData.HP = StatUserData.HP.CurrentValue;
        _saveData.StatSaveData.EXP = StatUserData.EXP.CurrentValue;
    }

    // 재화 저장.
    private void SaveCurrency()
    {
        _saveData.CurrencySaveData.GOLD = CurrencyUserData.Gold.CurrentValue;
    }

    #endregion

    #region Data Load Functions

    // 스탯 로드.
    private void LoadStat(SaveData data)
    {
        StatUserData.Level.Value = data.StatSaveData.LEVEL;
        StatUserData.HP.Value = data.StatSaveData.HP;
        StatUserData.EXP.Value = data.StatSaveData.EXP;
    }

    // 재화 로드.
    private void LoadCurrency(SaveData data)
    {
        CurrencyUserData.Gold.Value = data.CurrencySaveData.GOLD;
    }

    #endregion

    void OnApplicationPause(bool pause)
    {
        if (pause == true)
        {
            Save();
        }
    }

    void OnApplicationQuit()
    {
        Save();
    }
}
