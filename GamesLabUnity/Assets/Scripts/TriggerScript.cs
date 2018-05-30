using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

    public GameObject key;
    public GameObject message;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        key.gameObject.SetActive(true);
    }
    private void OnTriggerStay(Collider other)
    {
        key.transform.LookAt(Camera.main.transform.position, -Vector3.up);
        if (Input.GetKeyDown("Inspect"))
        {
            message.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        key.gameObject.SetActive(false);
    }
}
