using UnityEngine;

public class ChangeColorFeedback : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private SpriteFlash flash;

    [SerializeField] Color hitColor;
    Color actualColor;

    private void Awake()
    {
        flash = GetComponent<SpriteFlash>();
    }

    public void DamageFeedback()
    {
        StartCoroutine(flash.FlashCoroutine(0.5f, hitColor, 3));
    }
}
