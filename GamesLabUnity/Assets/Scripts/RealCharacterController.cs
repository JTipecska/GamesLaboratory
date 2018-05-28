﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealCharacterController : MonoBehaviour {
    public GameObject cam;
    private GameObject grabbedObject;
    private GameObject inquiredObject;
    private List<GameObject> grabableObjects = new List<GameObject>();
    private List<GameObject> inquirableObjects = new List<GameObject>();
    private GameObject currentLight;

	// Use this for initialization
	void Start ()
    {
        Data.realCharacter = gameObject;
        currentLight = GetFirstReachableLight();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0.5f)
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

        GetComponent<Rigidbody>().MovePosition(new Vector3(Input.GetAxis("CharacterHorizontal") * Time.deltaTime * Data.speed, 0, 0));
        Data.shadowCharacter.transform.position = new Vector3(transform.position.x, Data.shadowCharacter.transform.position.y, transform.position.y + 0.5f);
        cam.transform.position = new Vector3(transform.position.x, cam.transform.position.y, cam.transform.position.z);

        if (currentLight)
        {
            transform.Translate(Input.GetAxis("LightHorizontal") * Time.deltaTime * Data.speed, 0, 0);
            transform.Translate(0, Input.GetAxis("LightVertical") * Time.deltaTime * Data.speed, 0);
            
            //Light
            if (Vector3.Distance(transform.position, currentLight.transform.position) > Data.characterReach)
                currentLight = GetNextReachableLight(currentLight);
        }

        //Worlds
        if (Input.GetButtonDown("Switch World") && Data.lastWorldSwitch + Data.waitWorldSwitch < Time.time)
        {
            ChangeToShadowWorld();
            return;
        }

        if (Input.GetButtonDown("Switch Light"))
        {
            if(Data.lights.Count > 0)
            {
                if (currentLight)
                    currentLight = GetNextReachableLight(currentLight);
                else
                    currentLight = GetFirstReachableLight();
            }
        }

        //Grab
        if (Input.GetButtonDown("Grab"))
        {
            if (grabbedObject)
            {
                grabbedObject.transform.parent = GameObject.Find("World/Terrain").transform;
                grabbedObject = null;
            }
            else if (grabableObjects.Count > 0)
            {
                grabbedObject = Data.GetClosestGameObjectFromList(gameObject, grabableObjects);
                grabbedObject.transform.parent = transform;
            }
        }

        //Inquire
        if (Input.GetButtonDown("Inquire") && !inquiredObject && inquirableObjects.Count > 0)
        {
            GameObject targetObject = Data.GetClosestGameObjectFromList(gameObject, Data.inquirableObjects);
            if(Vector3.Distance(transform.position, targetObject.transform.position) <= Data.characterReach)
            {
                inquiredObject = targetObject;
                inquiredObject.SendMessage("Inquire", true, SendMessageOptions.DontRequireReceiver);
            }
        } else if (inquiredObject && (Input.GetButtonUp("Inquire") || Vector3.Distance(transform.position, inquiredObject.transform.position) > Data.characterReach))
        {
            inquiredObject.SendMessage("Inquire", false, SendMessageOptions.DontRequireReceiver);
            inquiredObject = null;
        }

        //Take Action
        if (Input.GetButtonDown("Action"))
        {
            GameObject targetObject = Data.GetClosestGameObjectFromList(gameObject, Data.interactableObjects);
            if (Vector3.Distance(transform.position, targetObject.transform.position) <= Data.characterReach)
            {
                targetObject.SendMessage("Action", true, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.ToLower().Contains("grabable"))
        {
            grabableObjects.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag.ToLower().Contains("grabable"))
        {
            grabableObjects.Remove(collision.gameObject);
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

    private GameObject GetFirstReachableLight()
    {
        if (Data.lights.Count <= 0)
            return null;

        Data.lights.Sort(CompareByDistanceToPlayer);
        for (int i = 0; i < Data.lights.Count; i++)
        {
            float currDist = Vector3.Distance(transform.position, Data.lights[i].transform.position);
            if (currDist > Data.characterReach)
            {
                if (i == 0)
                    return null;
                else
                    return Data.lights[i - 1];
            }
        }

        // No reachable Lights
        return null;
    }

    private GameObject GetNextReachableLight(GameObject currLight)
    {
        if (Data.lights.Count <= 0)
            return null;

        Data.lights.Sort(CompareByDistanceToPlayer);
        if (currLight)
        {
            int idx = Data.lights.IndexOf(currLight);
            if (idx == Data.lights.Count - 1 
                || Vector3.Distance(transform.position, Data.lights[idx + 1].transform.position) > Data.characterReach)
                return GetFirstReachableLight();
            else
                return Data.lights[idx + 1];

        }

        // No reachable Lights
        return null;
    }

    private int CompareByDistanceToPlayer(GameObject g1, GameObject g2)
    {
        if (g1.transform.position.x > g2.transform.position.x)
            return 1;
        if (g1.transform.position.x < g2.transform.position.x)
            return -1;
        return 0;
    }
}
