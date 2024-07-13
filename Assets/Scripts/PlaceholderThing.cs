using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderThing : MonoBehaviour
{
	public void DoTheThing()
	{
		LoadConnectScene.loading = null;
		LoadConnectScene.sceneToLoad = "Encyclopedia";
		Application.LoadLevel("PromScene");
	}
}
