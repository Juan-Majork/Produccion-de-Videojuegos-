using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(BackToMenu), 1f);
        }
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
