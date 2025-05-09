using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isKnockedBack = false;
    private float knockbackTimer = 0f;

    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private float knockbackForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
            {
                isKnockedBack = false;
            }
        }
    }

    public void ApplyKnockback(Vector2 direction)
    {
        isKnockedBack = true;
        knockbackTimer = knockbackDuration;
        rb.linearVelocity = Vector2.zero;

        Vector2 horizontalDirection = new Vector2(direction.x, 0).normalized;
        rb.AddForce(horizontalDirection * knockbackForce, ForceMode2D.Impulse);
        Debug.Log("Knockback aplicado a " + gameObject.name);
    }


    public bool IsBeingKnockedBack()
    {
        return isKnockedBack;
    }
}
