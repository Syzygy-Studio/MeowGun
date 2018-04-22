using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
public class EdgeDetection : PostEffectsBase
{
    private Shader edgeDetectionShader { get { return Shader.Find("EdgeDetection"); } }

    private Material material = null;
    public Material Material
    {
        get
        {
            material = CheckShaderAndCreateMaterial(edgeDetectionShader, material);
            return material;
        }
    }

    [Range(0.0f, 1.0f)]
    public float edgesOnly = 0.0f;
    public Color edgeColor = Color.black;
    public Color backgroundColor = Color.white;
    public float sampleDistance = 1.0f;
    public float sensitivityDepth = 1.0f;
    public float sensitivityNormals = 1.0f;

    private void OnEnable()
    {
        GetComponent<Camera>().depthTextureMode |= DepthTextureMode.DepthNormals;
    }

    [ImageEffectOpaque]
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(Material != null)
        {
            Material.SetFloat("_EdgeOnly", edgesOnly);
            Material.SetColor("_EdgeColor", edgeColor);
            Material.SetColor("_BackgroundColor", backgroundColor);
            Material.SetFloat("_SampleDistance", sampleDistance);
            Material.SetVector("_Sensitivity", new Vector4(sensitivityNormals, sensitivityDepth, 0, 0));

            Graphics.Blit(source, destination, Material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
