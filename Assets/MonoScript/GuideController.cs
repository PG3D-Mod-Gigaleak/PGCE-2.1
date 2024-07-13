using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GuideController : MonoBehaviour
{
	[Serializable]
	public class GuidePage
	{
		public GuideParagraph[] guideParagraphs;

		public GuideImage[] guideImages;
	}

	[Serializable]
	public class GuideParagraph
	{
		public Rect rect;

		public string paragraph;
	}

	[Serializable]
	public class GuideImage
	{
		public Rect rect;

		public Texture texture;
	}

	public GuidePage[] AttributeGuide;

	void OnGUI()
	{
		
	}

	public void ButtonPress(string str)
	{
		switch(str)
		{
			case"testbutton":
			Debug.LogError("the");
			break;
		}
	}
	
}
