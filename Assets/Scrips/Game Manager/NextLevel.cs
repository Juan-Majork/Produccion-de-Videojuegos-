using UnityEngine;
using UnityEngine.Events;

public class NextLevel : MonoBehaviour
{
    public UnityEvent transitionToLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            transitionToLevel.Invoke();
        }
    }
}
