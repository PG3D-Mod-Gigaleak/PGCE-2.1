using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugObject : MonoBehaviour
{
	private MeshRenderer _renderer;

	private float cooldown;

	void Start()
	{
		_renderer = GetComponent<MeshRenderer>();
		DoDrugs();
	}

	public void DoDrugs()
	{
		foreach (Material mat in GetComponent<MeshRenderer>().materials)
		{
			mat.SetFloat("_Frequency", UnityEngine.Random.Range(2f, 5f));
			mat.SetFloat("_Amplitude", UnityEngine.Random.Range(0.2f, 0.5f));
			mat.SetFloat("_Speed", UnityEngine.Random.Range(2f, 5f));
		}
		cooldown = UnityEngine.Random.Range(0.3f, 5f);
	}

	void Update()
	{
		if (cooldown > 0f)
		{
			cooldown -= 1 * Time.deltaTime;
			return;
		}
		DoDrugs();
	}
}
