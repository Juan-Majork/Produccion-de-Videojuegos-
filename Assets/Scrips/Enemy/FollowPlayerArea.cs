using UnityEngine;

public class FollowPlayerArea : MonoBehaviour
{
    [SerializeField] private float fidingRadio;

    public LayerMask playerLayer;

    public Transform playerTransform;

    [SerializeField] private float velocity;

    private float originalVelocity;

    [SerializeField] private float maxDistances;

    public Vector3 startingPoint;

    [SerializeField] private bool lookingRight;

    private Rigidbody2D rb2D;

    private bool isSlowed = false;

    private float slowTimer = 0f;

    private EnemyKnockback enemyKnockback;

    public MovementState movementState;
    public enum MovementState
    {
        waiting,
        following,
        back,
    }

    private void Start()
    {
        startingPoint = transform.position;
        rb2D = GetComponent<Rigidbody2D>();
        originalVelocity = velocity;
        enemyKnockback = GetComponent<EnemyKnockback>();
    }

    private void Update()
    {
        if (enemyKnockback != null && enemyKnockback.IsBeingKnockedBack())
        {
            return; // No se mueve mientras este en knockback
        }

        switch (movementState)
        {
            case MovementState.waiting:
                WaitingState();
                break;
            case MovementState.following:
                FollowingState();
                break;
            case MovementState.back:
                backState();
                break;
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

    private void WaitingState()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, fidingRadio, playerLayer);

        if (playerCollider)
        {
            playerTransform = playerCollider.transform;

            movementState = MovementState.following;
        }
    }

    private void FollowingState()
    {
        if (playerTransform == null)
        {
            movementState = MovementState.back;
            return;
        }

        if (transform.position.x < playerTransform.position.x)
        {
            rb2D.linearVelocity = new Vector2 (velocity, rb2D.linearVelocity.y);
        }
        else
        {
            rb2D.linearVelocity = new Vector2(-velocity, rb2D.linearVelocity.y);
        }

        LookToPlayer(playerTransform.position);

        if (Vector2.Distance(transform.position, startingPoint) > maxDistances||
            Vector2.Distance(transform.position, playerTransform.position) > maxDistances)
        {
            movementState = MovementState.back;
            playerTransform = null;
        }
    }

    private void backState()
    {
        if (transform.position.x < startingPoint.x)
        {
            rb2D.linearVelocity = new Vector2(velocity, rb2D.linearVelocity.y);
        }
        else
        {
            rb2D.linearVelocity = new Vector2(-velocity, rb2D.linearVelocity.y);
        }

        LookToPlayer(startingPoint);

        if(Vector2.Distance(transform.position, startingPoint) < 0.1f)
        {
            rb2D.linearVelocity = Vector2.zero;

            movementState = MovementState.waiting;
        }
    }

    private void LookToPlayer(Vector3 player)
    {
        if(player.x > transform.position.x && !lookingRight) 
        {
            flip();
        }
        else if(player.x < transform.position.x && lookingRight)
        {
            flip();
        }
    }

    private void flip()
    {
        lookingRight = !lookingRight;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fidingRadio);
        Gizmos.DrawWireSphere(startingPoint, maxDistances);
    }


    private void ResetSpeed()
    {
        velocity = originalVelocity;
        isSlowed = false;
    }

    public void ApplySlow(float slowFactor, float duration)
    {
        isSlowed = true;
        slowTimer = duration;
        velocity = originalVelocity * slowFactor;
    }

}
