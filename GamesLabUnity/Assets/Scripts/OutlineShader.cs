using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineShader : MonoBehaviour {

    public List<Renderer> materials = new List<Renderer>();
    public List<Renderer> dontChange = new List<Renderer>();
    Renderer[] all;
    float startPos;


    public void Start()
    {
        Data.outlineCam = gameObject;
        GameObject p = GameObject.Find("Puzzles");
        all  = p.GetComponentsInChildren<Renderer>();
        gameObject.SetActive(false);
        startPos = Data.cam.transform.position.y;

    }

    public void Update()
    {
        this.transform.position = new Vector3(Data.cam.transform.position.x, this.transform.position.y + Data.cam.transform.position.y - startPos, this.transform.position.z);
    }


    public void addOutline()
    {
        foreach (Renderer r in all)
        {
            r.enabled = false;
        }
        foreach (Renderer r in dontChange)
        {
            r.enabled = true;
        }
        foreach(Renderer m in materials)
        {
            m.enabled = true;
            m.material.shader = Shader.Find("Outlined/Silhouette Only");
            m.material.SetColor("_OutlineColor", new Color(0,56,255,0.5f));
            m.material.SetFloat("_Outline",0.1f);
            /*m.material.shader = Shader.Find("Additive Tint");
            m.material.SetColor("_Color", new Color(0, 56, 255, 1));*/
        }
    }

    public void removeOutline()
    {
        foreach (Renderer m in materials)
        {
            m.material.shader = Shader.Find("Standard");
            //m.material.SetColor("_Color", new Color(1, 1, 1, 1));
        }
        foreach (Renderer r in all)
            r.enabled = true;
    }
}
