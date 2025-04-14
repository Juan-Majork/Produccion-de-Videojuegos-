using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    [SerializeField] private float health;
    private HealthController healthController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthController = collision.GetComponent<HealthController>();
            healthController.restoreHP(health);
            Destroy(gameObject);
        }
    }
}
