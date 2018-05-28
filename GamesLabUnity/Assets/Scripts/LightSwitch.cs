using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {
    List<GameObject> lights = new List<GameObject>();

	// Use this for initialization
	void Start () {
        Data.interactableObjects.Add(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Action()
    {
        foreach (GameObject g in lights)
            g.SendMessage("Action", SendMessageOptions.DontRequireReceiver);
    }
}
