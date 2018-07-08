using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {
    public enum Mode {Toggle, Set };
    public List<GameObject> lights = new List<GameObject>();
    public Mode SwitchMode = Mode.Toggle;

	// Use this for initialization
	void Start () {
        Data.interactableObjects.Add(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Action()
    {
        bool setOn = false;
        if (GetComponent<Puzzle2>())
            GetComponent<Puzzle2>().ChangeLights();

        // Toggle Billboard_ON and Billboard_OFF
        foreach (Transform child in transform)
        {
            if (child.name.StartsWith("Billboard_O"))
            {
                child.gameObject.SetActive(!child.gameObject.activeSelf);
                if (child.gameObject.activeSelf)
                {
                    setOn = child.name.Equals("Billboard_ON");
                }
            }
        }

        // Toggle every light attached to this object
        foreach (GameObject g in lights)
        {
            if(SwitchMode == Mode.Toggle)
            {
                g.SendMessage("ToggleLight", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                g.SendMessage("SetLight", setOn, SendMessageOptions.DontRequireReceiver);
            }

        }
    }
}
