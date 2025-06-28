using UnityEngine;

public class ActivePausePanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    //private bool activePanel = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            panel.SetActive(true);
            //activePanel = true;
            Time.timeScale = 0f;
        }
    }

    public void DesactivePause()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
    }
}
