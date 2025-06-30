using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsActions : MonoBehaviour
{
    public void StartTheGame()
    {
        Invoke(nameof(ActionStartGame), 0.1f);
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
        Invoke(nameof(ActionGoToMenu), 0f);
    }

    private void ActionStartGame()
    {
        SceneManager.LoadScene("level1");
    }

    private void ActionExitGame()
    {
        Application.Quit();
    }

    private void ActionGoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("menu");
    }
}
