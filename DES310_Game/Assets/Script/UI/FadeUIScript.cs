
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class FadeUIScript : MonoBehaviour
{
    public float fadeSpeed;
    public float fadeTime;

    // can ignore the update, it's just to make the coroutines get called for example
    void Start()
    {
        var canvGroup = GetComponent<CanvasGroup>();

        canvGroup.alpha = 0.0f;

        StartCoroutine(FadeToFullAlpha(canvGroup, canvGroup.alpha, 1.0f));
    }

    public IEnumerator FadeToFullAlpha(CanvasGroup canvGroup, float start, float end)
    {
        float counter = 0.0f;

        while(counter < fadeTime)
        {
            counter += Time.deltaTime * fadeSpeed;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / fadeTime);

            yield return null;
        }
    }
}