using UnityEngine;
using R3;
using System;
using UnityEngine.UI;

public class LoopFadeEffect : MonoBehaviour
{
    [Header("페이드 아웃 시간")]
    [SerializeField]
    private float _fadeOutTime;

    // 페이드 아웃 현재 시간.
    private float _fadeOutCurrentTime;

    [Header("페이드 인 시간")]
    [SerializeField]
    private float _fadeInTime;

    // 페이드 인 현재 시간.
    private float _fadeInCurrentTime;
    
    private Text targetImage;

    void Awake()
    {
        // 대상이 될 Image 컴포넌트를 미리 찾아둡니다.
        targetImage = GetComponent<Text>();
    }

    void Start()
    {
        // 1. '페이드 인' 스트림을 만드는 방법을 정의합니다. (Defer를 사용해 구독 시점에 생성)
        var fadeIn = Observable.Defer(() =>
        {
            float startTime = Time.time;
            return Observable.EveryUpdate()
                .Select(_ => (Time.time - startTime) / _fadeInTime) // 진행률(0~1) 계산
                .TakeWhile(p => p <= 1.0f); // 1이 될 때까지 실행
        });        

        // 2. '페이드 아웃' 스트림을 만드는 방법을 정의합니다.
        var fadeOut = Observable.Defer(() =>
        {
            float startTime = Time.time;
            return Observable.EveryUpdate()
                .Select(_ => 1.0f - ((Time.time - startTime) / _fadeOutTime)) // 알파값(1~0) 계산
                .TakeWhile(a => a >= 0.0f); // 0이 될 때까지 실행
        });        

        // 3. 페이드 인 -> 페이드 아웃 순서로 스트림을 연결하고, 전체를 무한 반복시킵니다.
        fadeIn
            .Concat(fadeOut) // Concat: fadeIn이 끝나면 fadeOut을 시작
            .Subscribe(
                alpha => // 스트림에서 발행된 알파 값을 실제 이미지 색상에 적용
                {                    
                    Color newColor = targetImage.color;
                    newColor.a = Mathf.Clamp01(alpha); // 0과 1 사이 값으로 고정
                    targetImage.color = newColor;
                })
            .AddTo(this);
    }
}
