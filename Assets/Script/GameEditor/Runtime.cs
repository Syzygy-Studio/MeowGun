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
            if (v.x < 0) v = new Vector3(-1, v.y, v.z);
            Instantiate(V, v, Quaternion.identity);
        }

        foreach(var v in mesh.vertices)
        {
            Debug.Log(v);
        }

        MeshFilter.mesh = mesh;
	}

    private void OnDrawGizmos()
    {
        foreach(var v in MeshFilter.mesh.vertices)
        {
            Gizmos.DrawSphere(v, 0.04f);
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
