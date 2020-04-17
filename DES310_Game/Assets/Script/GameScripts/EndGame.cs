using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject BadEnd;
    public GameObject GoodEnd;

    public GameObject Stats;

    // Start is called before the first frame update
    void Awake()
    {
        //Checks 
        if(PlayerPrefs.GetInt("Ending") == 0)
        {
            BadEnd.SetActive(true);
            GoodEnd.SetActive(false);

            StartCoroutine(WaitToShowStats(5));
        }
        else
        {
            GoodEnd.SetActive(true);
            BadEnd.SetActive(false);

            StartCoroutine(WaitToShowStats(5));
        }
    }

    IEnumerator WaitToShowStats(int waitTime)
    {
        //float counter = 0;

        //while (counter < waitTime)
        //{
        //    counter += Time.deltaTime;

        //    yield return null;
        //}

        yield return new WaitForSeconds(waitTime);

        Stats.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        Stats.GetComponent<Animator>().SetTrigger("Start");

    }
}
