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

    Animator animator;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        Vector2 currentVelocity = rb2D.linearVelocity;

        if (inWaterfall) return; // bloquea input horizontal en la cascada

        animator.SetBool("isWalking", horizontalInput != 0);//animacion de caminar

        if (horizontalInput != 0)
        {
            currentVelocity.x = horizontalInput * speed;
        }

        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            //animator.SetTrigger("jump");

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


}
