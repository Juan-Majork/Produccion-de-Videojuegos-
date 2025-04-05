using System.Net.Sockets;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Events;

public enum Spells
{
    Empty,
    Fire,
    Water,
    Rock
};

public class MagicAttackController : MonoBehaviour
{
    private Movement move;
    private nearAttack faceAttack;

    [SerializeField] 
    private float mana;
    [SerializeField] 
    private float maxMana;

    private float speedMagic;

    private float waitShoot;
    private float lastShoot;

    private bool fireContinue1 = false;
    private bool fireContinue2 = false;
    private float timeToSwap = 0;

    [SerializeField] 
    private GameObject firePrefap;
    [SerializeField] 
    private GameObject waterPrefap;
    [SerializeField] 
    private GameObject rockPrefap;

    [SerializeField]
    private Transform baseSpawner;
    [SerializeField]
    private Transform upSpawner;


    public Spells[] slots = new Spells[2];

    public float manaPercentage
    {
        get
        {
            return mana / maxMana;
        }
    }

    private void Awake()
    {
        move = GetComponent<Movement>();
        faceAttack = GetComponentInChildren<nearAttack>();

        slots[0] = Spells.Empty;
        slots[1] = Spells.Empty;
    }

    private void FixedUpdate()
    {

        if (timeToSwap > 0)
        {
            timeToSwap -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.I))
        {
            Swap();
        }

        if (Input.GetKey(KeyCode.K))
        {
            fireContinue1 = true;
        }
        else
        {
            fireContinue1 = false;
        }
        if (fireContinue1)
        {
            waitShoot = 0.5f;
            float timeSinceShoot = Time.time - lastShoot;

            if (timeSinceShoot >= waitShoot)
            {
                faceAttack.actualTime = 0;
                lastShoot = Time.time;
            }

        }

        if (Input.GetKey (KeyCode.J))
        {
            fireContinue2 = true;
        }
        else 
        {
            fireContinue2 = false;
        }
        if (fireContinue2)
        {
            if (slots[0] == Spells.Fire && mana > 0)
            {

            }
            if (slots[0] == Spells.Water && mana > 0)
            {
                speedMagic = 20f;
                waitShoot = 0.1f;

                float timeSinceShoot = Time.time - lastShoot;

                if (timeSinceShoot >= waitShoot)
                {
                    SpawnMagic(waterPrefap, baseSpawner);
                    lastShoot = Time.time;
                    mana -= 10;
                }
            }
            if (slots[0] == Spells.Rock && mana > 0)
            {
                speedMagic = -0f;
                waitShoot = 1f;

                float timeSinceShoot = Time.time - lastShoot;

                if (timeSinceShoot >= waitShoot)
                {
                    SpawnMagic(rockPrefap, upSpawner);
                    lastShoot = Time.time;
                    mana -= 25;
                }
            }
        }

        if(mana < 0)
        {
            mana = 0;
        }

        changeMana.Invoke();
    }

    private void Swap()
    {
        if (timeToSwap <= 0)
        {
            var first = slots[0];
            slots[0] = slots[1];
            slots[1] = first;
            timeToSwap = 1;
        }
    }

    private void SpawnMagic(GameObject attackPrefap, Transform spawnPoint)
    {
        GameObject attack = Instantiate(attackPrefap, spawnPoint.position, spawnPoint.rotation);
        Rigidbody2D rb = attack.GetComponent<Rigidbody2D>();
        if (slots[0] == Spells.Rock)
        {
            return;
        }
        else
        {
            if (move.facingRight)
            {
                rb.linearVelocity = speedMagic * transform.right;
            }
            else
            {
                rb.linearVelocity = -speedMagic * transform.right;
            }
            
        }
    }

    public void restoreMana(float recharge)
    {
        if (mana == maxMana)
        {
            return;
        }

        mana += recharge;

        if (mana > maxMana)
        {
            mana = maxMana;
        }

        changeMana.Invoke();
    }

    public UnityEvent changeMana;
}
