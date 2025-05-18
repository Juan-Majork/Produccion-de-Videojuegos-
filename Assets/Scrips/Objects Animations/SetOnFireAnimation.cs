using UnityEngine;

public class SetOnFireAnimation : MonoBehaviour
{
    private Animator animator;
    private ParticleSystem particulas;

    [SerializeField] private bool activeAnimation;

    private void Awake()
    {
        if (activeAnimation)
        {
            animator = GetComponentInChildren<Animator>();
        }
        particulas = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("fireMagic"))
        {
            if (activeAnimation)
            {
                animator.SetTrigger("setOnFire");
            }
            particulas.Play();
        }
    }

    public void EndAnimation()
    {
        if (activeAnimation)
        {
            animator.SetTrigger("destroy");
        }
        particulas.Stop();
    }
}
