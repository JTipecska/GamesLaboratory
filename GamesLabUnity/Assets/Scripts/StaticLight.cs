using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticLight : MonoBehaviour {

    private bool isON;

	// Use this for initialization
	void Start () {

        isON = GetComponent<Light>().enabled;

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

    void ToggleLight()
    {
        GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
    }

    void Reset()
    {
        GetComponent<Light>().enabled = isON;
        Debug.Log("was reset");
    }
}
