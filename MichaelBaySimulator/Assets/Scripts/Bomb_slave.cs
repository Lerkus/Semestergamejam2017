using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_slave : MonoBehaviour
{
    public float _BombRadius = 0;
    public float _ExplosionRadius = 0;
    public float _ExplodingTimer = 0;


    private readonly string _BombControllerName = "BombController";

    public bool triggered; //TODO: Make private
    private bool explode;

    public void Start()
    {
        gameObject.transform.SetParent(GameObject.Find(_BombControllerName).transform);
        //gameObject.transform.parent.gameObject.GetComponent<Bomb_master>().addWaitingBomb(this);
        transform.GetChild(1).gameObject.SetActive(false);
        triggered = false;
        explode = false;
    }

    public void Update()
    {
        if (explode)
        {
            _ExplodingTimer -= Time.deltaTime;
            Debug.Log("Explosion in " + _ExplodingTimer + "s!");
            if (_ExplodingTimer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnMouseDown()
    {
        if (transform.GetChild(0).gameObject.activeSelf)
        {
            //Phase 1
        }
        else
        {
            transform.parent.gameObject.GetComponent<Bomb_master>().TriggerBomb(this);
        }
    }

    public void ExplodeBomb()
    {
        explode = true;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _ExplosionRadius);
    }
}
