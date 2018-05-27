using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableLight : MonoBehaviour {
    public Material movePlaneMaterial;
    public float limitX = 10;
    public float limitY = 10;

    GameObject movePlane;
    Vector3 anchor;


    // Use this for initialization
    void Start () {
        anchor = transform.position;

        // Create MovePlane
        movePlane = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Quad));
        movePlane.transform.position = anchor;
        movePlane.transform.localScale = new Vector3(2 * limitX, 2 * limitY, 1);
        movePlane.GetComponent<Renderer>().material = movePlaneMaterial;
        Destroy(movePlane.GetComponent<MeshCollider>());
        movePlane.SetActive(false);
	}

    // Update is called once per frame
    void Update () {
        transform.position = new Vector3(Clamp(anchor.x - limitX, anchor.x + limitX, transform.position.x), Clamp(anchor.y - limitY, anchor.y + limitY, transform.position.y), transform.position.z);
    }

    void Action()
    {
        GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
        if (GetComponent<Light>().enabled)
        {
            Data.lights.Add(gameObject);
        }
        else
        {
            Data.lights.Remove(gameObject);
        }
    }

    void ToggleLightMoveArea()
    {
        if (movePlane.activeSelf)
            movePlane.SetActive(false);
        else
            movePlane.SetActive(true);
    }

    private float Clamp(float min, float max, float val)
    {
        return val < min ? min : (val < max ? val : max);
    }
}
