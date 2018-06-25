using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerOrb : MonoBehaviour {

    public Sprite sphereBlue, CapsuleBlue;
    public Image UiSprite;


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
            UiSprite.sprite = sphereBlue;
        }
        if (this.gameObject.name == "Capsule")
        {
            Data.holdingRod = true;
            Debug.Log("Holding Capsule");
            UiSprite.sprite = CapsuleBlue;
        }
        Data.interactableObjects.Remove(gameObject);

    }
}
