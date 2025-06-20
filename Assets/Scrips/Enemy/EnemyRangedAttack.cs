using UnityEngine;
using static FollowPlayerArea;

public class EnemyRangedAttack : MonoBehaviour
{
    [SerializeField] private GameObject shot;
    [SerializeField] private Transform spawn;
    private SpriteRenderer render;
    private Color originalColor;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float fidingRadio;
    private Transform playerTransform;

    [SerializeField] private float waitToShot;
    [SerializeField] public float actualTime;

    [SerializeField] private bool isRight;

    [SerializeField] private float shotVelocity;

    public bool isCold = false;
    public float duration = 0;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        originalColor = render.color;
    }

    private void Update()
    {
        if (isCold)
        {
            render.color = Color.cyan;
            duration += Time.deltaTime;
            Debug.Log(duration.ToString());

            if (duration > 2)
            {
                render.color = originalColor;
                isCold = false;
                duration = 0;   
            }
        }

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
            if (isCold)
            {
                return;
            }

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
