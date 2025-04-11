using UnityEngine;

public class Waterfall : MonoBehaviour
{
    [SerializeField] private enum Direction { Left, Right } // define la direccion de el empuje de la cascada
    [SerializeField] private Direction pushDirection = Direction.Left; // direccion actual de el empuje

    [SerializeField] private float pushForce; //fuerza de el empuje
    private bool isFrozen = false;// si la cascada esta congelada

    private Collider2D col;//referencia a el colider
    private SpriteRenderer spriteRenderer;//referencia a el sprite renderer

    private void Awake()
    {
        //obtener lo componentes
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
                Vector2 force = Vector2.zero;

                if (pushDirection == Direction.Left)
                {
                    force = Vector2.left * pushForce;//empuja hacia la izquierda
                }
                else if (pushDirection == Direction.Right)
                {
                    force = Vector2.right * pushForce;//empuja hacia la derecha
                }

                rb.AddForce(force, ForceMode2D.Force);//aplica la fuerza a el rigibody de el jugador

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
            FreezeWaterfall();//congela la cascada

            // destruir la bala de hielo
            //Destroy(other.gameObject);
        }
    }

    public void FreezeWaterfall()//metodo para congelar la cascada
    {
        isFrozen = true;//cambia el bool a congelado

        spriteRenderer.color = Color.cyan; //cambia el color de el sprite (cambiarlo despues por un sprite)

        col.isTrigger = false; //remueve el trigger de el collider y lo hace solido

    }

}
