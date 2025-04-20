using UnityEngine;

public class PlayerDamageInvincibility : MonoBehaviour
{
    [SerializeField] private float invincibilityDuration;

    [SerializeField] private Color flashColor;

    [SerializeField] private int numOffFlashes;

    private InvicibleController invicibleController;

    private void Awake()
    {
        invicibleController = GetComponent<InvicibleController>();
    }

    public void StartInvincibility()
    {
        invicibleController.StartInvicibility(invincibilityDuration, flashColor, numOffFlashes);
    }
}
