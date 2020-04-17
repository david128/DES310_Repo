﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialEvents : MonoBehaviour
{
    public static TutorialEvents instance;
    public GameObject cam;
    public GameObject light;
    public Image FoodPanel;
    public Image FoodIcon;
    public Image MoneyPanel;
    public Image MoneyIcon;
    public GameObject DistributionPanel;
    public TextMeshProUGUI objectiveText;

    bool eventActive;

    //chicken event variables
    bool chickenBuilt;
    bool chickenMenuOpen;

    //carrot event variables
    bool carrotBuilt;
    bool carrotMenuOpen;

    //Radial Flash variables
    bool destroyedCarrot;
    
    //UI variables
    bool stopFoodFlash;
    bool startMoneyFlash;
    bool stopMoneyFlash;

    //Farmhouse
    bool farmhouse;

    //Distribution
    bool distributionExit;

    //PanCamera
    bool panCam = true;

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
    public void SetStopFoodUIFlash(bool s) { stopFoodFlash = s; }
    public void SetStopMoneyUIFlash(bool s) { stopMoneyFlash = s; }
    public void SetStartMoneyUIFlash(bool s) { startMoneyFlash = s; }
    public void SetDestroyedCarrot(bool d) { destroyedCarrot = d; }
    public void SetFarmhouse(bool f) { farmhouse = f; }
    public void SetDistributionExit(bool d) { distributionExit = d; }
    public void SetPanCamera(bool p) { panCam = p; }

    //getters
    public TutEvents GetCurrentEvent() { return currentEvent; }
    public bool GetChickenBuilt() { return chickenBuilt; }
    public bool GetCarrotBuilt() { return carrotBuilt; }
    public bool GetEventActive() { return eventActive; }

    Animator camAnim;
    TutEvents currentEvent;

    public enum TutEvents
    {
        ChickenField = 0, //camera pan, marketplace unlocker, light
        CarrotField = 1, //camera pan, marketplace unlocker, light, 
        RadialFlash = 2, //box/light around the carrot field that can only be selected, radial menu interactivity
        Farmhouse = 3, //camera pan, lightaround farmhouse, radial menu
        UIFlash = 4, //Food UI flash, 
        Distribution = 5, //Distribution Flash/Open menu automatically,
        PanCamera = 6, //pan camera to farmhouse position
        RevertCamera = 7 //pan camera to farmhouse position
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
                currentEvent = TutEvents.UIFlash;
                FoodUIFlash();
                break;

            case 5:
                currentEvent = TutEvents.Distribution;
                DistributionIn();
                break;

            case 6:
                currentEvent = TutEvents.PanCamera;
                PanCamera();
                break;

            case 7:
                currentEvent = TutEvents.RevertCamera;
                RevertCamera();
                break;
        }
    }

    void BuildChicken()
    {
        objectiveText.text = "Build a chicken field";

        if (cam.TryGetComponent(out camAnim))
        {
            camAnim.enabled = true;
            camAnim.Play("PanForChicken");
            StartCoroutine(TutorialManager.instance.UpdateClipLength(camAnim, true));
        }

        light.transform.position = new Vector3(54.15f, 19.6f, 39.04f);
        light.SetActive(true);

        StartCoroutine(WaitForEventToFinish(ChickenEvent()));
    }

    void BuildCarrot()
    {
        objectiveText.text = "Plant a carrot field";

        if (cam.TryGetComponent(out camAnim))
        {
            camAnim.enabled = true;
            camAnim.Play("PanForCarrot");
            StartCoroutine(TutorialManager.instance.UpdateClipLength(camAnim, true));
        }

        light.transform.position = new Vector3(53.71f, 19.6f, 25.36f);
        light.SetActive(true);

        StartCoroutine(WaitForEventToFinish(CarrotEvent()));
    }

    void RadialFlash()
    {
        objectiveText.text = "Destroy carrot field";

        light.SetActive(true);

        StartCoroutine(WaitForEventToFinish(RadialFlashEvent()));
    }

    void Farmhouse()
    {
        if (cam.TryGetComponent(out camAnim))
        {
            camAnim.enabled = true;
            camAnim.Play("PanForFarmhouse");
            StartCoroutine(TutorialManager.instance.UpdateClipLength(camAnim, true));
        }

        light.transform.position = new Vector3(71.51f, 19.6f, 38.43f);
        light.SetActive(true);

        StartCoroutine(WaitForEventToFinish(FarmhouseEvent()));
    }

    void FoodUIFlash()
    {
        objectiveText.text = "Look at food";

        if (FoodPanel.TryGetComponent(out Animator UIPanelAnim) && FoodIcon.TryGetComponent(out Animator UIIconAnim))
        {
            UIPanelAnim.Play("UIFlashAnim");
            StartCoroutine(TutorialManager.instance.UpdateClipLength(UIPanelAnim, false));

            UIIconAnim.Play("UIFlashAnim");
            StartCoroutine(TutorialManager.instance.UpdateClipLength(UIIconAnim, false));
        }

        StartCoroutine(WaitForEventToFinish(UIFlashEvent()));
    }

    void MoneyUIFlash()
    {
        objectiveText.text = "Look at money";

        if (MoneyPanel.TryGetComponent(out Animator UIPanelAnim) && MoneyIcon.TryGetComponent(out Animator UIIconAnim))
        {
            UIPanelAnim.Play("UIFlashAnim");
            StartCoroutine(TutorialManager.instance.UpdateClipLength(UIPanelAnim, false));

            UIIconAnim.Play("UIFlashAnim");
            StartCoroutine(TutorialManager.instance.UpdateClipLength(UIIconAnim, false));
        }
    }

    void DistributionIn()
    {
        objectiveText.text = "Select a distributer";

        if (DistributionPanel.TryGetComponent(out Animator UIPanelAnim))
        {
            UIPanelAnim.Play("DistributionAnim");
            StartCoroutine(TutorialManager.instance.UpdateClipLength(UIPanelAnim, false));
        }

        StartCoroutine(WaitForEventToFinish(DistributionEvent()));
    }

    void DistributionOut()
    {
        if (DistributionPanel.TryGetComponent(out Animator UIPanelAnim))
        {
            UIPanelAnim.Play("RevDistributionAnim");
            StartCoroutine(TutorialManager.instance.UpdateClipLength(UIPanelAnim, false));
        }
    }

    void PanCamera()
    {
        if (cam.TryGetComponent(out camAnim))
        {
            camAnim.enabled = true;
            camAnim.Play("PanCamAround");
            StartCoroutine(TutorialManager.instance.UpdateClipLength(camAnim, false));
        }

        StartCoroutine(WaitForEventToFinish(PanCameraEvent()));
    }

    void RevertCamera()
    {
        objectiveText.text = "Prepare for new beginnings";

        if (cam.TryGetComponent(out camAnim))
        {
            camAnim.enabled = false;
            camAnim.Play("Do Nothing");
        }

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

        Object fieldBlocker;

        fieldBlocker = Instantiate(Resources.Load("TutorialResources/Blocker"), new Vector3(46.76f, 10.0f, 19.05f), Quaternion.identity);

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

        Destroy(fieldBlocker);

        Instantiate(Resources.Load("TutorialResources/Blocker"), new Vector3(46.76f, 10.0f, 31.95f), Quaternion.identity);

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

    public IEnumerator RadialFlashEvent()
    {
        bool done = false;

        //Wait until the event is done
        while (!done)
        {
            if (InputScript.instance.GetAllowSelecting() == false)
            {
                //causing a bug
                //InputScript.instance.AllowSelecting();
            } 

            if (destroyedCarrot == true)
            {
                done = true;
            }

            yield return null;
        }

        eventActive = false;

        light.SetActive(false);
    }

    public IEnumerator FarmhouseEvent()
    {
        bool done = false;

        //Wait until the event is done
        while (!done)
        {
            if (farmhouse == true)
            {
                done = true;
            }

            yield return null;
        }

        eventActive = false;

        light.SetActive(false);
    }

    public IEnumerator UIFlashEvent()
    {
        bool done = false;

        //Wait until the event is done
        while (!done)
        {
            if (stopFoodFlash == true)
            {
                if (FoodPanel.TryGetComponent(out Animator UIPanelAnim) && FoodIcon.TryGetComponent(out Animator UIIconAnim))
                {
                    UIPanelAnim.Play("Do Nothing");
                    UIIconAnim.Play("Do Nothing");
                    stopFoodFlash = false;
                }
            }

            if (startMoneyFlash == true)
            {
                MoneyUIFlash();
                startMoneyFlash = false;
            }

            if (stopMoneyFlash == true)
            {
                if (MoneyPanel.TryGetComponent(out Animator UIAnim) && MoneyIcon.TryGetComponent(out Animator UIIconAnim))
                {
                    UIAnim.Play("Do Nothing");
                    UIIconAnim.Play("Do Nothing");
                }

                done = true;
            }

            yield return null;
        }

        eventActive = false;
    }

    public IEnumerator DistributionEvent()
    {
        bool done = false;

        //Wait until the event is done
        while (!done)
        {
            if (distributionExit == true)
            {
                DistributionOut();
                done = true;
            }

            yield return null;
        }
    }

    public IEnumerator PanCameraEvent()
    {
        bool done = false;

        //Wait until the event is done
        while (!done)
        {
            if (panCam == false)
            {
                if (cam.TryGetComponent(out camAnim))
                {
                    camAnim.enabled = true;
                    camAnim.Play("Do Nothing");
                }

                done = true;
            }

            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
