using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirePush : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private class BurnTracker
    {
        public Collider2D collider;// referencia al collider

        public BurnTracker(Collider2D col)
        {
            collider = col;
        }
    }

    private List<BurnTracker> tracked = new List<BurnTracker>();//lista de objetos dentro de el hechizo de fuego

    private void Update()
    {
        for (int i = tracked.Count - 1; i >= 0; i--)//recorre cada objeto dentro de el hechizo
        {
            BurnTracker tracker = tracked[i];

            MovePlatform move = tracker.collider.GetComponent<MovePlatform>();
            if (move != null)
            {
                if (move.CompareTag("IceEnemy"))
                {
                    Rigidbody2D rb = tracker.collider.GetComponent<Rigidbody2D>();

                    if (player.transform.position.x > tracker.collider.transform.position.x)
                    {
                        rb.AddForceX(-6.5f, ForceMode2D.Impulse);

                    }

                    if (player.transform.position.x < tracker.collider.transform.position.x)
                    {
                        rb.AddForceX(6.5f, ForceMode2D.Impulse);

                    }

                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//cuando un objeto entra a el hechizo
    {

        for (int i = tracked.Count - 1; i >= 0; i--)//asegura que no haya duplicados
        {
            if (tracked[i].collider == collision)
            {
                tracked.RemoveAt(i);
                break;

            }
        }


        tracked.Add(new BurnTracker(collision));//agrega nuevo objeto a rastrear
    }

    private void OnTriggerExit2D(Collider2D collision)//cuando un objeto sale del hechizo
    {
        for (int i = tracked.Count - 1; i >= 0; i--)
        {
            if (tracked[i].collider == collision)
            {
                MovePlatform move = collision.GetComponent<MovePlatform>();
                if (move != null)
                {
                    if (collision.gameObject.CompareTag("IceEnemy"))
                    {
                        move.ApplyPush(0.4f);
                        tracked.RemoveAt(i);

                        return;
                    }

                }
                tracked.RemoveAt(i);
                break;
            }
        }
    }
}
