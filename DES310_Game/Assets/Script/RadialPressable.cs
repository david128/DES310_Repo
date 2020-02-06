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

    public Action[] options;
    void OnMouseDown()
    {
        RadialMenuSpawner.instance.SpawnMenu(this);
    }
}
