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
        bool lightAtStart = !OneDirection(direction, Vector3.Normalize(transform.position - startPosition));
        bool lightAtMaxDistance = Vector3.Distance(startPosition, transform.position) > distance;

        if (lightAtStart)
        {
            currSpeed = speed;

        }
        else if (lightAtMaxDistance )
        {
            currSpeed = -speed;

        }

        transform.position += direction * Time.deltaTime * currSpeed;
    }

    bool OneDirection(Vector3 dir1, Vector3 dir2)
    {
        float length = Vector3.SqrMagnitude(dir1+dir2);
        return length > Vector3.SqrMagnitude(dir1);
    }
}

