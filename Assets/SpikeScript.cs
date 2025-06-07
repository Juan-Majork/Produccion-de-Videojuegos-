using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float timer = 1f; //tiempo antes de caer
    private bool hasDropped = false;

    [SerializeField] private int damage;

    private SpawnMagic spawnMagic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        spawnMagic = GetComponent<SpawnMagic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDropped)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                rb.gravityScale = 1f;
                hasDropped = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            if(spawnMagic != null)
            {
                spawnMagic.Drop();
            }
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();           
            healthController.takeDamage(damage);
            if (spawnMagic != null)
            {
                spawnMagic.Drop();
            }
            Destroy(gameObject);
        }
    }

}
