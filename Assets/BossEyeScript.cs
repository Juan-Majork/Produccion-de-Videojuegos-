using UnityEngine;

public class BossEyeScript : MonoBehaviour
{
    [SerializeField] private BossController bossController;
    private Animator animator;
    [SerializeField] private bool isNose;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int rocklayer = LayerMask.NameToLayer("RockMagic");

        if (collision.CompareTag("hit") || collision.CompareTag("iceMagic") || collision.gameObject.layer==rocklayer)
        {
            bossController.RegisterHit();
            if (isNose) 
            {
                animator.SetTrigger("Hit");
            }
        }
    }
    public void PlayHitAnimation()
    {
        animator.SetTrigger("Hit");
    }

}
