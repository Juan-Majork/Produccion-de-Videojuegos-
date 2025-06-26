using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakFloor : MonoBehaviour
{
    private Tilemap tilemap;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            gameObject.SetActive(false);
            //tilemap.ClearAllTiles();
        }
    }

}
