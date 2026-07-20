using UnityEngine;
using System.Collections;

public class MenuFade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float duration = 0.75f;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Clamp01(1 - (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
        gameObject.SetActive(false);  // Desactiva el GameObject al finalizar el FadeOut
    }

    public IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        gameObject.SetActive(true);  // Activa el GameObject antes de comenzar el FadeIn

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1;
    }
}
