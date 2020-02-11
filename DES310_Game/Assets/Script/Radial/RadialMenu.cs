using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    public Text label;
    public RadialButtonScript buttonPrefab;
    public RadialButtonScript selected;
    public GameObject gameManager;

    public void SpawnButtons(RadialPressable obj)
    {
        StartCoroutine(AnimateButtons(obj));
    }

    IEnumerator AnimateButtons (RadialPressable obj)
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
            newButton.myMenu = this;
            newButton.Anim();
            yield return new WaitForSeconds(0.06f);
        }

    }

    void Update()
    {
        gameManager = GameObject.FindWithTag("GameController");
        int selectedID = gameManager.GetComponent<InputScript>().GetSelectedID();

        if (Input.GetMouseButtonDown(0) && RadialMenuSpawner.instance.GetAwake() == true)
        {
            gameManager.GetComponent<InputScript>().SetAllowSelecting(false);

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
                    gameManager.GetComponent<InputScript>().AttemptBuild(selectedID);
                }
                else if (selected.title == "Demolish")
                {
                    gameManager.GetComponent<InputScript>().AttmeptDemolish(selectedID);
                }

                Destroy(gameObject);
                RadialMenuSpawner.instance.SetAwake(false);
                gameManager.GetComponent<InputScript>().SetAllowSelecting(true);
            }
        }
    }
}
