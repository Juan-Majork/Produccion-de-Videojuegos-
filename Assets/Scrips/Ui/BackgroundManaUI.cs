using UnityEngine;
using UnityEngine.UI;

public class BackgroundManaUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image manaImagen;

    [SerializeField] private Color[] spellColor;

    public void changeColor(MagicAttackController magicAttackController)
    {
        if (magicAttackController.currentSpell == Spells.Empty)
        {
            manaImagen.color = spellColor[0];
        }
        else if (magicAttackController.currentSpell == Spells.Fire)
        {
            manaImagen.color = spellColor[1];
        }
        if (magicAttackController.currentSpell == Spells.Water)
        {
            manaImagen.color = spellColor[2];
        }
        if (magicAttackController.currentSpell == Spells.Rock)
        {
            manaImagen.color = spellColor[3];
        }
    }
}
