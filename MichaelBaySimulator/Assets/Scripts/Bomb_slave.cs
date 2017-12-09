using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_slave : MonoBehaviour
{
    public float _BombRadius = 0;
    public float _ExplosionRadius = 0;
    public float _ExplodingTimer = 0;

    public GameObject explodingBomb;
    public Sprite triggeredSprite;


    private readonly string _BombControllerName = "BombController";
    private readonly float explosionScalingFactor = 0.1f;
    private readonly int blinkScale = 8;

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
                transform.parent.gameObject.GetComponent<Bomb_master>().TriggerOtherBombs(this);
                transform.parent.GetComponent<Bomb_master>().pm.calculateBombPoints(this);
                transform.parent.GetComponent<Bomb_master>().pm.AddScore(Mathf.CeilToInt(_ExplosionRadius));
                explodeAnim = Instantiate(explodingBomb, transform.position, Quaternion.identity);
                explodeAnim.transform.localScale = this.gameObject.transform.localScale * explosionScalingFactor;
                explodeAnim.transform.position = new Vector3(explodeAnim.transform.position.x,
                    explodeAnim.transform.position.y + explodeAnim.transform.localScale.y/2,
                    explodeAnim.transform.position.z);
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
            ExplodeBomb();
        }
    }

    public void ExplodeBomb()
    {
        triggered = true;
        explode = true;
    }

    //public void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, _ExplosionRadius);
    //}
}
