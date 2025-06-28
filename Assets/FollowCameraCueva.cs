using UnityEngine;

public class FollowCameraCueva : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] GameObject destroyExtra;

    private bool followPlayer = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (followPlayer)
        {
            transform.position = new Vector2(cam.transform.position.x, cam.transform.position.y);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            followPlayer = true;
            Destroy(destroyExtra);
        }
    }
}
