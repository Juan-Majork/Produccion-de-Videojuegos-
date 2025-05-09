using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private LayerMask faceDown;
    [SerializeField] private LayerMask faceFront;

    [SerializeField] private float velocity;
    private float originalVelocity;

    [SerializeField] private float distDown;
    [SerializeField] private float distFront;

    [SerializeField] private Transform down;
    [SerializeField] private Transform front;

    [SerializeField] private bool takeDown;
    private bool infoDown;
    private bool infoFront;

    [SerializeField] private bool lookRight;

    private bool isSlowed = false;
    private float slowTimer = 0f;
    private EnemyKnockback enemyKnockback;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalVelocity = velocity;
        enemyKnockback = GetComponent<EnemyKnockback>();
    }

    private void Update()
    {
        if (enemyKnockback == null || !enemyKnockback.IsBeingKnockedBack())
        {
            if (lookRight)
            {
                rb.linearVelocity = new Vector2(velocity, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(-velocity, rb.linearVelocity.y);
            }
        }


        infoFront = Physics2D.Raycast(front.position, transform.right, distFront, faceFront);
        infoDown = Physics2D.Raycast(down.position, transform.up * -1, distDown, faceDown);

        if (infoFront)
        {
            flip();
        }

        if (takeDown)
        {
            if (!infoDown)
            {
                flip();
            }
        }

        if (isSlowed)
        {
            slowTimer -= Time.deltaTime;
            if (slowTimer <= 0)
            {
                ResetSpeed();
            }
        }
    }

    private void flip()
    {
        lookRight = !lookRight;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(down.transform.position, down.transform.position + transform.up * -1 * distDown);
        Gizmos.DrawLine(front.transform.position, front.transform.position + transform.right * distFront);
    }

    public void ApplySlow(float slowFactor, float duration)
    {
        isSlowed = true;
        slowTimer = duration;
        velocity = originalVelocity * slowFactor;
    }


    private void ResetSpeed()
    {
        velocity = originalVelocity;
        isSlowed = false;
    }
}

