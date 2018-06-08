using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static Camera MipMapCamera;

    private RenderTexture smallMap;
	// Use this for initialization
	void Awake ()
    {
        MipMapCamera = GameObject.Find("MipMapCamera").GetComponent<Camera>();
        ResetMipMapCamera();
	}
	
    public static void ResetMipMapCamera()
    {
        MipMapCamera.transform.position = new Vector3(MapManager.Map.GetCenter().x, MapManager.Map.GetCenter().y, -5);

        float s_1 = (MapManager.Map.GetMapWidth() + 1) / 4f;
        float s_2 = MapManager.Map.GetMapHeight() / 2f;

        MipMapCamera.orthographicSize = (s_1 >= s_2 ? s_1 : s_2) + 1;
    }
}
