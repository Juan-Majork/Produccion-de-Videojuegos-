using UnityEngine;
using UnityEngine.Events;

public class BossManaTransfer : MonoBehaviour
{
    private void Awake()
    {
        PrepareBossScene.Invoke();
    }

    public UnityEvent PrepareBossScene;
}
