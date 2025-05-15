using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPointManager.Instance.SetCheckpoint(transform.position);
            Debug.Log("Checkpoint actualizado: " + transform.position);
        }
    }
}
