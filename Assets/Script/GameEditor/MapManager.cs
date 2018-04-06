using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapInfo Map;

    private void Awake()
    {
        Map = new MapInfo("A", 30, 10);
    }
}
