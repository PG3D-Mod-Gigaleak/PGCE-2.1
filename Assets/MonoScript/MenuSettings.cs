using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuSettings : MonoBehaviour 
{
	public List<Menu> menuInfos;

	public static Menu GetCurrentMenu
	{
		get
		{
			return Defs.menuSettings.menuInfos.Find(x => x.sceneName == Defs.CurrentMainMenuScene);
		}
	}

	public static Menu GetMenu(string menu)
	{
		return Defs.menuSettings.menuInfos.Find(x => x.sceneName == menu);
	}

	[Serializable]
	public class Menu
	{
		public string sceneName;

		public AudioClip song;

		public Texture loading;
	}
}
