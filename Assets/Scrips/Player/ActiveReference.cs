using UnityEngine;

public class ActiveReference : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private MagicAttackController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.slots[0] != Spells.Rock)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }
}
