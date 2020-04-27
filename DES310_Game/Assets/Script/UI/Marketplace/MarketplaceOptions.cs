using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketplaceOptions : MonoBehaviour
{
    //GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
    public ObjectInfo.ObjectType type;
    public ObjectFill.FillType filler;

    public ObjectInfo.ObjectType GetOptType() { return type; }
    public ObjectFill.FillType GetOptFill() { return filler; }

    //quit button will allow selecting again and then destroy marketplace
    public void Quit()
    {
        InputScript i = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputScript>();
        i.AllowSelecting();
        Destroy( GameObject.FindGameObjectWithTag("Marketplace"));
    }

    //build will attempt to build the field vased on the type and filler of the marketplace the player is on
    public void Build()
    {
        InputScript i = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputScript>();

        Debug.Log("Building " + type.ToString() + " with filler " + filler.ToString());

        i.AttemptBuild(type, filler);

        Quit();
    }


}
