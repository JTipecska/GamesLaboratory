using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]


public class TransformCamera : MonoBehaviour {
    public Camera c;
    bool shadow = false;
    public bool finished = true;
    public bool blendfinished = true;
    bool called = true;
    RaycastHit hit;
    public float counter = 43f;
    public int speed = 80;
    public float transitduration = .75f;
    public float offset = 1.2f;
    float saveY;
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
        float height = Data.realCharacter.transform.position.y > 3.8f ? 4f : 0;
        if (shadow && !finished)
        {
            if (counter + Time.deltaTime * speed > 90)
            {
                transform.RotateAround(new Vector3(transform.position.x, height, 0), new Vector3(1.0f, 0, 0), 90 - counter);
                transform.position = new Vector3(transform.position.x,transform.position.y,0);
                transform.forward = new Vector3(0, -1, 0);
            }

            else
            // this.gameObject.transform.Rotate(new Vector3(1.0f,0,0), Time.deltaTime,hit.point);
                transform.RotateAround(new Vector3(transform.position.x,height,0 ), new Vector3(1.0f, 0, 0), Time.deltaTime * speed);
            counter = counter + Time.deltaTime * speed;

        }

        if (!finished && counter >= 90 && shadow)
        {
            
            finished = true;
            saveY = Data.cam.transform.position.y;
            BlendToMatrix(c.projectionMatrix, transitduration);
        }
        if (!shadow && !finished && blendfinished)
        {
            if(counter - Time.deltaTime * speed < 52)
                transform.RotateAround(new Vector3(transform.position.x, height, 0), new Vector3(1.0f, 0, 0), 52-counter);
            else
                transform.RotateAround(new Vector3(transform.position.x,height, 0), new Vector3(1.0f, 0, 0), -Time.deltaTime * speed);
            counter = counter - Time.deltaTime * speed;
        }
        if (!finished && counter <=52 && !shadow)
        {
            Data.shadowCharacter.GetComponent<Collider>().isTrigger = true;
            finished = true;
            
        }
        if (finished && blendfinished && !called)
        {
            if (shadow)
            {
                Data.realCharacter.SendMessage("ChangeToShadowWorld");
                this.transform.parent.GetComponentInChildren<OutlineShader>().addOutline();
                Data.timer.SetActive(true);
                Data.cam.transform.position =  new Vector3(Data.cam.transform.position.x, saveY, Data.cam.transform.position.z);
            }
            called = true;
        }
    }


    public void changePlane()
    {
        if (finished && blendfinished)
        {
            Data.realCharacter.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Data.shadowCharacter.GetComponent<Rigidbody>().velocity = Vector3.zero;

            if (shadow)
            {
                //BlendToMatrix(GetComponent<Camera>().projectionMatrix, .75f);
                //Physics.Raycast(transform.position, transform.forward, out hit);
                Data.timer.SetActive(false);
                this.transform.parent.GetComponentInChildren<OutlineShader>().removeOutline();
                shadow = false;
                BlendToMatrix(pMatrix, transitduration);
                finished = false;
                Data.shadowCharacter.SendMessage("ChangeToRealWorld");

            }
            else
            {
                pMatrix = GetComponent<Camera>().projectionMatrix;
                //BlendToMatrix(c.projectionMatrix, .75f);
                //Physics.Raycast(transform.position, transform.forward, out hit);
                finished = false;
                shadow = true;
                called = false;
            }

            
        }

        


    }
}
