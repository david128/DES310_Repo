using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    public Text label;
    public RadialButtonScript buttonPrefab;
    public RadialButtonScript selected;
    GameObject gameManager;

    GameLevelStorer gls = new GameLevelStorer();

    int[] levels = new int[3];

    public void SpawnButtons(RadialPressable obj)
    {
        //finds game manager
        gameManager = GameObject.FindWithTag("GameController");

        StartCoroutine(AnimateButtons(obj));
    }

    IEnumerator AnimateButtons(RadialPressable obj)
    {
        for (int i = 0; i < obj.options.Length; i++)
        {
            RadialButtonScript newButton = Instantiate(buttonPrefab) as RadialButtonScript;

            newButton.transform.SetParent(transform, false);

            float theta = (2 * Mathf.PI / obj.options.Length) * i;
            float xPos = Mathf.Sin(theta);
            float yPos = Mathf.Cos(theta);

            newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * 100f;
            newButton.circle.color = obj.options[i].Color;
            newButton.symbol.sprite = obj.options[i].Symbol;
            newButton.title = obj.options[i].Title;

            if (TutorialEvents.instance != null && TutorialEvents.instance.GetCurrentEvent() == TutorialEvents.TutEvents.RadialFlash)
            {
                if (newButton.title == "Demolish")
                {
                    //Gives button an animator compnent to flash only if its the destroy button
                    newButton.gameObject.AddComponent<Animator>();

                    Animator butAnim = newButton.GetComponent<Animator>();

                    //sets animator
                    butAnim.runtimeAnimatorController = TutorialManager.instance.GetAnimContr();

                    //plays animation
                    butAnim.Play("UIFlashAnim");
                }
            }

            newButton.myMenu = this;
            newButton.Anim();

            yield return new WaitForSeconds(0.06f);
        }
    }

    public void RadialOption()
    {
        GameObject gridTile;

        gls.GetLevels();

        levels = gls.GetLvls();

        int currentFarmLevel = levels[0];
        int currentBarnLevel = levels[1];
        int currentResearchLevel = levels[2];

        bool failToUpgrade = false;

        //gets tile currently selected
        int selectedID = gameManager.GetComponent<InputScript>().GetSelectedID();

        gridTile = gameManager.GetComponent<GridScript>().GetGridTile(selectedID);

        if (selected)
        {
            //button function goes here
            Debug.Log(selected.title + "was selected");

            if (selected.title == "Upgrade")
            {
                if ((currentFarmLevel > gridTile.GetComponent<ObjectInfo>().GetObjectLevel() || gridTile.GetComponent<ObjectInfo>().GetObjectType() == ObjectInfo.ObjectType.FARMHOUSE || gridTile.GetComponent<ObjectInfo>().GetObjectType() == ObjectInfo.ObjectType.BARN || gridTile.GetComponent<ObjectInfo>().GetObjectType() == ObjectInfo.ObjectType.RESEARCH))
                {
                    gameManager.GetComponent<InputScript>().AttemptUpgrade(selectedID);
                }
                else
                {
                    failToUpgrade = true;
                    gameManager.GetComponent<GameLoop>().GetUpgradeWarning().gameObject.SetActive(true);
                    gameManager.GetComponent<InputScript>().SetAllowSelecting(false);
                }
            }
            else if (selected.title == "Build")
            {
                gameManager.GetComponent<InputScript>().AttemptBuild(ObjectInfo.ObjectType.FIELD, ObjectFill.FillType.NONE);
            }
            else if (selected.title == "Demolish")
            {
                gameManager.GetComponent<InputScript>().AttemptDemolish(selectedID);

                if (TutorialEvents.instance != null && TutorialEvents.instance.GetCurrentEvent() == TutorialEvents.TutEvents.RadialFlash)
                {
                    TutorialEvents.instance.SetDestroyedCarrot(true);
                    TutorialEvents.instance.RunEvent(3);
                }
            }

            Destroy(gameObject);

            RadialMenuSpawner.instance.SetAwake(false);

            if (failToUpgrade == false)
            { 
                gameManager.GetComponent<InputScript>().SetAllowSelecting(true);
            }
        }
    }
}
