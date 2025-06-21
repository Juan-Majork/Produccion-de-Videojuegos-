using UnityEngine;

public class BossEnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [SerializeField] private bool secondPhase = false;

    private bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!hasSpawned && collision.gameObject.CompareTag("Player"))
        {
            SpawnEnemies();
            hasSpawned = true;
        }
    }

    private void SpawnEnemies()
    {
        GameObject enemy1 = Instantiate(enemy, pointA.position, Quaternion.identity);
        InvokedSlime slime1 = enemy1.GetComponent<InvokedSlime>();
        if (slime1 != null)
        {
            slime1.Initialize(pointA, pointB, pointB.position); 
            slime1.SetBossSpawner(this);
        }

        if (secondPhase)
        {
            GameObject enemy2 = Instantiate(enemy, pointB.position, Quaternion.identity);
            InvokedSlime slime2 = enemy2.GetComponent<InvokedSlime>();
            if (slime2 != null)
            {
                slime2.Initialize(pointA, pointB, pointA.position); 
                slime2.SetBossSpawner(this);
            }
        }
    }

    public void ResetSpawn()
    {
        hasSpawned = false;
    }

}
