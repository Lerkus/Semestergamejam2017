using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

    public float shake = 0;

    float shakeAmount = 0.7f;
    float decreaseFactor = 1.0f;

    Vector3 startingPosition;

	// Use this for initialization
	void Start () {
        startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(shake > 0)
        {
            shake -= Time.deltaTime * decreaseFactor;
            Vector2 randomCircle = Random.insideUnitCircle;
            this.gameObject.transform.localPosition = new Vector3(randomCircle.x * shakeAmount, randomCircle.y * shakeAmount, transform.localPosition.z);
        } else
        {
            shake = 0;
            transform.position = startingPosition;
        }
	}
}
