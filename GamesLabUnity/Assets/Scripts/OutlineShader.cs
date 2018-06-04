using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineShader : MonoBehaviour {


    public Shader outlineMaterial;
    public Material outlineMaterial2;
    public List<Material> materials = new List<Material>();


    public void Start()
    {

        GameObject p = GameObject.Find("Puzzles");
        MeshRenderer[] mr = p.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer m in mr)
        {
            materials.Add(m.material);
        }

    }
 

    public void addOutline()
    {
        foreach(Material m in materials)
        {
            m.shader = Shader.Find("Outlined/Silhouette Only");
        }
    }

    public void removeOutline()
    {
        foreach (Material m in materials)
        {
            m.shader = Shader.Find("Standard");
        }
    }
}
