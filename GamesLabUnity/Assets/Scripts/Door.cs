using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Data.interactableObjects.Add(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Action()
    {
        GetComponent<Animator>().SetBool("character_nearby", !GetComponent<Animator>().GetBool("character_nearby"));
    }
}
