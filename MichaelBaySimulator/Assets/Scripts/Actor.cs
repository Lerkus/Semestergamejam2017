using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {
    
    [SerializeField]
    float moveSpeed, maxWalkDestination;
    
    float walkDestination; //X-Coordinate 
    int walkDirection;
    float startX;

	// Use this for initialization
	void Start ()
    {
        startX = transform.position.x;
        walkDestination = startX + maxWalkDestination;
        walkDirection = 1;
    }
	
	// Update is called once per frame
	void Update () {
        Walk();
	}

    void Walk()
    {
        transform.Translate(walkDirection * moveSpeed * Time.deltaTime, 0, 0);
        walkDirection = (int)Mathf.Sign(walkDestination - transform.position.x);     
          
        if(walkDirection <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            walkDestination = startX;
        } else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            walkDestination = startX + maxWalkDestination;
        }
        
    }
    
}