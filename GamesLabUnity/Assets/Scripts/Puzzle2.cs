using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2 : MonoBehaviour {
    public GameObject[] Switches;
    public GameObject[] Lights;
    int currIndex = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeLights()
    {
        currIndex++;
        foreach(GameObject s in Switches)
        {
            LightSwitch ls = s.GetComponent<LightSwitch>();
            if (ls)
            {
                for(int i = 0; i < ls.lights.Count; i++)
                {
                    ls.lights[i] = Lights[i + currIndex % ls.lights.Count];
                }
            }
        }
    }
}
