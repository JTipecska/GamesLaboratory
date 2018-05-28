using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCharacterController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Data.shadowCharacter = gameObject;
        GetComponent<ShadowCharacterController>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.z < 1f)
            transform.position = new Vector3(transform.position.x, transform.position.y, 1f);

        GetComponent<Rigidbody>().MovePosition(new Vector3(Input.GetAxis("CharacterHorizontal") * Time.deltaTime * Data.speed, 0, 0));
        Data.cam.transform.position = new Vector3(transform.position.x, Data.cam.transform.position.y, Data.cam.transform.position.z);
        if (Input.GetButtonDown("Switch World") && Data.cam.GetComponent<transformCamera>().blendfinished && Data.cam.GetComponent<transformCamera>().finished && Data.lastWorldSwitch + Data.waitWorldSwitch < Time.time)
        {
            ChangeToRealWorld();
            return;
        }
    }

    private void ChangeToRealWorld()
    {
        print("Real World");
        Data.lastWorldSwitch = Time.time;
        Data.cam.GetComponent<transformCamera>().changePlane();
        Physics.gravity = new Vector3(0, -9.81f, 0);
        foreach (GameObject g in Data.shadowObjects)
        {
            g.SendMessageUpwards("ToggleShadowCollider");
        }
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<ShadowCharacterController>().enabled = false;
        Data.realCharacter.transform.position = new Vector3(transform.position.x, transform.position.z - 0.5f, Data.realCharacter.transform.position.z);
        transform.position = new Vector3(transform.position.x, -0.499f, transform.position.z);
        Data.realCharacter.SetActive(true);
    }
}
