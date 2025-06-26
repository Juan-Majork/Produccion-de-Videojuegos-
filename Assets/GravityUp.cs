using UnityEngine;

public class GravityUp : MonoBehaviour
{
    [SerializeField] private bool gravityUp = true;
    [SerializeField] private float gravityScaleValue = 2f;

    private bool velocityApplied = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !velocityApplied)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            rb.gravityScale = gravityUp ? gravityScaleValue : -gravityScaleValue;

            rb.linearVelocityY = 15;

            velocityApplied = true;
        }
    }
}
