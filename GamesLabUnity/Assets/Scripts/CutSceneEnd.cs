using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneEnd : MonoBehaviour
{
    public bool exitGame = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnEnable()
    {
        if (exitGame)
        {
            Application.Quit();
        }
        SceneManager.LoadScene("Spaceship");
    }
}