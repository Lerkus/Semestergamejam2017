using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum phase
{
    place,
    explode,
    finish
}

public class Gamemaster : MonoBehaviour {

    private readonly string _Phase1Tag = "phase1";
    private readonly string _Phase2Tag = "phase2";

    public phase _CurrentPhase
    {
        private set;
        get;
    }


	public void Start () {
        _CurrentPhase = phase.place;
        if ((int)_CurrentPhase == 0)
        {
            SetStateOfStateObjects(true, _Phase1Tag);
            SetStateOfStateObjects(false, _Phase2Tag);
        }
    }

    public void StartNextPhase()
    {
        GameObject[] TempObjects = null;
        if ((int)_CurrentPhase < 2)
        {
            _CurrentPhase++;

            if ((int)_CurrentPhase == 1)
            {
                SetStateOfStateObjects(false, _Phase1Tag);
                SetStateOfStateObjects(true, _Phase2Tag);
            }

            if ((int)_CurrentPhase == 2)
            {
                SetStateOfStateObjects(false, _Phase1Tag);
                SetStateOfStateObjects(false, _Phase2Tag);
            }
        }
    } 

    private void SetStateOfStateObjects(bool enabled, string Phasetag)
    {
        GameObject[] TempObjects = GameObject.FindGameObjectsWithTag(Phasetag);
        for (int i = 0; i < TempObjects.GetLength(0); i++)
        {
            TempObjects[i].transform.GetChild(0).gameObject.SetActive(enabled);
        }
    }
}
