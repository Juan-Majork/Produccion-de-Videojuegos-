using UnityEngine;

public class dropMagic : MonoBehaviour
{
    private MagicAttackController magicController;

    [SerializeField] private GameObject fireDrop;
    [SerializeField] private GameObject waterDrop;
    [SerializeField] private GameObject rockDrop;

    [SerializeField] private Transform spawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        magicController = GetComponent<MagicAttackController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            if (magicController.slots[0] == Spells.Empty)
            {
                return;
            }
            else if (magicController.slots[0] == Spells.Fire)
            {
                Instantiate(fireDrop, spawn.position, spawn.rotation);
                magicController.slots[0] = Spells.Empty;
                return;
            }
            else if (magicController.slots[0] == Spells.Water)
            {
                Instantiate(waterDrop, spawn.position, spawn.rotation);
                magicController.slots[0] = Spells.Empty;
                return;
            }
            else if (magicController.slots[0] == Spells.Rock)
            {
                Instantiate(rockDrop, spawn.position, spawn.rotation);
                magicController.slots[0] = Spells.Empty;
                return;
            }
        }
    }
}
