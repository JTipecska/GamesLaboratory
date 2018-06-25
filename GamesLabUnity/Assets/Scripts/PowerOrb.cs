using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOrb : MonoBehaviour {


    // Use this for initialization
    void Start()
    {
        Data.interactableObjects.Add(gameObject);
    }



    void Action()
    {


        this.gameObject.SetActive(false);
        if (this.gameObject.name == "Sphere")
        {
            Data.holdingOrb = true;
            Debug.Log("Holding Sphere");
        }
        if (this.gameObject.name == "Capsule")
        {
            Data.holdingRod = true;
            Debug.Log("Holding Capsule");
        }
        Data.interactableObjects.Remove(gameObject);

    }
}
