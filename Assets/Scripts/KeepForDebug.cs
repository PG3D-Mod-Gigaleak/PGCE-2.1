using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepForDebug : MonoBehaviour
{
	void Start()
	{
		if (!Debug.isDebugBuild)
		{
			Destroy(gameObject);
		}
	}
}
