using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    public Text label;
    public RadialButtonScript buttonPrefab;
    public RadialButtonScript selected;
    public void SpawnButtons(RadialPressable obj)
    {
        StartCoroutine(AnimateButtons(obj));
    }

    IEnumerator AnimateButtons (RadialPressable obj)
    {
        for (int i = 0; i < obj.options.Length; i++)
        {
            RadialButtonScript newButton = Instantiate(buttonPrefab) as RadialButtonScript;
            newButton.transform.SetParent(transform, false);
            float theta = (2 * Mathf.PI / obj.options.Length) * i;
            float xPos = Mathf.Sin(theta);
            float yPos = Mathf.Cos(theta);
            newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * 100f;

            newButton.circle.color = obj.options[i].Color;
            newButton.symbol.sprite = obj.options[i].Symbol;
            newButton.title = obj.options[i].Title;
            newButton.myMenu = this;
            newButton.Anim();
            yield return new WaitForSeconds(0.06f);
        }

    }


    void Update()
    {
     if (Input.GetMouseButtonUp(0))
        {
            if (selected)
            {
                //button function goes here
                Debug.Log(selected.title + "was selected");
            }
            Destroy(gameObject);
        }   
    }
}
