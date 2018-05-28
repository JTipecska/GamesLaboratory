using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCharacterController : MonoBehaviour {
    public GameObject cam;

	// Use this for initialization
	void Start () {
        Data.shadowCharacter = gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.z < 1f)
            transform.position = new Vector3(transform.position.x, transform.position.y, 1f);

        GetComponent<Rigidbody>().MovePosition(new Vector3(Input.GetAxis("CharacterHorizontal") * Time.deltaTime * Data.speed, 0, 0));
        cam.transform.position = new Vector3(transform.position.x, cam.transform.position.y, cam.transform.position.z);
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
        Data.realCharacter.GetComponent<RealCharacterController>().cam.SetActive(true);
        cam.SetActive(false);
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
