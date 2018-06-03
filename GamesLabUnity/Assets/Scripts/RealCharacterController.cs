using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealCharacterController : MonoBehaviour {
    private GameObject grabbedObject;
    private GameObject inquiredObject;
    private List<GameObject> grabableObjects = new List<GameObject>();
    private List<GameObject> inquirableObjects = new List<GameObject>();
    private GameObject currentLight;
    private Transform grabParent;

	// Use this for initialization
	void Start ()
    {
        Data.realCharacter = gameObject;
        currentLight = GetFirstReachableLight();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Data.cam || !Data.cam.GetComponent<TransformCamera>().finished || !Data.cam.GetComponent<TransformCamera>().blendfinished)
            return;

        if (transform.position.y < 0.0f)
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);

        GetComponent<Rigidbody>().MovePosition(transform.position + Vector3.right * Input.GetAxis("CharacterHorizontal") * Time.deltaTime * Data.speed);
        Data.shadowCharacter.transform.position = new Vector3(transform.position.x, 0.16f, transform.position.y - 0.16f);
        Data.cam.transform.position = new Vector3(transform.position.x, Data.cam.transform.position.y, Data.cam.transform.position.z);

        if (currentLight)
        {
            currentLight.transform.Translate(Input.GetAxis("LightHorizontal") * Time.deltaTime * Data.speed, 0, 0);
            currentLight.transform.Translate(0, Input.GetAxis("LightVertical") * Time.deltaTime * Data.speed, 0);
            
            //Light
            if (Vector3.Distance(transform.position, currentLight.transform.position) > Data.characterReach)
                currentLight = GetNextReachableLight(currentLight);
        }

        //Worlds
        if (Input.GetButtonDown("Switch World") && Data.cam.GetComponent<TransformCamera>().blendfinished && Data.cam.GetComponent<TransformCamera>().finished && Data.lastWorldSwitch + Data.waitWorldSwitch < Time.time)
        {
            Data.cam.GetComponent<TransformCamera>().changePlane();
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
                grabbedObject.transform.parent = grabParent;
                grabbedObject = null;
            }
            else if (grabableObjects.Count > 0)
            {
                grabbedObject = Data.GetClosestGameObjectFromList(gameObject, grabableObjects);
                grabParent = grabbedObject.transform.parent;
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
                targetObject.SendMessage("Action", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                print("Cannot reach " + transform.name + ". Distance: " + Vector3.Distance(transform.position, targetObject.transform.position) + " > " + Data.characterReach);
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
        Physics.gravity = new Vector3(0, 0, -9.81f);
        /*foreach (GameObject g in Data.shadowObjects)
        {
            g.SendMessageUpwards("ToggleShadowCollider");
        }*/
        foreach (GameObject g in Data.shadowFloors)
        {
            g.SetActive(true);
        }
        Data.shadow = true;
        Data.shadowCharacter.transform.position = new Vector3(transform.position.x, Data.shadowCharacter.transform.position.y, transform.position.y - 0.3f);
        Data.shadowCharacter.GetComponent<ShadowCharacterController>().enabled = true;
        Data.shadowCharacter.GetComponent<Rigidbody>().isKinematic = false;
        Data.world.SetActive(false);
        gameObject.SetActive(false);
    }

    private GameObject GetFirstReachableLight()
    {
        if (Data.lights.Count <= 0)
            return null;

        Data.lights.Sort(CompareByDistanceToStart);
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

        Data.lights.Sort(CompareByDistanceToStart);
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

    private int CompareByDistanceToStart(GameObject g1, GameObject g2)
    {
        if (g1.transform.position.x > g2.transform.position.x)
            return 1;
        if (g1.transform.position.x < g2.transform.position.x)
            return -1;
        return 0;
    }
}
