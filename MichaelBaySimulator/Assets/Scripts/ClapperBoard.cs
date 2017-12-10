using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClapperBoard : MonoBehaviour {

    bool pressed = false;
    public Bomb_master bm;

	public void setPressed()
    {
        pressed = true;
    }

    void Update()
    {
        if (pressed && !gameObject.GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
            bm.checkChildCount();
        }
    }
}
