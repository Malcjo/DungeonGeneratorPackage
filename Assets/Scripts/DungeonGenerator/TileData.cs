using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceType
{
    Empty,
    Corner,
    Edge,
    Doorway,
    Hallway,
    DoorwayLeftCorner,
    DoorwayRightCorner
}

[System.Serializable]
public class TileData : MonoBehaviour
{
    [Header("Hallway, DoorwayLeftCorner, DoorwayRightCorner")]
    [Header("Empty, Corner, Edge, Doorway")]

    public PieceType type;
    public GameObject[] SquarePiecesGrp;
    public GameObject[] CircularPiecesGrp;
    
    private GameObject pieceToSpawn;
    public float rotation;
    public int tileX;
    public int tileY;
    private Vector2 minPosition;
    private Vector2 maxPosition;
    public bool isRound;

    public void Initialize()
    {
        FigureOutPiece();
        SpawnMesh();
    }
    private void SpawnMesh()
    {
        GameObject newMesh = Instantiate(pieceToSpawn, new Vector3(this.transform.position.x, 0, this.transform.position.z), Quaternion.Euler(0, rotation, 0)) as GameObject;
        newMesh.transform.parent = this.transform;
    }
    private void FigureOutPiece()
    {
        if (!isRound)
        {
            switch (type)
            {
                case PieceType.Empty:
                    pieceToSpawn = SquarePiecesGrp[0];
                    break;
                case PieceType.Corner:
                    pieceToSpawn = SquarePiecesGrp[1];
                    break;
                case PieceType.Edge:
                    pieceToSpawn = SquarePiecesGrp[2];
                    break;
                case PieceType.Hallway:
                    pieceToSpawn = SquarePiecesGrp[3];
                    break;
                case PieceType.Doorway:
                    pieceToSpawn = SquarePiecesGrp[4];
                    break;
                case PieceType.DoorwayLeftCorner:
                    pieceToSpawn = SquarePiecesGrp[5];
                    break;
                case PieceType.DoorwayRightCorner:
                    pieceToSpawn = SquarePiecesGrp[6];
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case PieceType.Empty:
                    pieceToSpawn = CircularPiecesGrp[0];
                    break;
                case PieceType.Corner:
                    pieceToSpawn = CircularPiecesGrp[1];
                    break;
                case PieceType.Edge:
                    pieceToSpawn = CircularPiecesGrp[2];
                    break;
                case PieceType.Hallway:
                    pieceToSpawn = CircularPiecesGrp[3];
                    break;
                case PieceType.Doorway:
                    pieceToSpawn = CircularPiecesGrp[4];
                    break;
                case PieceType.DoorwayLeftCorner:
                    pieceToSpawn = CircularPiecesGrp[5];
                    break;
                case PieceType.DoorwayRightCorner:
                    pieceToSpawn = CircularPiecesGrp[6];
                    break;
            }
        }
    }
}
