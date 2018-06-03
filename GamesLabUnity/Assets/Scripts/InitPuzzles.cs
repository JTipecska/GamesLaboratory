using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPuzzles : MonoBehaviour {

	// Use this for initialization

	void Start () {
        /*Collider[] c = this.GetComponentsInChildren<Collider>();
        foreach (Collider x in c)
        {
            x.gameObject.AddComponent<CollisionReal>();
        }*/
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeTrigger()
    {
        CollisionReal[] c = GetComponentsInChildren<CollisionReal>();
        foreach (CollisionReal x in c)
        {
            x.GetComponent<Collider>().isTrigger = !x.GetComponent<Collider>().isTrigger;
        }
    }
}
