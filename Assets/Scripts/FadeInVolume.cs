using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInVolume : MonoBehaviour
{
	public float speed;

	void Update()
	{
		GetComponent<AudioSource>().volume += speed * Time.deltaTime;
		if (GetComponent<AudioSource>().volume >= 1f)
		{
			Destroy(this);
		}
	}
}
