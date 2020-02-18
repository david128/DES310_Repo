using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_ButtonSound : MonoBehaviour
{
    // Start is called before the first frame update

    public void  onClick()

    {

        AkSoundEngine.PostEvent("ButtonSound", gameObject);

    }
}
