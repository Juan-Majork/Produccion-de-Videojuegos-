using UnityEngine;

public class ManaBarUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image[] manaImagen;
    [SerializeField] private UnityEngine.UI.Image[] simbolImagen;

    private int selectedSpell = 0;

    public void SelectMana(MagicAttackController magicAttackController)
    {
        if (magicAttackController.currentSpell == Spells.Fire)
        {
            selectedSpell = 0;
        }
        else if (magicAttackController.currentSpell == Spells.Water)
        {
            selectedSpell = 1;
        }
        else if (magicAttackController.currentSpell == Spells.Fire)
        {
            selectedSpell = 2;
        }

    }
    public void changeMana(MagicAttackController magicAttackController)
    {
        manaImagen[selectedSpell].fillAmount = magicAttackController.manaPercentage;
    }
    public void changeColor(MagicAttackController magicAttackController)
    {
        if (magicAttackController.currentSpell == Spells.Empty)
        {
            manaImagen[0].enabled = false;
            manaImagen[1].enabled = false;
            manaImagen[2].enabled = false;

            simbolImagen[0].enabled = false;
            simbolImagen[1].enabled = false;
            simbolImagen[2].enabled = false;
        }
        else if (magicAttackController.currentSpell == Spells.Fire)
        {
            manaImagen[0].enabled = true;

            manaImagen[1].enabled = false;
            manaImagen[2].enabled = false;

            simbolImagen[0].enabled = true;

            simbolImagen[1].enabled = false;
            simbolImagen[2].enabled = false;
        }
        else if (magicAttackController.currentSpell == Spells.Water)
        {
            manaImagen[1].enabled = true;

            manaImagen[0].enabled = false;
            manaImagen[2].enabled = false;

            simbolImagen[1].enabled = true;

            simbolImagen[0].enabled = false;
            simbolImagen[2].enabled = false;
        }
        else if (magicAttackController.currentSpell == Spells.Rock)
        {
            manaImagen[2].enabled = true;

            manaImagen[0].enabled = false;
            manaImagen[1].enabled = false;

            simbolImagen[2].enabled = true;

            simbolImagen[0].enabled = false;
            simbolImagen[1].enabled = false;
        }
    }
}
