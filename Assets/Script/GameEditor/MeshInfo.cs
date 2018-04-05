using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshInfo
{
    public Mesh Mesh { get; private set; }

    public List<Vector3> vertices;
    public List<int> triangles;
    public List<Vector2> uvs;
    public List<Vector3> normals;
    public List<Vector4> tangents;
    public Vector3 size, center;

    public MeshInfo()
    {
        Mesh = new Mesh();

        vertices = new List<Vector3>();
        triangles = new List<int>();
        uvs = new List<Vector2>();
        normals = new List<Vector3>();
        tangents = new List<Vector4>();
        size = center = Vector3.zero;
    }

    public MeshInfo(Mesh mesh)
    {
        Mesh = mesh;
    }

    
}
