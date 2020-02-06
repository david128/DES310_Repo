using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{

    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        Disable();

    }

    public void Disable()
    {
        
        //button.interactable = false;
        //button.image.enabled = false;
    }

    public void Enable()
    {
        
        button.interactable = true;
        button.image.enabled = true;
    }
}
