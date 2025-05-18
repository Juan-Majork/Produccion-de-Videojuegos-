using UnityEngine;

public class DestroyRock : MonoBehaviour
{
    [SerializeField] private float destroyTime;

    public void DestroyTime()
    {
        Destroy(gameObject, destroyTime);
    }
}
