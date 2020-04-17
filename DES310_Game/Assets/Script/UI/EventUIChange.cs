using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventUIChange : MonoBehaviour
{
    //store text
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descText;

    //change text
    public void ChangeText(string name, string desc)
    {
        nameText.text = name;
        descText.text = desc;
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }    
    
    public void Enable()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(WaitTime());
    }

    public IEnumerator WaitTime()
    {
        float counter = 0;
        float waitTime = 5;

        //Now, Wait until the current state is done playing
        while (counter < (waitTime))
        {
            counter += Time.deltaTime;
            yield return null;
        }

        Disable();
    }
}
