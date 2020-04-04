using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject cam;
    public GameObject TutorialBox;

    public List<Button> tutMsgs;

    public static TutorialManager instance;

    bool inTutorial;
    bool updateTutorial;

    public bool GetTutorial() { return inTutorial; }

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
           StartCoroutine(WaitForAnimationToShowTutorialBox(camAnim, "TutorialStartPan", true));
           updateTutorial = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(updateTutorial)
        {
            ChangeTutMsg(tutMsgs);
        }
    }

    void ChangeTutMsg(List<Button> tutMsg)
    {

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
    }
}
