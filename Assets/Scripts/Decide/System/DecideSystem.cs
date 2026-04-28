using System.Collections.Generic;

// 상태 변경 담당하는 클래스.
public class DecideSystem
{
    private List<(IDecide, Enums.eSTATE)> _rules = new();

    /// <summary>
    /// 해당 조건에 맞는 상태 추가.
    /// </summary>
    public void AddRule(IDecide decide, Enums.eSTATE state)
    {
        _rules.Add((decide, state));
    }

    /// <summary>
    /// 조건에 맞는 상태 반환.
    /// </summary>
    public Enums.eSTATE DecideState(BaseCharacter character)
    {
        foreach (var (rule, state) in _rules)
        {
            if (rule.Decide(character) == true)
            {
                return state;
            }
        }

        // 없으면 대기 상태 반환.
        return Enums.eSTATE.Idle;
    }
}
