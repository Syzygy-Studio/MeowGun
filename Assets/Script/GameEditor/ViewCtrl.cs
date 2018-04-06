using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCtrl : MonoBehaviour
{
    [Range(0, 1)]
    public float lerp;

    public float nearClip = -0.1f;
    public float size = 3f;

    private float limitXMin;
    private float limitXMax;
    private float limitYMin;
    private float limitYMax;

    private Vector3 oldMousePos;

    private void Start()
    {
        limitXMax = MapManager.Map.GetMapWidth();
        limitYMax = MapManager.Map.GetMapHeight();

        oldMousePos = Input.mousePosition;
    }

    private void Update()
    {
        if (lerp == 0)
        {
            transform.position = Vector3.Lerp(transform.position,
                                              new Vector3(Mathf.Clamp(transform.position.x, limitXMin, limitXMax),
                                                          Mathf.Clamp(transform.position.y, limitYMin, limitYMax),
                                                          Mathf.Clamp(transform.position.z + Input.GetAxis("Mouse ScrollWheel") * 20, -20f, -2f)),
                                              Time.deltaTime * 5);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position,
                                              new Vector3(Mathf.Clamp(transform.position.x, limitXMin, limitXMax),
                                                          Mathf.Clamp(transform.position.y, limitYMin, limitYMax),
                                                          Mathf.Clamp(transform.position.z + Input.GetAxis("Mouse ScrollWheel") * 20, -20f, -2f)),
                                              Time.deltaTime * 5);
            size = Mathf.Clamp(size - Input.GetAxis("Mouse ScrollWheel"), 1f, 10f);
        }
        convertPerspectiveOrOrtho();
        mouseMiddleKeyDrag();
    }

    private void mouseMiddleKeyDrag()
    {
        if(Input.GetKey(KeyCode.Mouse2))
        {
            Vector3 offset = Input.mousePosition - oldMousePos;
            transform.position -= offset / (800 + transform.position.z * 20);
        }
        oldMousePos = Input.mousePosition;
    }

    /// <summary>
    /// 摄像机在透视投影额正交投影间的差值变换。
    /// </summary>
    private void convertPerspectiveOrOrtho()
    {
        var ratio = Screen.width / (float)Screen.height;

        var a = Matrix4x4.Perspective(45, ratio, 0.1f, 5000f);
        var b = Matrix4x4.Ortho(-size * ratio, size * ratio, -size, size, nearClip, 5000f);

        Camera.main.projectionMatrix = Lerp(a, b, lerp);
    }

    /// <summary>
    /// 矩阵差值。
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="lerp"></param>
    /// <returns></returns>
    private Matrix4x4 Lerp(Matrix4x4 a, Matrix4x4 b, float lerp)
    {
        var result = new Matrix4x4();
        result.SetRow(0, Vector4.Lerp(a.GetRow(0), b.GetRow(0), lerp));
        result.SetRow(1, Vector4.Lerp(a.GetRow(1), b.GetRow(1), lerp));
        result.SetRow(2, Vector4.Lerp(a.GetRow(2), b.GetRow(2), lerp));
        result.SetRow(3, Vector4.Lerp(a.GetRow(3), b.GetRow(3), lerp));

        return result;
    }
}
