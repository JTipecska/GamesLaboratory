using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateStory : MonoBehaviour {
    public bool once = true;
    public GameObject g;
    bool triggered = false;
    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Data.realCharacter)
        {
            if ((once && !triggered) || !once)
            {
                triggered = true;
                g.SetActive(!g.activeSelf);
            }
        }

    }
}
