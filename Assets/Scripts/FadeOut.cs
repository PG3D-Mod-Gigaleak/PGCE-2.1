using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
	public float speed;

	public bool destroy;

	void Update()
	{
		GetComponent<UITexture>().alpha -= speed * Time.deltaTime;
		if (destroy)
		{
			if (GetComponent<UITexture>().alpha <= 0f)
			{
				Destroy(gameObject);
			}
		}
	}
}
