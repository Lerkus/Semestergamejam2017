using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points_slave : MonoBehaviour
{

    public float _BodyRadius = 0;
    public float _PointsAwarded = 0;

    private string _PointsControllerName = "PointsController";

    public void Start()
    {
        gameObject.transform.SetParent(GameObject.Find(_PointsControllerName).transform);
        transform.parent.gameObject.GetComponent<Points_master>().addNewIntactObject(this);
    }
}
