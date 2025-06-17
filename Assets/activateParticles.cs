using UnityEngine;

public class activateParticles : MonoBehaviour
{
    private BurnDamage burnDamage;
    [SerializeField] private ParticleSystem particle;

    void Start()
    {
        burnDamage = GetComponent<BurnDamage>();
    }

    public void StartParticles()
    {
        particle.Stop();
    }

    public void StopParticles()
    {
        particle.Stop();
    }
}
