using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public Material PlayerMaterial;


    private Texture2D rampTex;
	// Use this for initialization
	void Start ()
    {
        //rampTex = LoadByIO("RampTex.png", 32, 512);

        //PlayerMaterial.SetTexture("_ToonMap", rampTex);
	}


    #region static method
    /// <summary>
    /// 以IO方式进行加载
    /// </summary>
    public static Texture2D LoadByIO()
    {
        //创建文件读取流
        FileStream fileStream = new FileStream("RampTex.png", FileMode.Open, FileAccess.Read);
        fileStream.Seek(0, SeekOrigin.Begin);
        //创建文件长度缓冲区
        byte[] bytes = new byte[fileStream.Length];
        //读取文件
        fileStream.Read(bytes, 0, (int)fileStream.Length);
        //释放文件读取流
        fileStream.Close();
        fileStream.Dispose();
        fileStream = null;

        //创建Texture
        int width = 512;
        int height = 32;
        Texture2D texture = new Texture2D(width, height);
        texture.LoadImage(bytes);

        return texture;
    }

    public static Texture2D LoadByIO(string url)
    {
        //创建文件读取流
        FileStream fileStream = new FileStream(url, FileMode.Open, FileAccess.Read);
        fileStream.Seek(0, SeekOrigin.Begin);
        //创建文件长度缓冲区
        byte[] bytes = new byte[fileStream.Length];
        //读取文件
        fileStream.Read(bytes, 0, (int)fileStream.Length);
        //释放文件读取流
        fileStream.Close();
        fileStream.Dispose();
        fileStream = null;

        //创建Texture
        int width = 512;
        int height = 32;
        Texture2D texture = new Texture2D(width, height);
        texture.LoadImage(bytes);

        return texture;
    }

    public static Texture2D LoadByIO(string url, int height, int width)
    {
        //创建文件读取流
        FileStream fileStream = new FileStream(url, FileMode.Open, FileAccess.Read);
        fileStream.Seek(0, SeekOrigin.Begin);
        //创建文件长度缓冲区
        byte[] bytes = new byte[fileStream.Length];
        //读取文件
        fileStream.Read(bytes, 0, (int)fileStream.Length);
        //释放文件读取流
        fileStream.Close();
        fileStream.Dispose();
        fileStream = null;

        Texture2D texture = new Texture2D(width, height);
        texture.LoadImage(bytes);

        return texture;
    }

    #endregion
}
