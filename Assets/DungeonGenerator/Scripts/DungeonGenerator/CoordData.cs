using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is used for storing data from the JSON file when creating coordinates for a grid
[System.Serializable]
public class CoordData
{
    public Vector2Int rectPosition;
    public bool round = false;
    public CoordData(int x, int y)
    {
        rectPosition.x = x;
        rectPosition.y = y;
    }

    public void SetRound(bool set)
    {
        round = set;
    }
}
