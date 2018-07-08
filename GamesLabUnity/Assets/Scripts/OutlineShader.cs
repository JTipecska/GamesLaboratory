using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutlineShader : MonoBehaviour {

    public List<Renderer> materials = new List<Renderer>();
    public List<Renderer> dontChange = new List<Renderer>();
    public List<float> thiccness = new List<float>();
    Renderer[] all;
    public float startPos = 1.18f;
    float startPos2;


    public void Start()
    {
        Data.outlineCam = gameObject;
        GameObject p = GameObject.Find("Puzzles");
        all  = p.GetComponentsInChildren<Renderer>(true);
        gameObject.SetActive(false);
        //startPos = Data.cam.transform.position.y;
        startPos2 = transform.position.y;

    }

    public void Update()
    {
        if (Data.shadowCharacter.transform.position.y > 3.5f && SceneManager.GetActiveScene().name.Equals("Spaceship"))
            this.transform.position = new Vector3(Data.cam.transform.position.x, startPos + 4, this.transform.position.z);
        else
            this.transform.position = new Vector3(Data.cam.transform.position.x,  startPos, this.transform.position.z);
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
        for(int i = 0; i< materials.Count; i++)
        {
            materials[i].enabled = true;
            foreach(Material mat in materials[i].materials)
            {
                mat.shader = Shader.Find("Outlined/Silhouette Only");
                mat.SetColor("_OutlineColor", new Color(0, 56, 255, 0.5f));
                if (thiccness.Count > i && thiccness[i]>0)
                    mat.SetFloat("_Outline", thiccness[i]);
                else
                    mat.SetFloat("_Outline", 0.1f);
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
