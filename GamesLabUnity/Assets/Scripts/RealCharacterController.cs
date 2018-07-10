using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RealCharacterController : MonoBehaviour {
    private GameObject grabbedObject;
    private GameObject inquiredObject;
    private List<GameObject> grabableObjects = new List<GameObject>();
    private List<GameObject> inquirableObjects = new List<GameObject>();
    private GameObject currentLightController;
    private Transform grabParent;
    private Color controlledLightColor;
    public float lastShadowPlaneHeight;
    public float cameraOffsetY = 3.589873f;

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
    void Update()
    {

        if (GetComponent<Rigidbody>().velocity.y < -6f && Physics.Raycast(transform.position, Vector3.down, 0.2f))
            if(!(SceneManager.GetActiveScene().name.Equals("Spaceship")&&transform.position.x < -25))
                Data.dead.SetActive(true);

        RaycastHit hit;
        if (Physics.Raycast(transform.position + 0.16f * Vector3.up, Vector3.down, out hit, float.MaxValue, LayerMask.GetMask("ShadowPlane")))
        {
            lastShadowPlaneHeight = hit.collider.transform.position.y;
           // print("ShadowPlane \"" + hit.collider.name + "\": " + lastShadowPlaneHeight + ", Character Feet: " + transform.position);

        }

        if (!Data.cam || !Data.cam.GetComponent<TransformCamera>().finished || !Data.cam.GetComponent<TransformCamera>().blendfinished)
            return;


        if (!GUIController.GetMenuActive() && !StoryBoxContinue.storyBoxActive)
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
                    GetComponent<Transform>().Rotate(0, -180, 0);
                    rigidShadow.Rotate(0, -180, 0);
                    anim.SetBool("LookingForward", true);
                    animShadow.SetBool("LookingForward", true);
                }

                else if(Input.GetButton("Run")) {

                    animShadow.ResetTrigger("StopWalking");
                    animShadow.SetBool("Running", true);
                    anim.ResetTrigger("StopWalking");
                    anim.SetBool("Running", true);
                    Data.speed = 4f;
                }

                else
                {
                    animShadow.SetTrigger("StartWalking");
                    animShadow.ResetTrigger("StopWalking");
                    animShadow.SetBool("Running", false);
                    anim.SetTrigger("StartWalking");
                    anim.ResetTrigger("StopWalking");
                    anim.SetBool("Running", false);
                    Data.speed = 2f;

                }
            }
            else
            {
                animShadow.SetTrigger("StopWalking");
                animShadow.ResetTrigger("StartWalking");
                animShadow.SetBool("Running", false);
                anim.SetTrigger("StopWalking");
                anim.ResetTrigger("StartWalking");
                anim.SetBool("Running", false);
            }

            Rigidbody rig = GetComponent<Rigidbody>();
            rig.velocity = new Vector3(characterMovement * Data.speed, rig.velocity.y, rig.velocity.z);

            
            //Data.cam.transform.position = new Vector3(transform.position.x, Data.cam.transform.position.y, Data.cam.transform.position.z);

            if (currentLightController)
            {
                currentLightController.GetComponent<LightController>().controlledLightObject.transform.Translate(Input.GetAxis("LightHorizontal") * Time.deltaTime * Data.speed * Vector3.right, Space.World);
                currentLightController.GetComponent<LightController>().controlledLightObject.transform.Translate(Input.GetAxis("LightVertical") * Time.deltaTime * Data.speed * Vector3.forward, Space.World);

                //Light
                if (Mathf.Abs(transform.position.x - currentLightController.transform.position.x) > Data.characterReach)
                    currentLightController.SendMessage("Action");
            }

            //Worlds
            if (Input.GetButtonDown("Switch World") && Data.cam.GetComponent<TransformCamera>().blendfinished && Data.cam.GetComponent<TransformCamera>().finished && Data.lastWorldSwitch + Data.waitWorldSwitch < Time.time && CollisionShadow.canChange)
            {
                Data.shadowCharacter.GetComponent<Collider>().isTrigger = false;
                //Data.world.GetComponentInChildren<InitPuzzles>().changeTrigger();
                if (!Data.shadow)
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
          /*  if (Input.GetButtonDown("Grab"))
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
            }*/

            //Inquire
            if (Input.GetButtonDown("Inquire") && !inquiredObject && inquirableObjects.Count > 0)
            {
                GameObject targetObject = Data.GetClosestGameObjectFromList(gameObject, Data.inquirableObjects);
                if (Mathf.Abs(transform.position.x - targetObject.transform.position.x) <= Data.characterReach)
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
                print("Action pressed " + Time.frameCount);
                GameObject targetObject = Data.GetClosestGameObjectFromList(gameObject, Data.interactableObjects);
                if (Mathf.Abs(transform.position.x - targetObject.transform.position.x) <= Data.characterReach)
                {
                    print("Interacting with " + targetObject.name);
                    targetObject.SendMessage("Action", SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    //print("Cannot reach " + transform.name + ". Distance: " + Vector3.Distance(transform.position, targetObject.transform.position) + " > " + Data.characterReach);
                }
            }
        }
        if (!Data.shadow)
        {
            Data.shadowCharacter.transform.position = new Vector3(transform.position.x, lastShadowPlaneHeight, transform.position.y - lastShadowPlaneHeight - 1.3f);
            if (!Data.lockCamera)
            {
                if (Data.onElevator)
                {
                    Data.cam.transform.position = new Vector3(transform.position.x, cameraOffsetY + Data.realCharacter.transform.position.y -0.1f, Data.cam.transform.position.z);
                }
                else
                {
                    int floor = (int)Data.realCharacter.transform.position.y / 4;
                    Data.cam.transform.position = new Vector3(transform.position.x, cameraOffsetY + 4 * floor, Data.cam.transform.position.z);
                }
            }

        }
    }

  /*  private void OnCollisionEnter(Collision collision)
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
    }*/

    public void SetLightController(GameObject lightController)
    {
        //No LightConroller set or set LightController is not the controller found
        if (!currentLightController || currentLightController.GetInstanceID() != lightController.GetInstanceID())
        {
            currentLightController = lightController;
            currentLightController.GetComponent<LightController>().GetControlledLight().color = Color.white;
        }
        else // Controller is set and is the new LightController
        {
            currentLightController.GetComponent<LightController>().GetControlledLight().color = currentLightController.GetComponent<LightController>().GetControlledLightColor();
            currentLightController = null;
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
        GetComponent<RealCharacterController>().enabled = false;
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
