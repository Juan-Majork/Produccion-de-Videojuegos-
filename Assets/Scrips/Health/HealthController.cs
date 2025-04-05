using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;

    public float hpPercentage
    {
        get
        {
            return health / maxHealth;
        }
    }

    public bool isInvicible { get; set; }

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
            Death.Invoke();
        }
        if (health > 0) 
        { 
            Damage.Invoke();
        }

        HealthChanged.Invoke();
    }

    public UnityEvent Death;
    public UnityEvent Damage;
    public UnityEvent HealthRestore;
    public UnityEvent HealthChanged;
}
