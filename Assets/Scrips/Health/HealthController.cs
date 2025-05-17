using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;

    [SerializeField] private bool isPlayer = false; 
    private bool isDead = false;

    public float hpPercentage
    {
        get
        {
            return health / maxHealth;
        }
    }

    public bool isInvicible { get; set; }

    private void Awake()
    {
        HealthChanged.Invoke();
    }

    private void Start()
    {
        if (isPlayer && CheckPointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckPointManager.Instance.GetCheckpoint();
            health = maxHealth;
            isDead = false;
            HealthChanged.Invoke();
            Debug.Log("Jugador reapareció en el checkpoint.");

            //Resetea el estado del movimiento
            Movement movement = GetComponent<Movement>();
            if (movement != null)
            {
                movement.ResetMovementState();
            }
        }
    }


    public void restoreHP(float restore)
    {
        if (health == maxHealth)
        {
            return;
        }

        health += restore;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        HealthChanged.Invoke();
    }

    public void takeDamage(float damage)
    {
        if (isInvicible || isDead)
        {
            return;
        }

        health -= damage;

        if (health < 0)
        {
            health = 0;
        }
        if (health == 0)
        {
            isDead = true; 
            IsDeath();
        }
        else
        {
            Damage.Invoke();
        }

        HealthChanged.Invoke();
    }


    public void MaxHealth()
    {
        maxHealth += 25;
        health = maxHealth;
        HealthChanged.Invoke();
        Debug.Log("subio tu vida");
    }

    public void IsDeath()
    {
        if (isPlayer)
        {
            CheckPointManager.Instance.LoseLife();

            if (CheckPointManager.Instance.HasLivesLeft())
            {
                // Reinicia la escena para resetear enemigos, pero no perder el checkpoint
                Death.Invoke();
                Invoke(nameof(SceneCall), 5f);
                return;
            }
            else
            {
                // Sin vidas: resetear todo y volver al inicio
                CheckPointManager.Instance.Reset(); // Resetea checkpoint y vidas
                Death.Invoke();
                Debug.Log("Game Over, volviendo al principio");
                Invoke(nameof(SceneCall), 5f);
                return;
            }
        }

        // Muerte definitiva
        health = 0;
        Death.Invoke();
        Debug.Log("Game Over o Enemigo destruido");
    }



    public void SetCheckpoint(Vector2 position)
    {
        CheckPointManager.Instance.SetCheckpoint(position);
    }

    private void SceneCall()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public UnityEvent Death;
    public UnityEvent Damage;
    public UnityEvent HealthChanged;
}