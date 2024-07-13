using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
	public float speed;

	void Update()
	{
		GetComponent<UITexture>().alpha += speed * Time.deltaTime;
		if (GetComponent<UITexture>().alpha >= 1f)
		{
			Destroy(this);
		}
	}
}
