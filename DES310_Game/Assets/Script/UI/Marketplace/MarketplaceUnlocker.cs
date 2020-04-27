using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketplaceUnlocker : MonoBehaviour
{
    //public variables to be set in inspector
    public GameObject GrainTab;
    public GameObject VeggieTab;
    public GameObject AnimalTab;
    public GameObject SpecialsTab;
    public GameObject BuildingTab;

    public Button ChickenConfirm;
    public Button CarrotConfirm;

    public Image padLock;

    //This field should be set by the inspector to store requirements
    public int[] farmhouseRequirement;
    public int[] barnRequirement;
    public int[] researchRequirement;

    //instance of the calss to get level of buildings
    GameLevelStorer gls = new GameLevelStorer();
    
    //stores levels of each default building
    int[] levels = new int[3];

    //stores the buttons in the marketplace
    public Button[] marketButtons;

    //start is called before the first frame update
    void Start()
    {
        //Gets levels to be used for requirements
        gls.GetLevels();

        //sets local variable to levels
        levels = gls.GetLvls();

        //sets each current level of the default buildings
        int currentFarmLevel = levels[0];
        int currentBarnLevel = levels[1];
        int currentResearchLevel = levels[2];

        //creates boolean to have a requirement to prove its true
        bool unlock;

        //loops through how many buttosn there is to check if they should be interactable or not
        for (int i = 0; i < marketButtons.Length; i++)
        {
            //sets requirement for bollean to be true
            unlock = currentFarmLevel >= farmhouseRequirement[i] && currentBarnLevel >= barnRequirement[i] && currentResearchLevel >= researchRequirement[i];

            //if false then the button is set to non-interactable
            if (unlock == false)
            {
                marketButtons[i].interactable = false;

                //spawns the padlock button to the bottom right of the button
                Instantiate(padLock, new Vector3(marketButtons[i].transform.position.x + 38.1f, marketButtons[i].transform.position.y - 32.4f, marketButtons[i].transform.position.z), Quaternion.identity, marketButtons[i].transform);
            }
            else
            {
                //if true then the button will be interactable
                marketButtons[i].interactable = true;
            }

            //checks if the tutorial is the active scene
            if (TutorialManager.instance != null && TutorialManager.instance.GetTutorial() == true)
            {
                //sets temporary boolean to pass into listener
                bool tempTrue = true;

                //if the chicken event has not been completed yet
                if (TutorialEvents.instance.GetChickenBuilt() == false)
                {
                    //sets tabs in marketplace to show the correct one at the start
                    GrainTab.SetActive(false);
                    VeggieTab.SetActive(false);
                    AnimalTab.SetActive(true);
                    SpecialsTab.SetActive(false);
                    BuildingTab.SetActive(false);

                    //checks button name to add listener
                    if (marketButtons[i].name == "Chick")
                    {
                        //ovverides any previous settings
                        marketButtons[i].interactable = true;

                        //adds listeners to trigger events to happen in tutorial
                        marketButtons[i].onClick.AddListener(delegate { TutorialEvents.instance.SetChickenMenuOpen(tempTrue); });
                        ChickenConfirm.onClick.AddListener(delegate { TutorialEvents.instance.SetChickenBuilt(tempTrue); });
                        ChickenConfirm.onClick.AddListener(delegate { TutorialManager.instance.SetTutorialBox(tempTrue); });
                    }
                    else
                    {
                        //if button name is not chick then other buttons will be set non-interactable
                        marketButtons[i].interactable = false;

                        //spawns the padlock button to the bottom right of the button
                        Instantiate(padLock, new Vector3(marketButtons[i].transform.position.x + 38.1f, marketButtons[i].transform.position.y - 32.4f, marketButtons[i].transform.position.z), Quaternion.identity, marketButtons[i].transform);
                    }
                }

                //if the carrot event has not been completed yet and checks that the chicken event has been completed
                if (TutorialEvents.instance.GetCarrotBuilt() == false && TutorialEvents.instance.GetChickenBuilt() == true)
                {
                    //sets tabs in marketplace to show the correct one at the start
                    GrainTab.SetActive(false);
                    VeggieTab.SetActive(true);
                    AnimalTab.SetActive(false);
                    SpecialsTab.SetActive(false);
                    BuildingTab.SetActive(false);

                    //checks button name to add listener
                    if (marketButtons[i].name == "Carrot")
                    {
                        //ovverides any previous settings
                        marketButtons[i].interactable = true;

                        //adds listeners to trigger events to happen in tutorial
                        marketButtons[i].onClick.AddListener(delegate { TutorialEvents.instance.SetCarrotMenuOpen(tempTrue); });
                        CarrotConfirm.onClick.AddListener(delegate { TutorialEvents.instance.SetChickenBuilt(tempTrue); });
                        CarrotConfirm.onClick.AddListener(delegate { TutorialManager.instance.SetTutorialBox(tempTrue); });
                    }
                    else
                    {
                        //if button name is not chick then other buttons will be set non-interactable
                        marketButtons[i].interactable = false;

                        //spawns the padlock button to the bottom right of the button
                        Instantiate(padLock, new Vector3(marketButtons[i].transform.position.x + 38.1f, marketButtons[i].transform.position.y - 32.4f, marketButtons[i].transform.position.z), Quaternion.identity, marketButtons[i].transform);
                    }
                }
            }
        }
    }
}