using Cysharp.Threading.Tasks;
using R3;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatModel : BaseModel
{
    // 레벨.
    public ReactiveProperty<int> CurrentLevel = new();

    // 현재 체력.
    public ReactiveProperty<int> CurrentHP = new();

    // 최대 체력.
    public ReactiveProperty<int> MaxHP = new();

    // 현재 경험치.
    public ReactiveProperty<int> CurrentEXP = new();

    // 현재 레벨 기준 목표 도달 경험치.
    public ReactiveProperty<int> MaxEXP = new();

    // 스탯 테이블.
    private List<StatTableData> _statTableDatas;

    public PlayerStatModel(StatUserData userData, StatTableData tableData)
    {
        CurrentLevel = userData.Level;
        CurrentHP = userData.HP;
        CurrentEXP = userData.EXP;

        MaxHP.Value = tableData.HP;
        MaxEXP.Value = tableData.EXP;
    }

    public override UniTask InitializationAsync()
    {
        _statTableDatas = TableManager.Instance.StatTableDatas;

        if (_statTableDatas == null)
        {
            Debug.LogError("스탯 테이블이 비어있음");
            return UniTask.CompletedTask;
        }

        // 데이터 바인딩.
        CurrentLevel
            .Skip(1)
            .Subscribe(level =>
            {
                Debug.Log($"플레이어 레벨업 : {level}");
            });

        CurrentEXP
            .Subscribe(AddEXP);

        return UniTask.CompletedTask;
    }

    // 경험치 획득.
    private void AddEXP(int exp)
    {
        // 목표 경험치.
        if (_statTableDatas == null)
        {
            Debug.LogError("_statTableDatas 테이블 데이터가 존재하지 않음");
            return;
        }

        var data = _statTableDatas.Find(data => data.LEVEL == CurrentLevel.Value);

        if (data == null)
        {
            CurrentEXP.Value = 0;
            return;
        }

        // 저장 가능 상태로 변경
        // TODO :: 이 코드 뭔가 마음에 안들어서 추후 고칠 것.
        DataManager.Instance.IsDirty = true;

        // 레벨업 목표 경험치.
        int maxEXP = data.EXP;

        // 레벨업 조건 확인.
        if (CurrentEXP.Value >= maxEXP)
        {
            // 남은 경험치.
            int remainingEXP = CurrentEXP.Value - maxEXP;

            // 레벨업.
            LevelUP(1, remainingEXP);

            // 다시 레벨업 해야하는지 확인.
            data = _statTableDatas.Find(data => data.LEVEL == CurrentLevel.Value);

            if (data == null)
            {
                CurrentEXP.Value = 0;
                return;
            }

            if (remainingEXP >= data.EXP)
            {
                AddEXP(remainingEXP);
            }
        }
    }

    // 레벨업.
    private void LevelUP(int level, int remainingEXP = 0)
    {
        // 저장 가능 상태로 변경.
        DataManager.Instance.IsDirty = true;

        CurrentLevel.Value += level;

        // 남은 경험치로 셋팅.
        CurrentEXP.Value = remainingEXP;
    }
}
