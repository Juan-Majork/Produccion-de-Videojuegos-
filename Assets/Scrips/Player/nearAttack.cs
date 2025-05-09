using Unity.VisualScripting;
using UnityEngine;

public class nearAttack : MonoBehaviour
{
    [SerializeField] private float damage;

    private float waitToDestroy = 0.2f;
    public float actualTime = 1;

    private BoxCollider2D attack;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        attack = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        actualTime += Time.deltaTime;

        if (actualTime >= waitToDestroy)
        {
            attack.enabled = false;
            spriteRenderer.enabled = false;
        }
        else
        {
            attack.enabled = true;
            spriteRenderer.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("FireEnemy") ||
            collision.CompareTag("IceEnemy") || collision.CompareTag("RockEnemy"))
        {
            HealthController healthController = collision.GetComponent<HealthController>();
            if (healthController != null)
            {
                healthController.takeDamage(damage);
            }

            EnemyKnockback knockback = collision.GetComponent<EnemyKnockback>();
            if (knockback != null)
            {
                Vector2 knockbackDir = collision.transform.position.x > transform.root.position.x
                    ? Vector2.right
                    : Vector2.left;

                knockback.ApplyKnockback(knockbackDir);
            }

        }
    }

}
