using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead : MonoBehaviour
{
    Vector3 resetCamPos;
    Quaternion resetCamRot;
    // Use this for initialization
    void Start()
    {
        Data.dead = this.gameObject;
        resetCamPos = Data.cam.transform.position;
        resetCamRot = Data.cam.transform.localRotation;
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
        if (Data.realCharacter.transform.position.y > 3f)
        {
            Data.realCharacter.transform.position = new Vector3(12f, 4f, -1.3f);
        }
        else
        {
            Data.realCharacter.transform.position = new Vector3(-21.1f, 0.12f, -1.3f);
        }
        
        //Data.cam.transform.position = new Vector3(-21f,3.5f,-2.8f);
        //Data.cam.transform.localRotation = resetCamRot;
        //Data.cam.transform.SetPositionAndRotation(resetCamPos,resetCamRot);
        //gameObject.SetActive(false);
    }

    public void disable()
    {
        gameObject.SetActive(false);
    }



}
