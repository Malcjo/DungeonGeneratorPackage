using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//The the some coordinates are created from the JSON file thats parsed through the LevelData class
public class JsonLoader : MonoBehaviour
{
    private LevelData levelData;
    private Vector2Int oldMin;
    private Vector2Int oldMax;
    private Vector2 newMin;
    private Vector2 newMax;
    private Vector2Int offset;

    private List<CoordData> coordinates;

    public Vector2 GetMin { get { return newMin; } }
    public Vector2 GetMax { get { return newMax; } }
    public List<CoordData> GetCoordinates { get{ return coordinates; } }
    public void Intialize(TextAsset textJSON)
    {
        levelData = JsonUtility.FromJson<LevelData>(textJSON.text);
        coordinates = new List<CoordData>();
        SetUpCoordAndMinMax();
    }
    private void SetUpCoordAndMinMax()
    {
        GetInitialCoordinatess();
        GetInitialMinAndMax();
        SetNewMinAndMax();
        SetNewCoordinates();
    }
    private void GetInitialCoordinatess()
    {
        foreach (LevelData.Rect rect in levelData.rects)
        {
            int x = rect.x;
            int y = rect.y;
            int xLoop = rect.w;
            int yLoop = rect.h;
            for (int i = 0; i < xLoop; i++)
            {
                for (int ii = 0; ii < yLoop; ii++)
                {
                    CoordData newCoord = new CoordData(x + i, y + ii);
                    newCoord.SetRound(rect.rotunda);
                    coordinates.Add(newCoord);
                }
            }
        }
    }
    private void GetInitialMinAndMax()
    {
        int lowX = 0;
        int highX = 0;
        int lowY = 0;
        int highY = 0;
        foreach (CoordData rect in coordinates)
        {
            if (rect.rectPosition.x <= lowX)
                lowX = rect.rectPosition.x;
            else if (rect.rectPosition.x >= highX)
                highX = rect.rectPosition.x;
            if (rect.rectPosition.y <= lowY)
                lowY = rect.rectPosition.y;
            else if (rect.rectPosition.y >= highY)
                highY = rect.rectPosition.y;
        }
        oldMin.x = lowX;
        oldMin.y = lowY;
        oldMax.x = highX;
        oldMax.y = highY;
        offset = new Vector2Int((1 * Mathf.Abs(oldMin.x)), (1 * Mathf.Abs(oldMin.y)));
    }
    private void SetNewCoordinates()
    {
        foreach (CoordData coord in coordinates)
        {
            coord.rectPosition.x += offset.x;
            coord.rectPosition.y += offset.y;
        }
    }
    private void SetNewMinAndMax()
    {
        float x = (1 * Mathf.Abs(oldMin.x));
        float z = (1 * Mathf.Abs(oldMin.y));

        int newMaxX = (int)(oldMax.x + (x));
        int newMaxY = (int)(oldMax.y + (z));

        newMin.x = 0;
        newMin.y = 0;
        newMax.x = newMaxX + 1;
        newMax.y = newMaxY + 1;
    }
}
