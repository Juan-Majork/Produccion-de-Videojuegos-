using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(500);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(500);
        }
        if (collision.gameObject.CompareTag("FireEnemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(500);
        }
        if (collision.gameObject.CompareTag("IceEnemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(500);
        }
        if (collision.gameObject.CompareTag("RockEnemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(500);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(500);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(500);
        }
        if (collision.gameObject.CompareTag("FireEnemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(500);
        }
        if (collision.gameObject.CompareTag("IceEnemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(500);
        }
        if (collision.gameObject.CompareTag("RockEnemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(500);
        }
    }

}
