using Cysharp.Threading.Tasks;
using UnityEngine;
using R3;

public class BGMSoundSystem : MonoBehaviour
{
    private void Awake()
    {
        BGMSoundBus.OnBGM
            .Subscribe((_) =>
            {
                
            })
            .AddTo(this);
    }
}
