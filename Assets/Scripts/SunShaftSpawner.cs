using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunShaftSpawner : MonoBehaviour
{        
    public UnityStandardAssets.ImageEffects.SunShafts.SunShaftsResolution resolution = UnityStandardAssets.ImageEffects.SunShafts.SunShaftsResolution.Normal;

    public UnityStandardAssets.ImageEffects.SunShafts.ShaftsScreenBlendMode screenBlendMode = UnityStandardAssets.ImageEffects.SunShafts.ShaftsScreenBlendMode.Screen;

    public Transform sunTransform;

    public int radialBlurIterations = 2;

    public Color sunColor = Color.white;

    public Color sunThreshold = new Color(0.87f,0.74f,0.65f);

    public float sunShaftBlurRadius = 2.5f;

    public float sunShaftIntensity = 1.15f;

    public float maxRadius = 0.75f;

    public bool useDepthTexture = true;

    private Shader sunShaftsShader, simpleClearMaterial;

    void Start()
    {
        sunShaftsShader = Shader.Find("Hidden/SunShaftsComposite");
        simpleClearMaterial = Shader.Find("Hidden/SimpleClear");
    }

    void Update()
    {
        if (Camera.main == null)
        {
            return;
        }
        
        UnityStandardAssets.ImageEffects.SunShafts shafts = Camera.main.GetComponent<UnityStandardAssets.ImageEffects.SunShafts>();

        if (shafts == null)
        {
            shafts = Camera.main.gameObject.AddComponent<UnityStandardAssets.ImageEffects.SunShafts>();

            SetSunShaftData(shafts);
            shafts.enabled = true;
        }
    }

    public void SetSunShaftData(UnityStandardAssets.ImageEffects.SunShafts shafts)
    {
        shafts.resolution = resolution;
        shafts.screenBlendMode = screenBlendMode;

        shafts.sunTransform = sunTransform;
        shafts.radialBlurIterations = radialBlurIterations;

        shafts.sunColor = sunColor;
        shafts.sunThreshold = sunThreshold;

        shafts.sunShaftBlurRadius = sunShaftBlurRadius;
        shafts.sunShaftIntensity = sunShaftIntensity;

        shafts.maxRadius = maxRadius;
        shafts.useDepthTexture = useDepthTexture;

        shafts.sunShaftsShader = sunShaftsShader;
        shafts.simpleClearShader = simpleClearMaterial;
    }
}
