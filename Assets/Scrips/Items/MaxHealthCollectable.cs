using UnityEngine;

public class MaxHealthCollectable : MonoBehaviour
{
    private HealthController healthController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CheckPointManager.Instance.lives++;
            Destroy(gameObject);
        }
    }
}
