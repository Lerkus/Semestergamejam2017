using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points_slave : MonoBehaviour
{

    public float _BodyRadius = 0;
    public int _PointsAwarded = 0;
    public float _ExplosionSize = 1;
    public bool floorAnim = false;

    public GameObject explosion;

    private string _PointsControllerName = "PointsController";

    private bool isQuitting = false;

    public void Start()
    {
        gameObject.transform.SetParent(GameObject.Find(_PointsControllerName).transform);
        transform.parent.gameObject.GetComponent<Points_master>().addNewIntactObject(this);
    }

    void OnDestroy()
    {
        if (!isQuitting)
        {
            GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
            exp.transform.localScale *= _ExplosionSize;
            if (floorAnim)
            {
                exp.transform.position = new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2, transform.position.z);
            }
        }
    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }
}
