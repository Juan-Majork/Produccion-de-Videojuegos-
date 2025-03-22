using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image hpImagen;
    public void changeHP(HealthController hpControll)
    {
        hpImagen.fillAmount = hpControll.hpPercentage;
    }
}
