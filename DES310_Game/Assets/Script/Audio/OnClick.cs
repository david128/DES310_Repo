﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{
    public void onClick()
    {
        AkSoundEngine.PostEvent("ButtonPress", gameObject);

    }
}