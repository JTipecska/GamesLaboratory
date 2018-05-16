using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCharacterController : MonoBehaviour {
    public GameObject realCam;
    public GameObject shadowCam;
    public GameObject realCharacter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.z < 1f)
            transform.position = new Vector3(transform.position.x, transform.position.y, 1f);

        transform.Translate(Input.GetAxis("Character Horizontal") * Time.deltaTime * Data.speed, 0, 0);
        shadowCam.transform.position = new Vector3(transform.position.x, shadowCam.transform.position.y, shadowCam.transform.position.z);
        if (Input.GetButtonDown("Switch World") && Data.lastWorldSwitch + Data.waitWorldSwitch < Time.time)
        {
            ChangeToRealWorld();
            return;
        }
    }

    private void ChangeToRealWorld()
    {
        print("Real World");
        Data.lastWorldSwitch = Time.time;
        realCam.SetActive(true);
        shadowCam.SetActive(false);
        Physics.gravity = new Vector3(0, -9.81f, 0);
        foreach (GameObject g in Data.shadowObjects)
        {
            g.SendMessageUpwards("ToggleShadowCollider");
        }
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<ShadowCharacterController>().enabled = false;
        realCharacter.transform.position = new Vector3(transform.position.x, transform.position.z - 0.5f, realCharacter.transform.position.z);
        transform.position = new Vector3(transform.position.x, -0.499f, transform.position.z);
        realCharacter.SetActive(true);
    }
}
