using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager Instance { get; private set; }

    private Vector2 checkpoint = Vector2.zero;

    [SerializeField] private int lives = 3; // vidas máximas
    private int currentLives; // vidas restantes

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            currentLives = lives; // inicializar
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCheckpoint(Vector2 position)
    {
        checkpoint = position;
        Debug.Log("Checkpoint guardado: " + checkpoint);
    }

    public Vector2 GetCheckpoint()
    {
        return checkpoint;
    }

    public bool HasCheckpoint()
    {
        return checkpoint != Vector2.zero;
    }

    public void LoseLife()
    {
        currentLives--;
        Debug.Log("Vida perdida. Vidas restantes: " + currentLives);
    }

    public bool HasLivesLeft()
    {
        return currentLives > 0;
    }

    public void Reset()
    {
        currentLives = lives; // restablece vidas a su valor máximo
        checkpoint = Vector2.zero;
        Debug.Log("Juego reiniciado. Vidas: " + currentLives);
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }


}
