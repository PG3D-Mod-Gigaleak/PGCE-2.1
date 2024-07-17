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
			return (PT == PrefsType.PlayerPrefs ? PlayerPrefs.GetInt(key) : Storager.getInt(key, false)) + "";
			case PrefsDataType.String:
			return (PT == PrefsType.PlayerPrefs ? PlayerPrefs.GetString(key) : Storager.getString(key, false));
			case PrefsDataType.Float:
			return (PT == PrefsType.PlayerPrefs ? PlayerPrefs.GetFloat(key) : Storager.getFloat(key, false)) + "";
		}
		return "";
	}
}
