using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CurrentGoals : MonoBehaviour {

    public GameObject text;

    private bool secondLevel = false;

    // Use this for initialization
    void Start () {

            text.GetComponent<Text>().text = "Find the Power Orb to restore Power to the Generator";

    }

    // Update is called once per frame
    void Update() {

    
        if (secondLevel)
        {

            text.GetComponent<Text>().text = "Switch on the Main Console";
        }

        if (Data.activatedElevator) {

            text.GetComponent<Text>().text = "Proceed to the upper Level";
        }


        else if (Data.holdingOrb) {

            text.GetComponent<Text>().text = "Repair the broken Generator";

        }

        else if (Data.holdingOrb && Data.onElevator)
        {

            text.GetComponent<Text>().text = "Switch on the Main Console";
            secondLevel = true;
        }

        else if (Data.holdingRod) {

            text.GetComponent<Text>().text = "Find the Console and leave the Ship";
        }

    }
}
