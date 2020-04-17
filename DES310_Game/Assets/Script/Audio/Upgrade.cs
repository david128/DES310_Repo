using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public void onClick() 
    {
        AkSoundEngine.PostEvent("Upgrade", gameObject);
    }
}
