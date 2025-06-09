using UnityEngine;
using UnityEngine.InputSystem;

public class dropMagic : MonoBehaviour
{
    private MagicAttackController magicController;

    [SerializeField] private GameObject fireDrop;
    [SerializeField] private GameObject waterDrop;
    [SerializeField] private GameObject rockDrop;

    [SerializeField] private Transform spawn;

    private PlayerInput playerInput;

    private InputAction inputDrop;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        magicController = GetComponent<MagicAttackController>();

        playerInput = GetComponent<PlayerInput>();
        inputDrop = playerInput.actions["Drop spell"];
    }

    private void OnEnable()
    {
        inputDrop.started += _ => Drop();
    }

    private void OnDisable()
    {
        inputDrop.started -= _ => Drop();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.L))
        {
            Drop();
        }*/
    }

    private void Drop()
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
