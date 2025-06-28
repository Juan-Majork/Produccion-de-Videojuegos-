using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsActions : MonoBehaviour
{
    public void StartTheGame()
    {
        SceneManager.LoadScene("level1");
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }

    public void ActivePause()
    {
        Time.timeScale = 0f;
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("menu");
    }
}
