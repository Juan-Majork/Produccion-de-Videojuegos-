using TMPro;
using UnityEngine;

public class UiLives : MonoBehaviour
{
    private TextMeshProUGUI livesText;

    void Start()
    {
        livesText = GetComponent<TextMeshProUGUI>(); // toma el componente del mismo objeto
    }

    void Update()
    {
        if (CheckPointManager.Instance != null)
        {
            livesText.text = "Lives: " + CheckPointManager.Instance.GetCurrentLives();
        }
    }
}
