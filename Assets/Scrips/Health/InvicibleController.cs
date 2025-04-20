using System.Collections;
using UnityEngine;

public class InvicibleController : MonoBehaviour
{
    private HealthController healthController;
    private SpriteFlash flash;

    private void Awake()
    {
        healthController = GetComponent<HealthController>();
        flash = GetComponent<SpriteFlash>();
    }

    public void StartInvicibility(float invicibilytyDuration, Color flashColor, int numOffFlashes)
    {
        StartCoroutine(InvincibilityCoroutine(invicibilytyDuration, flashColor, numOffFlashes));
    }

    private IEnumerator InvincibilityCoroutine(float invicibilytyDuration, Color flashColor, int numOffFlashes)
    {
        healthController.isInvicible = true;
        yield return flash.FlashCoroutine(invicibilytyDuration, flashColor, numOffFlashes);
        healthController.isInvicible = false;
    }
}
