using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_slave : MonoBehaviour
{

    public float _BombRadius = 0;
    public float _ExplosionRadius = 0;

    private readonly string _BombControllerName = "BombController";

    public void Start()
    {
        gameObject.transform.SetParent(GameObject.Find(_BombControllerName).transform);
        gameObject.transform.parent.gameObject.GetComponent<Bomb_master>().addWaitingBomb(this);
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
