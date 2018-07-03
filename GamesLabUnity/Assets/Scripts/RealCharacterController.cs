﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealCharacterController : MonoBehaviour {
    private GameObject grabbedObject;
    private GameObject inquiredObject;
    private List<GameObject> grabableObjects = new List<GameObject>();
    private List<GameObject> inquirableObjects = new List<GameObject>();
    private GameObject currentLightController;
    private Transform grabParent;
    public float lastShadowPlaneHeight;


    private Animator anim;
    public Animator animShadow;
    public Transform rigidShadow;

    // Use this for initialization
    void Start ()
    {
        Data.realCharacter = gameObject;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + 0.16f * Vector3.up, Vector3.down, out hit, float.MaxValue, LayerMask.GetMask("ShadowPlane")))
        {
            lastShadowPlaneHeight = hit.collider.transform.position.y;
           // print("ShadowPlane \"" + hit.collider.name + "\": " + lastShadowPlaneHeight + ", Character Feet: " + transform.position);

        }

        if (!Data.cam || !Data.cam.GetComponent<TransformCamera>().finished || !Data.cam.GetComponent<TransformCamera>().blendfinished)
            return;


        if (!GUIController.GetMenuActive())
        {
            //if (transform.position.y < lastShadowPlaneHeight - 0.1f)
            //     transform.position = new Vector3(transform.position.x, lastShadowPlaneHeight, transform.position.z);

            float characterMovement = Input.GetAxis("CharacterHorizontal");

            if (characterMovement > 0.001f || characterMovement < -0.001f)
            {
                if (anim.GetBool("LookingForward") && characterMovement < -0.001f)
                {
                    GetComponent<Transform>().Rotate(0, 180, 0);
                    rigidShadow.Rotate(0, 180, 0); 
                    anim.SetBool("LookingForward", false);
                    animShadow.SetBool("LookingForward", false);
                }
                else if (!anim.GetBool("LookingForward") && characterMovement > 0.001f)
                {
                    GetComponent<Transform>().Rotate( 0, -180, 0);
                    rigidShadow.Rotate(0, -180, 0);
                    anim.SetBool("LookingForward", true);
                    animShadow.SetBool("LookingForward", true);
                }
                else
                {
                    animShadow.SetTrigger("StartWalking");
                    animShadow.ResetTrigger("StopWalking");
                    anim.SetTrigger("StartWalking");
                    anim.ResetTrigger("StopWalking");
                }
            }
            else
            {
                animShadow.SetTrigger("StopWalking");
                animShadow.ResetTrigger("StartWalking");
                anim.SetTrigger("StopWalking");
                anim.ResetTrigger("StartWalking");
            }
            Debug.Log((Vector3.right * characterMovement * Data.speed).ToString());
            Rigidbody rig = GetComponent<Rigidbody>();
            rig.velocity = new Vector3(characterMovement * Data.speed, rig.velocity.y, rig.velocity.z);

            if (!Data.shadow)
            {
                Data.shadowCharacter.transform.position = new Vector3(transform.position.x, 0.16f + lastShadowPlaneHeight, transform.position.y - lastShadowPlaneHeight - 1.3f);
                if (!Data.lockCamera)
                {
                    if (Data.onElevator)
                    {
                        Data.cam.transform.position = new Vector3(transform.position.x, 3.45f + Data.realCharacter.transform.position.y, Data.cam.transform.position.z);
                    }
                    else
                    {
                        int floor = (int)Data.realCharacter.transform.position.y / 4;
                        Data.cam.transform.position = new Vector3(transform.position.x, 3.589873f + 4 * floor, Data.cam.transform.position.z);
                    }
                }
               
            }
            //Data.cam.transform.position = new Vector3(transform.position.x, Data.cam.transform.position.y, Data.cam.transform.position.z);

            if (currentLightController)
            {
                currentLightController.GetComponent<LightController>().controlledLight.transform.Translate(Input.GetAxis("LightHorizontal") * Time.deltaTime * Data.speed, 0, 0);
                currentLightController.GetComponent<LightController>().controlledLight.transform.Translate(0, 0, -Input.GetAxis("LightVertical") * Time.deltaTime * Data.speed);

                //Light
                if (Vector3.Distance(transform.position, currentLightController.transform.position) > Data.characterReach)
                    currentLightController.SendMessage("Action");
            }

            //Worlds
            if (Input.GetButtonDown("Switch World") && Data.cam.GetComponent<TransformCamera>().blendfinished && Data.cam.GetComponent<TransformCamera>().finished && Data.lastWorldSwitch + Data.waitWorldSwitch < Time.time && CollisionShadow.canChange)
            {
                Data.shadowCharacter.GetComponent<Collider>().isTrigger = false;
                //Data.world.GetComponentInChildren<InitPuzzles>().changeTrigger();
                if (!Data.shadow || CollisionReal.canChange)
                    Data.cam.GetComponent<TransformCamera>().changePlane();
                return;
            }

            //if (Input.GetButtonDown("Switch Light"))
            //{
            //    if (Data.lights.Count > 0)
            //    {
            //        if (currentLightController)
            //            currentLightController = GetNextReachableLight(currentLightController);
            //        else
            //            currentLightController = GetFirstReachableLight();
            //        if (currentLightController)
            //            print("Current Light: " + currentLightController.name);
            //        else
            //            print(Vector3.Distance(transform.position, Data.lights[0].transform.position));
            //    }
            //    else
            //    {
            //        print("No Lights found!");
            //    }
            //}

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
                if (Vector3.Distance(transform.position, targetObject.transform.position) <= Data.characterReach)
                {
                    inquiredObject = targetObject;
                    inquiredObject.SendMessage("Inquire", true, SendMessageOptions.DontRequireReceiver);
                }
            }
            else if (inquiredObject && (Input.GetButtonUp("Inquire") || Vector3.Distance(transform.position, inquiredObject.transform.position) > Data.characterReach))
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
                    //print("Cannot reach " + transform.name + ". Distance: " + Vector3.Distance(transform.position, targetObject.transform.position) + " > " + Data.characterReach);
                }
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

    public void SetLightController(GameObject lightController)
    {
        //No LightConroller set or set LightController is not the controller found
        if (!currentLightController || currentLightController.GetInstanceID() != lightController.GetInstanceID())
            currentLightController = lightController;
        else // Controller is set and is the new LightController
            currentLightController = null;
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
        if (currentLightController)
            currentLightController.SendMessage("Action");
        Data.shadow = true;
        Data.shadowCharacter.GetComponent<ShadowCharacterController>().enabled = true;
        Data.shadowCharacter.GetComponent<Rigidbody>().isKinematic = false;
        Data.world.SetActive(false);
        Data.realCharacter.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        Data.cam.GetComponent<Camera>().cullingMask = LayerMask.GetMask("ShadowWorld", "ShadowPlane");
        Data.outlineCam.SetActive(true);
        InitPuzzles.changeTrigger();
    }

    //private GameObject GetFirstReachableLight()
    //{
    //    if (Data.lights.Count <= 0)
    //        return null;

    //    GameObject lastReachable = null;

    //    Data.lights.Sort(CompareByDistanceToStart);
    //    for (int i = 0; i < Data.lights.Count; i++)
    //    {
    //        float currDist = Vector3.Distance(transform.position, Data.lights[i].transform.position);
    //        if (currDist < Data.characterReach)
    //        {
    //            lastReachable = Data.lights[i];
    //        }
    //        else break;
    //    }

    //    // No reachable Lights
    //    return lastReachable;
    //}

    //private GameObject GetNextReachableLight(GameObject currLight)
    //{
    //    if (Data.lights.Count <= 0)
    //        return null;

    //    Data.lights.Sort(CompareByDistanceToStart);
    //    if (currLight)
    //    {
    //        int idx = Data.lights.IndexOf(currLight);
    //        if (idx == Data.lights.Count - 1 
    //            || Vector3.Distance(transform.position, Data.lights[idx + 1].transform.position) > Data.characterReach)
    //            return GetFirstReachableLight();
    //        else
    //            return Data.lights[idx + 1];

    //    }

    //    // No reachable Lights
    //    return null;
    //}

    private int CompareByDistanceToStart(GameObject g1, GameObject g2)
    {
        if (g1.transform.position.x > g2.transform.position.x)
            return 1;
        if (g1.transform.position.x < g2.transform.position.x)
            return -1;
        return 0;
    }
}
