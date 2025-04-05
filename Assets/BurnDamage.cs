using UnityEngine;

public class BurnDamage : MonoBehaviour
{
    private float burnDuration;
    private float tickInterval;
    private float damagePerTick;

    private float timer;
    private float burnTimeLeft;
    private bool isBurning = false;

    private HealthController targetHealth;

    void Start()
    {
        targetHealth = GetComponent<HealthController>(); 
    }

    void Update()
    {
        if (!isBurning || targetHealth == null) return;

        burnTimeLeft -= Time.deltaTime;
        timer += Time.deltaTime;

        if (timer >= tickInterval)
        {
            targetHealth.takeDamage(damagePerTick);
            timer = 0f;
        }

        if (burnTimeLeft <= 0f)
        {
            isBurning = false;
            timer = 0f;
            burnTimeLeft = 0f;
        }
    }

    public void ApplyBurn(float duration, float dps, float interval)
    {
        burnDuration = duration;
        damagePerTick = dps;
        tickInterval = interval;

        burnTimeLeft = burnDuration;
        timer = 0f;
        isBurning = true;
    }

 
}
