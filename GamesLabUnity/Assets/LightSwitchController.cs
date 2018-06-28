using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchController : MonoBehaviour {

    public GameObject lightswitch1, lightswitch2, light1, light2;

	
	// Update is called once per frame
	void Update () {

        if (!lightswitch1.transform.GetChild(0).gameObject.activeSelf)
        {
            lightswitch2.transform.GetChild(0).gameObject.SetActive(false);
            lightswitch2.transform.GetChild(1).gameObject.SetActive(false);

        }

        if (!lightswitch2.transform.GetChild(0).gameObject.activeSelf)
        {
            lightswitch1.transform.GetChild(0).gameObject.SetActive(false);
            lightswitch1.transform.GetChild(1).gameObject.SetActive(false);

        }


    }
}
