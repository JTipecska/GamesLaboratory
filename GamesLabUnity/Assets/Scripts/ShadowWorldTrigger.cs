using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowWorldTrigger : MonoBehaviour
{

    public GameObject message;
    public bool follow;

    // Use this for initialization
    void Start()
    {
        message.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Data.shadow)
        {
            message.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Data.shadow)
            message.gameObject.SetActive(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (Data.shadow)
            message.gameObject.SetActive(true);
        message.transform.parent.forward = Camera.main.transform.forward;
        if (follow)
            message.transform.position = new Vector3(Data.shadowCharacter.transform.position.x, message.transform.parent.position.y, message.transform.parent.position.z);


    }
    private void OnTriggerExit(Collider other)
    {
            message.SetActive(false);
    }

}
