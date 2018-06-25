using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSwitch : MonoBehaviour
{

    void Start()
    {
        Data.interactableObjects.Add(gameObject);
    }


    public void Action()
    {

        this.SendMessage("Action", SendMessageOptions.DontRequireReceiver);
        Debug.Log("elevator set aktive");
    }
}
