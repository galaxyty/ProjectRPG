using R3;
using UnityEngine;

public static class BGMSoundBus
{
    public static readonly Subject<AudioClip> OnBGM = new();
}
