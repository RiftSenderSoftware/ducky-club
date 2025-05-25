using UnityEngine;
using System.Collections;

public class FadeEffect : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    public IEnumerator FadeIn(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    public IEnumerator FadeOut(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0;
    }
}