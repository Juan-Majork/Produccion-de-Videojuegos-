using UnityEngine;

public class MagicCollectableBehaviour : MonoBehaviour, ICollectableBehaviour
{
    [SerializeField] private float ManaRestore;

    [SerializeField] private bool fireActive;
    [SerializeField] private bool waterActive;
    [SerializeField] private bool rockActive;

    public void onCollected(GameObject player)
    {
        player.GetComponent<MagicAttackController>().restoreMana(ManaRestore);
        player.GetComponent<MagicAttackController>().waterAttack = waterActive;
        player.GetComponent<MagicAttackController>().rockAttack = rockActive;
        player.GetComponent<MagicAttackController>().fireAttack = fireActive;
    }
}
