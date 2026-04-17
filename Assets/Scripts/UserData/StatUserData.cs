using System.Collections.Generic;
using R3;

public class StatUserData : BaseUserData
{
    /// <summary>
    /// 레벨.
    /// </summary>
    public ReactiveProperty<int> Level { get; private set; } = new();

    /// <summary>
    /// 체력.
    /// </summary>
    public ReactiveProperty<int> HP { get; private set; } = new();

    /// <summary>
    /// 경험치.
    /// </summary>
    public ReactiveProperty<int> EXP { get; private set; } = new();

    /// <summary>
    /// 경험치 획득 이벤트.
    /// </summary>
    public Subject<int> OnAddEXP = new();

    // 스탯 테이블.
    private List<StatTableData> _statTableDatas;

    public override void Initialization()
    {
        OnAddEXP
            .Subscribe(AddEXP)
            .AddTo(_dispose);

        _statTableDatas = TableManager.Instance.StatTableDatas;
    }

    /// <summary>
    /// 1레벨 데이터로 셋팅.
    /// </summary>
    public void InitFirstData()
    {
        var data = _statTableDatas[0];

        if (data == null)
        {
            return;
        }

        Level.Value = data.LEVEL;
        HP.Value = data.HP;
    }

    // 경험치 획득.
    private void AddEXP(int exp)
    {        
        // 목표 경험치.
        var data = _statTableDatas.Find(data => data.LEVEL == Level.Value);

        if (data == null)
        {
            EXP.Value = 0;
            return;
        }

        // 저장 가능 상태로 변경.
        DataManager.Instance.IsDirty = true;

        // 레벨업 목표 경험치.
        int maxEXP = data.EXP;

        // 경험치 증가.
        EXP.Value += exp;        

        // 레벨업 조건 확인.
        if (EXP.Value >= maxEXP)
        {           
            // 남은 경험치.
            int remainingEXP = EXP.Value - maxEXP;

            // 레벨업.
            LevelUP(1, remainingEXP);

            // 다시 레벨업 해야하는지 확인.
            data = _statTableDatas.Find(data => data.LEVEL == Level.Value);

            if (data == null)
            {
                EXP.Value = 0;
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
        Level.Value += level;

        // 남은 경험치로 셋팅.
        EXP.Value = remainingEXP;

        // 저장 가능 상태로 변경.
        DataManager.Instance.IsDirty = true;
    }
}
