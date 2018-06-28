using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Data.dead = this.gameObject;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        this.GetComponent<Animator>().Play("dead");
    }

    public void resetScene()
    {
        if (Data.shadow)
        {
            Data.cam.GetComponent<TransformCamera>().changePlane();
        }
        Data.realCharacter.transform.position = new Vector3(-21.44f, 0.12f, -1.3f);
        //gameObject.SetActive(false);
    }

    public void disable()
    {
        gameObject.SetActive(false);
    }



}
