using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShadowScript : MonoBehaviour {

    public Light lightSrc;
    public GameObject plane;
    GameObject shadow;

    // Use this for initialization
    private void Start() { }

    // Update is called once per frame
    void Update() { }

    void GenerateShadowCollider()
    {
        if (shadow)
            Destroy(shadow);

        Vector3[] vertices = transform.GetComponent<MeshFilter>().sharedMesh.vertices;
        Mesh shadowMesh = new Mesh();
        List<Vector3> shadowVertices = new List<Vector3>();

        foreach (Vector3 vertex in vertices)
        {
            Vector3 currVertex = transform.position + vertex;

            RaycastHit hit;
            if (Physics.Raycast(new Ray(currVertex, currVertex - lightSrc.transform.position), out hit, lightSrc.range, LayerMask.GetMask(new string[] { "ShadowPlane" })))
                shadowVertices.Add(hit.point);
            else
                shadowVertices.Add(currVertex);
        }

        shadowMesh.SetVertices(shadowVertices);
        shadowMesh.SetTriangles(transform.GetComponent<MeshFilter>().mesh.triangles, 0);
        
        shadow = new GameObject("Shadow of " + transform.name);
        shadow.AddComponent<MeshCollider>();
        shadow.GetComponent<MeshCollider>().sharedMesh = shadowMesh;
    }
}