using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class prefs {
	private static Dictionary<string, object> values = default(Dictionary<string, object>);
	private static bool loaded = false;
	private static void EnsureLoaded() {
		//if (!loaded) {
		//	values = new Dictionary<string, object>();
		//	values.Add("initialized", 1);
		//	if (!File.Exists(Application.persistentDataPath + "/lprefs"))
		//		File.WriteAllText(Application.persistentDataPath + "/lprefs", JsonConvert.SerializeObject(values));
		//	values = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(Application.persistentDataPath + "/lprefs"));
		//	loaded = true;
		//}
	}
	public static void Save() {
		//EnsureLoaded();
		//string resultText = JsonConvert.SerializeObject(values);
		//File.WriteAllText(Application.persistentDataPath + "/lprefs", resultText);
		PlayerPrefs.Save();
	}
	public static bool HasKey(string key) {
		//EnsureLoaded();
		//return values.ContainsKey(key);
		return PlayerPrefs.HasKey(key);
	}
	public static void DeleteKey(string key) {
		//EnsureLoaded();
		//if (prefs.HasKey(key)) {
		//	values.Remove(key);
		//}
		//Save();
		PlayerPrefs.DeleteKey(key);
	}
	public static void DeleteAll() {
		//EnsureLoaded();
		//values.Clear();
		//Save();
		PlayerPrefs.DeleteAll();
	}
	#region GET
	public static float GetFloat(string key, float defaultValue = 0) {
		//EnsureLoaded();
		//if (prefs.HasKey(key)) {
		//	return System.Convert.ToSingle(values[key]);
		//}
		//return defaultValue;
		return PlayerPrefs.GetFloat(key);
	}
	public static int GetInt(string key, int defaultValue = 0) {
		//EnsureLoaded();
		//if (prefs.HasKey(key)) {
		//	return System.Convert.ToInt32(values[key]);
		//}
		//return defaultValue;
		return PlayerPrefs.GetInt(key);
	}
	public static string GetString(string key, string defaultValue = "") {
		//EnsureLoaded();
		//if (prefs.HasKey(key)) {
		//	return System.Convert.ToString(values[key]);
		//}
		//return defaultValue;
		return PlayerPrefs.GetString(key);
	}
	#endregion
	#region SET
	public static void SetFloat(string key, float value) {
		//EnsureLoaded();
		//values[key] = value;
		//Save();
		PlayerPrefs.SetFloat(key, value);
	}
	public static void SetInt(string key, int value) {
		//EnsureLoaded();
		//values[key] = value;
		//Save();
		PlayerPrefs.SetInt(key, value);
	}
	public static void SetString(string key, string value) {
		//EnsureLoaded();
		//values[key] = value;
		//Save();
		PlayerPrefs.SetString(key, value);
	}
	#endregion
}
