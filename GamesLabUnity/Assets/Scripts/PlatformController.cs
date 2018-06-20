using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    public float speed;
    public float distance;
    public Vector3 direction = Vector3.up;
    private Vector3 startPosition;
    private float currSpeed;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
        currSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {
        direction = Vector3.Normalize(direction);
        bool platformAtStart = Vector3.Normalize(transform.position - startPosition) == -direction;
        bool platformAtMaxDistance = Vector3.Distance(startPosition, transform.position) > distance;
        if (platformAtStart)
            currSpeed = speed;
        if (platformAtMaxDistance)
            currSpeed = -speed;
        print("platformAtStart: " + platformAtStart + ", platformAtMaxDistance: " + platformAtMaxDistance);

        transform.position += direction * Time.deltaTime * currSpeed;
    }
}
