using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ManaTransfer : MonoBehaviour
{
    public static ManaTransfer Instance;
    private MagicAttackController playerController;

    public Spells[] saveSlots = new Spells[2];
    private float[] saveMagicMana = new float[3];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void TakePlayerData(MagicAttackController attackController)
    {
        playerController = attackController;

        for (int i = 0; i < saveMagicMana.Length; i++)
        {
            saveMagicMana[i] = playerController.magicMana[i];
        }

        for (int i = 0;i < saveSlots.Length; i++)
        {
            saveSlots[i] = playerController.slots[i];
        }

        Debug.Log("Take");
    }

    public void GiveBackPlayerData(MagicAttackController attackController)
    {

        playerController = attackController;

        for (int i = 0; i < playerController.magicMana.Length; i++)
        {
            playerController.magicMana[i] = saveMagicMana[i];
        }

        for (int i = 0; i < playerController.slots.Length; i++)
        {
            playerController.slots[i] = saveSlots[i];
        }

        Debug.Log("Out");
    }

}
