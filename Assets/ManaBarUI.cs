using UnityEngine;

public class ManaBarUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image manaImagen;
    public void changeMana(MagicAttackController magicAttackController)
    {
        manaImagen.fillAmount = magicAttackController.manaPercentage;
    }
}
