using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudmover : MonoBehaviour
{

    public float speed = 0.35f;

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("cloudmover.update");
        transform.position = new Vector3(transform.position.x + Time.deltaTime * speed, transform.position.y, transform.position.z);
        		
        if (transform.position.x > 12)
        {
            transform.position = new Vector3(-12, transform.position.y, transform.position.z);
        }
	}
}
