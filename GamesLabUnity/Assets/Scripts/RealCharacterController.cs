using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealCharacterController : MonoBehaviour {
    public GameObject cam;
    private GameObject grabbedObject;
    private GameObject grabableObject;
    private GameObject currentLight;

	// Use this for initialization
	void Start ()
    {
        Data.realCharacter = gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.y < 0.5f)
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * Data.speed, 0, 0);
        Data.shadowCharacter.transform.position = new Vector3(transform.position.x, Data.shadowCharacter.transform.position.y, transform.position.y + 0.5f);
        cam.transform.position = new Vector3(transform.position.x, cam.transform.position.y, cam.transform.position.z);

        if (currentLight)
        {
            transform.Translate(Input.GetAxis("LightHorizontal") * Time.deltaTime * Data.speed, 0, 0);
            transform.Translate(0, Input.GetAxis("LightVertical") * Time.deltaTime * Data.speed, 0);
        }

        if (Input.GetButtonDown("Switch World") && Data.lastWorldSwitch + Data.waitWorldSwitch < Time.time)
        {
            ChangeToShadowWorld();
            return;
        }

        if(Input.GetButtonDown("Switch Light") && Data.reachableLights.Count > 0)
        {
            if (Data.reachableLights.IndexOf(currentLight) == -1)
                currentLight = Data.reachableLights[0];
            else
                currentLight = Data.reachableLights[(Data.reachableLights.IndexOf(currentLight) + 1) % Data.reachableLights.Count];
        }

        if (Input.GetButtonDown("Grab"))
        {
            if (grabbedObject)
            {
                grabbedObject.transform.parent = GameObject.Find("World/Terrain").transform;
                grabbedObject = null;
            }
            else if (grabableObject)
            {
                grabbedObject = grabableObject;
                grabbedObject.transform.parent = transform;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Contains("grab"))
        {
            grabableObject = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.GetInstanceID().Equals(grabableObject.GetInstanceID()))
        {
            grabableObject = null;
        }
    }

    private void ChangeToShadowWorld()
    {
        print("Shadow World");
        Data.lastWorldSwitch = Time.time;
        cam.SetActive(false);
        Data.shadowCharacter.GetComponent<ShadowCharacterController>().cam.SetActive(true);
        Physics.gravity = new Vector3(0, 0, -9.81f);
        foreach(GameObject g in Data.shadowObjects)
        {
            g.SendMessageUpwards("ToggleShadowCollider");
        }
        Data.shadowCharacter.transform.position = new Vector3(Data.shadowCharacter.transform.position.x, 0f, Data.shadowCharacter.transform.position.z);
        Data.shadowCharacter.GetComponent<ShadowCharacterController>().enabled = true;
        Data.shadowCharacter.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.SetActive(false);
    }
}
