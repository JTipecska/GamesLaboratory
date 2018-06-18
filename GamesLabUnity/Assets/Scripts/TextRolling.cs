using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextRolling : MonoBehaviour {

    public float speedWhole;
    public float speedPerLetter = 100;
    Text text;
    string finaltext;
    string currenttext = "";
    float speed;
    int counter = 0;
    float timer = 0;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        finaltext = text.text;
        text.text = "";
        speed = speedPerLetter;
        if (speedWhole == 0)
        {
            //speed = speedWhole / finaltext.Length;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (counter != finaltext.Length)
        {
            if (timer >= speed)
            {
                text.text += finaltext[counter];
                counter++;
                timer = 0;
            }
            timer += Time.deltaTime;
        }
        
	}
}
