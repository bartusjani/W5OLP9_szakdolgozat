using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFading : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 0.5f;

    private float targetAlpha = 0f;
    private bool isFading = false;
    private Coroutine currentFadeRoutine;

    public void FadeToBlack()
    {
        if (currentFadeRoutine != null)
            StopCoroutine(currentFadeRoutine);

        currentFadeRoutine = StartCoroutine(FadeRoutine(1f));
    }

    public void FadeToClear()
    {
        if (currentFadeRoutine != null)
            StopCoroutine(currentFadeRoutine);

        currentFadeRoutine = StartCoroutine(FadeRoutine(0f));
    }

    private IEnumerator FadeRoutine(float target)
    {
        isFading = true;
        targetAlpha = target;

        while (!Mathf.Approximately(fadeImage.color.a, targetAlpha))
        {
            float newAlpha = Mathf.MoveTowards(
                fadeImage.color.a,
                targetAlpha,
                fadeSpeed * Time.deltaTime
            );

            fadeImage.color = new Color(
                fadeImage.color.r,
                fadeImage.color.g,
                fadeImage.color.b,
                newAlpha
            );

            yield return null;
        }

        isFading = false;
    }

    public bool IsFadingComplete()
    {
        return !isFading;
    }

}
