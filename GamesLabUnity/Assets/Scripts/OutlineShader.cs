using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineShader : MonoBehaviour {


    public Shader outlineMaterial;
    public Material outlineMaterial2;
    public void Start()
    {
       this.GetComponent<Camera>().SetReplacementShader(outlineMaterial, "Puzzles");
    }
    private void OnPreRender()
    {
        this.GetComponent<Camera>().SetReplacementShader(outlineMaterial, "Puzzles");
    }


}
