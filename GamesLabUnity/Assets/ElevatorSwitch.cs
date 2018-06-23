using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSwitch : MonoBehaviour
{
    public GameObject elevator;

    void Start()
    {
        Data.interactableObjects.Add(gameObject);
    }


    void Action()
    {

        elevator.SendMessage("Action", SendMessageOptions.DontRequireReceiver);
        this.transform.GetChild(0).gameObject.SetActive(false);
    }
}
