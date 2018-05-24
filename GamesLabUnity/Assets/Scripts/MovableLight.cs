using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableLight : MonoBehaviour {
    public Vector2 limitX = new Vector2(0, 0);
    public Vector2 limitY = new Vector2(10, 10);


    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    void Update () {
        transform.position = new Vector3(Clamp(limitX.x, limitX.y, transform.position.x), Clamp(limitY.x, limitY.y, transform.position.y), transform.position.z);
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

    private float Clamp(float min, float max, float val)
    {
        return val < min ? min : (val < max ? val : max);
    }
}
