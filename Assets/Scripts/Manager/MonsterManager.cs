using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class MonsterManager : BaseObjectSingleton<MonsterManager>
{
    // 몬스터 오브젝트.
    private GameObject _monster;

    // 몬스터 풀.
    private ObjectPool<BaseMonster> _pool;

    // 활성화 된 몬스터.
    private List<BaseMonster> _activeMonster = new();

    private bool _isReady = false;

    public bool IsReady
    {
        private set { }
        get { return _isReady; }
    }

    // 초기화.
    public async UniTask Initialization()
    {
        _monster = await ResourceManager.Instance.LoadAsync<GameObject>(Consts.kPATH_MONSTER_THIEF);

        _pool = new ObjectPool<BaseMonster>(
            OnCreatePool,
            OnGetPool,
            OnReturnPool,
            OnDestroyPool
            );

        _isReady = true;
    }

    /// <summary>
    /// 몬스터 스폰.
    /// </summary>
    public void Spawn()
    {
        var monster = _pool.Get();

        monster.Initialization();

        _activeMonster.Add(monster);
    }

    /// <summary>
    /// 몬스터 사망.
    /// </summary>
    public void Die(BaseMonster monster)
    {
        _pool.Release(monster);
        _activeMonster.Remove(monster);
    }

    /// <summary>
    /// 가까운 몬스터 반환.
    /// </summary>    
    public BaseMonster GetNearTarget(Vector3 transform)
    {
        // null 체크.
        if (_activeMonster == null || _activeMonster.Count <= 0)
        {
            return null;
        }

        // 타겟.
        BaseMonster target = null;

        // 현재 검색 된 최소 거리.
        float nearDistance = 0.0f;

        // 가장 가까운 대상 탐색.
        foreach (var monster in _activeMonster)
        {
            // 거리.
            float distance = Vector3.Distance(transform, monster.transform.position);

            // 현재 대상의 거리가 최소 거리보다 가까운지 확인.
            if (nearDistance > distance || nearDistance <= 0.0f)
            {
                // 최소 거리 갱신.
                nearDistance = distance;

                // 가까운 타겟 지정.
                target = monster;
            }
        }

        return target;
    }

    // 풀 생성 콜백 함수.
    private BaseMonster OnCreatePool()
    {
        Debug.Log("몬스터 풀 생성");

        var obj = Instantiate(_monster);
        return obj.GetComponent<BaseMonster>();
    }

    // 풀에서 꺼낼 시 콜백 함수.
    private void OnGetPool(BaseMonster monster)
    {
        Debug.Log($"몬스터 풀에서 꺼냄 : {_pool.CountActive}개");
        monster.gameObject.SetActive(true);
    }

    // 풀로 반환 시 콜백 함수.
    private void OnReturnPool(BaseMonster monster)
    {
        Debug.Log($"몬스터 풀로 반환 : {_pool.CountActive}개");
        monster.gameObject.SetActive(false);
    }

    // 풀에서 제거 시 콜백 함수.
    private void OnDestroyPool(BaseMonster monster)
    {
        Debug.Log("몬스터 풀에서 제거");
        Destroy(monster);
    }
}
