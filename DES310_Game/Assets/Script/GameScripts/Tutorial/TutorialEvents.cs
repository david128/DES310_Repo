using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvents : MonoBehaviour
{
    public static TutorialEvents instance;
    public GameObject cam;
    public GameObject light;

    bool eventActive;

    //chicken event variables
    bool chickenBuilt;
    bool chickenMenuOpen;

    //carrot event variables
    bool carrotBuilt;
    bool carrotMenuOpen;

    void Awake()
    {
        instance = this;    
    }

    int countCheck;
    
    //setters
    public void SetChickenMenuOpen(bool o) { chickenMenuOpen = o; }
    public void SetChickenBuilt(bool c) { chickenBuilt = c; }
    public void SetCarrotMenuOpen(bool o) { carrotMenuOpen = o; }
    public void SetCarrotBuilt(bool c) { carrotBuilt = c; }

    //getters
    public bool GetChickenBuilt() { return chickenBuilt; }
    public bool GetCarrotBuilt() { return carrotBuilt; }
    public bool GetEventActive() { return eventActive; }

    Animator camAnim;
    TutEvents currentEvent;

    public enum TutEvents
    {
        ChickenField = 0, //camera pan, marketplace unlocker
        CarrotField = 1, //marketplace unlocker, 
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
                currentEvent = TutEvents.ChickenField;
                BuildChicken();
                break;

            case 1:
                currentEvent = TutEvents.CarrotField;
                BuildCarrot();
                break;

            case 2:
                currentEvent = TutEvents.RadialFlash;
                RadialFlash();
                break;

            case 3:
                currentEvent = TutEvents.Farmhouse;
                Farmhouse();
                break;

            case 4:
                currentEvent = TutEvents.FoodBarFlash;
                FoodBarFlash();
                break;

            case 5:
                currentEvent = TutEvents.RevertCamera;
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

        light.transform.position = new Vector3(54.15f, 19.6f, 39.04f);
        light.SetActive(true);

        StartCoroutine(WaitForEventToFinish(ChickenEvent()));
    }

    void BuildCarrot()
    {
        if (cam.TryGetComponent(out camAnim))
        {
            camAnim.enabled = true;
            camAnim.Play("PanForCarrot");
            StartCoroutine(TutorialManager.instance.UpdateClipLength(camAnim));
        }

        light.transform.position = new Vector3(53.71f, 19.6f, 25.36f);
        light.SetActive(true);

        StartCoroutine(WaitForEventToFinish(CarrotEvent()));
    }

    void RadialFlash()
    {

    }

    void Farmhouse()
    {

    }

    void FoodBarFlash()
    {

    }

    void RevertCamera()
    {
        TutorialManager.instance.SetEndOfTut(true);
        InputScript.instance.SetCanMove(true);
    }

    public IEnumerator WaitForEventToFinish(IEnumerator eventName)
    {
        //Wait until the event is done
        while (eventActive)
        {
            if(currentEvent == TutEvents.ChickenField)
            { 
                
            }
            else if(currentEvent == TutEvents.CarrotField)
            {
               
            }

            yield return eventName;
        }

        TutorialManager.instance.SetTutorialBox(true);
    }

    public IEnumerator ChickenEvent()
    {
        bool done = false;
        //Wait until the event is done
        while (!done)
        {
            if (chickenMenuOpen == true && countCheck < 1)
            {
                TutorialManager.instance.SetTutorialBox(true);
                chickenMenuOpen = false;
                countCheck++;
            }

            if (chickenBuilt == true)
            {
                done = true;
            }

            yield return null;
        }

        eventActive = false;

        light.SetActive(false);
    }

    public IEnumerator CarrotEvent()
    {
        bool done = false;
        //Wait until the event is done
        while (!done)
        {
            if (carrotBuilt == true)
            {
                done = true;
            }

            yield return null;
        }

        eventActive = false;

        light.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
