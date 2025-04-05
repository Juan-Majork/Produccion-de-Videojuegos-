using System.Collections;
using UnityEngine;

public class InvicibleController : MonoBehaviour
{
    private HealthController healthController;

    private void Awake()
    {
        healthController = GetComponent<HealthController>();
    }

    public void StartInvicibility(float invicibilytyDuration)
    {
        StartCoroutine(InvincibilityCoroutine(invicibilytyDuration));
    }

    private IEnumerator InvincibilityCoroutine(float invicibilytyDuration)
    {
        healthController.isInvicible = true;
        yield return new WaitForSeconds(invicibilytyDuration);
        healthController.isInvicible = false;
    }
}
