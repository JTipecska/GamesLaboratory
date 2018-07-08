using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticLight : MonoBehaviour {

    private bool isON;
    public float duration = -1;
    float currentTime;
    public List<Light> correspondingLights = new List<Light>();
	// Use this for initialization
	void Start () {

        isON = GetComponent<Light>().enabled;
        currentTime = duration;
        if (isON && duration > 0)
            transform.parent.GetComponentInChildren<Text>(true).gameObject.SetActive(true);
    }
	
	void OnEnable(){
		Data.staticLights.Add(gameObject);
	}

	void OnDisable(){
		Data.staticLights.Remove(gameObject);
	}

	// Update is called once per frame
	void Update () {
        if (GetComponent<Light>().enabled && duration > 0)
        {
            if (currentTime > 0)
            {
                transform.parent.GetComponentInChildren<Text>(true).text = (int)currentTime + "";
               
                currentTime -= Time.deltaTime;
            }
            else
            {
                transform.parent.GetComponentInChildren<Text>(true).gameObject.SetActive(false);
                GetComponent<Light>().enabled = false;
            }
        }

        if (duration > 0)
            transform.parent.GetComponentInChildren<Canvas>().transform.forward = Data.cam.transform.forward;

    }

    void ToggleLight()
    {
        GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
        foreach(Light l in correspondingLights)
        {
            l.enabled = GetComponent<Light>().enabled;
        }

        if (duration > 0)
        {
            transform.parent.GetComponentInChildren<Text>(true).gameObject.SetActive(true);
            if ((int) currentTime <= 0)
            {
                currentTime = duration;
            }
        }
    }
    void SetLight(bool setOn)
    {
        GetComponent<Light>().enabled = setOn;
        foreach (Light l in correspondingLights)
        {
            l.enabled = setOn;
        }
    }

    void Reset()
    {
        GetComponent<Light>().enabled = isON;
        Debug.Log("was reset");
    }
}
