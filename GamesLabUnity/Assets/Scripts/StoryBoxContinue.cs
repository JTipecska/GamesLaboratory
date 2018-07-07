using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBoxContinue : MonoBehaviour {
    public static bool storyBoxActive = false;
    public GameObject next;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        storyBoxActive = true;
        if (Input.GetButtonDown("Action"))
        {
            storyBoxActive = false;
            
            if (next != null)
            {
                next.SetActive(true);
            }
            gameObject.SetActive(false);
        }
	}
}
