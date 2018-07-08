using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBoxContinue : MonoBehaviour {
    public static bool storyBoxActive = false;
    public GameObject next;
    RealCharacterController c;
    Animator anim;
    
	// Use this for initialization
	void Start () {
        c = Data.realCharacter.GetComponent<RealCharacterController>();
        anim = c.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Data.realCharacter.GetComponent<Rigidbody>().velocity = Vector3.zero;
        storyBoxActive = true;
        c.animShadow.SetTrigger("StopWalking");
        c.animShadow.ResetTrigger("StartWalking");
        c.animShadow.SetBool("Running", false);
        anim.SetTrigger("StopWalking");
        anim.ResetTrigger("StartWalking");
        anim.SetBool("Running", false);
        if (Input.GetButtonDown("Action"))
        {
            storyBoxActive = false;
            Data.radio = true;
            
            if (next != null)
            {
                next.SetActive(true);
            }
            gameObject.SetActive(false);
        }
	}
}
