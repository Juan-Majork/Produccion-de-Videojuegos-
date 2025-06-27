using UnityEngine;

public class InvokedSlime : MonoBehaviour
{
    [SerializeField] private float chargeTime = 2f;
    [SerializeField] private float moveSpeed = 8f;

    private Transform pointA;
    private Transform pointB;

    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private bool isCharging = true;
    private HealthController healthController;
    private BossEnemySpawn bossSpawner;

    private Vector2 lastTargetPosition;

    public void SetBossSpawner(BossEnemySpawn spawner)
    {
        bossSpawner = spawner;
    }

    public void SetPoints(Transform startPoint, Transform endPoint)
    {
        pointA = startPoint;
        pointB = endPoint;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthController = GetComponent<HealthController>();

        lastTargetPosition = pointB.position;
        Invoke(nameof(StartMoving), chargeTime);
    }

    private void StartMoving()
    {
        isCharging = false;

        targetPosition = lastTargetPosition == (Vector2)pointA.position ? pointB.position : pointA.position;
        lastTargetPosition = targetPosition;

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    void Update()
    {
        if (!isCharging)
        {
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                rb.linearVelocity = Vector2.zero;
                isCharging = true;
                Invoke(nameof(StartMoving), chargeTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCharging && collision.gameObject.CompareTag("Player"))
        {
            targetPosition = (Vector2)transform.position == (Vector2)pointA.position ? pointB.position : pointA.position;

            isCharging = true;
            rb.linearVelocity = Vector2.zero;
            Invoke(nameof(ResumeFromCollision), chargeTime);
        }
    }

    private void ResumeFromCollision()
    {
        isCharging = false;

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        lastTargetPosition = targetPosition; 
    }
}
