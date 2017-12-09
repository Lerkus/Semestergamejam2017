using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSingelton : MonoBehaviour {

    public GameObject _MusicPrefab;
    private readonly string _MusicCloneObjectName = "Musik(Clone)";
	
	public void Start () {
		if(GameObject.Find(_MusicCloneObjectName) == null)
        {
            GameObject temp = Instantiate(_MusicPrefab);
            DontDestroyOnLoad(temp);
        }
	}
}
