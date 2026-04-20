using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using R3;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField]
    [Header("몬스터 스폰 범위")]
    private float _spawnRadius;

    [SerializeField]
    [Header("몬스터 스폰 시간 주기")]
    private double _spawnTime;

    // 구독 해제.
    private IDisposable _dispose;

    async UniTask Start()
    {
        // 몬스터 매니저 초기화 끝난 다음 실행 시킬 것.
        await UniTask.WaitUntil(() => MonsterManager.Instance.IsReady);

        _dispose = Observable
        .Timer(TimeSpan.Zero, TimeSpan.FromSeconds(_spawnTime)) // 첫 실행까지 걸리는 시간, 이후 반복 시간.
        .Subscribe(_ =>
        {
            Spawn();
            Spawn();
            Spawn();
            Spawn();
            Spawn();
        });
    }

    private void Spawn()
    {
        var monster = MonsterManager.Instance.Spawn(Consts.kPATH_MONSTER_THIEF);

        if (monster == null)
        {
            Debug.LogError("몬스터 스폰 반환이 정상적으로 되지 않았습니다");
        }

        // 몬스터 스폰 위치.
        float mixX = transform.position.x - _spawnRadius;
        float mixY = transform.position.y - _spawnRadius;
        float maxX = transform.position.x + _spawnRadius;
        float maxY = transform.position.y + _spawnRadius;

        monster.transform.position = new Vector3(UnityEngine.Random.Range(mixX, maxX), UnityEngine.Random.Range(mixY, maxY), 0);
    }

    void OnDestroy()
    {
        _dispose?.Dispose();
    }

    private void OnDrawGizmos()
    {
        // 스픈 범위 시각화.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
    }
}
