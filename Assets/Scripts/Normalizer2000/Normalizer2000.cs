using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normalizer2000 : MonoBehaviour
{
    public Vector2Int[] inputGrid;
    private Vector2Int[] normalizedGrid;

    public GameObject tile;

    public bool redrawGrid = false;
    public bool normalizeGrid = false;
    public bool resetGrid = false;

    void NormalizeGrid()
    {
        ResetGrid();

        var min = Vector2Int.zero;
        min = inputGrid[0];//set initial minimum
        for (int i =1; i < inputGrid.Length; i++)
        {
            min = Vector2Int.Min(min, inputGrid[i]);
        }
        Debug.Log(min);

        for (int i = 0; i < inputGrid.Length; i ++)
        {
            normalizedGrid[i] = new Vector2Int(
                inputGrid[i].x + Mathf.Abs(min.x),
                inputGrid[i].y + Mathf.Abs(min.y));
        }

        DrawGrid(normalizedGrid);
    }

    void ResetGrid()
    {
        
        if (normalizedGrid == null)
            normalizedGrid = new Vector2Int[inputGrid.Length];
        for (int i = 0; i < inputGrid.Length; i++)
        {
            normalizedGrid[i] = inputGrid[i];
        }
    }

    void ClearGrid()
    {
        int childrenCount = this.transform.childCount;

        for (int i =0; i < childrenCount; i++)
        {
            GameObject.Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (inputGrid.Length == 0)
        {
            inputGrid = new Vector2Int[25];
            int idx = 0;
            for (int y = -2; y < 3; y++)
            {
                for (int x = -2; x < 3; x++)
                {
                    inputGrid[idx++] = new Vector2Int(x, y);
                }
            }
        }
        ResetGrid();
    }

    void DrawGrid(Vector2Int[] drawGrid)
    {
        ClearGrid();
        foreach (var g in drawGrid)
        {
            var point3d = new Vector3(g.x, g.y, 0); //Change z for layering
            var gridTile = Instantiate<GameObject>(tile, point3d, this.transform.rotation, this.transform);
            if (g == Vector2Int.zero)
            {
                gridTile.GetComponent<SpriteRenderer>().color = Color.red;
            } else
            {
                gridTile.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (redrawGrid)
        {
            redrawGrid = false;
            DrawGrid(normalizedGrid);
        }
        if (normalizeGrid)
        {
            normalizeGrid = false;
            NormalizeGrid();
            redrawGrid = true;
        }
        if (resetGrid)
        {
            resetGrid = false;
            ResetGrid();
            redrawGrid = true;
        }
    }
}
