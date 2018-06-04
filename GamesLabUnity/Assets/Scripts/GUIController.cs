using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour {

    public GameObject IngameMenu;

    private static bool menuActive = false;

    private void Update()
    {
        if (Input.GetKeyDown("escape") || Input.GetButtonDown("Start")) {
            Action();
        }
    }

    public void Action()
    {
        IngameMenu.SetActive(!IngameMenu.gameObject.activeSelf);
        menuActive = !menuActive;

    }

    public static bool GetMenuActive() {
        return menuActive;
    }
}
