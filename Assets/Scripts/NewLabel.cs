using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLabel : MonoBehaviour
{
	[HideInInspector] public Transform target;

	void Update()
	{
		transform.LookAt(Camera.main.transform);
		Vector3 eulers = transform.localRotation.eulerAngles;
		transform.localRotation = Quaternion.Euler(0, eulers.y - 180, 0);
	}
}
