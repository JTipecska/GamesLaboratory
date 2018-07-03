using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCamera : MonoBehaviour {

    public bool active = true;

    void OnTriggerEnter(Collider other)
    {
        if (active && other.transform.root == Data.realCharacter.transform.root)
            Data.lockCamera = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.root == Data.realCharacter.transform.root)
            Data.lockCamera = false;
    }
}
