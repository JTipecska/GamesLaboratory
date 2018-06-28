using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {
    public List<GameObject> lights = new List<GameObject>();

	// Use this for initialization
	void Start () {
        Data.interactableObjects.Add(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Action()
    {
        // Toggle Billboard_ON and Billboard_OFF

        if (lights.Count == 2) {

           // if (lights[0].GetComponent<Light>().enabled == false)
           // {

                lights[0].SendMessage("Action", SendMessageOptions.DontRequireReceiver);
                lights[1].SendMessage("Action", SendMessageOptions.DontRequireReceiver);
           // }

        }


        else
        {
            foreach (Transform child in transform)
            {
                if (child.name.StartsWith("Billboard_O"))
                    child.gameObject.SetActive(!child.gameObject.activeSelf);
            }

            // Toggle every light attached to this object
            foreach (GameObject g in lights)
                g.SendMessage("Action", SendMessageOptions.DontRequireReceiver);
        }
    }
}
