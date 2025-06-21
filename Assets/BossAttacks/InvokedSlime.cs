using UnityEngine;

public class InvokedSlime : MonoBehaviour
{
    [SerializeField] private float chargeTime = 2f;
    [SerializeField] private float moveSpeed = 8f;

    private Transform pointA;
    private Transform pointB;
    private Vector2 targetPosition;

    private Rigidbody2D rb;
    private bool isCharging = true;
    private HealthController healthController;
    private BossEnemySpawn bossSpawner;

    public void SetBossSpawner(BossEnemySpawn spawner)
    {
        bossSpawner = spawner;
    }

    public void Initialize(Transform pointA, Transform pointB, Vector2 initialTarget)
    {
        this.pointA = pointA;
        this.pointB = pointB;
        this.targetPosition = initialTarget;

        rb = GetComponent<Rigidbody2D>();
        healthController = GetComponent<HealthController>();

        Invoke(nameof(StartMoving), chargeTime);
    }

    private void StartMoving()
    {
        isCharging = false;
    }

    void FixedUpdate()
    {
        if (!isCharging)
        {
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;

            if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
            {
                // Detenemos el movimiento y preparamos el cambio de dirección
                rb.linearVelocity = Vector2.zero;

                targetPosition = (Vector2)targetPosition == (Vector2)pointA.position ? pointB.position : pointA.position;

                isCharging = true;
                Invoke(nameof(StartMoving), chargeTime);
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Asegura que no se mueva mientras carga
        }
    }

    void Update()
    {
        if (healthController != null && healthController.IsDead)
        {
            Destroy(gameObject);
            bossSpawner.ResetSpawn();
        }
    }
}
