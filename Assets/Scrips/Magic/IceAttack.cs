using UnityEngine;

public class IceAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float lowDamage;
    [SerializeField] private float riseDamage;

    [SerializeField] private float waitToDestroy;
    private float actualTime = 0;

    [SerializeField] private float slowDuration;

    [SerializeField] private GameObject icePlataform;
    private bool hasCreatedPlataform = false;

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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(damage);
            //Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("FireEnemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            FireEnemyBehavior fireEnemy = collision.gameObject.GetComponent<FireEnemyBehavior>();
            fireEnemy.duration = 0;
            fireEnemy.isCold = true;
            healthController.takeDamage(lowDamage);
            //Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("IceEnemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.takeDamage(damage);
            //Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("RockEnemy"))
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            EnemyRangedAttack rangeEnemy = collision.gameObject.GetComponent<EnemyRangedAttack>();
            rangeEnemy.duration = 0;
            rangeEnemy.isCold = true;
            healthController.takeDamage(riseDamage);
            Destroy(gameObject);
        }

        if (collision.GetComponent<FollowPlayerArea>() != null)
        {
            FollowPlayerArea movement = collision.GetComponent<FollowPlayerArea>();
            movement.ApplySlow  (0.5f, slowDuration); // Reduce velocidad a la mitad durante 2 segundos
        }

        if (!hasCreatedPlataform && collision.gameObject.layer == LayerMask.NameToLayer("WaterFallLayer"))
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.z = 0f;
            GameObject newPlataform = Instantiate(icePlataform, spawnPosition, Quaternion.identity);
            Waterfall waterfallScript = collision.GetComponent <Waterfall>();
            if (waterfallScript != null) 
            {
                waterfallScript.RegisterIcePlataform(newPlataform);
            }


            hasCreatedPlataform = true;
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            Destroy(gameObject);
        }
    }
}
