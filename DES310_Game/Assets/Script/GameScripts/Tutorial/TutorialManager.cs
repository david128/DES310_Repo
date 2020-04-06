using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject cam;
    public GameObject TutorialBox;

    public Button[] tutMsgs;

    Button currentTut;
    Button prevTut;
    Button nextTut;

    public static TutorialManager instance;

    bool inTutorial;
    bool updateTutorial = false;
    bool showTutorialBox;

    public bool GetTutorial() { return inTutorial; }
    public void SetCurrentTut(Button c) { currentTut = c; }
    public void SetPreviousTut(Button p) { prevTut = p; }
    public void UpdateTutorialBox(bool b) { updateTutorial = b; }
    public void SetTutorialBox(bool t) { showTutorialBox = t; }

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Animator camAnim;

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
            StartCoroutine(UpdateClipLength(camAnim));
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
            TutorialBox.SetActive(true);
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

    public IEnumerator UpdateClipLength(Animator anim)
    {
        bool newFrame = false;

        while (!newFrame)
        {
            newFrame = true;
            yield return new WaitForEndOfFrame();
        }

        print("current clip length = " + anim.GetCurrentAnimatorStateInfo(0).length);

        StartCoroutine(WaitForAnimationToShowTutorialBox(anim, true));
    }
}
