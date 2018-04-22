using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static Camera MipMapCamera;

    private RenderTexture smallMap;
	// Use this for initialization
	void Start ()
    {
        MipMapCamera = GameObject.Find("MipMapCamera").GetComponent<Camera>();
        ResetMipMapCamera();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void ResetMipMapCamera()
    {
        MipMapCamera.transform.position = new Vector3((MapManager.Map.GetMapWidth() / 2) - 0.5f, (MapManager.Map.GetMapHeight() / 2) + 0.5f, -5);

        float s_1 = (MapManager.Map.GetMapWidth() + 1) / 4f;
        float s_2 = MapManager.Map.GetMapHeight() / 2f;

        MipMapCamera.orthographicSize = (s_1 >= s_2 ? s_1 : s_2) + 1;
    }
}
