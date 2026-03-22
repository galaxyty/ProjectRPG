using Cysharp.Threading.Tasks;
using UnityEngine;
using R3;

public class BGMSoundSystem : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    private void Awake()
    {        
        BGMSoundBus.OnBGM
            .Subscribe((clip) =>
            {
                _audioSource.clip = clip;
                _audioSource.Play();
            })
            .AddTo(this);                
    }
}
