using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData {

    public string version;
    public string title;
    public string story;
    public Rect[] rects;
    public Door[] doors;
    public Notes[] notes;
    public Columns[] columns;
    public Water[] water;

    [System.Serializable]
    public class Version
    {
        public string version;
    }
    [System.Serializable]
    public class Title
    {
        public string title;
    }
    [System.Serializable]
    public class Story
    {
        public string story;
    }

    [System.Serializable]
    public class Rect
    {
        public string name = "Rect";
        public float x;
        public float y;
        public float w;
        public float h;
        public bool rotunda = false;
    }
    [System.Serializable]
    public class Door
    {
        public float x = 0;
        public float y = 0;
        public Dir dir;
        public int type = 0;
    }
    [System.Serializable]
    public class Dir
    {
        public float x = 0;
        public float y = 0;
    }
    [System.Serializable]
    public class Notes
    {
        public string text;
        public string reference;
        public Pos pos;
    }
    [System.Serializable]
    public class Pos
    {
        public float x;
        public float y;
    }
    [System.Serializable]
    public class Columns
    {
        //empty
    }
    [System.Serializable]
    public class Water
    {
        //empty
    }

}
