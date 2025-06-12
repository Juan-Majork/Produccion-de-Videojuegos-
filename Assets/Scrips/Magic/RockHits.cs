using UnityEngine;
using UnityEngine.Events;

public class RockHits : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float lowDamage;
    [SerializeField] private float riseDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(damage);

        }
        else if (collision.gameObject.CompareTag("FireEnemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(riseDamage);

        }
        else if (collision.gameObject.CompareTag("IceEnemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(lowDamage);

        }
        else if (collision.gameObject.CompareTag("RockEnemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(damage);
        }

        if (collision.gameObject)
        {
            timeToDestroy.Invoke();
        }

    }
    
    public UnityEvent timeToDestroy;
}

