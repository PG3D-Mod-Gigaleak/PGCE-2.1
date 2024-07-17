using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPrefs
{
	public static string CurrentSkin
	{
		get
		{
			if (PlayerPrefs.GetString("base64_multskin") == "")
			{
				PlayerPrefs.SetString("base64_multskin", System.Convert.ToBase64String(ImageConversion.EncodeToPNG(Resources.Load<Texture2D>("multiplayer skins/multi_skin_1"))));
			}
			return PlayerPrefs.GetString("base64_multskin");
		}
		set
		{
			PlayerPrefs.SetString("base64_multskin", value);
		}
	}
}
