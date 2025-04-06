using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    [SerializeField] private float burnDuration = 5f;
    [SerializeField] private float damagePerSecond = 2f;
    [SerializeField] private float tickInterval = 1f;
    [SerializeField] private float requiredStayTime = 0.2f;

    private class BurnTracker
    {
        public Collider2D collider;
        public float timeInside;
        public bool burnApplied;

        public BurnTracker(Collider2D col)
        {
            collider = col;
            timeInside = 0f;
            burnApplied = false;
        }
    }

    private List<BurnTracker> tracked = new List<BurnTracker>();

    private void Update()
    {
        for (int i = tracked.Count - 1; i >= 0; i--)
        {
            BurnTracker tracker = tracked[i];

            if (tracker.burnApplied) continue;

            tracker.timeInside += Time.deltaTime;

            if (tracker.timeInside >= requiredStayTime)
            {
                BurnDamage burn = tracker.collider.GetComponent<BurnDamage>();
                if (burn != null)
                {
                    burn.ApplyBurn(burnDuration, damagePerSecond, tickInterval);
                }

                tracker.burnApplied = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        for (int i = tracked.Count - 1; i >= 0; i--)
        {
            if (tracked[i].collider == collision)
            {
                tracked.RemoveAt(i);
                break;
            }
        }


        tracked.Add(new BurnTracker(collision));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = tracked.Count - 1; i >= 0; i--)
        {
            if (tracked[i].collider == collision)
            {
                tracked.RemoveAt(i);
                break;
            }
        }
    }
}
