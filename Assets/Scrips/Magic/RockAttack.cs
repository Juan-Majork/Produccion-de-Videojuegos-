using UnityEngine;
using UnityEngine.Events;

public class RockAttack : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float damage;
    [SerializeField] private float lowDamage;
    [SerializeField] private float riseamage;

    private float gravity = 2;
    [SerializeField] private float waitToFall;
    private float actualTime = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        actualTime += Time.deltaTime;

        if (actualTime >= waitToFall)
        {
            rb.gravityScale = gravity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(damage);

            timeToDestroy.Invoke();
        }
        if (collision.gameObject.CompareTag("FireEnemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(damage);

            timeToDestroy.Invoke();
        }
        if (collision.gameObject.CompareTag("IceEnemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(damage);

            timeToDestroy.Invoke();
        }
        if (collision.gameObject.CompareTag("RockEnemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(damage - lowDamage);

            timeToDestroy.Invoke();
        }

        else if (collision.gameObject)
        {
            timeToDestroy.Invoke();
        }

    }

    public UnityEvent timeToDestroy;
}
