using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public IEnumerator FlashCoroutine(float flashDuration, Color flashColor, int numOfFlashes)
    {
        Color startColor = spriteRenderer.color;
        float elapseFlashTime = 0;
        float elapseFlashPercentage = 0;    

        while (elapseFlashTime < flashDuration)
        {
            elapseFlashTime += Time.deltaTime;
            elapseFlashPercentage = elapseFlashTime / flashDuration;

            if (elapseFlashPercentage > 1 )
            {
                elapseFlashPercentage = 1;
            }

            float pingPongPercentage = Mathf.PingPong(elapseFlashPercentage * 2 * numOfFlashes, 1);
            spriteRenderer.color = Color.Lerp(startColor, flashColor, pingPongPercentage);

            yield return null;
        }
    }
}
