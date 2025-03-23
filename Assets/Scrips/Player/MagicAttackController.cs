using System.Net.Sockets;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Events;

public class MagicAttackController : MonoBehaviour
{
    private Movement move;

    [SerializeField] 
    private float mana;
    [SerializeField] 
    private float maxMana;

    private float speedMagic;
    private float waitShoot;
    private float lastShoot;

    private bool fireContinue = false;

    [SerializeField] 
    private GameObject basicPrefap;
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

    public bool baseAttack = true;
    public bool fireAttack;
    public bool waterAttack;
    public bool rockAttack;

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

        baseAttack = true;

        fireAttack = false;
        waterAttack = false;
        rockAttack = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.P))
        {
            fireContinue = true;
        }
        else
        {
            fireContinue = false;
        }
        if (fireContinue)
        {
            if (baseAttack)
            {
                speedMagic = 8f;
                waitShoot = 0.25f;
                float timeSinceShoot = Time.time - lastShoot;

                if (timeSinceShoot >= waitShoot)
                {
                    SpawnMagic(basicPrefap, baseSpawner);
                    lastShoot = Time.time;
                }
            }
            if (fireAttack) 
            {
                
            }
            if (waterAttack)
            {
                speedMagic = 20f;
                waitShoot = 0.1f;

                if (mana > 0)
                {
                    float timeSinceShoot = Time.time - lastShoot;

                    if (timeSinceShoot >= waitShoot)
                    {
                        SpawnMagic(waterPrefap, baseSpawner);
                        lastShoot = Time.time;
                        mana -= 10;
                    }
                }
            }
            if (rockAttack)
            {
                speedMagic = -4f;
                waitShoot = 1f;

                if (mana > 0)
                {
                    float timeSinceShoot = Time.time - lastShoot;

                    if (timeSinceShoot >= waitShoot)
                    {
                        SpawnMagic(rockPrefap, upSpawner);
                        lastShoot = Time.time;
                        mana -= 25;
                    }
                }
            }
        }

        if (mana > 0)
        {
            baseAttack = false;
        }
        else
        {
            baseAttack = true;
        }

        if (mana <= 0)
        {
            fireAttack = false;
            waterAttack = false;
            rockAttack = false;
        }

        changeMana.Invoke();
    }

    private void SpawnMagic(GameObject attackPrefap, Transform spawnPoint)
    {
        GameObject attack = Instantiate(attackPrefap, spawnPoint.position, spawnPoint.rotation);
        Rigidbody2D rb = attack.GetComponent<Rigidbody2D>();
        if (rockAttack)
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
