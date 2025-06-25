using UnityEngine;

public class SpikesSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] roofSpawns;
    [SerializeField] private GameObject roofSpikes;
    [SerializeField] private GameObject[] roofSpawnsWithMagic;
    [SerializeField] private GameObject roofSpikesWithMagic;

    private bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasSpawned && collision.gameObject.CompareTag("Player"))
        {

            foreach (GameObject spawnPoint in roofSpawns) 
            { 
                Instantiate(roofSpikes,spawnPoint.transform.position, roofSpikes.transform.rotation);
            }
            foreach (GameObject spawnPoint in roofSpawnsWithMagic)
            {
                Instantiate(roofSpikesWithMagic, spawnPoint.transform.position, roofSpikesWithMagic.transform.rotation);
            }

            hasSpawned = true;
        }

        
    }

}
