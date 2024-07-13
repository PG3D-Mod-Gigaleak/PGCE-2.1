using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailFade : MonoBehaviour
{
    [SerializeField] private float speed;

    private float timer;

    private LineRenderer _renderer;

    private LineRenderer renderer
    {
        get
        {
            if (_renderer == null)
            {
                _renderer = GetComponent<LineRenderer>();
            }
            return _renderer;
        }
    }

    void Start()
    {
        //speed is divided by 1000f because typing in 0.001 is stupid... (remove it if you dont care)
        speed /= 1000f;
        timer = 0f;
    }

    void Update()
    {
        timer += (Time.deltaTime * speed);

        GradientAlphaKey[] rendererAlphaKeys = renderer.colorGradient.alphaKeys;
        for (int i = 0; i < rendererAlphaKeys.Length; i++)
        {
            rendererAlphaKeys[i].alpha -= timer;
        }

        //yes I have to do this, no I dont like it
        Gradient newGradient = renderer.colorGradient;
        newGradient.alphaKeys = rendererAlphaKeys;
        renderer.colorGradient = newGradient;
    }

    public void UpdateColor(Color color)
    {
        GradientColorKey[] rendererColorKeys = renderer.colorGradient.colorKeys;
        for (int i = 0; i < rendererColorKeys.Length; i++)
        {
            rendererColorKeys[i].color = color;
        }

        //yes I have to do this, no I dont like it
        Gradient newGradient = renderer.colorGradient;
        newGradient.colorKeys = rendererColorKeys;
        renderer.colorGradient = newGradient;
    }
}