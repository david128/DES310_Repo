using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketplaceSpawner : MonoBehaviour
{
    public GameObject menuPrefab;
    
    public void SpawnMenu()
    {
        GameObject newMenu = Instantiate(menuPrefab, GameObject.FindGameObjectWithTag("Canvas").transform) as GameObject;

        //Gives marketplace menu a while awake script component
        newMenu.gameObject.AddComponent<WhileAwake>();

        newMenu.transform.SetSiblingIndex(6);
    }

    public void OnDestroyMP()
    {
        
        StartCoroutine(WaitForMarketPlace());
    }

    public IEnumerator WaitForMarketPlace()
    {
        float counter = 0;
        float waitTime = 0.5f;

        //Now, Wait until the current state is done playing
        while (counter < (waitTime))
        {
            counter += Time.deltaTime;
            yield return null;
        }

        //allow selecting again
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        gameManager.GetComponent<InputScript>().AllowSelecting();
    }
}
