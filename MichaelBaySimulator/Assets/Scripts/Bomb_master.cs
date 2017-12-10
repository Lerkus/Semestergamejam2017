using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Diese Klasse wird keinesfalls Speichereffiszient sein...
public class Bomb_master : MonoBehaviour {

    public Points_master pm;

    public bool bombAdded = false;

    void Start()
    {
    }

    void Update()
    {
        if (bombAdded)
        {
            checkChildCount();
        }
    }

    public void TriggerOtherBombs(Bomb_slave explodingBomb)
    {
        bombAdded = true;
        for(int i = 0; i < transform.childCount; i++)
        {
            Bomb_slave childBomb = transform.GetChild(i).GetComponent<Bomb_slave>();
            if (childBomb._ChainedExplosions == 0)
            {
                CompareAndTrigger(explodingBomb, childBomb);
            }
        }
    }


    void CompareAndTrigger(Bomb_slave explodingBomb, Bomb_slave bombToTrigger)
    {
        if((explodingBomb.transform.position - bombToTrigger.transform.position).magnitude <= explodingBomb._BombRadius + bombToTrigger._ExplosionRadius)
        {
            bombToTrigger.ExplodeBomb(explodingBomb._ChainedExplosions);
        }
    }

    public void checkChildCount()
    {
        if(transform.childCount <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    public IEnumerator GameOver()
    {
        pm.SetSlavesQuitting();
        GameObject.Find("Score(Clone)").GetComponent<Score>().score = (int)pm.Points;
        yield return new WaitForSeconds(2);
        Debug.Log("Game Over!");
        SceneManager.LoadScene("GameOver");
    }
}
