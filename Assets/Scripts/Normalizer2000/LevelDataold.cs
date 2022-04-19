
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelDataold {
    public string version;
    public string title;
    public string story;
    public Rect[] rects;
    public Door[] doors;
    public Notes[] notes;
    public Colmumns[] colmumns;
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
        public Vector2 position;
        public Vector2 size;
        public bool rotunda = false;
    }
    [System.Serializable]
    public class Door
    {
        public Vector2 position;
        public Vector2 dir;
        public int type = 0;
    }

    [System.Serializable]
    public class Notes
    {
        public string text;
        public string reference;
        public Vector2 pos;
    }

    [System.Serializable]
    public class Colmumns
    {
        //empty
    }
    [System.Serializable]
    public class Water
    {
        //empty
    }
}