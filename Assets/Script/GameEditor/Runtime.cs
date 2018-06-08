using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runtime : MonoBehaviour
{
    public MeshFilter MeshFilter;

    public GameObject V;

    private Mesh mesh;
	// Use this for initialization
	void Awake ()
    {
        mesh = MeshFilter.mesh;

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            var v = mesh.vertices[i];
            Instantiate(V, MeshFilter.transform.TransformPoint(v), Quaternion.identity);
        }

        foreach(var v in mesh.vertices)
        {
            //Debug.Log(v);
        }

        MeshFilter.mesh = mesh;
	}

    private void OnDrawGizmos()
    {
        foreach(var v in MeshFilter.mesh.vertices)
        {
            Gizmos.DrawSphere(MeshFilter.transform.TransformPoint(v), 0.04f);
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
