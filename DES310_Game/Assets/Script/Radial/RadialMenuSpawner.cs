using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuSpawner : MonoBehaviour
{
    public static RadialMenuSpawner instance;
    public RadialMenu menuPrefab;


    private void Awake()
    {
        instance = this; // should be using a singleton pattern or something.
    }

    public void SpawnMenu(RadialPressable obj)
    {
        RadialMenu newMenu = Instantiate(menuPrefab) as RadialMenu;
        newMenu.transform.SetParent(transform, false);
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        if (gameManager.GetComponent<InputScript>().GetControlType() == false)
        {
            newMenu.transform.position = Input.mousePosition;
        }
        else
        {
            newMenu.transform.position = Input.GetTouch(0).position;
        }
        
        
        newMenu.label.text = obj.title.ToUpper();
        newMenu.SpawnButtons(obj);
        
    }
}
  