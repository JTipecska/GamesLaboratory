using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFloor : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Data.shadowFloors.Add(gameObject);
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
