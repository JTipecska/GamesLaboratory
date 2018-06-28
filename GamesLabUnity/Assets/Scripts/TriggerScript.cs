using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

    public GameObject key;
    public GameObject message;
    public bool needPress;
    public bool follow;
    public GameObject door;

	// Use this for initialization
	void Start () {
        key.SetActive(false);
        message.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Data.shadow)
        {
            key.gameObject.SetActive(false);
            message.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        key.gameObject.SetActive(true);
    }
    private void OnTriggerStay(Collider other)
    {
        key.transform.parent.forward = Camera.main.transform.forward;
        if (follow)
            key.transform.position = new Vector3(Data.shadowCharacter.transform.position.x, key.transform.parent.position.y, key.transform.parent.position.z);
        if (Input.GetButtonDown("Inquire") && needPress)
        {
            message.SetActive(true);
            key.SetActive(false);
        }
        if(Input.GetButtonDown("Action") && door!=null && other.transform.root == Data.realCharacter.transform.root && door.tag != "Final")
        {
            door.GetComponent<Animator>().SetBool("character_nearby", !door.GetComponent<Animator>().GetBool("character_nearby"));
            
        }  

    }
    private void OnTriggerExit(Collider other)
    {
        key.gameObject.SetActive(false);
        message.SetActive(false);
    }

}
