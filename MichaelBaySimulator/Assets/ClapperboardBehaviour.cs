using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClapperboardBehaviour : MonoBehaviour {

    public Gamemaster gm;

    bool pressed = false;

    public void OnStartFilmSceneButtonPressed()
    {
            gm.StartNextPhase();
    }
}
