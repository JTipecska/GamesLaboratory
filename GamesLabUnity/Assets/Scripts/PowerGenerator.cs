using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerGenerator : MonoBehaviour {

    //public List<GameObject> objects = new List<GameObject>();

    public GameObject elevator, elevatorShadow;
    public Sprite sphere, capsule;


    // Use this for initialization
    void Start()
    {
        Data.interactableObjects.Add(gameObject);
    }



    void Action()
    {

        // Aktivate Power of every object attached to this object
        if (Data.holdingOrb)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            elevator.SendMessage("Action", SendMessageOptions.DontRequireReceiver);
            elevatorShadow.SetActive(true);
            elevatorShadow.GetComponent<Collider>().enabled = true;
            //GameObject.Find("GUI/HUD/Sphere").GetComponent<Image>().sprite = sphere;
            Data.activatedElevator = true;
            Data.interactableObjects.Remove(gameObject);
        }

        if (Data.holdingRod) {

            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            elevator.SetActive(true);
            elevatorShadow.GetComponent<ConsoleSwitch>().enabled = true;
            Data.activeDoor = true;
            //GameObject.Find("GUI/HUD/Sphere").GetComponent<Image>().sprite = capsule;
            Data.interactableObjects.Remove(gameObject);
        }
    }
}
