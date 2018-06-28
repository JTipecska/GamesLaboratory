using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConsoleSwitch : MonoBehaviour {

    public GameObject door;
    public GameObject message;

    // Use this for initialization
    void Start () {
        Data.interactableObjects.Add(gameObject);
    }

    // Update is called once per frame
    void Action()
    {


        if (Input.GetButtonDown("Action") && door != null && door.tag == "Final" && Data.activeDoor)
        {
            door.GetComponent<Animator>().SetBool("character_nearby", !door.GetComponent<Animator>().GetBool("character_nearby"));
            message.SetActive(true);
            Debug.Log("final door");

            StartCoroutine(FinalScene());
        }



    }

    IEnumerator FinalScene()
    {

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("Final");
    }
}
