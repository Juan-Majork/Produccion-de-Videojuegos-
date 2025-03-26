using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();

            healthController.takeDamage(damage);
        }
    }
}
