using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealCharacterController : MonoBehaviour {
    public GameObject shadowCharacter;
    public GameObject realCam;
    public GameObject shadowCam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.y < 0.5f)
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

        transform.Translate(Input.GetAxis("Character Horizontal") * Time.deltaTime * Data.speed, 0, 0);
        shadowCharacter.transform.position = new Vector3(transform.position.x, shadowCharacter.transform.position.y, transform.position.y + 0.5f);
        realCam.transform.position = new Vector3(transform.position.x, realCam.transform.position.y, realCam.transform.position.z);

        if (Input.GetButtonDown("Switch World") && Data.lastWorldSwitch + Data.waitWorldSwitch < Time.time)
        {
            ChangeToShadowWorld();
            return;
        }
    }

    private void ChangeToShadowWorld()
    {
        print("Shadow World");
        Data.lastWorldSwitch = Time.time;
        realCam.SetActive(false);
        shadowCam.SetActive(true);
        Physics.gravity = new Vector3(0, 0, -9.81f);
        foreach(GameObject g in Data.shadowObjects)
        {
            g.SendMessageUpwards("ToggleShadowCollider");
        }
        shadowCharacter.transform.position = new Vector3(shadowCharacter.transform.position.x, 0f, shadowCharacter.transform.position.z);
        shadowCharacter.GetComponent<ShadowCharacterController>().enabled = true;
        shadowCharacter.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.SetActive(false);
    }
}
