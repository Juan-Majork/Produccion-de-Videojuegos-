using UnityEngine;
using static FollowPlayerArea;

public class EnemyRangedAttack : MonoBehaviour
{
    [SerializeField] private GameObject shot;
    [SerializeField] private Transform spawn;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float fidingRadio;
    private Transform playerTransform;

    [SerializeField] private float waitToShot;
    [SerializeField] private float actualTime;

    [SerializeField] private bool isRight;

    [SerializeField] private float shotVelocity;


    private void Update()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, fidingRadio, playerLayer);

        if (playerCollider)
        {
            playerTransform = playerCollider.transform;
        }
        else
        {
            playerTransform = null;
        }

        if (playerTransform != null)
        {
            actualTime += Time.deltaTime;

            if (actualTime > waitToShot)
            {
                SpawnMagic(shot, spawn);
                actualTime = 0;
            }
            
            if (playerTransform.position.x > transform.position.x && !isRight)
            {
                flip();
            }
            else if (playerTransform.position.x < transform.position.x && isRight)
            {
                flip();
            }
        }
    }

    private void SpawnMagic(GameObject attackPrefap, Transform spawnPoint)
    {
        GameObject attack = Instantiate(attackPrefap, spawnPoint.position, spawnPoint.rotation);
        Rigidbody2D rigid = attack.GetComponent<Rigidbody2D>();

         if (isRight)
         {
             rigid.linearVelocity = shotVelocity * transform.right;

         }
         else if (!isRight)
         {
             rigid.linearVelocity = shotVelocity * transform.right;
         }
    }

    private void flip()
    {
        isRight = !isRight;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fidingRadio);
    }

}
