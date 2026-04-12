using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterManager : BaseObjectSingleton<MonsterManager>
{
    // 몬스터 프리팹 캐시용.
    private Dictionary<string, GameObject> _monsterPrefabs = new();

    // 몬스터 풀.
    private Dictionary<GameObject, ObjectPool<BaseMonster>> _poolDic = new();

    // 활성화 된 몬스터.
    private Dictionary<Enums.MonsterType, List<BaseMonster>> _activeMonsterDic = new();

    // 하이어라키 그룹.
    private Dictionary<Enums.MonsterType, GameObject> _hierachyDic = new();

    private bool _isReady = false;

    /// <summary>
    /// 초기화 완료 여부.
    /// </summary>
    public bool IsReady
    {
        private set { }
        get { return _isReady; }
    }

    // 초기화.
    public async UniTask Initialization()
    {
        // 몬스터 프리팹.
        _monsterPrefabs[Consts.kPATH_MONSTER_THIEF] = await ResourceManager.Instance.LoadAsync<GameObject>(Consts.kPATH_MONSTER_THIEF);

        // 풀링 생성.
        foreach (var prefab in _monsterPrefabs.Values)
        {
            _poolDic[prefab] = new ObjectPool<BaseMonster>(
                () => OnCreatePool(prefab),
                (monster) => OnGetPool(monster, _poolDic[prefab]),
                (monster) => OnReturnPool(monster, _poolDic[prefab]),
                OnDestroyPool
            );
        }

        // 활성화 딕셔너리 생성.
        _activeMonsterDic[Enums.MonsterType.Boss] = new();
        _activeMonsterDic[Enums.MonsterType.Elite] = new();
        _activeMonsterDic[Enums.MonsterType.Normal] = new();

        // 하이어라키 창 생성.
        var group = new GameObject("MonsterPoolGroup");
        var boss = new GameObject("MonsterBoss");
        var elite = new GameObject("MonsterElite");
        var normal = new GameObject("MonsterNormal");

        group.transform.position = Vector3.zero;

        boss.transform.parent = group.transform;
        elite.transform.parent = group.transform;
        normal.transform.parent = group.transform;

        _hierachyDic[Enums.MonsterType.Boss] = boss;
        _hierachyDic[Enums.MonsterType.Elite] = elite;
        _hierachyDic[Enums.MonsterType.Normal] = normal;

        // 준비 완료.
        _isReady = true;
    }

    /// <summary>
    /// 해당 타입 몬스터 스폰.
    /// </summary>
    public void Spawn(string name)
    {
        var pool = _poolDic[_monsterPrefabs[name]];

        if (pool == null)
        {
            Debug.Log($"{name} 이(가) _poolDic 키에 존재하지 않음");
            return;
        }

        // 풀에서 가져옴.
        var monster = _poolDic[_monsterPrefabs[name]].Get();

        if (monster == null)
        {
            Debug.Log($"{monster} 이(가) 풀링에 없어서 못 가져옴");
            return;
        }

        // 초기화.
        monster.Initialization();

        // 활성화 딕셔너리 추가.
        _activeMonsterDic[monster.Type].Add(monster);

        // 해당 하이어라키 창에 생성.
        monster.transform.parent = _hierachyDic[monster.Type].transform;
    }

    /// <summary>
    /// 몬스터 사망.
    /// </summary>
    public void Die(BaseMonster monster, string name)
    {        
        var pool = _poolDic[_monsterPrefabs[name]];

        if (pool == null)
        {
            Debug.Log($"{name} 이(가) _poolDic 키에 존재하지 않음");
            return;
        }

        // 풀로 반환.
        pool.Release(monster);

        // 활성화 딕셔너리에서 제거.
        _activeMonsterDic[monster.Type].Remove(monster);
    }

    /// <summary>
    /// 가까운 몬스터 반환.
    /// </summary>
    public BaseMonster GetNearTarget(Vector3 transform)
    {
        if (_activeMonsterDic == null || _activeMonsterDic.Count <= 0)
        {
            Debug.Log("_activeMonsterDic가 비어있음");
            return null;
        }

        // 타겟.
        BaseMonster target = null;

        // 현재 검색 된 최소 거리.
        float nearDistance = 0.0f;

        foreach (var (type, monsters) in _activeMonsterDic)
        {
            // 탐색 여부 확인.
            bool isSearch = false;

            foreach (var monster in monsters)
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

                isSearch = true;
            }

            if (isSearch == true)
            {
                break;
            }
        }

        return target;
    }

    // 풀 생성 콜백 함수.
    private BaseMonster OnCreatePool(GameObject prefab)
    {
        Debug.Log("몬스터 풀 생성");

        var obj = Instantiate(prefab);
        return obj.GetComponent<BaseMonster>();
    }

    // 풀에서 꺼낼 시 콜백 함수.
    private void OnGetPool(BaseMonster monster, ObjectPool<BaseMonster> pool)
    {
        Debug.Log($"몬스터 풀에서 꺼냄 : {pool.CountActive}개");
        monster.gameObject.SetActive(true);
    }

    // 풀로 반환 시 콜백 함수.
    private void OnReturnPool(BaseMonster monster, ObjectPool<BaseMonster> pool)
    {
        Debug.Log($"몬스터 풀로 반환 : {pool.CountActive}개");
        monster.gameObject.SetActive(false);
    }

    // 풀에서 제거 시 콜백 함수.
    private void OnDestroyPool(BaseMonster monster)
    {
        Debug.Log("몬스터 풀에서 제거");
        Destroy(monster);
    }
}
