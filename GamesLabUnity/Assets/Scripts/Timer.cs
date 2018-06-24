using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {


    public float duration;
    float currPos;
    float end = 300f;
    float start = 30f;
    float speed;
    RectTransform t;
	// Use this for initialization
	void Start () {
        t = GetComponent<RectTransform>();
        Data.timer = gameObject;
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (currPos > -end)
        {
            currPos -= Time.deltaTime * speed;
            t.offsetMax = new Vector2(t.offsetMax.x, currPos);
        }
        else
        {
            if (CollisionReal.canChange)
                Data.cam.GetComponent<TransformCamera>().changePlane();
            else
                Data.dead.SetActive(true);
        }
        
	}

    void OnEnable()
    {
        currPos = -start;
        speed = (end - start)/ duration;
    }
}
