using UnityEngine;

public class ChangeColorFeedback : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private SpriteFlash flash;

    [SerializeField] Color hitColor;
    [SerializeField] Color actualColor;

    private void Awake()
    {
        flash = GetComponent<SpriteFlash>();
    }

    private void Update()
    {
        if (flash.CountOfFlashes >= 3)
        {
            spriteRenderer.color = actualColor;
        }
    }

    public void DamageFeedback()
    {
        StartCoroutine(flash.FlashCoroutine(0.5f, hitColor, 3));
    }
}
