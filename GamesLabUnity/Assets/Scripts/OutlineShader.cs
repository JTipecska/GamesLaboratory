using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineShader : MonoBehaviour {

    public List<Renderer> materials = new List<Renderer>();


    public void Start()
    {

       /* GameObject p = GameObject.Find("Puzzles");
        MeshRenderer[] mr = p.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer m in mr)
        {
            materials.Add(m.material);
        }*/

    }

    public void Update()
    {
        this.transform.position.Set(Data.cam.transform.position.x, this.transform.position.y, this.transform.position.z);
    }


    public void addOutline()
    {
        foreach(Renderer m in materials)
        {
            
            m.material.shader = Shader.Find("Outlined/Silhouette Only");
            m.material.SetColor("_OutlineColor", Color.green);
            m.material.SetFloat("_Outline",0.03f);
        }
    }

    public void removeOutline()
    {
        foreach (Renderer m in materials)
        {
            m.material.shader = Shader.Find("Standard");
        }
    }
}
