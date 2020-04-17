using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributionPress : MonoBehaviour
{
    public void onClick()
    {
        AkSoundEngine.PostEvent("Distribution_open", gameObject);
    }
}