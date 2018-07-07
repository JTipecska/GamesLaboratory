using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {
    public Timer timer;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Data.realCharacter.transform.position = new Vector3(-24, 0.12f, -1.3f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Data.realCharacter.transform.position = new Vector3(14.0f, 0.12f, -1.3f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Data.realCharacter.transform.position = new Vector3(32.0f, 0.12f, -1.3f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Data.realCharacter.transform.position = new Vector3(18.0f, 4.12f, -1.3f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Data.realCharacter.transform.position = new Vector3(-2.0f, 4.12f, -1.3f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Data.realCharacter.transform.position = new Vector3(-16.0f, 4.12f, -1.3f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Data.realCharacter.transform.position = new Vector3(-22.0f, 4.12f, -1.3f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Data.realCharacter.transform.position = new Vector3(-40.0f, 0, -1.3f);
        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            timer.enabled = false;
        }
    }
}
