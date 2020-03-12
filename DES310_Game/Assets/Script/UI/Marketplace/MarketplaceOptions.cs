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
        LeanTween.scale(GameObject.FindGameObjectWithTag("Marketplace"), new Vector3(0, 0, 0), 0.3f).setOnComplete(DestroyMe);
        //Destroy( GameObject.FindGameObjectWithTag("Marketplace"));
    }
    public void Build()
    {
        InputScript i = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputScript>();
        Debug.Log("Building " + type.ToString() + " with filler " + filler.ToString());
        i.AttemptBuild(type, filler);
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 3.0f);
        Quit();
    }
    void DestroyMe()
    {
        Destroy(GameObject.FindGameObjectWithTag("Marketplace"));
    }

    

}
