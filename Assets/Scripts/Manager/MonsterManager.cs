using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterManager : BaseObjectSingleton<MonsterManager>
{
    // 몬스터 프리팹 캐시용.
    private Dictionary<int, GameObject> _monsterPrefabs = new();

    // 몬스터 풀.
    private Dictionary<GameObject, ObjectPool<BaseMonster>> _poolDic = new();

    // 활성화 된 몬스터.
    private Dictionary<Enums.MonsterType, List<BaseMonster>> _activeMonsterDic = new();

    // 하이어라키 그룹.
    private Dictionary<Enums.MonsterType, GameObject> _hierachyDic = new();

    private bool _isReady = false;

    // 몬스터 최대 수.
    private const int kMAX_MONSTER_COUNT = 3;

    /// <summary>
    /// 초기화 완료 여부.
    /// </summary>
    public bool IsReady
    {
        private set { }
        get { return _isReady; }
    }

    // 초기화.
    public async UniTask Initialization(List<MonsterGroupTableData> list)
    {
        if (list == null)
        {
            Debug.LogError("MonsterGroupTableData 테이블 데이터가 비어있음");
            return;
        }

        // 몬스터 테이블에서 모든 몬스터를 프리팹 생성한다음 캐싱.
        foreach (var monster in list)
        {
            _monsterPrefabs[monster.INDEX] = await ResourceManager.Instance.LoadAsync<GameObject>(Consts.MonsterKeyMap[monster.INDEX]);
        }

        // 각 몬스터 키를 기준으로 풀링 생성.
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
    /// 해당 인덱스 몬스터 스폰.
    /// </summary>
    public BaseMonster Spawn(int index)
    {
        // 몬스터 소환 수 확인.
        int totalMonsterCount = _activeMonsterDic.Values.Sum(list => list.Count);

        if (kMAX_MONSTER_COUNT <= totalMonsterCount)
        {
            return null;
        }

        // 풀에서 해당 몬스터 키를 가져온다.
        var pool = _poolDic[_monsterPrefabs[index]];

        // 풀에 있는지 null 체크.
        if (pool == null)
        {
            Debug.Log($"{name} 이(가) _poolDic 키에 존재하지 않음");
            return null;
        }

        // 해당 몬스터가 있다면 풀에서 가져옴.
        var monster = pool.Get();

        // null 체크.
        if (monster == null)
        {
            Debug.Log($"{monster} 이(가) 풀링에 없어서 못 가져옴");
            return null;
        }        

        // 활성화 딕셔너리 추가.
        _activeMonsterDic[monster.Type].Add(monster);

        // 해당 하이어라키 창에 생성.
        monster.transform.parent = _hierachyDic[monster.Type].transform;

        return monster;
    }

    /// <summary>
    /// 몬스터 사망.
    /// </summary>
    public void Die(BaseMonster monster, int index)
    {        
        var pool = _poolDic[_monsterPrefabs[index]];

        if (pool == null)
        {
            Debug.LogError($"{monster} 이(가) _poolDic 키에 존재하지 않음");
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
