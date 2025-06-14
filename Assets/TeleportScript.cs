using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    [SerializeField] private Transform teleportDestination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.position = teleportDestination.position;
        }
    }
}
