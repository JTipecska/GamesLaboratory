using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]


public class transformCamera : MonoBehaviour {
    public Camera c;
    bool shadow = false;
    bool finished = true;
    bool blendfinished = true;
    RaycastHit hit;
    public float counter = 52f;
    public int speed = 80;
    public float transitduration = .75f;

    public static Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float time)
    {
        Matrix4x4 ret = new Matrix4x4();
        for (int i = 0; i < 16; i++)
            ret[i] = Mathf.Lerp(from[i], to[i], time);
        return ret;
    }
 
    private IEnumerator LerpFromTo(Matrix4x4 src, Matrix4x4 dest, float duration)
    {
        blendfinished = false;
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            GetComponent<Camera>().projectionMatrix = MatrixLerp(src, dest, (Time.time - startTime) / duration);
            yield return 1;
        }
            GetComponent<Camera>().projectionMatrix = dest;
        blendfinished = true;
    }
 
    public Coroutine BlendToMatrix(Matrix4x4 targetMatrix, float duration)
    {
        StopAllCoroutines();
        return StartCoroutine(LerpFromTo(GetComponent<Camera>().projectionMatrix, targetMatrix, duration));
    } 

    public void blend()
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (shadow && !finished)
        {


            // this.gameObject.transform.Rotate(new Vector3(1.0f,0,0), Time.deltaTime,hit.point);
            transform.RotateAround(hit.point, new Vector3(1.0f, 0, 0), Time.deltaTime * speed);
            counter = counter + Time.deltaTime * speed;

        }

        if (!finished && counter >= 90 && shadow)
        {
            
            finished = true;
            BlendToMatrix(c.projectionMatrix, transitduration);
        }
        if (!shadow && !finished)
        {
            transform.RotateAround(hit.point, new Vector3(-1.0f, 0, 0), Time.deltaTime * speed);
            counter = counter - Time.deltaTime * speed;
        }
        if (!finished && counter <=52 && !shadow)
        {
            finished = true;
            BlendToMatrix(GetComponent<Camera>().projectionMatrix, transitduration);
        }
    }


    public void changePlane()
    {
        if (!shadow && finished && blendfinished)
        {
            //BlendToMatrix(c.projectionMatrix, .75f);
            Physics.Raycast(transform.position, transform.forward, out hit);
            finished = false;
            shadow = true;
        }
        if (shadow && finished && blendfinished)
        {
            //BlendToMatrix(GetComponent<Camera>().projectionMatrix, .75f);
            Physics.Raycast(transform.position, transform.forward, out hit);
            finished = false;
            shadow = false;
        }
        
        

        


    }
}
