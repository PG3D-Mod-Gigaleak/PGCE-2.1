using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
	public Transform otherPortal;

	void OnTriggerEnter(Collider other)
	{
		other.transform.position = otherPortal.position + otherPortal.transform.forward;
		other.transform.rotation = otherPortal.rotation;
	}
}
