using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadly : MonoBehaviour {

    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Data.realCharacter)
            Data.dead.SetActive(true); 
    }
}
