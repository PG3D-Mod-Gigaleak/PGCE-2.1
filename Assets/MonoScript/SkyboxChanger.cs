using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour {

public Material[] skyboxes;

public Color[] colors;

public float interval;

private bool isChanging;

private int index;

void Update()
{
	if (index > skyboxes.Length - 1)
	{
		index = 0;
	}
	if (!isChanging)
	{
		StartCoroutine(Change());
	}
}

public IEnumerator Change()
{
	isChanging = true;
	yield return new WaitForSeconds(interval);
	RenderSettings.skybox = skyboxes[index];
	RenderSettings.ambientLight = colors[index];
	index++;
	isChanging = false;
}
}
