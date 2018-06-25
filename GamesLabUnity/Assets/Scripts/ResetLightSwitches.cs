using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLightSwitches : MonoBehaviour {

    public List<GameObject> lights = new List<GameObject>();
    public List<GameObject> lightSwitch = new List<GameObject>();
    public Dictionary<GameObject, bool> switches = new Dictionary<GameObject, bool>();

    void Start()
    {
        Data.interactableObjects.Add(gameObject);

        foreach (GameObject ls in lightSwitch) {

            switches.Add(ls, ls.transform.GetChild(0).gameObject.activeSelf);
        }
    }
        void Action()
    {

        // Reset every light attached to this object
        foreach (GameObject g in lights) 
            g.SendMessage("Reset", SendMessageOptions.DontRequireReceiver);
        
        foreach (GameObject s in switches.Keys) {
            print(s.name);

                if (switches[s])
                {
                    s.transform.GetChild(0).gameObject.SetActive(true);
                    s.transform.GetChild(1).gameObject.SetActive(false);
                Debug.Log("set ON");
                }
                else {

                    s.transform.GetChild(0).gameObject.SetActive(false);
                    s.transform.GetChild(1).gameObject.SetActive(true);
                }
        }
    }
}

