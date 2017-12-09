using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClapperboardBehaviour : MonoBehaviour {

    public Gamemaster gm;

    public void OnStartFilmSceneButtonPressed()
    {
        // maybe animated clapperboard?
        gm.StartNextPhase();
    }
}
