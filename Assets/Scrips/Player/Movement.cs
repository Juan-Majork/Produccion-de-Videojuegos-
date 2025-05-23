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
    Vector3 currentVelocity;

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
                rb2D.linearVelocity = Vector3.zero;
            }
            return; // Ignora el movimiento mientras knockback true
        }

        horizontalInput = Input.GetAxis("Horizontal");
        currentVelocity = rb2D.linearVelocity;

        // Siempre actualiza la animacion del aire
        animator.SetFloat("inAir", rb2D.linearVelocity.y);

        // Si esta en cascada, no permite el movimiento, pero sigue las animaciones
        if (inWaterfall)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        // Solo permitir movimiento si no esta en cascada
        animator.SetBool("isWalking", horizontalInput != 0);


        if (horizontalInput != 0)
        {
            currentVelocity.x = horizontalInput * speed;
        }

        if (Input.GetKey(KeyCode.K) && canJump)
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
        if (collision.gameObject.CompareTag("Floor"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f) // El contacto desde arriba
                {
                    canJump = true;
                    break;
                }
            }
        }

        if (collision.gameObject.CompareTag("Rock"))
        {
            canJump = true;
        }
    }

    public void SetInWaterfall(bool state)
    {
        inWaterfall = state;
    }

    public void ApplyKnockback(float forceX,bool isRight,float forceY)
    {
        isKnockedBack = true;
        knockbackTimer = knockbackDuration;

        rb2D.linearVelocity = Vector3.zero;
        if (isRight)
        {
            rb2D.AddForce(Vector3.left * forceX, ForceMode2D.Impulse);
            rb2D.AddForce(Vector3.up * forceY, ForceMode2D.Impulse);
        }

        if (!isRight) {
            rb2D.AddForce(Vector3.right * forceX, ForceMode2D.Impulse);
            rb2D.AddForce(Vector3.up * forceY, ForceMode2D.Impulse);
        }
        

 
    }

    public void ResetMovementState()
    {
        isKnockedBack = false;
        knockbackTimer = 0f;
        inWaterfall = false;
        canJump = true;
        rb2D.linearVelocity = Vector3.zero;
    }


}
