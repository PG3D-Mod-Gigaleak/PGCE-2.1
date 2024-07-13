using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsLabelUpdater : MonoBehaviour
{
	public PrefsType prefsType;

	public PrefsDataType prefsDataType;

	public string key;

	void Update() 
	{
		GetComponent<UILabel>().text = GetFromPrefsDataType(prefsType, prefsDataType, key);
	}

	public string GetFromPrefsDataType(PrefsType PT, PrefsDataType PDT, string key)
	{
		switch (PDT)
		{
			case PrefsDataType.Integer:
			return (PT == PrefsType.PlayerPrefs ? prefs.GetInt(key) : Storager.getInt(key, false)) + "";
			case PrefsDataType.String:
			return (PT == PrefsType.PlayerPrefs ? prefs.GetString(key) : Storager.getString(key, false));
			case PrefsDataType.Float:
			return (PT == PrefsType.PlayerPrefs ? prefs.GetFloat(key) : Storager.getFloat(key, false)) + "";
		}
		return "";
	}
}
