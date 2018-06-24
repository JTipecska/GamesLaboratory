using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{


    public float duration;
    float currPos;
    public float end = 300f;
    float start = 30f;
    float speed;
    RectTransform t;
    // Use this for initialization
    void Start()
    {
        t = GetComponent<RectTransform>();
        Data.timer = gameObject;
        gameObject.SetActive(false);
        start = t.rect.height;
        end = t.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (end > 0)
        {
            end -= Time.deltaTime * speed;
            t.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, end);
            //t.offsetMax = new Vector2(t.offsetMax.x, currPos);
        }
        else
        {
            Debug.Log("else works");
            if (CollisionReal.canChange)
                Data.cam.GetComponent<TransformCamera>().changePlane();
            else
                Data.dead.SetActive(true);
        }

    }

    void OnEnable()
    {
        end = start;        
        speed = end / duration;
    }
}
