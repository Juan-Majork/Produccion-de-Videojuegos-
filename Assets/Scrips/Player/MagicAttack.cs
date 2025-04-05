using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    [SerializeField] private float damage;

    [SerializeField] private float waitToDestroy;
    private float actualTime = 0;

    [SerializeField] private bool triger;

    // Update is called once per frame
    void Update()
    {
        actualTime += Time.deltaTime;

        if (actualTime >= waitToDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triger)
        {
            if (collision.gameObject.CompareTag("Enemy")) 
            {
                HealthController healthController = collision.gameObject.GetComponent<HealthController>();
                healthController.takeDamage(damage);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triger)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                HealthController healthController = collision.gameObject.GetComponent<HealthController>();
                healthController.takeDamage(damage);
            }
        }
    }
}
