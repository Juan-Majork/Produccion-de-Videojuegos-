using UnityEngine;

public class Collectable : MonoBehaviour
{
    private ICollectableBehaviour m_Collectable;
    private MagicCollectableBehaviour m_MagicCollectable;

    private void Awake()
    {
        m_Collectable = GetComponent<ICollectableBehaviour>();
        m_MagicCollectable = GetComponent<MagicCollectableBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Movement>();

        if (player != null)
        {
            m_Collectable.onCollected(player.gameObject);

            if (m_MagicCollectable.Take == true) 
            {
                Destroy(gameObject);
            }
            else
            {
                return;
            }
        }
    }
}
