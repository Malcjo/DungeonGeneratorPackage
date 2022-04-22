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
    DoorwayRightCorner,
    Deadend
}

[System.Serializable]
public class TileData : MonoBehaviour
{
    [Header("Hallway, DoorwayLeftCorner, DoorwayRightCorner")]
    [Header("Empty, Corner, Edge, Doorway")]

    public PieceType type;
    public GameObject[] CircularPiecesGrp;

    public GameObject[] SquareEmptyPieceGroup;
    private GameObject SquareEmptyPiece;

    public GameObject[] SquareEdgePieceGroup;
    private GameObject SquareEdgePiece;

    public GameObject[] SquareCornerPieceGroup;
    private GameObject SquareCornerPiece;

    public GameObject[] SquareHallwayPieceGroup;
    private GameObject SquareHallwayPiece;

    public GameObject[] SquareDoorwayPieceGroup;
    private GameObject SquareDoorwayPiece;

    public GameObject[] SquareDoorwayLeftCornerPieceGroup;
    private GameObject SquareDoorwayLeftCornerPiece;

    public GameObject[] SquareDoorwayRightCornerPieceGroup;
    private GameObject SquareDoorwayRightCornerPiece;

    public GameObject[] SquareDeadendPieceGroup;
    private GameObject SquareDeadendPiece;

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
                    int num = Random.Range(0, SquareEmptyPieceGroup.Length);
                    SquareEmptyPiece = SquareEmptyPieceGroup[num];
                    pieceToSpawn = SquareEmptyPiece;
                    break;
                case PieceType.Corner:
                    int num2 = Random.Range(0, SquareCornerPieceGroup.Length);
                    SquareCornerPiece = SquareCornerPieceGroup[num2];
                    pieceToSpawn = SquareCornerPiece;
                    break;
                case PieceType.Edge:
                    int num3 = Random.Range(0, SquareEdgePieceGroup.Length);
                    SquareEdgePiece = SquareEdgePieceGroup[num3];
                    pieceToSpawn = SquareEdgePiece;
                    break;
                case PieceType.Hallway:
                    int num4 = Random.Range(0, SquareHallwayPieceGroup.Length);
                    SquareHallwayPiece = SquareHallwayPieceGroup[num4];
                    pieceToSpawn = SquareHallwayPiece;
                    break;
                case PieceType.Doorway:
                    int num5 = Random.Range(0, SquareDoorwayPieceGroup.Length);
                    SquareDoorwayPiece = SquareDoorwayPieceGroup[num5];
                    pieceToSpawn = SquareDoorwayPiece;
                    break;
                case PieceType.DoorwayLeftCorner:
                    int num6 = Random.Range(0, SquareDoorwayLeftCornerPieceGroup.Length);
                    SquareDoorwayLeftCornerPiece = SquareDoorwayLeftCornerPieceGroup[num6];
                    pieceToSpawn = SquareDoorwayLeftCornerPiece;
                    break;
                case PieceType.DoorwayRightCorner:
                    int num7 = Random.Range(0, SquareDoorwayRightCornerPieceGroup.Length);
                    SquareDoorwayRightCornerPiece = SquareDoorwayRightCornerPieceGroup[num7];
                    pieceToSpawn = SquareDoorwayRightCornerPiece;
                    break;
                case PieceType.Deadend:
                    int num8 = Random.Range(0, SquareDeadendPieceGroup.Length);
                    SquareDeadendPiece = SquareDeadendPieceGroup[num8];
                    pieceToSpawn = SquareDeadendPiece;
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
