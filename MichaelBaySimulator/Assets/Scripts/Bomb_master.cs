using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Diese Klasse wird keinesfalls Speichereffiszient sein...
public class Bomb_master : MonoBehaviour {
    private List<Bomb_slave> _WaitingBombs = new List<Bomb_slave>();
    private List<Bomb_slave> _TempExplodedBombs = new List<Bomb_slave>();

    /// <summary>
    /// Gibt die Anzahl der explodierten Bomben an. Also maximal soviele Bomben sind explodiert...
    /// Duplikate in der Liste sind möglich... Fixe ich morgen....
    /// </summary>
    /// <param name="bombToTrigger"></param>
    /// <returns></returns>
    public int triggerThatBomb(Bomb_slave bombToTrigger)
    {
        List<Bomb_slave> TempList = new List<Bomb_slave>();
        CopyBombListAInBombListB(_WaitingBombs, TempList);
        triggerBombRecursiv(bombToTrigger, _WaitingBombs);

        for(int i = 0; i < _TempExplodedBombs.Count; i++)
        {
            _WaitingBombs.Remove(_TempExplodedBombs[i]);
            LetThatBombExplode(_TempExplodedBombs[i]);
        }
        int temp = _TempExplodedBombs.Count;
        _TempExplodedBombs = new List<Bomb_slave>();

        return temp;
    }

    public void addWaitingBomb(Bomb_slave BombToAdd)
    {
        _WaitingBombs.Add(BombToAdd);
    }

    private bool canThisBombTriggerThatBomb(Bomb_slave triggerBomb, Bomb_slave waitingBomb)
    {
        bool temp = false;

        Vector2 distance = triggerBomb.gameObject.transform.position - waitingBomb.gameObject.transform.position;
        float sqrTriggerRadi = triggerBomb._ExplosionRadius + waitingBomb._BombRadius;
        sqrTriggerRadi *= sqrTriggerRadi;

        if (distance.sqrMagnitude <= sqrTriggerRadi)
        {
            temp = true;
        }

        for(int i = 0; i < _TempExplodedBombs.Count; i++)
        {
            if(waitingBomb == _TempExplodedBombs[i])
            {
                temp = false;
            }
        }
        return temp;
    }
    private void triggerBombRecursiv(Bomb_slave bombToTrigger, List<Bomb_slave> notYetExplodedBombs)
    {
        _TempExplodedBombs.Add(bombToTrigger);
        List<Bomb_slave> TempList = new List<Bomb_slave>();
        CopyBombListAInBombListB(notYetExplodedBombs, TempList);
        TempList.Remove(bombToTrigger);

        for (int i = 0; i < TempList.Count; i++)
        {
            if (canThisBombTriggerThatBomb(bombToTrigger, TempList[i]))
            {
                triggerBombRecursiv(TempList[i], TempList);
            }
        }
    }
    private void CopyBombListAInBombListB(List<Bomb_slave> A, List<Bomb_slave> B)
    {
        for(int i = 0; i < A.Count; i++)
        {
            B.Add(A[i]);
        }
    }
    private void LetThatBombExplode(Bomb_slave explodingBomb)
    {
        //Here you can do Stuff for an exploding Bomb.
    }
}
