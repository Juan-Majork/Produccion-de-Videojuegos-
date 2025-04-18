using UnityEngine;

public class ManaBarUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image manaImagen;

    [SerializeField] private Color[] spellColor;

    public void changeMana(MagicAttackController magicAttackController)
    {
        manaImagen.fillAmount = magicAttackController.manaPercentage;
    }
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
        else if (magicAttackController.currentSpell == Spells.Water)
        {
            manaImagen.color = spellColor[2];
        }
        else if (magicAttackController.currentSpell == Spells.Rock)
        {
            manaImagen.color = spellColor[3];
        }
    }
}
