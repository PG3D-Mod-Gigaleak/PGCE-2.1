using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnderAIPoint : MonoBehaviour
{
	public Transform[] connectedPoints;

	public Action actionHere;

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 1f);
		
		for (int i = 0; i < connectedPoints.Length; i++)
		{
			Gizmos.DrawLine(transform.position, connectedPoints[i].position);
		}
	}

	[System.Serializable]
	public class Action
	{
		public string action;

		public float chance;
	}
}
