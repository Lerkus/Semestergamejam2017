using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSingelton : MonoBehaviour {

    public GameObject _ScorePrefab;
    private readonly string _ScoreCloneObjectName = "Score(Clone)";
	
	public void Start () {
		if(GameObject.Find(_ScoreCloneObjectName) == null)
        {
            GameObject temp = Instantiate(_ScorePrefab);
            DontDestroyOnLoad(temp);
        } else
        {
            Debug.Log("hi");
        }
	}
}
