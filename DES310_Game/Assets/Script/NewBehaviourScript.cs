using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public RuntimeAnimatorController animator;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Animator>();
        gameObject.GetComponent<Animator>().runtimeAnimatorController = animator;
    }

    void Update()
    {
        gameObject.GetComponent<Animator>().SetTrigger("trig");
    }


}
