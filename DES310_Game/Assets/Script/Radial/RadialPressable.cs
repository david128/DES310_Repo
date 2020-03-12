﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RadialPressable : MonoBehaviour
{
    [System.Serializable]
    public class Action
    {
        public Color Color;
        public Sprite Symbol;
        public string Title;

    }
    public string title;
    public Action[] options;

    void Start()
    {
        if (title == "" || title == null)
        {
            title = gameObject.name;
        }
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        InputScript i = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputScript>();

        if (RadialMenuSpawner.instance.GetAwake() == false && i.GetAllowSelecting() == true)
        {
            RadialMenuSpawner.instance.SpawnMenu(this);
        }
    }
}
