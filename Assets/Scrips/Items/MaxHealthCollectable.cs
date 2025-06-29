using UnityEngine;

public class MaxHealthCollectable : MonoBehaviour
{
    private HealthController healthController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioPlayer.audioPlayer.PickItem();
            CheckPointManager.Instance.currentLives++;
            Destroy(gameObject);
        }
    }
}
