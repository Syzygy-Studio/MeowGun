using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MapManager : MonoBehaviour
{
    public static MapInfo Map;

    public static Transform MapManagerTransform;

    public int width;
    public int height;

    private void Awake()
    {
        Map = new MapInfo("A", width, height);
        MapManagerTransform = transform;
    }

    private void Update()
    {
        Map.SetMapWidth(width);
        Map.SetMapHeight(height);
    }

    public static void CheckIsOverRange()
    {
        foreach (Transform t in MapManagerTransform)
        {
            if (t.transform.position.x > Map.GetMapWidth() - 1 || t.transform.position.y > Map.GetMapHeight() - 1) DestroyImmediate(t.gameObject);
        }
    }
}
