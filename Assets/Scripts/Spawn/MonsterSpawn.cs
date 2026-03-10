using Cysharp.Threading.Tasks;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{    
    async UniTask Start()
    {
        // 몬스터 매니저 초기화 끝난 다음 실행 시킬 것.
        await UniTask.WaitUntil(() => MonsterManager.Instance.IsReady);
        MonsterManager.Instance.Spawn(Consts.kPATH_MONSTER_THIEF);
        MonsterManager.Instance.Spawn(Consts.kPATH_MONSTER_THIEF);
        MonsterManager.Instance.Spawn(Consts.kPATH_MONSTER_THIEF);
    }
}
