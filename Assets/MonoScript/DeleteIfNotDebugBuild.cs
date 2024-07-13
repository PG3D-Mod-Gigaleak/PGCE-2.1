using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteIfNotDebugBuild : MonoBehaviour
{
	void Start()
	{
		if (!Debug.isDebugBuild || Application.platform == RuntimePlatform.WindowsEditor)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}
}
