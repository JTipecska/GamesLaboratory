using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GUIController : MonoBehaviour {

    static int levels = 1;

    public GameObject IngameMenu, Eventsystem;

    public static bool menuActive = false;


    private void Start()
    {
        if (PlayerPrefs.HasKey("level"))
            levels = PlayerPrefs.GetInt("level");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start")) {
            SetMenuActive();
        }
    }

    public void SetMenuActive()
    {
        Eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
        IngameMenu.SetActive(!IngameMenu.gameObject.activeSelf);
        Eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(IngameMenu.transform.Find("Main/Play").gameObject);

        menuActive = !menuActive;


        if (!menuActive) {

            IngameMenu.transform.Find("Main").gameObject.SetActive(true);
            IngameMenu.transform.Find("Image").gameObject.SetActive(true);

            if (IngameMenu.transform.Find("ControlsPicture") != null)
                IngameMenu.transform.Find("ControlsPicture").gameObject.SetActive(false);
            if (IngameMenu.transform.Find("OptionsMenu") != null)
                IngameMenu.transform.Find("OptionsMenu").gameObject.SetActive(false);
            if (IngameMenu.transform.Find("LevelSelection") != null)
                IngameMenu.transform.Find("LevelSelection").gameObject.SetActive(false);

        }
    }

    public static bool GetMenuActive() {
        return menuActive;
    }


    public void exit()
    {
        Application.Quit();
    }

    public void sceneChange(string name)
    {
        if (name.Equals("MainMenu") && Data.shadow)
            Data.cam.GetComponent<TransformCamera>().changePlane();
        if (name.Equals("cutscene00") && levels == 1)
        {
            levels++;
            PlayerPrefs.SetInt("level", levels);
        }

        SceneManager.LoadScene(name);
    }

}
