using UnityEngine;

public class CheckPointTake : MonoBehaviour
{
    private bool takeIt = false;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!takeIt)
        {
            takeIt = true;
            animator.SetTrigger("take");
        }
    }
}
