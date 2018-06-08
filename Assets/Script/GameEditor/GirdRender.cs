using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class GirdRender : MonoBehaviour
{
    public Material Material;
    public Material Material_Yellow;
    public bool IsActive = true;
    private MapInfo map;

	void Start ()
    {
        map = MapManager.Map;
	}

    // Update is called once per frame
    void Update()
    {
        if (IsActive)
        {
            Material.color = new Color(1, 1, 1, Mathf.Lerp(Material.color.a, 200f / 255f, Time.deltaTime * 3));
            Material_Yellow.color = new Color(1, 0.92f, 0.016f, Mathf.Lerp(Material_Yellow.color.a, 200f / 255f, Time.deltaTime * 3));
        }
        else
        {
            Material.color = new Color(1, 1, 1, Mathf.Lerp(Material.color.a, 0, Time.deltaTime * 3));
            Material_Yellow.color = new Color(1, 0.92f, 0.016f, Mathf.Lerp(Material_Yellow.color.a, 0, Time.deltaTime * 3));
        }
    }
    
    private void OnPostRender()
    {
        Material.SetPass(0);
        GL.wireframe = true;
        GL.Color(new Color(1, 1, 1, 1));
        GL.PushMatrix();
        GL.Begin(GL.LINES);
        {
            //渲染从左到右的横线，从前到后共五条，包含高度。
            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j <= map.GetMapHeight(); j++)
                {
                    GL.Vertex3(-0.5f, j, i - 0.5f);
                    GL.Vertex3(map.GetMapWidth() - 0.5f, j, i - 0.5f);
                }
            }
            //渲染从前到后的宽线，包含高度。
            for (int i = 0; i <= map.GetMapWidth(); i++)
            {
                for (int j = 0; j <= map.GetMapHeight(); j++)
                {
                    GL.Vertex3(i - 0.5f, j, -0.5f);
                    GL.Vertex3(i - 0.5f, j, 4.5f);
                }
            }
            //渲染从下往上的高度线条。
            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j <= map.GetMapWidth(); j++)
                {
                    GL.Vertex3(j - 0.5f, 0, i - 0.5f);
                    GL.Vertex3(j - 0.5f, map.GetMapHeight(), i - 0.5f);
                }
            }
        }
        GL.End();
        GL.PopMatrix();
        GL.wireframe = false;
    }

    private void OnRenderObject()
    {
        Material_Yellow.SetPass(0);
        GL.wireframe = true;
        GL.Color(Color.yellow);
        GL.PushMatrix();
        GL.Begin(GL.LINES);
        {
            GL.Vertex3(-0.5f, 0f, 2f);
            GL.Vertex3(MapManager.Map.GetMapWidth() - 0.5f, 0f, 2f);

            GL.Vertex3(-0.5f, MapManager.Map.GetMapHeight(), 2f);
            GL.Vertex3(MapManager.Map.GetMapWidth() - 0.5f, MapManager.Map.GetMapHeight(), 2f);

            GL.Vertex3(-0.5f, 0f, 2f);
            GL.Vertex3(-0.5f, MapManager.Map.GetMapHeight(), 2f);

            GL.Vertex3(MapManager.Map.GetMapWidth() - 0.5f, 0f, 2f);
            GL.Vertex3(MapManager.Map.GetMapWidth() - 0.5f, MapManager.Map.GetMapHeight(), 2f);
        }
        GL.End();
        GL.PopMatrix();
        GL.wireframe = false;
    }
}
