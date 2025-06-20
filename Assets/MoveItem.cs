using UnityEngine;

public class MoveItem : MonoBehaviour
{
    private float timeToMove = 0;
    private bool up = false;

    // Update is called once per frame
    void Update()
    {
        if (up)
        {
            timeToMove += Time.deltaTime;
            transform.Translate(Vector2.up * Time.deltaTime);

            if (timeToMove > 0.5f) 
            {
                timeToMove = 0;
                up = false;
            }
        }

        if (!up)
        {
            timeToMove += Time.deltaTime;
            transform.Translate(Vector2.down * Time.deltaTime);

            if (timeToMove > 0.5f)
            {
                timeToMove = 0;
                up = true;
            }
        }
    }
}
