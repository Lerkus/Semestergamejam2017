using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddedPoints : MonoBehaviour {

    public float moveSpeed;
    public float fadeDuration;

    Text text;

	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        text.CrossFadeAlpha(0.0f, fadeDuration, false);
	}
}
