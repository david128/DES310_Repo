using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_MenuMusicEnd : MonoBehaviour
{
    public void onClick()

    {

        AkSoundEngine.PostEvent("MenuSoundEnd", gameObject);

    }
}
