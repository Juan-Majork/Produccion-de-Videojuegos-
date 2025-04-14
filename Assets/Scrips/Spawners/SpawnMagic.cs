using UnityEngine;

public class SpawnMagic : MonoBehaviour
{
    [SerializeField] private GameObject magic;

    public void Drop()
    {
        Instantiate(magic, transform.position, transform.rotation); 
    }
}
