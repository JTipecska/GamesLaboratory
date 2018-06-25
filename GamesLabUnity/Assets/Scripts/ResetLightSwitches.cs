using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLightSwitches : MonoBehaviour {

    public List<GameObject> lights = new List<GameObject>();
    public List<GameObject> switches = new List<GameObject>();

    void Start()
    {
        Data.interactableObjects.Add(gameObject);
    }

    void Action()
    {

        // Reset every light attached to this object
        foreach (GameObject g in lights) 
            g.SendMessage("Reset", SendMessageOptions.DontRequireReceiver);

        foreach (GameObject s in switches) { 
            s.transform.GetChild(0).gameObject.SetActive(true);
            s.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}

