using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

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
        SceneManager.LoadScene("SampleScene");
    }
}
