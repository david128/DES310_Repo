using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_ChickenSound : MonoBehaviour
{
    public void onClick()

    {

        AkSoundEngine.PostEvent("Chicken", gameObject);

    }
}
