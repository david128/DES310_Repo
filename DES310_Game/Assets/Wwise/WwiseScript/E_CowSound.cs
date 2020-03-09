using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_CowSound : MonoBehaviour
{
    public void onClick()

    {

        AkSoundEngine.PostEvent("Cow", gameObject);

    }
}
