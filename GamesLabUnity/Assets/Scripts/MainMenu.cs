using System;
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
    public GameObject levelparent;
    public bool ingame;
    Resolution[] resolutions;
    public GameObject music;
    static bool startedMusic = false;
    static int levels = 1;
    bool applyMenu = false;
    float timer = 10;
    Resolution old;

    // Use this for initialization
    void Start () {
        if (!ingame)
        {
            if (PlayerPrefs.HasKey("width") && PlayerPrefs.HasKey("height"))
            {
                if (Screen.currentResolution.width != PlayerPrefs.GetInt("width") || Screen.currentResolution.height != PlayerPrefs.GetInt("height"))
                    Screen.SetResolution(PlayerPrefs.GetInt("width"), PlayerPrefs.GetInt("height"), true);
            }
            if (PlayerPrefs.HasKey("volume"))
                AudioListener.volume = PlayerPrefs.GetFloat("volume");

            if (PlayerPrefs.HasKey("level"))
                levels = PlayerPrefs.GetInt("level");

            for (int i = levels; i < levelparent.transform.childCount-1; i++)
            {
                levelparent.transform.GetChild(i).GetComponent<Button>().interactable = false;
            }
        }
        

        resolutions = Screen.resolutions;
        s.maxValue = resolutions.Length - 1;
        t.text = Screen.currentResolution.ToString ();
        s.value = Array.IndexOf (resolutions, Screen.currentResolution);
        if (music != null && startedMusic)
        {
            music.SetActive(false);
        }
        if (music!= null && !startedMusic)
        {
            startedMusic = true;
            DontDestroyOnLoad(music);
        }
        
            
    }

    void Update()
    {
        if (applyMenu)
        {
            timer -= Time.deltaTime;
            countdown.text = "Resolution will reset in " + (int)timer + " sec.";
        }
        if (timer <= 0)
        {
            resetResBut.onClick.Invoke();
        }
    }

    public void SoundChange (float s) {

        AudioListener.volume = s / 25;
        PlayerPrefs.SetFloat("volume", s / 25);
    }

    public void resChange (float index) {
        t.text = resolutions[(int) index].ToString ();
    }

    public void apply () {
        old = Screen.currentResolution;
        Screen.SetResolution (resolutions[(int) s.value].width, resolutions[(int) s.value].height, true);
        PlayerPrefs.SetInt("width", resolutions[(int) s.value].width);
        PlayerPrefs.SetInt("height", resolutions[(int)s.value].height);
    }

    public void exit () {
        Application.Quit ();
    }

    public void sceneChange (string name) {
        if (name.Equals("MainMenu") && Data.shadow)
            Data.cam.GetComponent<TransformCamera>().changePlane();
        if (name.Equals("cutscene00") && levels == 1)
        {
            levels++;
            PlayerPrefs.SetInt("level",levels);
        }
            
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
        PlayerPrefs.SetInt("width", old.width);
        PlayerPrefs.SetInt("height", old.height);
    }
}