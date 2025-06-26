using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(cam.transform.position.x, transform.position.y);
    }
}
