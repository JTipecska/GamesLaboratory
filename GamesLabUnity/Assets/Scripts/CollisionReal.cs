using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionReal : MonoBehaviour {

    public static bool canChange = true;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.IsChildOf(Data.realCharacter.transform.root))
        {
            canChange = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("parent:" + other.gameObject.transform.root.name);
        if (other.gameObject.transform.IsChildOf(Data.realCharacter.transform.root))
        {
            canChange = false;
        }
    }
}
