using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mac : MonoBehaviour
{
    public void onClick()
    {
        AkSoundEngine.PostEvent("Mac", gameObject);
    }
}
