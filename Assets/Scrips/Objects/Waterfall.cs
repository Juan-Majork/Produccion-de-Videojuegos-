using UnityEngine;
using UnityEngine.Events;

public class Waterfall : MonoBehaviour
{
    [SerializeField] private enum Direction { Left, Right, Up, Down, LU } // define la direccion de el empuje de la cascada
    [SerializeField] private Direction pushDirection = Direction.Left; // direccion actual de el empuje

    [SerializeField] private float pushForceX; //fuerza de el empuje
    [SerializeField] private float pushForceY; //fuerza de el empuje
    private bool isFrozen = false;// si la cascada esta congelada

    private Collider2D col;//referencia a el colider
    private SpriteRenderer spriteRenderer;//referencia a el sprite renderer

    private string newTag = "Floor";//cambia el tag

    [SerializeField] private float timeToUnfreeze = 20;

    [SerializeField] private bool haveRender;

    public bool ice = false;


    private void Awake()
    {
        //obtener lo componentes
        col = GetComponent<Collider2D>();

        if (haveRender)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isFrozen) return;// si esta congelada no hace nada

        if (other.CompareTag("Player"))//solo funciona si el objeto tiene tag Player (se puede agregar enemigos)
        {

            other.GetComponent<Movement>().SetInWaterfall(true);// marca si el player esta en la cascada

            //empuja a el jugador si esta en la cascada
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 force = pushDirection switch
                {
                    Direction.Left => Vector2.left * pushForceX,
                    Direction.Right => Vector2.right * pushForceX,
                    Direction.Up => Vector2.up * pushForceX,
                    Direction.Down => Vector2.down * pushForceX,
                    Direction.LU => new Vector2 (-1 * pushForceX, 1 * pushForceY),
                    _ => Vector2.zero
                };

                if (pushDirection == Direction.LU)
                {
                    pushForceY = 2.5f;
                    rb.AddForce(force, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(force, ForceMode2D.Force);//aplica la fuerza a el rigibody de el jugador  
                }

            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Movement>().SetInWaterfall(false);//indica que el jugador ya esta en la cascada
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {    
        if (other.CompareTag("iceMagic") && !isFrozen)
        {
            changeIce.Invoke();
            FreezeWaterfall();//congela la cascada

            Destroy (other.gameObject);
            // destruir la bala de hielo
            //Destroy(other.gameObject);
        }
    }

    public void FreezeWaterfall()//metodo para congelar la cascada
    {
        isFrozen = true;//cambia el bool a congelado

        if (haveRender)
        {
            spriteRenderer.color = Color.cyan; //cambia el color de el sprite (cambiarlo despues por un sprite)
        }

        ice = true;

        col.isTrigger = false; //remueve el trigger de el collider y lo hace solido

        gameObject.tag = newTag;//cambia el tag

        //fuerza la salida del estado adentro del agua
        Collider2D[] hits = Physics2D.OverlapBoxAll(col.bounds.center, col.bounds.size, 0f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<Movement>().SetInWaterfall(false);
            }
        }

        // Descongela despues 20 segundos
        Invoke(nameof(UnfreezeWaterfall), timeToUnfreeze);
    }

    public void UnfreezeWaterfall() // metodo para descongelar
    {
        isFrozen = false;

        if (haveRender)
        {
            spriteRenderer.color = Color.blue; //color original
        }

        ice = false;

        col.isTrigger = true; // vuelve a ser un trigger
        gameObject.tag = "Untagged"; // cambiar el tag
    }


    public UnityEvent changeIce;
}
