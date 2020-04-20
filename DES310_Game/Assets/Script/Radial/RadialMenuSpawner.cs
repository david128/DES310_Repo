using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenuSpawner : MonoBehaviour
{
    public static RadialMenuSpawner instance;

    public GameObject[] statsMenus;

    public RadialMenu menuPrefab;
    public bool awake = false;

    //getters
    public bool GetAwake() { return awake; }
    public void DisableStatsMenus() { for (int i = 0; i < statsMenus.Length; i++) { statsMenus[i].SetActive(false); } }

    //setters
    public void SetAwake(bool a) { awake = a; }

    private void Awake()
    {
        instance = this; // should be using a singleton pattern or something.
    }

    public void SpawnMenu(RadialPressable obj)
    {
        RadialMenu newMenu = Instantiate(menuPrefab) as RadialMenu;

        newMenu.transform.SetParent(transform, false);

        newMenu.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f);

        newMenu.label.text = obj.title.ToUpper();

        if (TutorialEvents.instance != null && TutorialEvents.instance.GetCurrentEvent() == TutorialEvents.TutEvents.RadialFlash)
        {
            TutorialManager.instance.SetTutorialBox(true);
        }

        newMenu.SpawnButtons(obj);

        newMenu.transform.SetSiblingIndex(0);

        SetAwake(true);
    }

    public void SpawnStats(ObjectInfo.ObjectType objType, ObjectFill.FillType objFill)
    {
        for (int i = 0; i < statsMenus.Length; i++)
        {
            statsMenus[i].SetActive(false);
        }

        switch (objType)
        {
            case ObjectInfo.ObjectType.FIELD:

                switch(objFill)
                {
                    case ObjectFill.FillType.WHEAT:
                        statsMenus[0].SetActive(true);
                        break;

                    case ObjectFill.FillType.CORN:
                        statsMenus[2].SetActive(true);
                        break;

                    case ObjectFill.FillType.CARROT:
                        statsMenus[3].SetActive(true);
                        break;

                    case ObjectFill.FillType.POTATO:
                        statsMenus[4].SetActive(true);
                        break;

                    case ObjectFill.FillType.TURNIP:
                        statsMenus[5].SetActive(true);
                        break;

                    case ObjectFill.FillType.SUGARCANE:
                        statsMenus[9].SetActive(true);
                        break;

                    case ObjectFill.FillType.SUNFLOWER:
                        statsMenus[10].SetActive(true);
                        break;

                    case ObjectFill.FillType.COCCOA:
                        statsMenus[11].SetActive(true);
                        break;
                }

                break;

            case ObjectInfo.ObjectType.RICE:
                statsMenus[1].SetActive(true);
                break;

            case ObjectInfo.ObjectType.COW_FIELD:
                statsMenus[6].SetActive(true);
                break;

            case ObjectInfo.ObjectType.CHICKEN_COOP:
                statsMenus[7].SetActive(true);
                break;

            case ObjectInfo.ObjectType.PIG_PEN:
                statsMenus[8].SetActive(true);
                break;

            case ObjectInfo.ObjectType.MEAT_LAB:
                statsMenus[13].SetActive(true);
                break;

            case ObjectInfo.ObjectType.VERTICAL_FARM:
                statsMenus[14].SetActive(true);
                break;

            case ObjectInfo.ObjectType.FARMHOUSE:
                statsMenus[15].SetActive(true);
                break;

            case ObjectInfo.ObjectType.BARN:
                statsMenus[16].SetActive(true);
                break;

            case ObjectInfo.ObjectType.RESEARCH:
                statsMenus[17].SetActive(true);
                break;

            default:
                return;
        }

    }
}
  