using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//The the some coordinates are created from the JSON file thats parsed through the LevelData class
public class JsonLoader : MonoBehaviour
{
    private LevelData levelData;
    private Vector2 oldMin;
    private Vector2 oldMax;
    private Vector2 newMin;
    private Vector2 newMax;
    private Vector2 offset;

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
            float x = rect.x;
            float y = rect.y;
            float xLoop = rect.w;
            float yLoop = rect.h;
            for (float i = 0; i < xLoop; i++)
            {
                for (float ii = 0; ii < yLoop; ii++)
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
        float lowX = 0;
        float highX = 0;
        float lowY = 0;
        float highY = 0;
        foreach (CoordData rect in coordinates)
        {
            if (rect.positon.x <= lowX)
                lowX = rect.positon.x;
            else if (rect.positon.x >= highX)
                highX = rect.positon.x;
            if (rect.positon.y <= lowY)
                lowY = rect.positon.y;
            else if (rect.positon.y >= highY)
                highY = rect.positon.y;
        }
        oldMin.x = lowX;
        oldMin.y = lowY;
        oldMax.x = highX;
        oldMax.y = highY;
        offset = new Vector2((1 * Mathf.Abs(oldMin.x)), (1 * Mathf.Abs(oldMin.y)));
    }
    private void SetNewCoordinates()
    {
        foreach (CoordData coord in coordinates)
        {
            coord.positon.x += offset.x;
            coord.positon.y += offset.y;
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
