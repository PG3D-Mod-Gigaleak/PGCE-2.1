using UnityEngine;

public class PlayerPrefsX
{
	public static string SndSetting
	{
		get
		{
			return "SndSetting";
		}
	}

	public static void SetBool(string name, bool booleanValue)
	{
		prefs.SetInt(name, booleanValue ? 1 : 0);
	}

	public static bool GetBool(string name)
	{
		return prefs.GetInt(name) == 1;
	}

	public static bool GetBool(string name, bool defaultValue)
	{
		if (prefs.HasKey(name))
		{
			return GetBool(name);
		}
		return defaultValue;
	}
}
