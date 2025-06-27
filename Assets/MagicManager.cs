using System.Collections;
using UnityEngine;

public class MagicManager : MonoBehaviour
{
    [SerializeField] private int maxMagicAllowed = 5;
    [SerializeField] private float checkInterval = 1f;
    [SerializeField] private GameObject magicPrefab; // Prefab para identificar qué objetos limitar

    void Start()
    {
        StartCoroutine(CheckMagic());
    }

    private IEnumerator CheckMagic()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            GameObject[] allMagic = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            int count = 0;

            // Contar solo los que son del mismo prefab
            foreach (GameObject obj in allMagic)
            {
                if (obj != null && obj.name.Contains(magicPrefab.name))
                {
                    count++;
                }
            }

            if (count > maxMagicAllowed)
            {
                Debug.Log("Se excedió el límite de objetos mágicos. Destruyendo extras.");

                int extrasToRemove = count - maxMagicAllowed;
                int removed = 0;

                // Segunda pasada: eliminar extras (empezando por los más viejos)
                foreach (GameObject obj in allMagic)
                {
                    if (obj != null && obj.name.Contains(magicPrefab.name))
                    {
                        Destroy(obj);
                        removed++;
                        if (removed >= extrasToRemove) break;
                    }
                }
            }
        }
    }
}
