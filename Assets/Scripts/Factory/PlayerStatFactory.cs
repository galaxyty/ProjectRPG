using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatFactory : IFactory
{
    public async UniTask CreateAsync()
    {
        Debug.Log("PlayerStatFactory ÆÑÅä¸® »ý¼º");        

        // Äµ¹ö½º Root.
        Transform uiRoot = UIManager.Instance.GetRoot(UIManager.CanvasType.UI);

        // ·Îºñ ¾À¿¡¼­ »ý¼º ½ÃÅ³ ÇÁ¸®ÆÕ.
        GameObject loadPrefab = await ResourceManager.Instance.LoadAsync<GameObject>(Consts.kPATH_PLAYER_STAT_VIEW);

        // ¸ðµ¨ »ý¼º µ¥ÀÌÅÍ.    
        TextAsset json = await ResourceManager.Instance.LoadAsync<TextAsset>(Consts.kPATH_JSON_TEST);
        List<TestData> list = JsonConvert.DeserializeObject<List<TestData>>(json.text);

        foreach (var data in list)
        {
            // ÇÁ¸®ÆÕ »ý¼º.
            GameObject prefab = Object.Instantiate(loadPrefab, uiRoot);

            // ºä.
            PlayerStatView playerStatView = prefab.GetComponent<PlayerStatView>();

            // ¸ðµ¨.
            PlayerStatModel model = new(data);

            await model.InitializationAsync();

            // ÇÁ·¹Á¨Æ®.
            PlayerStatPresenter playerStatPresenter = new(model);

            playerStatPresenter.SetView(playerStatView);
            playerStatPresenter.SetModel(model);
            playerStatPresenter.Initialization();
        }        
    }
}
