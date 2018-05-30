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
        key.transform.parent.forward = Camera.main.transform.forward;
        if (Input.GetButtonDown("Inquire"))
        {
            message.SetActive(true);
            key.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        key.gameObject.SetActive(false);
        message.SetActive(false);
    }
}
