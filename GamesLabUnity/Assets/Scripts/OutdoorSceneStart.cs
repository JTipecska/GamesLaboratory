using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorSceneStart : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }
    private void Awake()
    {
        Data.shadow = false;
        Data.holdingOrb = false;
        Data.holdingRod = false;
        Data.onElevator = true;
        Data.activatedElevator = false;
        Data.activeDoor = false;
        Data.lockCamera = false;
        Data.amtLights = 0;
        Data.lastWorldSwitch = 0;
        Data.shadowObjects = new List<GameObject>();
        Data.shadowFloors = new List<GameObject>();
        Data.lights = new List<GameObject>();
        Data.staticLights = new List<GameObject>();
        Data.inquirableObjects = new List<GameObject>();
        Data.interactableObjects = new List<GameObject>();

        StoryBoxContinue.storyBoxActive = false;
        GUIController.menuActive = false;

        CollisionShadow.canChange = true;
        CollisionReal.canChange = true;


    }
    // Update is called once per frame
    void Update()
    {

    }
}
