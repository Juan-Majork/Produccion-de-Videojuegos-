using UnityEngine;

public class BossEyeScript : MonoBehaviour
{
    [SerializeField] private BossController bossController;

    private int hitCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("hit") && !bossController.IsMoving())
        {
            hitCount++;

            switch (hitCount)
            {
                case 1:
                    bossController.StartMoveToArena2();
                    break;
                case 2:
                    bossController.StartMoveToArena3();
                    break;
                case 3:
                    bossController.StartMoveToArena4();
                    break;
                case 4:
                    bossController.StartMoveToArena5();
                    break;
                case 5:
                    bossController.StartMoveToArena6();
                    break;
                default:
                    break;
            }
        }
    }
}
