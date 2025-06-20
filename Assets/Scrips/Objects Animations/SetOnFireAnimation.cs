using UnityEngine;

public class SetOnFireAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private ParticleSystem particulas;

    [SerializeField] private bool activeAnimation;
    [SerializeField] private bool activeParticulas;

    private void Awake()
    {
        if (activeAnimation)
        {
            if (animator == null) 
            {
                animator = GetComponentInChildren<Animator>();
            }
            
        }

        if (activeParticulas)
        {
            particulas = GetComponentInChildren<ParticleSystem>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("fireMagic"))
        {
            if (activeAnimation)
            {
                animator.SetTrigger("setOnFire");
            }

            if (activeParticulas)
            {
                particulas.Play();
            }
        }
    }

    public void EndAnimation()
    {
        if (activeAnimation)
        {
            animator.SetTrigger("destroy");
        }

        if (activeParticulas)
        {
            particulas.Stop();
        }
    }
}
