using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float knockbackForceX = 10f;
    [SerializeField] private float knockbackForceY = 10f;

    private HealthController healthController;

    private void Start()
    {
        healthController = GetComponent<HealthController>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (healthController != null && healthController.IsDead)
        {
            return;
        }
        

        if (collision.gameObject.CompareTag("Player"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();

            Movement movement = collision.gameObject.GetComponent<Movement>();

            if (healthController.isInvicible == false)
            {
                healthController.takeDamage(damage);

                if (collision.transform.position.x - transform.position.x < 0)
                {
                    movement.ApplyKnockback(knockbackForceX, true, knockbackForceY);

                }
                if (collision.transform.position.x - transform.position.x > 0)
                {
                    movement.ApplyKnockback(knockbackForceX, false, knockbackForceY);

                }
            }

        }
    }
}
