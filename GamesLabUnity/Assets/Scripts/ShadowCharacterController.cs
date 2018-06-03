﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCharacterController : MonoBehaviour {

    public GUIController test;

    void Start () {
        Data.shadowCharacter = gameObject;
        GetComponent<ShadowCharacterController>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (!Data.cam || !Data.cam.GetComponent<TransformCamera>().finished || !Data.cam.GetComponent<TransformCamera>().blendfinished)
            return;

        if (!test.GetMenuActive())
        {
            GetComponent<Rigidbody>().MovePosition(transform.position + Vector3.right * Input.GetAxis("CharacterHorizontal") * Time.deltaTime * Data.speed);
            //transform.Translate( Vector3.right * Input.GetAxis("CharacterHorizontal") * Time.deltaTime * Data.shadowSpeed);
            Data.cam.transform.position = new Vector3(transform.position.x, Data.cam.transform.position.y, Data.cam.transform.position.z);
            if (Input.GetButtonDown("Switch World") && Data.cam.GetComponent<TransformCamera>().blendfinished && Data.cam.GetComponent<TransformCamera>().finished && Data.lastWorldSwitch + Data.waitWorldSwitch < Time.time)
            {
                Data.cam.GetComponent<TransformCamera>().changePlane();
                return;
            }
        }
    }

    private void ChangeToRealWorld()
    {
        print("Real World");
        Data.lastWorldSwitch = Time.time;
        Data.world.SetActive(true);
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
        Data.realCharacter.transform.position = new Vector3(transform.position.x, transform.position.z, Data.realCharacter.transform.position.z);
        Data.realCharacter.SetActive(true);
    }
}
