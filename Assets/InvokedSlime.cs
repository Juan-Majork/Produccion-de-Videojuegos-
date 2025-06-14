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

    public void SetBossSpawner(BossEnemySpawn spawner)
    {
        bossSpawner = spawner;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthController = GetComponent<HealthController>();

        Invoke(nameof(StartMoving), chargeTime);
    }
    public void SetPoints(Transform startPoint, Transform endPoint)
    {
        pointA = startPoint;
        pointB = endPoint;
    }


    private void StartMoving()
    {
        isCharging = false;
        targetPosition = transform.position == pointA.position ? pointB.position : pointA.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCharging)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position,targetPosition)< 0.1f)
            {
                Destroy(gameObject);
                bossSpawner.ResetSpawn();
            }
        }

        if (healthController != null && healthController.IsDead)
        {
            Destroy(gameObject);
            bossSpawner.ResetSpawn();
        }

    }

}
