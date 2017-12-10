using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_slave : MonoBehaviour
{
    public float _BombRadius = 0;
    public float _ExplosionRadius = 0;
    public float _ExplodingTimer = 0;

    public GameObject[] explodingBomb;
    public Sprite triggeredSprite;


    private readonly string _BombControllerName = "BombController";
    private readonly float explosionScalingFactor = 0.1f;
    private readonly int blinkScale = 8;

    public int _ChainedExplosions = 0;
    private bool explode;

    public void Start()
    {
        gameObject.transform.SetParent(GameObject.Find(_BombControllerName).transform);
        //gameObject.transform.parent.gameObject.GetComponent<Bomb_master>().addWaitingBomb(this);
        transform.GetChild(1).gameObject.SetActive(false);
        _ChainedExplosions = 0;
        explode = false;
    }

    public void Update()
    {
        if (explode)
        {
            if(Mathf.FloorToInt((_ExplodingTimer * 10) % blinkScale) == 0)
            {
                transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.red;
            } else if(Mathf.FloorToInt((_ExplodingTimer * 10) % blinkScale) == blinkScale/2)
            {
                transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
            }
            _ExplodingTimer -= Time.deltaTime;
            //Debug.Log("Explosion in " + _ExplodingTimer + "s!");
            if (_ExplodingTimer <= 0)
            {
                GameObject explodeAnim;
                int randomExplosion = Random.Range(0, 2);
                transform.parent.gameObject.GetComponent<Bomb_master>().TriggerOtherBombs(this);
                transform.parent.GetComponent<Bomb_master>().pm.calculateBombPoints(this);
                transform.parent.GetComponent<Bomb_master>().pm.AddScore(Mathf.CeilToInt(_ExplosionRadius), transform.position);
                explodeAnim = Instantiate(explodingBomb[randomExplosion], transform.position, Quaternion.identity);
                explodeAnim.transform.localScale = this.gameObject.transform.localScale * explosionScalingFactor;
                explodeAnim.transform.position = new Vector3(explodeAnim.transform.position.x,
                    explodeAnim.transform.position.y + explodeAnim.transform.localScale.y/2,
                    explodeAnim.transform.position.z);
                Camera.main.GetComponent<ScreenShake>().shake = 0.3f;
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
            ExplodeBomb(0);
        }
    }

    public void ExplodeBomb(int AmountOfPreviousChainedExplosions)
    {
        _ChainedExplosions = AmountOfPreviousChainedExplosions + 1;
        explode = true;
    }

    //public void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, _ExplosionRadius);
    //}
}
