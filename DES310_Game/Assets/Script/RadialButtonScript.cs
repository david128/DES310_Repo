﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadialButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 

    public Image circle;
    public Image symbol;
    public string title;
    public RadialMenu myMenu;
    public float speed = 8f;

    Color defaultColor;

    public void Anim()
    {
        StartCoroutine(AnimateButtonIn());
    }
    
        
    

    IEnumerator AnimateButtonIn()
    {
        transform.localScale = Vector3.zero;
        float timer = 0f;
        while (timer < (1 / speed)) ;
        {
           transform.localScale = Vector3.one * timer * speed;
           timer += Time.deltaTime;
           yield return null;
        }
        transform.localScale = Vector3.one;
    }

   

    public void OnPointerEnter(PointerEventData eventData)
    {
        myMenu.selected = this;
        defaultColor = circle.color;
        circle.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myMenu.selected = null;
        circle.color = defaultColor;
    }
}


