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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(damage);
        }
    }
}
