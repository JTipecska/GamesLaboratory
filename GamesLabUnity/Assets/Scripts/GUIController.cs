using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GUIController : MonoBehaviour {

    public GameObject IngameMenu, Eventsystem;

    private static bool menuActive = false;

    private void Update()
    {
        if (Input.GetKeyDown("escape") || Input.GetButtonDown("Start")) {
            SetMenuActive();
        }
    }

    public void SetMenuActive()
    {
        Eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
        IngameMenu.SetActive(!IngameMenu.gameObject.activeSelf);
        Eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(IngameMenu.transform.GetChild(1).gameObject);//.GetComponent<Button>().Select();

        menuActive = !menuActive;

    }

    public static bool GetMenuActive() {
        return menuActive;
    }
}
