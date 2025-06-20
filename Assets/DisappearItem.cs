using UnityEngine;
using UnityEngine.Events;

public class DisappearItem : MonoBehaviour
{
    private float timeToDesappear;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private SpriteFlash flash;

    [SerializeField] Color noColor;
    [SerializeField] Color actualColor;

    private void Awake()
    {
        flash = GetComponent<SpriteFlash>();
    }

    private void Update()
    {
        if (timeToDesappear < 12)
        {
            timeToDesappear += Time.deltaTime;
        }
        else
        {
            timeToDesappear = 0;
            disappearTime.Invoke();
        }
    }

    public void Disappear()
    {
        StartCoroutine(flash.FlashCoroutine(3f, noColor, 7));
    }

    public UnityEvent disappearTime;
}
