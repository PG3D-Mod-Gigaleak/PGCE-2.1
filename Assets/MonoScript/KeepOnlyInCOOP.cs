using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOnlyInCOOP : MonoBehaviour
{
	void Start()
	{
		if (PlayerPrefs.GetInt("COOP") == 0)
		{
			Destroy(gameObject);
		}
	}
}
