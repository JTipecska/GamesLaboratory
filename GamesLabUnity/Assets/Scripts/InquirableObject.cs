using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InquirableObject : MonoBehaviour {
    public string InquireString = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void Inquire(bool show)
    {
        if (show)
        {
            if (Data.inquireLabel.GetComponent<Text>().text.Equals(""))
                Data.inquireLabel.GetComponent<Text>().text = InquireString;
        }
        else
        {
            if (Data.inquireLabel.GetComponent<Text>().text.Equals(InquireString))
                Data.inquireLabel.GetComponent<Text>().text = "";
        }

    }
}
