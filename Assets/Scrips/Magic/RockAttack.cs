using UnityEngine;
using UnityEngine.Events;

public class RockAttack : MonoBehaviour
{
    private Rigidbody2D rb;

    private float gravity = 2;
    [SerializeField] private float waitToFall;
    private float actualTime = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        actualTime += Time.deltaTime;

        if (actualTime >= waitToFall)
        {
            rb.gravityScale = gravity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            timeToDestroy.Invoke();
        }
    }

    public UnityEvent timeToDestroy;
}
