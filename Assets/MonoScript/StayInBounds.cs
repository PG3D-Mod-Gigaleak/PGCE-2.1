using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInBounds : MonoBehaviour {

	public float min;

	public float max;

	// Update is called once per frame
	void Update () {
		if (this.transform.rotation.x < min)
		{
			this.transform.Rotate(min, 0f, 0f);
		}
		if (this.transform.rotation.x > max)
		{
			this.transform.Rotate(max, 0f, 0f);
		}
	}
}
