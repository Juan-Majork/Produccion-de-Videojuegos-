using UnityEngine;

public class CallManaTransfer : MonoBehaviour
{
    [SerializeField] MagicAttackController controller;

    public void CallTakePlayerData()
    {
        ManaTransfer.Instance.TakePlayerData(controller);
    }

    public void CallGiveBackPlayerData()
    {
        ManaTransfer.Instance.GiveBackPlayerData(controller);
    }
}
