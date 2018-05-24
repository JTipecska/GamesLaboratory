using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour {

    public Slider s;
    public Text t;
    Resolution[] resolutions;
    // Use this for initialization
    void Start () {
       resolutions = Screen.resolutions;
        s.maxValue = resolutions.Length - 1;
        t.text = Screen.currentResolution.ToString();
        s.value = Array.IndexOf(resolutions, Screen.currentResolution);
	}

    public void SoundChange(float s)
    {
        
        AudioListener.volume = s/25;
    }

    public void resChange(float index)
    {
        t.text = resolutions[(int) index].ToString();
    }

    public void apply()
    {
        Screen.SetResolution(resolutions[(int) s.value].width, resolutions[(int)s.value].height,true);
    }

    public void exit()
    {
        Application.Quit();
    }

    public void sceneChange(string name){
        SceneManager.LoadScene(name);
        }

}
