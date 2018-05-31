using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackWhiteCam : MonoBehaviour
{
    public Material standardMaterial;
    public Material blackWhiteMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (Data.shadow)
        {
            if (Data.cam.GetComponent<TransformCamera>().finished && Data.cam.GetComponent<TransformCamera>().blendfinished)
                Graphics.Blit(source, destination, blackWhiteMaterial);
            else
                Graphics.Blit(source, destination);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
