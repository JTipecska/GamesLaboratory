﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShadowScript : MonoBehaviour {

    public Material shadowMat;

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
            if(!Physics.Raycast(g.transform.position, lightDir, out hit, light.range, LayerMask.GetMask(new string[] { "Default" })))
                continue;
            print(hit.collider.name + transform.name);

            // LightSource does not hit GameObject
            if (!hit.transform.Equals(transform))
                continue;

            // Calculate Intensity and compare with current max
            float intensityAtGameObject = Mathf.Abs(light.intensity/Vector3.Distance(hit.point,g.transform.position));
            print("Intensity: " + intensityAtGameObject);

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
            lastLightSrc = null;
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
        shadow.AddComponent<MeshFilter>();
        shadow.AddComponent<MeshRenderer>().material = shadowMat;
        shadow.AddComponent<MeshCollider>();

        StartCoroutine(CalculateShadowVerticesAndTriangles());
    }

    IEnumerator CalculateShadowVerticesAndTriangles()
    {
        if (!shadow)
            yield break;

        PickLightSource();
        // Something changed -> recalculate
        if (!lightSrc
            || (lightSrc.Equals(lastLightSrc)
            && lastLightPos.Equals(lightSrc.transform.position)
            && lastLightRot.Equals(lightSrc.transform.rotation)
            && lastPos.Equals(transform.position)))
            yield break;

        ////////////////////////////////////////
        Vector3[] vertices = transform.GetComponent<MeshFilter>().mesh.vertices;
        Mesh shadowMesh = new Mesh();
        List<Vector3> shadowVertices = new List<Vector3>();
        List<int> keptVertices = new List<int>();

        // calculate shadow position for each vertex
        for (int i = 0; i < vertices.Length; i++)
        {
            // Local -> World
            Vector3 currVertex = transform.TransformPoint(vertices[i]);
            Vector3 lightDir = currVertex - lightSrc.transform.position;

            RaycastHit hit;
            // Check if shadow hits Shadowplane 
            if (Physics.Raycast(new Ray(currVertex, lightDir), out hit, float.MaxValue, LayerMask.GetMask(new string[] { "ShadowPlane" })))
            {
                // Store shadow vertex
                shadowVertices.Add(hit.point + Vector3.up * 0.01f);
                ////keptVertices.Add(i);
            }
            else//
                shadowVertices.Add(currVertex);//
        }

        // set vertices (calculated) and triangles (same as in original mesh)
        shadowMesh.SetVertices(shadowVertices);
        shadowMesh.SetTriangles(transform.GetComponent<MeshFilter>().mesh.triangles, 0);//

        int[] oldTriangles = transform.GetComponent<MeshFilter>().mesh.triangles;
        List<int> newTriangles = new List<int>();
        for (int i = 0; i < oldTriangles.Length; i += 3)
        {
            int fstV = keptVertices.BinarySearch(oldTriangles[i]);
            int sndV = keptVertices.BinarySearch(oldTriangles[i+1]);
            int thdV = keptVertices.BinarySearch(oldTriangles[i+2]);
            if (fstV >= 0
                && sndV >= 0
                && thdV >= 0)
            {
                newTriangles.Add(fstV);
                newTriangles.Add(sndV);
                newTriangles.Add(thdV);
            }
        }
        ////shadowMesh.SetTriangles(newTriangles, 0);
        ////////////////////////////////////////////////

        shadow.GetComponent<MeshCollider>().sharedMesh = shadowMesh;
        shadow.GetComponent<MeshFilter>().mesh = shadowMesh;

        // Set last positions/rotations
        lastPos = transform.position;
        lastLightSrc = lightSrc;
        lastLightPos = lightSrc.transform.position;
        lastLightRot = lightSrc.transform.rotation;

        yield return 1;
    }
}