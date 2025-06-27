using UnityEngine;

public class BossEyeScript : MonoBehaviour
{
    [SerializeField] private BossController bossController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("hit"))
        {
            bossController.RegisterHit();
        }
    }

}
