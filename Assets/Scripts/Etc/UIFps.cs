using UnityEngine;
using UnityEngine.UI;

public class UIFps : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    private float _deltaTime;

    void Update()
    {
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        float fps = 1.0f / _deltaTime;
        _text.text = $"{Mathf.Ceil(fps)}";
    }
}
