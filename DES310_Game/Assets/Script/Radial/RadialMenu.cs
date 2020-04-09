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
        //gets tile currently selected
        int selectedID = gameManager.GetComponent<InputScript>().GetSelectedID();

        if (selected)
        {
            //button function goes here
            Debug.Log(selected.title + "was selected");

            if (selected.title == "Upgrade")
            {
                gameManager.GetComponent<InputScript>().AttemptUpgrade(selectedID);
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
            gameManager.GetComponent<InputScript>().SetAllowSelecting(true);
        }
    }
}
