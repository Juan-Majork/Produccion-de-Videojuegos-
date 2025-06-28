using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private BossEyeScript[] eyes;
    //arena 1 a arena 2
    [SerializeField] private Transform[] arena2Path;

    //arena 2 a arena 3
    [SerializeField] private Transform[] arena3Path;

    //arena 3 a arena 4
    [SerializeField] private Transform[] arena4Path;

    private Transform[] currentPath;
    private int currentTargetIndex = 0;
    private bool isMoving = false;

    private int hitCount = 0;   
    public bool IsMoving() => isMoving;

    public void StartMoveToArena2() => StartPath(arena2Path);
    public void StartMoveToArena3() => StartPath(arena3Path);
    public void StartMoveToArena4() => StartPath(arena4Path);

    private void StartPath(Transform[] path)
    {
        if (path == null || path.Length == 0) return;

        currentPath = path;
        currentTargetIndex = 0;
        isMoving = true;
    }

    void Update()
    {
        if (!isMoving || currentPath == null || currentTargetIndex >= currentPath.Length)
            return;

        Transform target = currentPath[currentTargetIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            currentTargetIndex++;

            if (currentTargetIndex >= currentPath.Length)
            {
                isMoving = false;
            }
        }
    }

    public void RegisterHit()
    {
        foreach(var eye in eyes)
        {
            eye.PlayHitAnimation();
        }

        if (isMoving) return;

        hitCount++;

        Invoke(nameof(MoveToArea), 0.15f);

        /*switch (hitCount)
        {
            case 1:
                StartMoveToArena2();
                break;
            case 2:
                StartMoveToArena3();
                break;
            case 3:
                StartMoveToArena4();
                break;
            case 4:
                Debug.Log("Victoria");//matar al jefe
                break;
            default:
                break;
        }*/
    }

    private void MoveToArea()
    {
        switch (hitCount)
        {
            case 1:
                StartMoveToArena2();
                break;
            case 2:
                StartMoveToArena3();
                break;
            case 3:
                StartMoveToArena4();
                break;
            case 4:
                Debug.Log("Victoria");//matar al jefe
                break;
            default:
                break;
        }
    }
}
