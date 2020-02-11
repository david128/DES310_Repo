using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuSpawner : MonoBehaviour
{
    public static RadialMenuSpawner instance;
    public RadialMenu menuPrefab;
    public bool awake = false;

    private void Awake()
    {
        instance = this; // should be using a singleton pattern or something.
    }

    public bool GetAwake() { return awake; }

    public void SetAwake(bool a) { awake = a; }

    public void SpawnMenu(RadialPressable obj)
    {
        RadialMenu newMenu = Instantiate(menuPrefab) as RadialMenu;

        newMenu.transform.SetParent(transform, false);

        newMenu.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f);

        //GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        //if (gameManager.GetComponent<InputScript>().GetControlType() == false)
        //{
        //    newMenu.transform.position = Input.mousePosition;
        //}
        //else
        //{
        //    newMenu.transform.position = Input.GetTouch(0).position;
        //}
       
        newMenu.label.text = obj.title.ToUpper();
        newMenu.SpawnButtons(obj);

        SetAwake(true);
       
    }
}
  