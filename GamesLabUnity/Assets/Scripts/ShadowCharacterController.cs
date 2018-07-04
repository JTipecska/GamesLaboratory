using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCharacterController : MonoBehaviour {

    private Animator animShadow;
    public Animator animCharacter;
    public Transform rigidCharacter;

    void Start () {
        Data.shadowCharacter = gameObject;
        GetComponent<ShadowCharacterController>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        animShadow = this.GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!Data.cam || !Data.cam.GetComponent<TransformCamera>().finished || !Data.cam.GetComponent<TransformCamera>().blendfinished)
            return;

        if (!GUIController.GetMenuActive())
        {
            float characterMovement = Input.GetAxis("CharacterHorizontal");

           if (characterMovement > 0.001f || characterMovement < -0.001f)
            {
                if (animShadow.GetBool("LookingForward") && characterMovement < -0.001f)
                {
                    GetComponent<Transform>().Rotate(0, 180, 0);
                    rigidCharacter.Rotate(0, 180, 0);
                    animShadow.SetBool("LookingForward", false);
                    animCharacter.SetBool("LookingForward", false);
                }
                else if (!animShadow.GetBool("LookingForward") && characterMovement > 0.001f)
                {
                    GetComponent<Transform>().Rotate(0, -180, 0);
                    rigidCharacter.Rotate(0, -180, 0);
                    animShadow.SetBool("LookingForward", true);
                    animCharacter.SetBool("LookingForward", true);
                }
                else if (Input.GetButtonDown("Run"))
                {

                    animShadow.SetTrigger("StartRunning");
                    animShadow.ResetTrigger("StopWalking");
                    animCharacter.SetTrigger("StartRunning");
                    animCharacter.ResetTrigger("StopWalking");
                }
                else
                {
                    animShadow.SetTrigger("StartWalking");
                    animShadow.ResetTrigger("StopWalking");
                    animShadow.ResetTrigger("StartRunning");
                    animCharacter.SetTrigger("StartWalking");
                    animCharacter.ResetTrigger("StopWalking");
                    animCharacter.ResetTrigger("StartRunning");
                }
            }
            else
            {
                animShadow.SetTrigger("StopWalking");
                animShadow.ResetTrigger("StartWalking");
                animShadow.ResetTrigger("StartRunning");
                animCharacter.SetTrigger("StopWalking");
                animCharacter.ResetTrigger("StartWalking");
                animCharacter.ResetTrigger("StartRunning");
            }

            Rigidbody rig = GetComponent<Rigidbody>();
            rig.velocity = new Vector3(characterMovement * Data.speed, rig.velocity.y, rig.velocity.z);
            if (!Data.lockCamera)
                Data.cam.transform.position = new Vector3(transform.position.x, Data.cam.transform.position.y, Data.cam.transform.position.z);
            Data.realCharacter.transform.position = new Vector3(transform.position.x, transform.position.z + Data.realCharacter.GetComponent<RealCharacterController>().lastShadowPlaneHeight + 1.3f, -1.3f);
            //.GetComponent<Rigidbody>().MovePosition(transform.position + Vector3.right * characterMovement * Time.deltaTime * Data.speed);
            if (Input.GetButtonDown("Switch World") && Data.cam.GetComponent<TransformCamera>().blendfinished && Data.cam.GetComponent<TransformCamera>().finished && Data.lastWorldSwitch + Data.waitWorldSwitch < Time.time )
            {
                if (CollisionReal.canChange) {
                    //Data.world.GetComponentInChildren<InitPuzzles>().changeTrigger();
                    //Data.cam.GetComponent<TransformCamera>().changePlane();
                    GetComponent<Collider>().isTrigger = true;
                    return;
                }
               
            }
        }

    }

    private void ChangeToRealWorld()
    {
        
        print("Real World");
        Data.lastWorldSwitch = Time.time;
        Data.cam.GetComponent<Camera>().cullingMask = -1;
        Data.world.SetActive(true);
        Data.outlineCam.SetActive(false);
        Physics.gravity = new Vector3(0, -9.81f, 0);
        /*foreach (GameObject g in Data.shadowObjects)
        {
            g.SendMessageUpwards("ToggleShadowCollider");
        }*/
        foreach (GameObject g in Data.shadowFloors)
        {
            g.SetActive(false);
        }
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<ShadowCharacterController>().enabled = false;
        Data.shadow = false;
        Data.realCharacter.transform.position = new Vector3(transform.position.x, transform.position.z + Data.realCharacter.GetComponent<RealCharacterController>().lastShadowPlaneHeight + 1.3f, -1.3f);
        Data.realCharacter.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        InitPuzzles.changeTrigger();

    }
}
