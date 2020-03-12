using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FadingText : MonoBehaviour
{
    public float FadeTime = 0.01f;

    public IEnumerator FadeTextToFullAlpha(float t)
    {
        while (this.GetComponent<Text>().color.a < 1.0f)
        {
            this.GetComponent<Text>().color = new Color(this.GetComponent<Text>().color.r, this.GetComponent<Text>().color.g, this.GetComponent<Text>().color.b, this.GetComponent<Text>().color.a + (t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t)
    {
      
        while (this.GetComponent<Text>().color.a > 0.0f)
        {
            this.GetComponent<Text>().color = new Color(this.GetComponent<Text>().color.r, this.GetComponent<Text>().color.g, this.GetComponent<Text>().color.b, this.GetComponent<Text>().color.a - (t));
            yield return null;
        }
    }

    void Start()
    {
        this.GetComponent<Text>().color = new Color(this.GetComponent<Text>().color.r, this.GetComponent<Text>().color.g, this.GetComponent<Text>().color.b, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FadeTextToFullAlpha(Time.deltaTime * FadeTime));
    }
}
