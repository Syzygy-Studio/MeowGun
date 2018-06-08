using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo
{
    private string name;
    private int width;
    private int height;

    public MapInfo(string name, int width, int height)
    {
        this.name = name;
        this.width = width;
        this.height = height;
    }

    public string GetMapName() => name;
    public int GetMapWidth() => width;
    public int GetMapHeight() => height;

    public Vector3 GetCollisionSize() => new Vector3(width, height, 0);

    public Vector3 GetCenter()
    {
        float w, h;
        if (width % 2 == 0) w = width / 2 - 0.5f;
        else w = width / 2;

        if (height % 2 == 0) h = height / 2;
        else h = height / 2 + 0.5f;

        return new Vector3(w, h, 0);
    }

    public void SetMapName(string value)
    {
        name = value;
    }
    public void SetMapWidth(int value)
    {
        width = value;
        UIManager.ResetMipMapCamera();
        MapManager.CheckIsOverRange();
    }
    public void SetMapHeight(int value)
    {
        height = value;
        UIManager.ResetMipMapCamera();
        MapManager.CheckIsOverRange();
    }
}
