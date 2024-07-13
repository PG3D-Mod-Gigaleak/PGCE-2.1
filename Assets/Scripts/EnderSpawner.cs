using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnderSpawner : MonoBehaviour
{
	public GameObject cake, drink;
	
	public Transform objectStack, pointStack, enderStack;

	void Awake()
	{
		instance = this;
	}

	void Update()
	{
		if (Defs.isMulti)
		{
			if (PhotonNetwork.isMasterClient && !GameObject.FindGameObjectWithTag("EnderAI"))
			{
				GameObject ender = PhotonNetwork.Instantiate("EnderAI", Vector3.zero, Quaternion.identity, 0);
			}
		}
	}

	public static EnderSpawner instance;
}
