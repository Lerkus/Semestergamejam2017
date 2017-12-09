using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

    [SerializeField]
    float minWaitingTime, maxWaitingTime; //waiting time
    [SerializeField]
    float moveSpeed, maxWalkDestination;
    
    int actionToPerform; //0: get next action; 1: walk; 2: wait
    bool startWalk;
    float timeToWait;
    float walkDestination; //X-Coordinate 
    int walkDirection;

	// Use this for initialization
	void Start () {
        actionToPerform = 0;
        timeToWait = 0.0f;
        startWalk = true;
        walkDestination = transform.position.x;
    }
	
	// Update is called once per frame
	void Update () {
        RandomWalk();
	}

    void RandomWalk()
    {

        switch (actionToPerform)
        {
            case 0: //Walk
                Walk();
                break;
            case 1: //Wait
                Wait();
                break;
            default:
                Debug.Log("FATAL ERROR!!11!");
                break;
        }
    }

    void Walk()
    {
        if (startWalk)
        {
            walkDestination = Random.Range(-maxWalkDestination, maxWalkDestination);
            startWalk = false;
        } else
        {
            transform.Translate(Mathf.Sign(walkDestination - transform.position.x) * moveSpeed * Time.deltaTime, 0, 0);
            if (Mathf.Abs(walkDestination - transform.position.x) <= 1.0f)
            {
                Debug.Log("asdf");
                actionToPerform = 1;
                startWalk = true;
            }
        }
    }

    void Wait()
    {
        if (timeToWait <= 0)
        {
            timeToWait = Random.Range(minWaitingTime, maxWaitingTime);
        }
        else
        {
            timeToWait -= Time.deltaTime;
            if (timeToWait <= 0)
            {
                Debug.Log("Wait");
                actionToPerform = 0;
            }
        }
    }


}