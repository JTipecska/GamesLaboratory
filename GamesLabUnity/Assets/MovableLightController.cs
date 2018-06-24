using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableLightController : MonoBehaviour {


    public float speed = 0.5f;
    public float distance = 2;
    public Vector3 direction = Vector3.right;
    private Vector3 startPosition;
    private float currSpeed;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        currSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        direction = Vector3.Normalize(direction);
        bool lightAtStart = Vector3.Normalize(transform.position - startPosition) == -direction;
        bool lightAtMaxDistance = Vector3.Distance(startPosition, transform.position) > distance;

        

        if (lightAtStart)
        {
            currSpeed = speed;

        }

        if (lightAtMaxDistance )
        {
            currSpeed = -speed;

        }

        transform.position += direction * Time.deltaTime * currSpeed;
    }


}

