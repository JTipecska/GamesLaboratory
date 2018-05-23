using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableLight : MonoBehaviour {
    private bool wasReachable = false;
    public Vector2 limitX = new Vector2(0, 0);
    public Vector2 limitY = new Vector2(10, 10);


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(Data.realCharacter.transform.position, transform.position) < Data.characterReach && !wasReachable)
            Data.reachableLights.Add(gameObject);
        else if (Vector3.Distance(Data.realCharacter.transform.position, transform.position) > Data.characterReach && wasReachable)
            Data.reachableLights.Remove(gameObject);
        transform.position = new Vector3(clamp(limitX.x, limitX.y, transform.position.x), clamp(limitY.x, limitY.y, transform.position.y), transform.position.z);
    }

    float clamp(float min, float max, float val)
    {
        return val < min ? min : (val < max ? val : max);
    }
}
