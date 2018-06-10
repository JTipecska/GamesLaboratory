﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Slider s;
    public Button resetResBut;
    public Text t;
    public Text countdown;
    Resolution[] resolutions;
    public GameObject music;
    static bool startedMusic = false;
    bool applyMenu = false;
    float timer = 10;
    Resolution old;

    // Use this for initialization
    void Start () {
        resolutions = Screen.resolutions;
        s.maxValue = resolutions.Length - 1;
        t.text = Screen.currentResolution.ToString ();
        s.value = Array.IndexOf (resolutions, Screen.currentResolution);
        if (music!= null && !startedMusic)
        {
            startedMusic = true;
            DontDestroyOnLoad(music);
        }
            
    }

    void Update()
    {
        timer -= Time.deltaTime;
        countdown.text = "Resolution will reset in " + (int)timer + " sec.";
        if (timer <= 0)
        {
            resetResBut.onClick.Invoke();
        }
    }

    public void SoundChange (float s) {

        AudioListener.volume = s / 25;
    }

    public void resChange (float index) {
        t.text = resolutions[(int) index].ToString ();
    }

    public void apply () {
        old = Screen.currentResolution;
        Screen.SetResolution (resolutions[(int) s.value].width, resolutions[(int) s.value].height, true);
    }

    public void exit () {
        Application.Quit ();
    }

    public void sceneChange (string name) {
        SceneManager.LoadScene (name);
    }

    public void startApplyMenu()
    {
        applyMenu = true;
        timer = 10;
    }
    public void resetRes()
    {
        Screen.SetResolution(old.width, old.height, true);
    }
}