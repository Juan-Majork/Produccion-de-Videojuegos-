using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestroy = 5f;

    private float timer = 0f;

    private void Update()
    {
        // Suma el tiempo mientras
        timer += Time.deltaTime;

        // Si el tiempo ha superado el limite, destruye el objeto
        if (timer >= timeBeforeDestroy)
        {
            Destroy(gameObject);
        }
    }
}
