using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Data.dead = gameObject;
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnable()
    {
        this.GetComponent<Animator>().StartPlayback();
    }

    public void resetScene()
    {
        if (Data.shadow)
            Data.cam.GetComponent<TransformCamera>().changePlane();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


   
}
