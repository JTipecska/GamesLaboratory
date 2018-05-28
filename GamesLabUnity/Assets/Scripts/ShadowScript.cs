using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShadowScript : MonoBehaviour {

    Light lightSrc;
    GameObject shadow;
    Light lastLightSrc;
    Vector3 lastLightPos;
    Quaternion lastLightRot;
    Vector3 lastPos;

    // Use this for initialization
    private void Start() {
        Data.shadowObjects.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateShadowVerticesAndTriangles();
    }

    void PickLightSource()
    {
        Light result = null;
        float maxIntensity = 0f;
        foreach(GameObject g in Data.lights.Union<GameObject>(Data.staticLights).ToList<GameObject>())
        {
            Light light = g.GetComponent<Light>();
            RaycastHit hit;
            Vector3 lightDir = transform.position - g.transform.position;
            if(!Physics.Raycast(g.transform.position, lightDir, out hit, light.range))
                continue;

            // LightSource does not hit GameObject
            if (!hit.transform.Equals(transform))
                continue;

            // Calculate Intensity and compare with current max
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


        shadow = new GameObject("Shadow of " + transform.name)
        {
            layer = LayerMask.NameToLayer("ShadowWorld")
        };
        shadow.transform.parent = GameObject.Find("_Dynamic").transform;
        shadow.AddComponent<MeshCollider>();

        CalculateShadowVerticesAndTriangles();
    }

    void CalculateShadowVerticesAndTriangles()
    {
        if (!shadow)
            return;

        PickLightSource();
        // Something changed -> recalculate
        if(!lightSrc
            || (lightSrc.Equals(lastLightSrc)
            && lastLightPos.Equals(lightSrc.transform.position)
            && lastLightRot.Equals(lightSrc.transform.rotation)
            && lastPos.Equals(transform.position)))
                return;

        Vector3[] vertices = transform.GetComponent<MeshFilter>().sharedMesh.vertices;
        Mesh shadowMesh = new Mesh();
        List<Vector3> shadowVertices = new List<Vector3>();

        // calculate shadow position for each vertex
        foreach (Vector3 vertex in vertices)
        {
            // Local -> World
            Vector3 currVertex = transform.position + vertex;

            RaycastHit hit;
            print(lightSrc == null);
            // Check if shadow hits Shadowplane 
            if (Physics.Raycast(new Ray(currVertex, currVertex - lightSrc.transform.position), out hit, lightSrc.range - Vector3.Distance(currVertex, lightSrc.transform.position), LayerMask.GetMask(new string[] { "ShadowPlane" })))
                // Store shadow vertex
                shadowVertices.Add(hit.point);
            else
                // Store actual vertex (behind shadowplane)
                shadowVertices.Add(currVertex);
        }

        // set vertices (calculated) and triangles (same as in original mesh)
        shadowMesh.SetVertices(shadowVertices);
        shadowMesh.SetTriangles(transform.GetComponent<MeshFilter>().mesh.triangles, 0);
        
        shadow.GetComponent<MeshCollider>().sharedMesh = shadowMesh;

        // Set last positions/rotations
        lastPos = transform.position;
        lastLightSrc = lightSrc;
        lastLightPos = lightSrc.transform.position;
        lastLightRot = lightSrc.transform.rotation;

    }
}