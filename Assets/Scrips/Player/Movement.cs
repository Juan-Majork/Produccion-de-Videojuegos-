using UnityEngine;

public class Movement : MonoBehaviour
{
    private float horizontalInput;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;

    private Rigidbody2D rb2D;

    public bool facingRight = true;
    private bool canJump = true;

    private bool inWaterfall = false;
    Vector2 currentVelocity;

    private bool isKnockedBack = false;
    private float knockbackTimer = 0f;
    [SerializeField] private float knockbackDuration = 0.2f;

    Animator animator;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (isKnockedBack)
        {
            knockbackTimer -= Time.fixedDeltaTime;
            if (knockbackTimer <= 0)
            {
                isKnockedBack = false;
            }
            return; // Ignora el movimiento mientras knockback true
        }

        horizontalInput = Input.GetAxis("Horizontal");
        currentVelocity = rb2D.linearVelocity;

        if (inWaterfall) return;

        animator.SetBool("isWalking", horizontalInput != 0);

        if (horizontalInput != 0)
        {
            currentVelocity.x = horizontalInput * speed;
        }

        if (Input.GetKey(KeyCode.W) && canJump)
        {
            currentVelocity.y = jumpForce;
            canJump = false;
        }

        rb2D.linearVelocity = currentVelocity;

        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }

        animator.SetFloat("inAir", rb2D.linearVelocity.y);
    }



    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Rock"))
        {
            canJump = true;
        }
    }

    public void SetInWaterfall(bool state)
    {
        inWaterfall = state;
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        isKnockedBack = true;
        knockbackTimer = knockbackDuration;

        rb2D.linearVelocity = Vector2.zero;
        rb2D.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }


}
