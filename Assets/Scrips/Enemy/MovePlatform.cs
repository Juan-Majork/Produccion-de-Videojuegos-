using UnityEngine;
using UnityEngine.Rendering;
using static FollowPlayerArea;

public class MovePlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    private HealthController healthController;
    private Animator animator;
    private bool detectPLayer;

    [SerializeField] private LayerMask faceDown;
    [SerializeField] private LayerMask faceFront;
    public LayerMask playerLayer;
    public Transform playerTransform;

    [SerializeField] public float velocity;
    private float originalVelocity;
    private float maxVelocity;

    [SerializeField] private float distDown;
    [SerializeField] private float distFront;
    [SerializeField] private float fidingRadio;
    float waitingTime = 0f;

    [SerializeField] private Transform down;
    [SerializeField] private Transform front;

    [SerializeField] private bool takeDown;
    private bool infoDown;
    private bool infoFront;

    [SerializeField] private bool lookRight;

    private bool isSlowed = false;
    private float slowTimer = 0f;
    private EnemyKnockback enemyKnockback;

    public patrollerState state = patrollerState.normal;
    public enum patrollerState
    {
        normal,
        detecting,
        following,
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalVelocity = velocity;
        maxVelocity = velocity * 2f;
        enemyKnockback = GetComponent<EnemyKnockback>();
        healthController = GetComponent<HealthController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        

        if(healthController != null && healthController.IsDead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        switch (state)
        {
            case patrollerState.normal:
                NormalState();
                break;
            case patrollerState.detecting:
                DetectingState();
                break;
            case patrollerState.following:
                FollowingState();
                break;
        }

        infoFront = Physics2D.Raycast(front.position, transform.right, distFront, faceFront);
        infoDown = Physics2D.Raycast(down.position, transform.up * -1, distDown, faceDown);

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

    private void NormalState()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, fidingRadio, playerLayer);
        detectPLayer = false;

        if (playerCollider)
        {
            detectPLayer = true;
            state = patrollerState.detecting;
            animator.SetBool("detectPLayer", detectPLayer);
        }
    }

    private void DetectingState()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, fidingRadio, playerLayer);
        velocity = 0;

        waitingTime += Time.deltaTime;

        if (waitingTime > 1f)
        {
            if (playerCollider == null)
            {
                detectPLayer = false;
                animator.SetBool("detectPLayer", detectPLayer);
                state = patrollerState.normal;
                velocity = originalVelocity;
                waitingTime = 0f;
                return;
            }
            else if (playerCollider != null)
            {
                detectPLayer = true;
                animator.SetBool("detectPLayer", detectPLayer);
                state = patrollerState.following;
                velocity = maxVelocity;
                waitingTime = 0f;
                return;
            }
        }
    }

    private void FollowingState()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, fidingRadio, playerLayer);
        Debug.Log(state.ToString());

        if (playerCollider == null)
        {
            detectPLayer = false;
            animator.SetBool("detectPLayer", detectPLayer);
            state = patrollerState.normal;
            velocity = originalVelocity;
            return;
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
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, fidingRadio);
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

    public void ApplyPush(float duration)
    {
        isSlowed = true;
        slowTimer = duration;
        velocity = originalVelocity * 0;
    }

    public void ApplyIce(float duration)
    {
        isSlowed = true;
        slowTimer = duration;
        velocity = 0;


    }
}

