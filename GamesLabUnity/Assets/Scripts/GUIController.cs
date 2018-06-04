using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour {

    public GameObject IngameMenu;

    private bool menuActive = false;

    private void Update()
    {
        if (Input.GetKey("escape")) {
            Action();
        }
    }

    public void Action()
    {
        IngameMenu.SetActive(!IngameMenu.gameObject.activeSelf);
        menuActive = !menuActive;

    }

    public bool GetMenuActive() {
        return menuActive;
    }
}
