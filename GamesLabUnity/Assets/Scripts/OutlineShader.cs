using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineShader : MonoBehaviour {

    public List<Renderer> materials = new List<Renderer>();
    public List<Renderer> dontChange = new List<Renderer>();
    Renderer[] all;
    float startPos = 4.620232f;
    float startPos2;


    public void Start()
    {
        Data.outlineCam = gameObject;
        GameObject p = GameObject.Find("Puzzles");
        all  = p.GetComponentsInChildren<Renderer>();
        gameObject.SetActive(false);
        //startPos = Data.cam.transform.position.y;
        startPos2 = transform.position.y;

    }

    public void Update()
    {
        //if (Data.cam.transform.position.y > 5f)
            this.transform.position = new Vector3(Data.cam.transform.position.x, startPos2 + Data.cam.transform.position.y - startPos, this.transform.position.z);
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
            foreach(Material mat in m.materials)
            {
                mat.shader = Shader.Find("Outlined/Silhouette Only");
                mat.SetColor("_OutlineColor", new Color(0, 56, 255, 0.5f));
                mat.SetFloat("_Outline", 0.1f);
                /*m.material.shader = Shader.Find("Additive Tint");
                m.material.SetColor("_Color", new Color(0, 56, 255, 1));*/
            }

        }
    }

    public void removeOutline()
    {
        foreach (Renderer m in materials)
        {
            foreach (Material mat in m.materials)
            mat.shader = Shader.Find("Standard");
            //m.material.SetColor("_Color", new Color(1, 1, 1, 1));
        }
        foreach (Renderer r in all)
            r.enabled = true;
    }
}
