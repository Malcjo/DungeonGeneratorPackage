using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cell
{
    public enum Contents
    {
        Empty,
        Tile,
    }
    public PieceType pieceType;
    public bool isRound;
    public Contents content = new Contents();
    public Vector2 position;
    public float rotation = 0;
    
    public Cell(float x, float y, Contents cont) 
    {
        this.position.x = x;
        this.position.y = y;
        this.content = cont;
    }
}
