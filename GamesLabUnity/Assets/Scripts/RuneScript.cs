using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneScript : MonoBehaviour {


    public List<GameObject> lights = new List<GameObject>();
    // Use this for initialization
    void Start () {
        Data.interactableObjects.Add(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    void Action()
    {
        // Toggle Billboard_ON and Billboard_OFF
        if (gameObject.transform.Find("activation") != null)
            gameObject.transform.Find("activation").gameObject.SetActive(true);

        // Toggle every light attached to this object
        foreach (GameObject g in lights)
        {
            g.SendMessage("ToggleLight", SendMessageOptions.DontRequireReceiver);
        }
    }
}
