﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnime : MonoBehaviour
{
   public void OnClose()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 3.0f);
    }
}
