using UnityEngine;

public class PlayerDamageInvincibility : MonoBehaviour
{
    [SerializeField] private float invincibilityDuration;

    private InvicibleController invicibleController;

    private void Awake()
    {
        invicibleController = GetComponent<InvicibleController>();
    }

    public void StartInvincibility()
    {
        invicibleController.StartInvicibility(invincibilityDuration);
    }
}
