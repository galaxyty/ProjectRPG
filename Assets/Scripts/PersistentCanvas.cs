using UnityEngine;

// 씬 넘어가도 유지되는 캔버스.
public class PersistentCanvas : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
