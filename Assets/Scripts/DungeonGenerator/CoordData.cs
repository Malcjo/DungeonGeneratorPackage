using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CoordData
{
    public Vector2 positon;
    public bool round = false;
    public CoordData(float x, float y)
    {
        positon.x = x;
        positon.y = y;
    }

    public void SetRound(bool set)
    {
        round = set;
    }
}
