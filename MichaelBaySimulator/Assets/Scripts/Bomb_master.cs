using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Diese Klasse wird keinesfalls Speichereffiszient sein...
public class Bomb_master : MonoBehaviour {
    
    void Start()
    {
    }

    public void TriggerBomb(Bomb_slave explodingBomb)
    {
        Debug.Log("Bomb triggered!");
        explodingBomb.triggered = true;
        for(int i = 0; i < transform.childCount; i++)
        {
            Bomb_slave childBomb = transform.GetChild(i).GetComponent<Bomb_slave>();
            if (!childBomb.triggered)
            {
                TriggerOtherBomb(explodingBomb, childBomb);
            }
        }
        explodingBomb.ExplodeBomb();
    }


    void TriggerOtherBomb(Bomb_slave explodingBomb, Bomb_slave bombToTrigger)
    {
        if((explodingBomb.transform.position - bombToTrigger.transform.position).magnitude <= explodingBomb._BombRadius + bombToTrigger._ExplosionRadius)
        {
            TriggerBomb(bombToTrigger);
        }
    }
}
