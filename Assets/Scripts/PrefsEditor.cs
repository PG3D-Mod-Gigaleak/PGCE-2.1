#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PrefsEditor : Editor
{
	[MenuItem("LinuxPrefs/Clear")]
	public static void ClearPrefs()
	{
		prefs.DeleteAll();
	}
}
#endif