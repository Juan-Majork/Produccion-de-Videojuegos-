using UnityEngine;

public class DistanceEnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float waitToDestroy;
    private float actualTime;

    // Update is called once per frame
    void Update()
    {
        actualTime += Time.deltaTime;

        if (actualTime > waitToDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(damage);
            Destroy(gameObject);
        }

    }
}
