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
}
