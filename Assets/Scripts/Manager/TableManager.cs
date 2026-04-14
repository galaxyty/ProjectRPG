using Cysharp.Threading.Tasks;
using UnityEngine;

public class TableManager : BaseObjectSingleton<TableManager>
{
    private PlayerHPModelRepository _playerHPModelRepository = new();

    /// <summary>
    /// 테이블 매니저 초기화.
    /// </summary>
    public async UniTask InitializationAsync()
    {
        await LoadPlayerHP();
    }

    // Player HP 테이블 데이터 로드.
    private async UniTask LoadPlayerHP()
    {

    }
}