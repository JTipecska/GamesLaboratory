using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSuit : MonoBehaviour {
    public GameObject CharacterWithoutSuit;
    public GameObject CharacterWithSuit;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Action()
    {
        Instantiate(CharacterWithSuit, CharacterWithoutSuit.transform.position, CharacterWithoutSuit.transform.rotation, CharacterWithoutSuit.transform.parent);

        Destroy(CharacterWithoutSuit);
        Destroy(gameObject);
    }
}
