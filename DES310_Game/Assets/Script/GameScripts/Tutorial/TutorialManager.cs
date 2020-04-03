using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject gameManager;

    public static TutorialManager instance;

    bool inTutorial;

    public bool GetTutorial() { return inTutorial; }

    void Awake()
    {
        instance = this;
    }

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
