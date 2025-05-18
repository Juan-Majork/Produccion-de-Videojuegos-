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

    private float mana;
    private float [] magicMana = new float[3];
    //0 = Fire, 1 = Water, 2 = Rock

    [SerializeField] 
    private float maxMana;

    private float speedMagic;

    private float waitShoot;
    private float lastShoot;

    private float timeToSwap = 0;
    private bool selectSlot = false;

    [SerializeField] 
    private GameObject firePrefap;
    private bool setFire;

    [SerializeField] 
    private GameObject waterPrefap;
    [SerializeField] 
    private GameObject rockPrefap;

    [SerializeField]
    private Transform baseSpawner;
    [SerializeField]
    private Transform upSpawner;


    public Spells[] slots = new Spells[2];
    public Spells currentSpell;

    Animator animator;

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
        animator = transform.GetChild(0).GetComponent<Animator>();

        slots[0] = Spells.Empty;
        slots[1] = Spells.Empty;

    }

    private void FixedUpdate()
    {

        if (timeToSwap > 0)
        {
            timeToSwap -= Time.deltaTime;
        }

        /*
        if (Input.GetKey(KeyCode.Alpha1) && selectSlot)
        {
            Swap();
        }
        if (Input.GetKey(KeyCode.Alpha2) && !selectSlot)
        {
            Swap();
        }
        */

        if (Input.GetKey(KeyCode.E))
        {
            Swap();
        }

        if (/*Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.W)*/ Input.GetMouseButton(0))
            {           
            {
                waitShoot = 0.5f;
            float timeSinceShoot = Time.time - lastShoot;

            if (timeSinceShoot >= waitShoot)
            {
                animator.SetTrigger("baseAttack");
                faceAttack.actualTime = 0;
                lastShoot = Time.time;
            }
            }
        }

       
            
            if (/*Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.J)*/ Input.GetMouseButton(1))
            {

                {
                if (slots[0] == Spells.Fire)
            {
                waitShoot = 0.2f;

                float timeSinceShoot = Time.time - lastShoot;

                if (magicMana[0] > 0 && !setFire && timeSinceShoot >= waitShoot)
                {
                    firePrefap.SetActive(true);
                    setFire = true;
                    lastShoot = Time.time;
                }
                else if (setFire && timeSinceShoot >= waitShoot)
                {
                    firePrefap.SetActive(false);
                    setFire = false;
                    lastShoot = Time.time;
                }
            }
            if (slots[0] == Spells.Water && mana > 0)
            {
                speedMagic = 10f;
                waitShoot = 0.1f;

                float timeSinceShoot = Time.time - lastShoot;

                if (timeSinceShoot >= waitShoot)
                {
                    animator.SetTrigger("iceAttack");
                    SpawnMagic(waterPrefap, baseSpawner);
                    lastShoot = Time.time;
                    magicMana[1] -= 10;

                    if (magicMana[1] < 0)
                    {
                        magicMana[1] = 0;
                    }
                }
            }
            if (slots[0] == Spells.Rock && mana > 0)
            {
                speedMagic = 0f;
                waitShoot = 1f;

                float timeSinceShoot = Time.time - lastShoot;

                if (timeSinceShoot >= waitShoot)
                {
                    animator.SetTrigger("castRock");
                    SpawnMagic(rockPrefap, upSpawner);
                    lastShoot = Time.time;
                    magicMana[2] -= 20;
                    if (magicMana[2] < 0)
                    {
                        magicMana[2] = 0;
                    }
                }
            }

            }
        }

        if (setFire)
        {
            magicMana[0] -= 0.2f;
        }

        if (magicMana[0] <= 0 || slots[0] != Spells.Fire)
        {
            firePrefap.SetActive (false);
            setFire = false;
        }

        animator.SetBool("isFire", setFire);

        if (slots[0] == Spells.Empty)
        {
            mana = 0;
        }
        if (slots[0] == Spells.Fire)
        {
            mana = magicMana[0];
        }
        if (slots[0] == Spells.Water)
        {
            mana = magicMana[1];
        }
        if (slots[0] == Spells.Rock)
        {
            mana = magicMana[2];
        }

        if (mana < 0)
        {
            mana = 0;
        }
        else if (mana > 100)
        {
            mana = maxMana;
        }

        currentSpell = slots[0];

        changeMana.Invoke();
        changeManaColor.Invoke();
    }

    private void Swap()
    {
        if (timeToSwap <= 0)
        {
            var first = slots[0];
            slots[0] = slots[1];
            slots[1] = first;
            timeToSwap = 0.5f;
            selectSlot = !selectSlot;
        }
    }

    public void DeactiveFire()
    {
        setFire = false;
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

    public void restoreMana(float recharge, int magic)
    {
        if (magicMana[magic] == maxMana)
        {
            return;
        }

        magicMana[magic] += recharge;

        if (magicMana[magic] > maxMana)
        {
            magicMana[magic] = maxMana;
        }

        changeMana.Invoke();
    }

    public UnityEvent changeMana;
    public UnityEvent changeManaColor;
}
