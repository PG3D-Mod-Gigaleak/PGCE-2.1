using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxSpinner : MonoBehaviour
{ 
    public float spinSpeed = 1f;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", (RenderSettings.skybox.GetFloat("_Rotation") >= 360f ? 0f :RenderSettings.skybox.GetFloat("_Rotation") + spinSpeed * Time.deltaTime));
    }
}
