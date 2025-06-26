using UnityEngine;

public class BackgroundMoves : MonoBehaviour
{
    [SerializeField] private Vector2 velMovimiento;

    private Vector2 offset;

    private Material material;

    private Rigidbody2D playerRB2D;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        playerRB2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        offset = (playerRB2D.linearVelocityX * 0.1f) * velMovimiento * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
