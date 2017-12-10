using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeAnimation : MonoBehaviour {

    Animator anim;

    float delay = 3.0f;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length + delay);
    }	
}
