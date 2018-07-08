using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Outdoor");
        Data.realCharacter.GetComponentInChildren<Animator>().SetTrigger("TurnForward");
        Data.realCharacter.GetComponentInChildren<Animator>().ResetTrigger("TurnBack");
        Data.shadowCharacter.GetComponentInChildren<Animator>().SetTrigger("TurnForward");
        Data.shadowCharacter.GetComponentInChildren<Animator>().ResetTrigger("TurnBack");
    }
}
