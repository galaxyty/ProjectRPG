using System;

[Serializable]
public class SaveData
{
    public StatSaveData StatSaveData = new();

    public CurrencySaveData CurrencySaveData = new();

    public StageSaveData StageSaveData = new();
}
