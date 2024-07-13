using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skins : ScriptableObject
{
	[System.Serializable]
	public class Skin
	{
		public string skin;

		public Texture skinTexture;

		public Color color = new Color(1, 1, 1, 1);

		public Skin(string name, Texture skinTex, Color clr)
		{
			skin = name;
			skinTexture = skinTex;
			color = clr;
		}
	}

	public Skin[] skins;
}
