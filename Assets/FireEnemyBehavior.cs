using UnityEngine;
using UnityEngine.Rendering;

public class FireEnemyBehavior : MonoBehaviour
{
    [SerializeField] private LayerMask faceFront;
    [SerializeField] private Transform front;
    private SpriteRenderer render;
    private Color originalColor;
    [SerializeField] private float distFront;
    private bool infoFront;

    [SerializeField] private bool lookRight;

    [SerializeField] private float moveX;
    [SerializeField] private float moveY;
    private bool canJump;

    private float timeToJump = 0.5f;
    private float actualTime;

    [SerializeField] private int numOfJumps;
    private int actualJump = 0;

    private Rigidbody2D rb;
    private Animator animator;

    public bool isCold = false;
    public float duration = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //enemyKnockback = GetComponent<EnemyKnockback>();
        render = GetComponent<SpriteRenderer>();
        originalColor = render.color;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCold)
        {
            render.color = Color.cyan;
            duration += Time.deltaTime;
            Debug.Log(duration.ToString());

            if (duration > 4)
            {
                render.color = originalColor;
                isCold = false;
                duration = 0;
                animator.SetTrigger("hitGround");
            }
        }

        if (isCold) return;

        actualTime += Time.deltaTime;
        animator.SetFloat("inAir", rb.linearVelocity.y);

        if (actualTime > timeToJump) 
        {
            if (canJump)
            {
                animator.SetTrigger("jump");
                actualTime = 0;
            }
        }

        infoFront = Physics2D.Raycast(front.position, transform.right, distFront, faceFront);

        if (infoFront)
        {
            flip();
            actualJump = 0;
        }
    }

    private void flip()
    {
        lookRight = !lookRight;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    public void jump()
    {
        if (!lookRight)
        {
            rb.AddForceX(-moveX, ForceMode2D.Impulse);
            rb.AddForceY(moveY, ForceMode2D.Impulse);
        }
        if (lookRight)
        {
            rb.AddForceX(moveX, ForceMode2D.Impulse);
            rb.AddForceY(moveY, ForceMode2D.Impulse);
        }

        actualJump++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        {

            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f) // El contacto desde arriba
                {

                    if (actualJump >= numOfJumps)
                    {
                        actualJump = 0;
                        flip();
                    }

                    canJump = true;
                    actualTime = 0;
                    break;
                }
            }
        }

        if (collision.gameObject.CompareTag("Rock"))
        {
            if (actualJump >= numOfJumps)
            {
                actualJump = 0;
                flip();
            }

            canJump = true;
            actualTime = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(front.transform.position, front.transform.position + transform.right * distFront);
    }
}
