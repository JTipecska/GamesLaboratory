using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static GameObject realCharacter;
    public static GameObject shadowCharacter;
    public static float lastWorldSwitch = 0.0f;
    public static readonly float waitWorldSwitch = 0.3f;
    public static readonly float speed = 2f;
    public static readonly float characterReach = 5f;
    public static List<GameObject> shadowObjects = new List<GameObject>();
    public static List<GameObject> reachableLights = new List<GameObject>();
}
