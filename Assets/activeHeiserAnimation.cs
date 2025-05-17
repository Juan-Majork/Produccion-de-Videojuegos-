using UnityEngine;

public class activeHeiserAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Waterfall waterfall;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isIce", waterfall.ice);
    }
}
