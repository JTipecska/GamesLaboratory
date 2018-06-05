using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPuzzles : MonoBehaviour {

	// Use this for initialization

	void Start () {
        Collider[] c = this.GetComponentsInChildren<Collider>();
        foreach (Collider x in c)
        {
            if (x.enabled)
                x.gameObject.AddComponent<CollisionReal>();
        }
        Data.puzzles = gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void changeTrigger()
    {
        /*CollisionReal[] c = GetComponentsInChildren<CollisionReal>();
        foreach (CollisionReal x in c)
        {
            x.GetComponent<Collider>().isTrigger = !x.GetComponent<Collider>().isTrigger;
        }*/

        Collider[] c = Data.puzzles.GetComponentsInChildren<Collider>();
        foreach (Collider x in c)
        {
            if (x is MeshCollider && ((MeshCollider) x).convex)
                x.isTrigger = !x.isTrigger;
            if (!(x is MeshCollider))
                x.isTrigger = !x.isTrigger;
        }
        c = Data.realCharacter.GetComponentsInChildren<Collider>();

        foreach (Collider x in c)
        {
            if (x is MeshCollider && ((MeshCollider)x).convex)
                x.isTrigger = !x.isTrigger;
            if (!(x is MeshCollider))
                x.isTrigger = !x.isTrigger;
        }
    }
}
