using Unity.Cinemachine;
using UnityEngine;

public class ArenaTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraPosition;

    [SerializeField] private float cameraSize = 10f;
    [SerializeField] private bool followPlayer = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var lens = cinemachineCamera.Lens;
            lens.OrthographicSize = cameraSize;
            cinemachineCamera.Lens = lens;

            if (followPlayer) 
            { 
                cinemachineCamera.Follow = player;
                cinemachineCamera.LookAt = player;
            }else if(!followPlayer)
            {
                cinemachineCamera.Follow = cameraPosition;
                cinemachineCamera.LookAt = cameraPosition;
            }
        }
    }
}
