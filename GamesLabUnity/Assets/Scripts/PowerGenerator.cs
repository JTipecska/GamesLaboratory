using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerGenerator : MonoBehaviour {

    //public List<GameObject> objects = new List<GameObject>();

    public GameObject elevator, elevatorShadow;


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

        }

        if (Data.holdingRod) {

            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            elevator.SetActive(true);

        }
    }
}
