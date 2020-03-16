using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketplaceUnlocker : MonoBehaviour
{
    //This field should be set by the inspector
    [SerializeField] int[] farmhouseRequirement;
    [SerializeField] int[] barnRequirement;
    [SerializeField] int[] researchRequirement;

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
            unlock = currentFarmLevel >= farmhouseRequirement[i] && currentBarnLevel >= barnRequirement[i] && currentResearchLevel >= researchRequirement[i];

            if (unlock == false)
            {
                marketButtons[i].interactable = false;
            }
            else
            {
                marketButtons[i].interactable = true;
            }
        }
    }

}