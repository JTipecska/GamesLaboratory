﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    public float speed;
    public float distance;
    public Vector3 direction = Vector3.up;
    private Vector3 startPosition;
    private float currSpeed;
    private float finishedMoving = 0;
    private bool setFinishedMoving = false; 

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
   
        if (platformAtStart && !setFinishedMoving)
        {
            finishedMoving = Time.time;
            setFinishedMoving = true;
        }

        if (platformAtMaxDistance && !setFinishedMoving)
        {
            finishedMoving = Time.time;
            setFinishedMoving = true;
        }

        if (platformAtStart && finishedMoving + 2 <= Time.time)
        {
            currSpeed = speed;
            finishedMoving = 0;
            setFinishedMoving = false;
        }

        if (platformAtMaxDistance && finishedMoving + 2 <= Time.time)
        {
            currSpeed = -speed;
            finishedMoving = 0;
            setFinishedMoving = false;
        }


        if (finishedMoving == 0)
            transform.position += direction * Time.deltaTime * currSpeed;
    }

    void Action()
    {
        speed = 0.5f;
        distance = 4;
        Start();
    }
}
