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
        Data.holdingOrb = true;
        Data.interactableObjects.Remove(gameObject);

    }
}
