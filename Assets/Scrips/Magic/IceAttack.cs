using UnityEngine;

public class IceAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float lowDamage;
    [SerializeField] private float riseamage;

    [SerializeField] private float waitToDestroy;
    private float actualTime = 0;

    [SerializeField] private float slowDuration;

    void Update()
    {
        actualTime += Time.deltaTime;

        if (actualTime >= waitToDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("FireEnemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(damage + riseamage);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("IceEnemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(damage - lowDamage);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("RockEnemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.GetComponent<FollowPlayerArea>() != null)
        {
            FollowPlayerArea movement = collision.GetComponent<FollowPlayerArea>();
            movement.ApplySlow(0.5f, slowDuration); // Reduce velocidad a la mitad durante 2 segundos
        }

        if (collision.GetComponent<MovePlatform>() != null)
        {
            MovePlatform platform = collision.GetComponent<MovePlatform>();
            platform.ApplySlow(0.5f, slowDuration); // Reduce velocidad a la mitad durante 2 segundos
        }
    }
}
