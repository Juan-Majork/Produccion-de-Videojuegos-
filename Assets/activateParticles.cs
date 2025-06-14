using UnityEngine;

public class activateParticles : MonoBehaviour
{
    private BurnDamage burnDamage;
    [SerializeField] private ParticleSystem particle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        burnDamage = GetComponent<BurnDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (burnDamage.isBurning == true)
        {
            particle.Play();
        }
        if (burnDamage.isBurning == false)
        {
            particle.Stop();
        }
    }

    public void StopParticles()
    {
        particle.Stop();
    }
}
