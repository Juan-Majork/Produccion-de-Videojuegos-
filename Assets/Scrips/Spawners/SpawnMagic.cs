using UnityEngine;

public class SpawnMagic : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    private int selectItem;

    public void Drop()
    {
        selectItem = Random.Range(0, items.Length);
        Instantiate(items[selectItem], transform.position, transform.rotation); 
    }
}
