using UnityEngine;
using static FollowPlayerArea;
using static MovePlatform;

public class EnemyRangedAttack : MonoBehaviour
{
    [SerializeField] private AudioClip shotSound;
    private AudioSource audioSource;

    [SerializeField] private GameObject shot;
    [SerializeField] private Transform spawn;

    private SpriteRenderer render;
    private Animator animator;
    private Color originalColor;
    [SerializeField] private Color iceColor;
    private bool detectingPlayer = false;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float fidingRadio;
    private Transform playerTransform;

    [SerializeField] private float waitToShot;
    [SerializeField] public float actualTime;

    [SerializeField] private bool isRight;

    [SerializeField] private float shotVelocity;

    private float waitUp = 0;
    private float waitDown = 0;

    public bool isCold = false;
    public float duration = 0;

    public distanceState state = distanceState.normal;
    public enum distanceState
    {
        normal,
        detecting,
        attack,
        back
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        originalColor = render.color;
    }

    private void Update()
    {
        if (isCold)
        {
            render.color = iceColor;
            duration += Time.deltaTime;
            Debug.Log(duration.ToString());

            if (duration > 2)
            {
                render.color = originalColor;
                isCold = false;
                duration = 0;   
            }
        }

        switch (state)
        {
            case distanceState.normal:
                NormalState();
                break;
            case distanceState.detecting:
                DetectingState();
                break;
            case distanceState.attack:
                AttackState();
                break;
            case distanceState.back:
                BackState();
                break;
        }

    }

    private void NormalState()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, fidingRadio, playerLayer);

        //Debug.Log("normal");

        if (playerCollider)
        {
            state = distanceState.detecting;
            detectingPlayer = true;
            animator.SetBool("detecting", detectingPlayer);
        }
    }

    private void DetectingState()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, fidingRadio, playerLayer);

        //Debug.Log("detecting");

        waitUp += Time.deltaTime;

        if (waitUp > 1f)
        {
            if (playerCollider == null)
            {
                state = distanceState.back;
                detectingPlayer = false;
                animator.SetBool("detecting", detectingPlayer);
                waitUp = 0f;
                return;
            }
            else if (playerCollider != null)
            {
                state = distanceState.attack;
                detectingPlayer = true;
                animator.SetBool("detecting", detectingPlayer);
                waitUp = 0f;
                return;
            }
        }
    }

    private void AttackState()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, fidingRadio, playerLayer);

        //Debug.Log("attack");

        if (playerCollider)
        {
            playerTransform = playerCollider.transform;
        }
        else
        {
            playerTransform = null;
            detectingPlayer = false;
            animator.SetBool("detecting", detectingPlayer);
            state = distanceState.back;
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
                //SpawnMagic();
                animator.SetTrigger("attack");
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

    private void BackState()
    {
        waitDown += Time.deltaTime;

        //Debug.Log("back");

        if (waitDown > 1.5f)
        {
            state = distanceState.normal;
            waitDown = 0f;
            return;
        }
    }

    private void SpawnMagic()
    {
        GameObject attack = Instantiate(shot, spawn.position, spawn.rotation);
        Rigidbody2D rigid = attack.GetComponent<Rigidbody2D>();

         if (isRight)
         {
            audioSource.PlayOneShot(shotSound);
            rigid.linearVelocity = shotVelocity * transform.right;

         }
         else if (!isRight)
         {
            audioSource.PlayOneShot(shotSound);
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
