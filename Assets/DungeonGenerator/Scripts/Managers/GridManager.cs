using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public TileCalculator tileCalculator;
    JsonLoader loader;

    public GameObject defaultCamera;
    public GameObject worldGrp;

    public bool flip;
    [SerializeField] private Vector2 min;
    [SerializeField] private Vector2 max;
    public Cell[,] grid;

    public float offset;

    public GameObject tile;
    
    public bool playerSpawned = false;
    public GameObject playerOBJ;
    private GameObject player;
    private Vector3 playerStartCoord;

    public TextAsset textJSON;
    public TextAsset[] JSONFiles;

    public void SpawnPlayer() 
    {
        if(playerStartCoord == Vector3.zero)
        {
            print("No Dungeon to Spawn Player");
        }
        else
        {
            if (playerSpawned == false)
            {
                defaultCamera.SetActive(false);
                playerSpawned = true;
                player = Instantiate(playerOBJ, playerStartCoord, Quaternion.identity);

            }
        }
    }

    public void BackToMenu()
    {
        if (playerSpawned)
        {
            playerSpawned = false;
            defaultCamera.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Destroy(player.gameObject);
        }
    }
    public void GenerateDungeon()
    {
        if (loader != null)
        {
            loader = null;
            this.min = Vector2.zero;
            this.max = Vector2.zero;
            playerStartCoord = Vector3.zero;
            grid = null;
            Destroy(worldGrp);
        }
        loader = GetComponent<JsonLoader>();
        int randomNumber = Random.Range(0, JSONFiles.Length);
        loader.Intialize(JSONFiles[randomNumber]);

        this.min = loader.GetMin;
        this.max = loader.GetMax;

        grid = new Cell[(int)(max.x), (int)(max.y)];
        worldGrp = new GameObject();
        SetUpGrid(loader.GetCoordinates);
    }


    private void SetUpGrid(List<CoordData> coordinates)
    {
        float yOffset = 0;
        int flipDirection = 1;
        if (flip)
        {
            yOffset = max.y;
            flipDirection = -1;
        }
        //URGENT!!
        //This needs to be a recursive call I'm guessing
        foreach (CoordData data in coordinates)
        {
            for (int x = 0; x < (max.x); x++)
            {
                for (int y = 0; y < (max.y); y++)
                {
                    if (data.positon.x == x && data.positon.y == y)
                    {
                        Cell newCell = new Cell(x, y, Cell.Contents.Tile);
                        if (data.round)
                        {
                            newCell.isRound = true;
                        }
                        else
                        {
                            newCell.isRound = false;
                        }
                        grid[x, y] = newCell;
                    }
                    else
                    {
                        if (grid[x, y] == null)
                        {
                            Cell newCell = new Cell(x, y, Cell.Contents.Empty);
                            newCell.isRound = false;
                            grid[x, y] = newCell;
                        }
                    }
                }
            }
        }
        tileCalculator = new TileCalculator(grid, min, max);
        //Creating the tiles in the grid
        foreach (Cell cell in grid)
        {
            if (cell != null)
            {

                if (cell.content == Cell.Contents.Tile)
                {

                    if (cell.isRound)
                    {
                        CreateTile(cell, yOffset, flipDirection, true);
                    }
                    else
                    {
                        CreateTile(cell, yOffset, flipDirection, false);
                    }
                    playerStartCoord = new Vector3(cell.position.x * offset, 0.5f, ((cell.position.y * flipDirection) + yOffset) * offset);
                }
            }
        }
    }

    void CreateTile(Cell cell, float yOffset, float flipDirection, bool isRound)
    {
        float posX = cell.position.x;
        float posY = (cell.position.y * flipDirection) + yOffset;
        GameObject newtile = Instantiate(tile, new Vector3(posX * offset, 2, posY * offset), Quaternion.identity);
        newtile.transform.parent = worldGrp.transform;
        TileData tileData = newtile.GetComponent<TileData>();
        tileData.tileX = (int)cell.position.x;
        tileData.tileY = (int)cell.position.y;
        tileData.type = tileCalculator.CheckPiece(tileData.tileX, tileData.tileY);
        tileData.rotation = cell.rotation;
        tileData.isRound = isRound;
        tileData.Initialize();
    }
    public static void printText(string text)
    {
        print(text);
    }
}

