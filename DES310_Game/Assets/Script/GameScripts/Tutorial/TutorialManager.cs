using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject cam;
    public GameObject TutorialBox;

    public RuntimeAnimatorController animContr;

    public Button[] tutMsgs;

    Button currentTut;
    Button prevTut;

    public static TutorialManager instance;

    bool inTutorial;
    bool updateTutorial = false;
    bool showTutorialBox;
    bool endOfTut = false;

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

        if (gameManager.GetComponent<MainMenu>().GetFromLoad() == false)
        {
            inTutorial = true;
        }
        else
        {
            inTutorial = false;
        }

        if (gameManager.GetComponent<GameLoop>().GetInTutorial() == true && cam.TryGetComponent(out camAnim))
        {
            currentTut = tutMsgs[0];
            prevTut = currentTut;
            //Waits until animation is done to perform next task
            camAnim.enabled = true;
            camAnim.Play("TutorialStartPan");
            StartCoroutine(UpdateClipLength(camAnim, true));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(updateTutorial)
        {
            ChangeTutMsg();
            updateTutorial = false;
        }

        if(showTutorialBox == true)
        {
            InputScript.instance.SetAllowSelecting(false);
            TutorialBox.SetActive(true);
            showTutorialBox = false;
        }
    }

    public void ChangeTutMsg()
    {
        ///Destroy(prevTut);

        //Changes what is being shown in the tutorial box
        prevTut.gameObject.SetActive(false);
        currentTut.gameObject.SetActive(true);
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
}
