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
        Data.realCharacter.GetComponent<Animator>().SetTrigger("TurnForward");
        Data.realCharacter.GetComponent<Animator>().ResetTrigger("TurnBack");
        Data.shadowCharacter.GetComponent<Animator>().SetTrigger("TurnForward");
        Data.shadowCharacter.GetComponent<Animator>().ResetTrigger("TurnBack");
    }
}
