using UnityEngine;

public class FloorSpikeSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] floorSpike;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (GameObject spike in floorSpike) 
            { 
                spike.SetActive(true);
            }

        }
    }

}
