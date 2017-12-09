using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points_master : MonoBehaviour
{

    private List<Points_slave> _IntactObjects = new List<Points_slave>();

    public float Points
    {
        get;
        private set;
    }

    public void Start()
    {
        Points = 0;
    }

    public void addNewIntactObject(Points_slave IntactObject)
    {
        _IntactObjects.Add(IntactObject);
    }

    public void calculateBombPoints(List<Bomb_slave> ExplodedBombs)
    {
        List<Points_slave> temp = null;
        for (int i = 0; i < ExplodedBombs.Count; i++)
        {
            temp = whatDidThatBombHit(ExplodedBombs[i]);
            for (int j = 0; j < temp.Count; j++)
            {
                Points += temp[j]._PointsAwarded;
            }
        }
    }

    private List<Points_slave> whatDidThatBombHit(Bomb_slave ExplodedBomb)
    {
        List<Points_slave> temp = new List<Points_slave>();

        for (int i = 0; i < _IntactObjects.Count; i++)
        {
            if (canThatBombHitThat(ExplodedBomb, _IntactObjects[i]))
            {
                temp.Add(_IntactObjects[i]);
            }
        }

        return temp;
    }

    private bool canThatBombHitThat(Bomb_slave ExplodedBomb, Points_slave possibleHit)
    {
        bool temp = false;

        Vector2 distance = ExplodedBomb.gameObject.transform.position - possibleHit.gameObject.transform.position;
        float sqrTriggerRadi = ExplodedBomb._ExplosionRadius + possibleHit._BodyRadius;
        sqrTriggerRadi *= sqrTriggerRadi;

        if (distance.sqrMagnitude <= sqrTriggerRadi)
        {
            temp = true;
        }

        return temp;
    }
}
