using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShadowScript : MonoBehaviour {

    Light lightSrc;
    GameObject shadow;

    // Use this for initialization
    private void Start() {
        Data.shadowObjects.Add(gameObject);
        transform.parent = GameObject.Find("_Dynamic").transform;
    }

    // Update is called once per frame
    void Update()
    {
        PickLightSource();
        CalculateShadowVerticesAndTriangles();
    }

    void PickLightSource()
    {
        Light result = null;
        float maxIntensity = 0f;
        foreach(GameObject g in Data.lights)
        {
            Light light = g.GetComponent<Light>();
            RaycastHit hit;
            Vector3 lightDir = transform.position - g.transform.position;
            if(!Physics.Raycast(g.transform.position, lightDir, out hit, light.range))
                continue;

            //LightSource does not hit GameObject
            if (!hit.transform.Equals(g.transform) || Vector3.SignedAngle(g.transform.forward, lightDir, Vector3.Cross(g.transform.forward, lightDir)) > light.spotAngle)
                continue;

            float intensityAtGameObject = Vector3.Dot(hit.normal, lightDir) * light.intensity;

            if(intensityAtGameObject > maxIntensity)
            {
                maxIntensity = intensityAtGameObject;
                result = light;
            }
        }
        lightSrc = result;
    }

    void ToggleShadowCollider()
    {
        if (shadow)
        {
            print("Changing to OverWorld");
            Destroy(shadow);
            transform.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            return;
        }

        print("Changing to ShadowWorld");

        transform.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;


        shadow = new GameObject("Shadow of " + transform.name);
        shadow.layer = LayerMask.NameToLayer("ShadowWorld");
        shadow.AddComponent<MeshCollider>();

        CalculateShadowVerticesAndTriangles();
    }

    void CalculateShadowVerticesAndTriangles()
    {
        if (!shadow)
            return;

        Vector3[] vertices = transform.GetComponent<MeshFilter>().sharedMesh.vertices;
        Mesh shadowMesh = new Mesh();
        List<Vector3> shadowVertices = new List<Vector3>();

        // calculated shadow position for each vertex
        foreach (Vector3 vertex in vertices)
        {
            // Local -> World
            Vector3 currVertex = transform.position + vertex;

            RaycastHit hit;
            // Check if shadow hits Shadowplane 
            if (Physics.Raycast(new Ray(currVertex, currVertex - lightSrc.transform.position), out hit, lightSrc.range, LayerMask.GetMask(new string[] { "ShadowPlane" })))
                // Store shadow vertex
                shadowVertices.Add(hit.point);
            else
                // Store actual vertex (behind shadowplane)
                shadowVertices.Add(currVertex);
        }

        // set vertices (calculated) and triangles (same as in original vertex)
        shadowMesh.SetVertices(shadowVertices);
        shadowMesh.SetTriangles(transform.GetComponent<MeshFilter>().mesh.triangles, 0);
        
        shadow.GetComponent<MeshCollider>().sharedMesh = shadowMesh;
    }
}