﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableLight : MonoBehaviour {
    public Material movePlaneMaterial;
    public float limitX = 1;
    public float limitZ = 1;

    GameObject movePlane;
    Vector3 anchor;


    // Use this for initialization
    void Start () {
        anchor = transform.position;

        if (movePlaneMaterial)
        {
            // Create MovePlane
            movePlane = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Quad));
            movePlane.name = "MovePlane of " + transform.name;
            movePlane.transform.parent = GameObject.Find("_Dynamic").transform;
            movePlane.transform.position = anchor;
            movePlane.transform.localScale = new Vector3(2 * limitX, 1, 2 * limitZ);
            movePlane.GetComponent<Renderer>().material = movePlaneMaterial;
            Destroy(movePlane.GetComponent<MeshCollider>());
            movePlane.SetActive(false);
        }
    }

    void OnEnable()
    {
        Data.lights.Add(gameObject);
    }

    void OnDisable()
    {
        Data.lights.Remove(gameObject);
    }

    // Update is called once per frame
    void Update () {
        transform.position = new Vector3(Clamp(anchor.x - limitX, anchor.x + limitX, transform.position.x), transform.position.y, Clamp(anchor.z - limitZ, anchor.z + limitZ, transform.position.z));
    }

    void Action()
    {
        Light light;
        if (GetComponent<Light>())
        {
            light = GetComponent<Light>();
        }
        else
        {
            light = transform.GetComponentInChildren<Light>();
        }

        if(light)
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
    }

    void ToggleLightMoveArea()
    {
        if(movePlane)
            movePlane.SetActive(!movePlane.activeSelf);
    }

    private float Clamp(float min, float max, float val)
    {
        return val < min ? min : (val < max ? val : max);
    }
}
