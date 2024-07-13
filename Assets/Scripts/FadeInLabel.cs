using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInLabel : MonoBehaviour
{
	public float speed;

	void Update()
	{
		GetComponent<UILabel>().alpha += speed * Time.deltaTime;
		if (GetComponent<UILabel>().alpha >= 1f)
		{
			Destroy(this);
		}
	}
}
