using UnityEngine;

public class BurnDamage : MonoBehaviour
{
    [SerializeField] private float burnDuration;//duracion de la quemadura
    [SerializeField] private float tickInterval = 1;//tiempo entre cada tick de dano
    [SerializeField] private float damagePerTick;//dano por tick

    public float timer;//timer de el dano
    private float burnTimeLeft;//tiempo restante de el burn
    public bool isBurning = false;//si el objeto esta quemandose

    private HealthController targetHealth;
    void Start()
    {
        targetHealth = GetComponent<HealthController>();
    }

    void Update()
    {
        if (!isBurning || targetHealth == null) return;//si no esta quemandose o no tiene HealthController sale

        //timers
        burnTimeLeft -= Time.deltaTime;
        timer += Time.deltaTime;

        if (timer >= tickInterval)//si pasa el tiempo de es siguiente tick hace dano
        {
            targetHealth.takeDamage(damagePerTick);
            timer = 0f;
        }

        if (burnTimeLeft <= 0f)//si se acaba el tiempo, detiene el dano
        {
            isBurning = false;
            timer = 0f;
            burnTimeLeft = 0f;
        }
    }

    public void ApplyBurn()// void publico para aplicar la quemadura
    {
        burnTimeLeft = burnDuration;//reinicia el tiempo de quemadura
        timer = 0f;// reinicia el timer
        isBurning = true;// activa el burn
    }
}
