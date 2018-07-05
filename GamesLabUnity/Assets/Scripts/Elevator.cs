using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {
    public bool box = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RealCharacterController>())
        {
            Data.onElevator = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<RealCharacterController>())
        {
            Data.onElevator = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (box)
            Data.cam.transform.position = new Vector3(Data.cam.transform.position.x, 8.05f, Data.cam.transform.position.z);
    }
}
