using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LanternMove : MonoBehaviour {


    public GameObject l;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y, l.transform.position.z);
        transform.up = -l.transform.position + transform.position;
	}
}
