using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPrefs
{
	public static string CurrentSkin
	{
		get
		{
			if (prefs.GetString("base64_multskin") == "")
			{
				prefs.SetString("base64_multskin", System.Convert.ToBase64String(ImageConversion.EncodeToPNG(Resources.Load<Texture2D>("multiplayer skins/multi_skin_1"))));
			}
			return prefs.GetString("base64_multskin");
		}
		set
		{
			prefs.SetString("base64_multskin", value);
		}
	}
}
