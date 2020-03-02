using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketplaceOptions : MonoBehaviour
{
    //GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
    public ObjectInfo.ObjectType type;
    public ObjectFill.FillType filler;

    public void Quit()
    {
        InputScript i = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputScript>();
        i.AllowSelecting();
        Destroy( GameObject.FindGameObjectWithTag("Marketplace"));
    }
    public void Build()
    {
        InputScript i = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputScript>();
        Debug.Log("Building " + type.ToString() + " with filler " + filler.ToString());
        i.AttemptBuild(type, filler);

        Quit();
    }


}
