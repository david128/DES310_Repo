using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvents : MonoBehaviour
{
    public static TutorialEvents instance;
    public GameObject cam;
    public GameObject light;

    bool eventActive;
    bool chickenBuilt;

    void Awake()
    {
        instance = this;    
    }

    public bool GetEventActive() { return eventActive; }

    public void SetChickenBuilt(bool c) { chickenBuilt = c; }

    Animator camAnim;

    public enum TutEvents
    {
        ChickenField = 0, //camera pan, marketplace unlocker
        Carrots = 1, //marketplace unlocker, 
        RadialFlash = 2, //box/light around the carrot field that can only be selected, radial menu interactivty
        Farmhouse = 3, //camera pan, lightaround farmhouse, radial menu
        FoodBarFlash = 4, //Food UI flash, 
        RevertCamera = 5 //pan camera to farmhouse position
    }

    public void RunEvent(int i)
    {
        eventActive = true;

        switch (i)
        {
            case 0:
                BuildChicken();
                break;

            case 1:
                BuildCarrot();
                break;

            case 2:
                RadialFlash();
                break;

            case 3:
                FoodBarFlash();
                break;

            case 4:
                RevertCamera();
                break;
        }
    }

    void BuildChicken()
    {
        if (cam.TryGetComponent(out camAnim))
        {
            camAnim.enabled = true;
            camAnim.Play("PanForChicken");
            StartCoroutine(TutorialManager.instance.UpdateClipLength(camAnim));
        }

        light.SetActive(true);

        StartCoroutine(WaitForEventToFinish(ChickenEvent()));
    }

    void BuildCarrot()
    {

    }

    void RadialFlash()
    {

    }

    void FoodBarFlash()
    {

    }

    void RevertCamera()
    {

    }

    public IEnumerator WaitForEventToFinish(IEnumerator eventName)
    {
        //Wait until the event is done
        while (eventActive)
        {
            yield return eventName;
        }
    }

    public IEnumerator ChickenEvent()
    {
        bool done = false;
        //Wait until the event is done
        while (!done)
        {
            if(chickenBuilt == true)
            {
                done = true;
            }

            yield return null;
        }

        eventActive = false;

        light.SetActive(false);

        TutorialManager.instance.SetTutorialBox(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
