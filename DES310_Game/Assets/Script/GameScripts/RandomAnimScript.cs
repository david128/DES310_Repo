using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animator anim = GetComponent<Animator>();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        anim.Play(state.fullPathHash, -1, Random.Range(0.0f, 2.5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
