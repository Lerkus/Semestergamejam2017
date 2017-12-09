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
    private readonly string _BaseTag = "base";

    public phase _CurrentPhase
    {
        private set;
        get;
    }


	public void Start () {
        _CurrentPhase = phase.place;
        if ((int)_CurrentPhase == 0)
        {
            SetStateOfStateObjects(true, phase.place);
            SetStateOfStateObjects(false, phase.explode);
        }

        //StartNextPhase();
    }

    public void StartNextPhase()
    {
        if ((int)_CurrentPhase < 2)
        {
            _CurrentPhase++;

            if ((int)_CurrentPhase == 1)
            {
                SetStateOfStateObjects(false, phase.place);
                SetStateOfStateObjects(true, phase.explode);
            }

            if ((int)_CurrentPhase == 2)
            {
                SetStateOfStateObjects(false, phase.place);
                SetStateOfStateObjects(false, phase.explode);
            }
        }
    } 

    private void SetStateOfStateObjects(bool enabled, phase phaseChildsToBeAffected)
    {
        GameObject[] TempObjects = GameObject.FindGameObjectsWithTag(_BaseTag);
        for (int i = 0; i < TempObjects.GetLength(0); i++)
        {
            TempObjects[i].transform.GetChild((int)phaseChildsToBeAffected).gameObject.SetActive(enabled);
        }
    }
}
