using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Sprites;
using UnityEngine;

public class MagicCollectableBehaviour : MonoBehaviour, ICollectableBehaviour
{
    public bool Take;

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
        }
        if (waterActive)
        {
            magic = Spells.Water;
        }
        if (rockActive)
        {
            magic = Spells.Rock;
        }

        for (int i = 0; i < player.GetComponent<MagicAttackController>().slots.Length; i++)
        {
            if (player.GetComponent<MagicAttackController>().slots[i] == Spells.Empty)
            {
                player.GetComponent<MagicAttackController>().slots[i] = magic;
                player.GetComponent<MagicAttackController>().restoreMana(ManaRestore);
                Take = true;
                Debug.Log("equipado");

                return;
            }
            else if(player.GetComponent<MagicAttackController>().slots[i] == magic)
            {
                player.GetComponent<MagicAttackController>().restoreMana(ManaRestore);
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
