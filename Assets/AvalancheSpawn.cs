using UnityEngine;

public class AvalancheSpawn : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject Avalanche;
    [SerializeField] private float spawnHeight;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("hit"))
        {
            SpawnAvalanche();
        }
    }

    private void SpawnAvalanche()
    {
        Vector2 spawnPos = new Vector2(player.transform.position.x, player.transform.position.y + spawnHeight);

        GameObject rock = Instantiate(Avalanche, spawnPos, Quaternion.identity);
    }

    
}
