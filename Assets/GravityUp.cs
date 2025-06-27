using UnityEngine;
using System.Collections;


public class GravityUp : MonoBehaviour
{
    [SerializeField] private bool gravityUp = true;
    [SerializeField] private float gravityScaleValue = 2f;
    [SerializeField] private BreakFloor breakFloor;

    private bool velocityApplied = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !velocityApplied)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            rb.gravityScale = gravityUp ? gravityScaleValue : -gravityScaleValue;

            rb.linearVelocityY = 15;

            velocityApplied = true;

            if (breakFloor != null)
            {
                StartCoroutine(ActivateAfterDelay(0.5f));
            }
        }
    }
    private IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        breakFloor.ActivateTilemap();
    }
}
