using UnityEngine;

public class SetOnFireAnimation : MonoBehaviour
{
    private Animator animator;
    private ParticleSystem particulas;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        particulas = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("fireMagic"))
        {
            animator.SetTrigger("setOnFire");
            particulas.Play();
        }
    }

    public void EndAnimation()
    {
        animator.SetTrigger("destroy");
        particulas.Stop();
    }
}
