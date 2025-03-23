using UnityEngine;

public class Collectable : MonoBehaviour
{
    private ICollectableBehaviour m_Collectable;

    private void Awake()
    {
        m_Collectable = GetComponent<ICollectableBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Movement>();

        if (player != null)
        {
            m_Collectable.onCollected(player.gameObject);
            Destroy(gameObject);
        }
    }
}
