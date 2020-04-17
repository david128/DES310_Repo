using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Market : MonoBehaviour
{
    public void onClick()
    {
        AkSoundEngine.PostEvent("OpenMarket", gameObject);
    }
}
