using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    //arena 1 a arena 2
    [SerializeField] private Transform[] arena2Path;

    //arena 2 a arena 3
    [SerializeField] private Transform[] arena3Path;

    //arena 3 a arena 4
    [SerializeField] private Transform[] arena4Path;

    //arena 4 a arena 5
    [SerializeField] private Transform[] arena5Path;

    //arena 5 a arena 6
    [SerializeField] private Transform[] arena6Path; 

    private Transform[] currentPath;
    private int currentTargetIndex = 0;
    private bool isMoving = false;

    public bool IsMoving() => isMoving;

    public void StartMoveToArena2() => StartPath(arena2Path);
    public void StartMoveToArena3() => StartPath(arena3Path);
    public void StartMoveToArena4() => StartPath(arena4Path);
    public void StartMoveToArena5() => StartPath(arena5Path);
    public void StartMoveToArena6() => StartPath(arena6Path);

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
}
