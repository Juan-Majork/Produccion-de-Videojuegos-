using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float knockbackForce = 10f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();

            Movement movement = collision.gameObject.GetComponent<Movement>();

            healthController.takeDamage(damage);

            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            movement.ApplyKnockback(knockbackDirection, knockbackForce);
        }
    }
}
