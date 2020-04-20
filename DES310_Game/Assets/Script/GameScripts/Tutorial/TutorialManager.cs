using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    //Public variables
    public GameObject gameManager;
    public GameObject cam;
    public GameObject TutorialBox;
    public RuntimeAnimatorController animContr;
    public Button[] tutMsgs;
    public TextMeshProUGUI countdownText;
    public static TutorialManager instance;

    //Tutorial buttons
    Button currentTut;
    Button prevTut;

    //Tutorial varibales
    bool inTutorial;
    bool updateTutorial = false;
    bool showTutorialBox;

    //End of tutorail varibales
    bool endOfTut = false;
    float endOfTutCountdown;
    float endCountdownTime;
    bool showCountdown;

    public bool GetTutorial() { return inTutorial; }
    public GameObject GetTutorialBox() { return TutorialBox; }
    public RuntimeAnimatorController GetAnimContr() { return animContr; }

    //Setters
    public void SetCurrentTut(Button c) { currentTut = c; }
    public void SetPreviousTut(Button p) { prevTut = p; }
    public void UpdateTutorialBox(bool b) { updateTutorial = b; }
    public void SetTutorialBox(bool t) { showTutorialBox = t; }
    public void SetEndOfTut(bool e) { endOfTut = e; }

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Animator camAnim;

        InputScript.instance.SetCanMove(false);
        InputScript.instance.SetAllowSelecting(false);

        inTutorial = true;

        if (cam.TryGetComponent(out camAnim))
        {
            currentTut = tutMsgs[0];
            prevTut = currentTut;
            //Waits until animation is done to perform next task
            camAnim.enabled = true;
            camAnim.Play("TutorialStartPan");
            StartCoroutine(UpdateClipLength(camAnim, true));
        }

        endCountdownTime = 15;
    }

    // Update is called once per frame
    void Update()
    {
        //Updates button to be displayed
        if(updateTutorial)
        {
            ChangeTutMsg();
            updateTutorial = false;
        }

        //Displays tutorial box
        if(showTutorialBox == true)
        {
            InputScript.instance.SetAllowSelecting(false);
            TutorialBox.SetActive(true);
            showTutorialBox = false;
        }

        //End of tutorial
        if(endOfTut == true)
        {
            if(showCountdown == false)
            {
                showCountdown = true;
                countdownText.gameObject.SetActive(true);
            }
         
            endOfTutCountdown += Time.deltaTime;
            endCountdownTime -= 1 * endOfTutCountdown;

            countdownText.text = "Time until game starts:" + endCountdownTime;

            if (endCountdownTime <= 0)
            {
                EndTutorial();

                endOfTut = false;
            }
        }
    }

    public void ChangeTutMsg()
    {
        ///Destroy(prevTut);

        //Changes what is being shown in the tutorial box
        prevTut.gameObject.SetActive(false);

        currentTut.gameObject.SetActive(true);
        currentTut.interactable = false;
        StartCoroutine(WaitForButton());
    }

    public IEnumerator WaitForAnimationToShowTutorialBox(Animator anim, bool showTutBox)
    {
        float counter = 0;
        float waitTime = anim.GetCurrentAnimatorStateInfo(0).length;

        //Now, Wait until the current state is done playing
        while (counter < (waitTime))
        {
            counter += Time.deltaTime;
            yield return null;
        }

        if(showTutBox == true)
        {
            TutorialBox.SetActive(true);
        }
        else
        {
            TutorialBox.SetActive(false);
        }

        updateTutorial = true;

        anim.enabled = false;
    }

    public IEnumerator WaitForAnimation(Animator anim)
    {
        float counter = 0;
        float waitTime = anim.GetCurrentAnimatorStateInfo(0).length;

        //Now, Wait until the current state is done playing
        while (counter < (waitTime))
        {
            counter += Time.deltaTime;
            yield return null;
        }

        updateTutorial = true;
    }

    public IEnumerator WaitForButton()
    {
        float counter = 0;
        float waitTime = 1.0f;

        //Now, Wait until the current state is done playing
        while (counter < (waitTime))
        {
            counter += Time.deltaTime;
            yield return null;
        }

        currentTut.interactable = true;
    }

    public IEnumerator UpdateClipLength(Animator anim, bool waitFortutBox)
    {
        bool newFrame = false;

        while (!newFrame)
        {
            newFrame = true;
            yield return new WaitForEndOfFrame();
        }

        print("current clip length = " + anim.GetCurrentAnimatorStateInfo(0).length);

        if (waitFortutBox == true)
        {
            StartCoroutine(WaitForAnimationToShowTutorialBox(anim, true));
        }
        else
        {
            StartCoroutine(WaitForAnimation(anim));
        }
    }

    public void EndTutorial()
    {
        SceneLoader.instance.LoadScene(3);
        inTutorial = false;
    }
}
