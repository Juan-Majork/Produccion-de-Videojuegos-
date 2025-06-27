using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private LayerMask player;
    [SerializeField] private LayerMask EnemyItem;
    [SerializeField] private GameObject[] enemies;

    private int selectEnemy;

    [SerializeField] private float fidingRadio;
    [SerializeField] private float spawnTime;
    [SerializeField] private float waitToSpawn;

    private bool isPlayer;
    private bool haveSomething;

    // Update is called once per frame
    void Update()
    {
        isPlayer = Physics2D.OverlapCircle(transform.position, fidingRadio, player);

        haveSomething = Physics2D.OverlapCircle(transform.position, fidingRadio, EnemyItem);

        if (haveSomething)
        {
            return;
        }
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
