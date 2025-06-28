using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsActions : MonoBehaviour
{
    public void StartTheGame()
    {
        Invoke(nameof(ActionStartGame), 0.2f);
    }

    public void ExitTheGame()
    {
        Invoke(nameof(ActionExitGame), 0.2f);
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

    private void ActionStartGame()
    {
        SceneManager.LoadScene("level1");
    }

    private void ActionExitGame()
    {
        Application.Quit();
    }
}
