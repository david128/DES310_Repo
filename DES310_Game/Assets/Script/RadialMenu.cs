using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    public RadialButtonScript buttonPrefab;
    public RadialButtonScript selected;
    public void SpawnButtons(RadialPressable obj)
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
        }

    }


    void Update()
    {
     if (Input.GetMouseButtonUp(0))
        {
            Destroy(gameObject);
        }   
    }
}
