using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]private float timer = 1f; //tiempo antes de caer
    private bool hasDropped = false;

    [SerializeField] private int damage;

    private SpawnMagic spawnMagic;

    [SerializeField] private GameObject magic;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor") || collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Rock"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                HealthController healthController = collision.gameObject.GetComponent<HealthController>();
                if (healthController != null)
                {
                    healthController.takeDamage(damage);
                }
            }
            if(magic != null) 
            {
                DropMagic();
            }
            
            Destroy(gameObject);
        }
            
    }

    private void DropMagic()
    {
        if (magic != null)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 1.5f, 0);
            Instantiate(magic, spawnPosition, Quaternion.identity);
        }
    }

}
