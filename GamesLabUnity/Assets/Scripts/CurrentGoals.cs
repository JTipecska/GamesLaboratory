using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CurrentGoals : MonoBehaviour {

    public GameObject text;


    // Use this for initialization
    void Start () {

            text.GetComponent<Text>().text = "Find the Power Orb to restore Power to the Generator";

    }

    // Update is called once per frame
    void Update() {

        if (Data.holdingOrb && Data.realCharacter.gameObject.transform.position.y > 4)
        {

            text.GetComponent<Text>().text = "Switch on the Main Console";

        }

        else if (Data.activatedElevator) {

            text.GetComponent<Text>().text = "Proceed to the upper Level";
        }


        else if (Data.holdingOrb && !Data.activatedElevator) {

            text.GetComponent<Text>().text = "Repair the broken Generator";

        }

        else if (Data.holdingRod) {

            text.GetComponent<Text>().text = "Find the Console and leave the Ship";
        }

    }
}
