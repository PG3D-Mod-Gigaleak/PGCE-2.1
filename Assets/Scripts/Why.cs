using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Why : MonoBehaviour
{
	void Start()
	{	
		if (GetComponent<UITexture>() != null)
		{
			GetComponent<UITexture>().enabled = false;
			GetComponent<UITexture>().enabled = true;
			return;
		}
		GetComponent<UISprite>().enabled = false;
		GetComponent<UISprite>().enabled = true;
	}
}
