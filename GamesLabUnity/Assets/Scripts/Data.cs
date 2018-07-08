using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static GameObject realCharacter;
    public static GameObject shadowCharacter;
    public static GameObject cam;
    public static GameObject inquireLabel;
    public static GameObject world;
    public static GameObject outlineCam;
    public static GameObject puzzles;
    public static GameObject timer;
    public static GameObject dead;
    public static bool shadow = false;
    public static bool holdingOrb = false;
    public static bool holdingRod = false;
    public static bool onElevator = false;
    public static bool activatedElevator = false;
    public static bool activeDoor = false;
    public static bool lockCamera = false;
    public static int amtLights = 0;
    public static float lastWorldSwitch = 0.0f;
    public static readonly float waitWorldSwitch = 0.3f;
    public static  float speed = 4f;
    public static readonly float characterReach = 2f;
    public static List<GameObject> shadowObjects = new List<GameObject>();
    public static List<GameObject> shadowFloors = new List<GameObject>();
    public static List<GameObject> lights = new List<GameObject>();
    public static List<GameObject> staticLights = new List<GameObject>();
    public static List<GameObject> inquirableObjects = new List<GameObject>();
    public static List<GameObject> interactableObjects = new List<GameObject>();

    public static GameObject GetClosestGameObjectFromList(GameObject target, List<GameObject> objects)
    {
        GameObject closest = null;
        float distance = float.MaxValue;
        foreach (GameObject g in objects)
        {
            if ((int)(g.transform.position.y / 4) != (int)(target.transform.position.y / 4)) continue;
            float tempDist = Mathf.Abs(g.transform.position.x - target.transform.position.x);
            if (tempDist < distance)
            {
                closest = g;
                distance = tempDist;
            }
        }

        print("Closest Object: " + closest.name);
        return closest;
    }
}
