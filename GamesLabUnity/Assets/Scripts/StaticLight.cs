using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	void OnEnable(){
		Data.staticLights.Add(gameObject);
	}

	void OnDisable(){
		Data.staticLights.Remove(gameObject);
	}

	// Update is called once per frame
	void Update () {

    }

    void Action()
    {
        GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
    }

    void Reset()
    {
        GetComponent<Light>().enabled = true;
        Debug.Log("was reset");
    }
}
