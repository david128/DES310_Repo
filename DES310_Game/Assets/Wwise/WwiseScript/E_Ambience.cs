using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Ambience : MonoBehaviour
{
    public void onClick()

    {

        AkSoundEngine.PostEvent("Ambience", gameObject);

    }
}
