using UnityEngine;

public class AvalancheScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] private float followDuration = 4;
    [SerializeField] private float followSpeed = 5;
    private bool isFollowing = true;
    private float fixedHeight;
    [SerializeField] private int damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f;
        }

        player = GameObject.FindGameObjectWithTag("Player");
        fixedHeight = transform.position.y;

        Invoke(nameof(StartFalling), followDuration);
    }

    void FixedUpdate()
    {
        if (isFollowing && player != null)
        {
            Vector2 targetPos = new Vector2(player.transform.position.x, fixedHeight);
            Vector2 newPos = Vector2.MoveTowards(rb.position, targetPos, followSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
    }

    private void StartFalling()
    {
        isFollowing = false;
        rb.gravityScale = 2f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(damage);            
            Destroy(gameObject);
        }
    }
}
