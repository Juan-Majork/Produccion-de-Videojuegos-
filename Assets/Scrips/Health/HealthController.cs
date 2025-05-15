using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;


public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;

    [SerializeField] private bool isPlayer = false; // Para distinguir entre enemigo y jugador
    [SerializeField] private int lives = 2;
    private Vector2 lastCheckpointPosition;

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
        if (isInvicible)
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
            IsDeath();
        }
        if (health > 0)
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
            lives--;

            if (lives > 0)
            {
                // Respawn al checkpoint
                transform.position = lastCheckpointPosition;
                health = maxHealth;
                HealthChanged.Invoke();
                Debug.Log("Respawn, vidas restantes: " + lives);
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
        lastCheckpointPosition = position;
    }

    public UnityEvent Death;
    public UnityEvent Damage;
    public UnityEvent HealthChanged;
}