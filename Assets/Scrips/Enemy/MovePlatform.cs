using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private LayerMask faceDown;
    [SerializeField] private LayerMask faceFront;

    [SerializeField] private float velocity;

    [SerializeField] private float distDown;
    [SerializeField] private float distFront;

    [SerializeField] private Transform down;
    [SerializeField] private Transform front;

    [SerializeField] private bool takeDown;
    private bool infoDown;
    private bool infoFront;

    [SerializeField] private bool lookRight;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (lookRight)
        {
            rb.linearVelocityX = velocity;
        }
        else
        {
            rb.linearVelocityX = -velocity;
        }


        infoFront = Physics2D.Raycast(front.position, transform.right, distFront, faceFront);
        infoDown = Physics2D.Raycast(down.position, transform.up * -1, distDown, faceDown);

        if (infoFront)
        {
            flip();
        }

        if (takeDown)
        {
            if (!infoDown)
            {
                flip();
            }
        }
    }

    private void flip()
    {
        lookRight = !lookRight;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(down.transform.position, down.transform.position + transform.up * -1 * distDown);
        Gizmos.DrawLine(front.transform.position, front.transform.position + transform.right * distFront);
    }
}

