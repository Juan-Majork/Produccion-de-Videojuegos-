using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            levelSelect();
        }
    }

    public void loadLevel(float time)
    {
       Invoke(nameof(levelSelect), time);
    }

    private void levelSelect()
    {
        if (SceneManager.GetActiveScene().name == "level1")
        {
            SceneManager.LoadScene("level1");
        }
        if (SceneManager.GetActiveScene().name == "transicion")
        {
            SceneManager.LoadScene("transicion");
        }
        else if (SceneManager.GetActiveScene().name == "boss1")
        {
            SceneManager.LoadScene("boss1");
        }
       
    }

    public void NextLevel(int select)
    {
        // 1 = primer nivel
        // 2 = nivel del jefe

        if (select == 1)
        {
            SceneManager.LoadScene("level1");
        }
        else if (select == 2)
        {
            SceneManager.LoadScene("transicion");
        }
        else if (select == 3)
        {
            SceneManager.LoadScene("boss1");
        }
    }

    public void GoNivelTransicion()
    {
        SceneManager.LoadScene("transicion");
    }
}
