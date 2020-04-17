using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dis_Close : MonoBehaviour
{
    public void onClick()
    {
        AkSoundEngine.PostEvent("Distribution_Close", gameObject);
    }
}
