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

    public bool GetTutorial() { return inTutorial; }

    public void SetCurrentTut(Button c) { currentTut = c; }
    public void SetPreviousTut(Button p) { prevTut = p; }
    public void UpdateTutorialBox(bool b) { updateTutorial = b; }

    void Awake()
    {
        instance = this;
    }

    Animator camAnim;

    // Start is called before the first frame update
    void Start()
    {
        if(gameManager.GetComponent<MainMenu>().GetFromLoad() == false)
        {
            inTutorial = true;
        }
        else
        {
            inTutorial = false;
        }

        if (gameManager.GetComponent<GameLoop>().GetInTutorial() == true && cam.TryGetComponent(out camAnim))
        {
            //Waits until animation is done to perform next task
            currentTut = tutMsgs[0];
            prevTut = currentTut;
            StartCoroutine(WaitForAnimationToShowTutorialBox(camAnim, "TutorialStartPan", true));
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
    }

    public void ChangeTutMsg()
    {
        ///Destroy(prevTut);

        //Changes what is being shown in the tutorial box
        prevTut.gameObject.SetActive(false);
        currentTut.gameObject.SetActive(true);
    }

    IEnumerator WaitForAnimationToShowTutorialBox(Animator anim, string stateName, bool showTutBox)
    {
        anim.Play(stateName);

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
    }

    IEnumerator WaitForAnimationTo(Animator anim, string stateName)
    {
        anim.Play(stateName);

        float counter = 0;
        float waitTime = anim.GetCurrentAnimatorStateInfo(0).length;

        //Now, Wait until the current state is done playing
        while (counter < (waitTime))
        {
            counter += Time.deltaTime;
            yield return null;
        }


    }
}
