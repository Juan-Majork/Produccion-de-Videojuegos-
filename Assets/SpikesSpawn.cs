using UnityEngine;

public class SpikesSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] roofSpawns;
    [SerializeField] private GameObject roofSpikes;
    [SerializeField] private GameObject[] roofSpawnsWithMagic;
    [SerializeField] private GameObject roofSpikesWithMagic;

    [SerializeField] private float spawnTime = 3;

    private bool isSpawning = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSpawning && collision.gameObject.CompareTag("Player"))
        {
            isSpawning = true;
            InvokeRepeating(nameof(SpawnSpikes), 0f, spawnTime);
        }


    }

    private void SpawnSpikes()
    {
        foreach (GameObject spawnPoint in roofSpawns)
        {
            Instantiate(roofSpikes, spawnPoint.transform.position, roofSpikes.transform.rotation);
        }
        foreach (GameObject spawnPoint in roofSpawnsWithMagic)
        {
            Instantiate(roofSpikesWithMagic, spawnPoint.transform.position, roofSpikesWithMagic.transform.rotation);
        }
    }
}
