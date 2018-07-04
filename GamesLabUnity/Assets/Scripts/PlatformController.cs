using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    public float speed;
    public float distance;
    public Vector3 direction = Vector3.up;
    private Vector3 startPosition;
    //private float currSpeed;
    private float currDistance = 0;
    private float finishedMoving = 0;
    private bool setFinishedMoving = false; 

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
        //currSpeed = speed;
        direction = Vector3.Normalize(direction);
	}
	
	// Update is called once per frame
	void Update () {
        /*bool platformAtStart = Vector3.Normalize(transform.position - startPosition) == -direction;
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
            transform.position += direction * Time.deltaTime * currSpeed;*/

        currDistance += Time.deltaTime * speed;
        currDistance = Clamp(0, distance, currDistance);
        transform.position = startPosition + currDistance * direction;

        if (currDistance == 0 || currDistance == distance)
            speed = -speed;

    }

    void Action()
    {
        transform.position += new Vector3(0, 0.05f, 0);
        speed = 0.5f;
        distance = 4;
        Start();
    }

    float Clamp(float min, float max, float val)
    {
        return val < min ? min : max < val ? max : val;
    }
}
