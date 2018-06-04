using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionShadow : MonoBehaviour {

    public static bool canChange = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.root.name.Equals("_Dynamic"))
        {
            canChange = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("parent:" + other.gameObject.transform.parent.name);
        if (other.gameObject.transform.root.name.Equals("_Dynamic"))
        {
            canChange = false;
        }
    }
}
