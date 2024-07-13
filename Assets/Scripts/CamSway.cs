using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSway : MonoBehaviour
{
	[HideInInspector] public float value;
	[HideInInspector] public float offset;

	void Update()
	{
		Vector3 eulers = transform.rotation.eulerAngles;
		Vector3 position = transform.localPosition;
		transform.rotation = Quaternion.Euler(eulers.x, eulers.y, 0 + value);
		transform.localPosition = new Vector3(0 + offset, position.y, position.z);
	}
}
