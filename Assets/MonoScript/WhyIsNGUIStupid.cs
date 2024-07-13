using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhyIsNGUIStupid : MonoBehaviour
{
	void Start()
	{
		transform.localScale = new Vector3(transform.localScale.x * 100f, transform.localScale.y * 100f, 0);
	}
}
