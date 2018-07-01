using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerOrb : MonoBehaviour {

    public GameObject collectible;


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
            collectible.SetActive(true);
        }
        if (this.gameObject.name == "Capsule")
        {
            Data.holdingRod = true;
            Debug.Log("Holding Capsule");
            collectible.SetActive(true);
        }
        Data.interactableObjects.Remove(gameObject);

    }
}
