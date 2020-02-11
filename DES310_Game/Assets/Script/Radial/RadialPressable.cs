using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (RadialMenuSpawner.instance.GetAwake() == false)
        {
            RadialMenuSpawner.instance.SpawnMenu(this);
        }
    }
}
