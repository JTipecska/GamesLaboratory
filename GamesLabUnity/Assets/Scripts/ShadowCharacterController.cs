using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCharacterController : MonoBehaviour {

    private Animator animeShadow;

    void Start () {
        Data.shadowCharacter = gameObject;
        GetComponent<ShadowCharacterController>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        animeShadow = this.GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!Data.cam || !Data.cam.GetComponent<TransformCamera>().finished || !Data.cam.GetComponent<TransformCamera>().blendfinished)
            return;

        if (!GUIController.GetMenuActive())
        {
            float characterMovement = Input.GetAxis("CharacterHorizontal");

            if (characterMovement > 0.001f || characterMovement < -0.001f)
            {
                if (animeShadow.GetBool("LookingForward") && characterMovement < -0.001f)
                {
                    GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler( 180, 90, 90));
                    animeShadow.SetBool("LookingForward", false);
                }
                else if (!animeShadow.GetBool("LookingForward") && characterMovement > 0.001f)
                {
                    GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(0, 90, 90));
                    animeShadow.SetBool("LookingForward", true);
                }
                else
                {
                    animeShadow.SetTrigger("StartWalking");
                    animeShadow.ResetTrigger("StopWalking");
                }
            }
            else
            {
                animeShadow.SetTrigger("StopWalking");
                animeShadow.ResetTrigger("StartWalking");
            }

            GetComponent<Rigidbody>().MovePosition(transform.position + Vector3.right * characterMovement * Time.deltaTime * Data.speed);
            Data.cam.transform.position = new Vector3(transform.position.x, Data.cam.transform.position.y, Data.cam.transform.position.z);
            if (Input.GetButtonDown("Switch World") && Data.cam.GetComponent<TransformCamera>().blendfinished && Data.cam.GetComponent<TransformCamera>().finished && Data.lastWorldSwitch + Data.waitWorldSwitch < Time.time && CollisionReal.canChange)
            {
                //Data.world.GetComponentInChildren<InitPuzzles>().changeTrigger();
                Data.cam.GetComponent<TransformCamera>().changePlane();
                GetComponent<Collider>().isTrigger = true;
                return;
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
        Data.realCharacter.transform.position = new Vector3(transform.position.x, transform.position.z + 1.3f, Data.realCharacter.transform.position.z);
        Data.realCharacter.SetActive(true);
        InitPuzzles.changeTrigger();

    }
}
