using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class MagicCollectableBehaviour : MonoBehaviour, ICollectableBehaviour
{
    public bool Take;
    private int magicTake;

    [SerializeField] private float ManaRestore;

    [SerializeField] private bool fireActive;
    [SerializeField] private bool waterActive;
    [SerializeField] private bool rockActive;

    public void onCollected(GameObject player)
    {
        
        var magic = Spells.Empty;

        if (fireActive)
        {
            magic = Spells.Fire;
            magicTake = 0;
        }
        if (waterActive)
        {
            magic = Spells.Water;
            magicTake = 1;
        }
        if (rockActive)
        {
            magic = Spells.Rock;
            magicTake = 2;
        }

        for (int i = 0; i < player.GetComponent<MagicAttackController>().slots.Length; i++)
        {
            if (player.GetComponent<MagicAttackController>().slots[i] == Spells.Empty)
            {
                player.GetComponent<MagicAttackController>().slots[i] = magic;
                player.GetComponent<MagicAttackController>().restoreMana(ManaRestore, magicTake);
                Take = true;
                Debug.Log("equipado");

                return;
            }
            else if(player.GetComponent<MagicAttackController>().slots[i] == magic)
            {
                player.GetComponent<MagicAttackController>().restoreMana(ManaRestore, magicTake);
                Take = true;
                Debug.Log("restaurado");

                return;
            }
            else
            {
                Take = false;
            }
        }

    }
}
