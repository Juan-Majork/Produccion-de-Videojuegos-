using UnityEngine;

public class ButtonsAppear : MonoBehaviour
{
    [SerializeField] GameObject buttonStart;
    [SerializeField] GameObject buttonOptions;
    [SerializeField] GameObject buttonExit;
    [SerializeField] GameObject buttonCredit;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ActiveButtons()
    {
        buttonStart.SetActive(true);
        buttonOptions.SetActive(true);
        buttonExit.SetActive(true);
        buttonCredit.SetActive(true);
    }

    public void DesactiveButtons()
    {
        animator.SetTrigger("GoMenu");
        buttonStart.SetActive(false);
        buttonOptions.SetActive(false);
        buttonExit.SetActive(false);
        buttonCredit.SetActive(false);
    }
}
