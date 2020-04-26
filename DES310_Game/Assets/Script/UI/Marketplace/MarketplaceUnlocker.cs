using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketplaceUnlocker : MonoBehaviour
{
    public GameObject GrainTab;
    public GameObject VeggieTab;
    public GameObject AnimalTab;
    public GameObject SpecialsTab;
    public GameObject BuildingTab;

    public Button ChickenConfirm;
    public Button CarrotConfirm;

    public Image padLock;

    //This field should be set by the inspector
    public int[] farmhouseRequirement;
    public int[] barnRequirement;
    public int[] researchRequirement;

    GameLevelStorer gls = new GameLevelStorer();

    int[] levels = new int[3];

    public Button[] marketButtons;

    void Start()
    {
        gls.GetLevels();

        levels = gls.GetLvls();

        int currentFarmLevel = levels[0];
        int currentBarnLevel = levels[1];
        int currentResearchLevel = levels[2];

        bool unlock;

        for (int i = 0; i < marketButtons.Length; i++)
        {
            Image locked;

            locked = padLock;

            unlock = currentFarmLevel >= farmhouseRequirement[i] && currentBarnLevel >= barnRequirement[i] && currentResearchLevel >= researchRequirement[i];

            if (unlock == false)
            {
                marketButtons[i].interactable = false;

                Instantiate(padLock, new Vector3(marketButtons[i].transform.position.x + 38.1f, marketButtons[i].transform.position.y - 32.4f, marketButtons[i].transform.position.z), Quaternion.identity, marketButtons[i].transform);
                //Instantiate(Resources.Load("Padlock"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, marketButtons[i].transform);
            }
            else
            {
                marketButtons[i].interactable = true;
            }

            if (TutorialManager.instance != null && TutorialManager.instance.GetTutorial() == true)
            {
                if (TutorialEvents.instance.GetChickenBuilt() == false)
                {
                    GrainTab.SetActive(false);
                    VeggieTab.SetActive(false);
                    AnimalTab.SetActive(true);
                    SpecialsTab.SetActive(false);
                    BuildingTab.SetActive(false);

                    bool tempTrue = true;

                    if (marketButtons[i].name == "Chick")
                    {
                        marketButtons[i].interactable = true;

                        marketButtons[i].onClick.AddListener(delegate { TutorialEvents.instance.SetChickenMenuOpen(tempTrue); });
                        ChickenConfirm.onClick.AddListener(delegate { TutorialEvents.instance.SetChickenBuilt(tempTrue); });
                        ChickenConfirm.onClick.AddListener(delegate { TutorialManager.instance.SetTutorialBox(tempTrue); });
                    }
                    else
                    {
                        marketButtons[i].interactable = false;

                        Instantiate(padLock, new Vector3(marketButtons[i].transform.position.x + 38.1f, marketButtons[i].transform.position.y - 32.4f, marketButtons[i].transform.position.z), Quaternion.identity, marketButtons[i].transform);
                    }
                }

                if (TutorialEvents.instance.GetCarrotBuilt() == false && TutorialEvents.instance.GetChickenBuilt() == true)
                {
                    GrainTab.SetActive(false);
                    VeggieTab.SetActive(true);
                    AnimalTab.SetActive(false);
                    SpecialsTab.SetActive(false);
                    BuildingTab.SetActive(false);

                    bool tempTrue = true;

                    if (marketButtons[i].name == "Carrot")
                    {
                        marketButtons[i].interactable = true;

                        marketButtons[i].onClick.AddListener(delegate { TutorialEvents.instance.SetCarrotMenuOpen(tempTrue); });
                        CarrotConfirm.onClick.AddListener(delegate { TutorialEvents.instance.SetChickenBuilt(tempTrue); });
                        CarrotConfirm.onClick.AddListener(delegate { TutorialManager.instance.SetTutorialBox(tempTrue); });
                    }
                    else
                    {
                        marketButtons[i].interactable = false;

                        Instantiate(padLock, new Vector3(marketButtons[i].transform.position.x + 38.1f, marketButtons[i].transform.position.y - 32.4f, marketButtons[i].transform.position.z), Quaternion.identity, marketButtons[i].transform);
                    }
                }
            }
        }

       
    }
}