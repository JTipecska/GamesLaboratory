using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveCamera : MonoBehaviour {


    public bool shadow = false;
    public bool finished = true;
    RaycastHit hit;
    float counter = 52f;
    int speed = 80;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	/* var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);*/
		

        if (shadow && !finished)
        {


                // this.gameObject.transform.Rotate(new Vector3(1.0f,0,0), Time.deltaTime,hit.point);
                transform.RotateAround(hit.point, new Vector3(1.0f, 0, 0), Time.deltaTime*speed);
            counter = counter + Time.deltaTime * speed;

        }
        
        if (counter >= 90)
        {
            finished = true;
        }
	}

    public void changePlane()
    {
        Physics.Raycast(transform.position, transform.forward, out hit);
        shadow = !shadow;

        finished = false;


    }
}
