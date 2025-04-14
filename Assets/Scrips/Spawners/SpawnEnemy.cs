using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private LayerMask player;
    [SerializeField] private GameObject[] enemies;

    private int selectEnemy;

    [SerializeField] private float fidingRadio;
    [SerializeField] private float spawnTime;
    [SerializeField] private float waitToSpawn;

    private bool isPlayer;

    // Update is called once per frame
    void Update()
    {
        isPlayer = Physics2D.OverlapCircle(transform.position, fidingRadio, player);

        if (isPlayer)
        {
            spawnTime += Time.deltaTime;

            if (spawnTime > waitToSpawn)
            {
                selectEnemy = Random.Range(0, enemies.Length);
                spawnTime = 0;
                Instantiate(enemies[selectEnemy], transform.position, transform.rotation);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, fidingRadio);
    }
}
