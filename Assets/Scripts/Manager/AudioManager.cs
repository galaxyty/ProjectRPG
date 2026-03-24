using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseObjectSingleton<AudioManager>
{
    // 싱글톤 초기화 여부.
    private bool _isInit = false;

    // 오디오 캐싱.
    private Dictionary<string, AudioClip> _audioCache = new();

    /// <summary>
    /// 오디오 매니저 초기화.
    /// </summary>
    public async UniTask InitializationAsync()
    {
        if (_isInit == true) return;

        _audioCache.Add(Consts.kAUDIO_MAIN, await ResourceManager.Instance.LoadAsyncToResource<AudioClip>(Consts.kAUDIO_MAIN));

        _isInit = true;
    }

    /// <summary>
    /// 오디오 클립 반환.
    /// </summary>
    public AudioClip GetClip(string key)
    {
        return _audioCache[key];
    }
}
