using System.Collections.Generic;

public class StatTableDataRepository
{
    private Dictionary<int, StatTableData> _datas = new();

    /// <summary>
    /// 리포지토리 모델 추가.
    /// </summary>
    public void Add(int index, StatTableData data)
    {
        _datas.Add(index, data);
    }

    /// <summary>
    /// 해당 Index 모델 반환.
    /// </summary>
    public StatTableData Get(int index) => _datas[index];
}
