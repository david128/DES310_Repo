using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_MenuMusic : MonoBehaviour
{

    public void onClick()

    {

        AkSoundEngine.PostEvent("MenuSound", gameObject);

    }
}
