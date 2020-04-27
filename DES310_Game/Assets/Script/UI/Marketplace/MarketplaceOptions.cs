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
        //find gamemanager and input scrip
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        InputScript i = gameManager.GetComponent<InputScript>();

        //call OnDestroyMP to make sure another marketplace doesnt open immediately.
        gameManager.GetComponent<MarketplaceSpawner>().OnDestroyMP();


        //destroy the marketplace
        Destroy( GameObject.FindGameObjectWithTag("Marketplace"));
    }

    //build will attempt to build the field vased on the type and filler of the marketplace the player is on
    public void Build()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        InputScript i = gameManager.GetComponent<InputScript>();

        Debug.Log("Building " + type.ToString() + " with filler " + filler.ToString());

        i.AttemptBuild(type, filler);

        //close marketplace
        Quit();
    }


}
