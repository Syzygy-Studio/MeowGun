using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo
{
    private string name { get; set; }
    private int width { get; set; }
    private int height { get; set; }

    public MapInfo(string name,int width, int height)
    {
        this.name = name;
        this.width = width;
        this.height = height;
    }

    public string GetMapName() => name;
    public int GetMapWidth() => width;
    public int GetMapHeight() => height;

    public void SetMapName(string value) => name = value;
    public void SetMapWidth(int value) => width = value;
    public void SetMapHeight(int value) => height = value;
}
