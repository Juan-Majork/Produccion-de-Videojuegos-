using UnityEngine;

public class InitialExplanation : MonoBehaviour
{
    public static InitialExplanation instance;
    [SerializeField] private GameObject panel;
    private static bool active = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (active)
        {
            panel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            panel.SetActive(false);
            Time.timeScale = 1.0f;
        }

    }

    public void RestoredTime()
    {
        Time.timeScale = 1.0f;
        active = false;
    }
}
