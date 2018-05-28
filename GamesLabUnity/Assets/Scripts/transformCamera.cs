using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]


public class transformCamera : MonoBehaviour {
    public Camera c;
    bool shadow = false;
    public bool finished = true;
    public bool blendfinished = true;
    RaycastHit hit;
    public float counter = 52f;
    public int speed = 80;
    public float transitduration = .75f;
    public float offset = 1.2f;
    Matrix4x4 pMatrix;

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
        Data.cam = gameObject;
        pMatrix = GetComponent<Camera>().projectionMatrix;

    }
	
	// Update is called once per frame
	void Update () {
        if (shadow && !finished)
        {
            if (counter + Time.deltaTime * speed > 90)
                transform.RotateAround(new Vector3(transform.position.x, 0, 0), new Vector3(1.0f, 0, 0), 90-counter);
            else
            // this.gameObject.transform.Rotate(new Vector3(1.0f,0,0), Time.deltaTime,hit.point);
                transform.RotateAround(new Vector3(transform.position.x,0,0 ), new Vector3(1.0f, 0, 0), Time.deltaTime * speed);
            counter = counter + Time.deltaTime * speed;

        }

        if (!finished && counter >= 90 && shadow)
        {
            
            finished = true;
            BlendToMatrix(c.projectionMatrix, transitduration);
        }
        if (!shadow && !finished && blendfinished)
        {
            if(counter - Time.deltaTime * speed < 52)
                transform.RotateAround(new Vector3(transform.position.x, 0, 0), new Vector3(1.0f, 0, 0), 52-counter);
            else
                transform.RotateAround(new Vector3(transform.position.x,0, 0), new Vector3(1.0f, 0, 0), -Time.deltaTime * speed);
            counter = counter - Time.deltaTime * speed;
        }
        if (!finished && counter <=52 && !shadow)
        {
            finished = true;
            
        }
    }


    public void changePlane()
    {
        if (!shadow && finished && blendfinished)
        {
            pMatrix = GetComponent<Camera>().projectionMatrix;
            //BlendToMatrix(c.projectionMatrix, .75f);
            //Physics.Raycast(transform.position, transform.forward, out hit);
            finished = false;
            shadow = true;
        }
        if (shadow && finished && blendfinished)
        {
            //BlendToMatrix(GetComponent<Camera>().projectionMatrix, .75f);
            //Physics.Raycast(transform.position, transform.forward, out hit);
            shadow = false;
            BlendToMatrix(pMatrix, transitduration);
            finished = false;

        }
        
        

        


    }
}
