using UnityEngine;

public class InitialExplanation : MonoBehaviour
{
    public static InitialExplanation instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (InitialExplanation.instance == null)
        {
            InitialExplanation.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Time.timeScale = 0f;
    }

    public void RestoredTime()
    {
        Time.timeScale = 1.0f;
    }
}
