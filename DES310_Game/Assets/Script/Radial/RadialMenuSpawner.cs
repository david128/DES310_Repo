﻿using System.Collections;
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

        newMenu.label.text = obj.title.ToUpper();

        if (TutorialEvents.instance != null && TutorialEvents.instance.GetCurrentEvent() == TutorialEvents.TutEvents.RadialFlash)
        {
            TutorialManager.instance.SetTutorialBox(true);
        }

        newMenu.SpawnButtons(obj);

        newMenu.transform.SetSiblingIndex(0);

        SetAwake(true);
    }
}
  