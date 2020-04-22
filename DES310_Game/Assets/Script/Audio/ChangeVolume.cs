using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    public Slider thisSlider;
    public float masterVolume;
    public float musicVolume;
    public float SFXVolume;
 
    public void SetSpecificVolume(string valueType)
    {
        float sliderValue = thisSlider.value;

        if(valueType == "Master")
        {
            masterVolume = thisSlider.value;

            AkSoundEngine.SetRTPCValue("MasterVolume", masterVolume);
        }

        if (valueType == "Music")
        {
            musicVolume = thisSlider.value;

            AkSoundEngine.SetRTPCValue("MusicVolume", musicVolume);
        }

        if (valueType == "Sounds")
        {
            SFXVolume = thisSlider.value;

            AkSoundEngine.SetRTPCValue("SFXVolume", SFXVolume);
        }
    }
}
