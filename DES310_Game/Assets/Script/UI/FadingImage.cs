using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FadingImage : MonoBehaviour
{
    public float FadeTime = 0.01f;

    public IEnumerator FadeImageToFullAlpha(float t)
    {
        while (this.GetComponent<Image>().color.a < 1.0f)
        {
            this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r, this.GetComponent<Image>().color.g, this.GetComponent<Image>().color.b, this.GetComponent<Image>().color.a + (t));
            yield return null;
        }
    }

    public IEnumerator FadeImageToZeroAlpha(float t)
    {

        while (this.GetComponent<Image>().color.a > 0.0f)
        {
            this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r, this.GetComponent<Image>().color.g, this.GetComponent<Image>().color.b, this.GetComponent<Image>().color.a - (t));
            yield return null;
        }
    }

    void Start()
    {
        this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r, this.GetComponent<Image>().color.g, this.GetComponent<Image>().color.b, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FadeImageToFullAlpha(Time.deltaTime * FadeTime));
    }
}
