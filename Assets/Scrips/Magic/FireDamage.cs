using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    [SerializeField] private float burnDuration;//duracion de la quemadura
    [SerializeField] private float damagePerSecond;//dano por segundo
    [SerializeField] private float tickInterval;//cada cuanto se aplica la quemadura
    [SerializeField] private float requiredStayTime;//tiempo minimo que tiene que estar para aplicar burn

    private class BurnTracker
    {
        public Collider2D collider;// referencia al collider
        public float timeInside;// cuanto tiempo esta adentro
        public bool burnApplied;// si ya se aplico el burn

        public BurnTracker(Collider2D col)
        {
            collider = col;
            timeInside = 0f;
            burnApplied = false;
        }
    }

    private List<BurnTracker> tracked = new List<BurnTracker>();//lista de objetos dentro de el hechizo de fuego

    private void Update()
    {
        for (int i = tracked.Count - 1; i >= 0; i--)//recorre cada objeto dentro de el hechizo
        {
            BurnTracker tracker = tracked[i];

            MovePlatform move = tracker.collider.GetComponent<MovePlatform>();         
            if (move != null)
            {
                move.ApplySlow(0.5f, 0.2f);
            }

            if (tracker.burnApplied) continue;//si ya  se aplico el burn

            tracker.timeInside += Time.deltaTime;//suma el tiempo adentro

            if (tracker.timeInside >= requiredStayTime)//si se cumple el tiempo, aplica el burn
            {
                BurnDamage burn = tracker.collider.GetComponent<BurnDamage>();
                if (burn != null)
                {
                    burn.ApplyBurn(burnDuration, damagePerSecond, tickInterval);//aplica el burn
                }

                tracker.burnApplied = true;//marca que fue aplicado
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//cuando un objeto entra a el hechizo
    {

        for (int i = tracked.Count - 1; i >= 0; i--)//asegura que no haya duplicados
        {
            if (tracked[i].collider == collision)
            {
                tracked.RemoveAt(i);
                break;

            }
        }


        tracked.Add(new BurnTracker(collision));//agrega nuevo objeto a rastrear
    }

    private void OnTriggerExit2D(Collider2D collision)//cuando un objeto sale del hechizo
    {
        for (int i = tracked.Count - 1; i >= 0; i--)
        {
            if (tracked[i].collider == collision)
            {
                MovePlatform move = collision.GetComponent<MovePlatform>();
                if (move != null)
                {
                    move.ApplySlow(1f, 0f); // restaurar velocidad al salir del fuego
                }
                tracked.RemoveAt(i);
                break;
            }
        }
    }
}
