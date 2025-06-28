using UnityEngine;

public class SpikeStop : MonoBehaviour
{
    [SerializeField] private SpikesSpawn spikeSpawner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spikeSpawner.StopSpawning();
        }
    }

}
