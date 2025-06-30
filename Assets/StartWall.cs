using UnityEngine;
using UnityEngine.Tilemaps;

public class StartWall : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(ActivateWall), 0.2f);
        }
    }

    private void ActivateWall()
    {
        wall.SetActive(true);
    }
}
