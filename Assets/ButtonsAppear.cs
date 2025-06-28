using UnityEngine;

public class ButtonsAppear : MonoBehaviour
{
    [SerializeField] GameObject buttonStart;
    [SerializeField] GameObject buttonOptions;
    [SerializeField] GameObject buttonExit;
    [SerializeField] GameObject buttonCredit;

    public void ActiveButtons()
    {
        buttonStart.SetActive(true);
        buttonOptions.SetActive(true);
        buttonExit.SetActive(true);
        buttonCredit.SetActive(true);
    }
}
