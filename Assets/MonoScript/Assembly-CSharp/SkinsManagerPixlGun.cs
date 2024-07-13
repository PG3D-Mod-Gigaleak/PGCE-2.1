using System.Collections;
using UnityEngine;

public class SkinsManagerPixlGun : MonoBehaviour
{
	public Hashtable skins = new Hashtable();

	private void OnLevelWasLoaded(int idx)
	{
		try {
			Debug.Log("OnLevelWasLoaded");
			if (skins.Count > 0)
			{
				skins.Clear();
				Debug.Log("Clear");
			}
			string path = ((prefs.GetInt("COOP", 0) != 0) ? Defs.GetThisCoopPath(Application.loadedLevelName) : ("EnemySkins/Level" + ((GlobalGameController.currentLevel != GlobalGameController.levelMapping[0]) ? (GlobalGameController.previousLevel + 1) : 0)));
			Object[] array = Resources.LoadAll(path);
			Object[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string the = "";
				if (Defs.InCoopScene && prefs.GetInt("COOP", 0) == 1 && !Defs.IsDefaultCoopSettings)
				{
					the = Application.loadedLevelName;
				}
				Texture texture = array2[i] as Texture;
				if (texture.name.Contains("_Level") || Defs.InCoopScene && prefs.GetInt("COOP", 0) == 1)
				{
					if (!skins.ContainsKey(texture.name + the))
					{
						skins.Add(texture.name + the, texture);
					}
				}
			}
		} catch {
			Debug.Log("this code makes me wanna kill myself");
		}
	}

	private void Start()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}
}
