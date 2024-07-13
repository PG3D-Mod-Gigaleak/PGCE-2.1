using System;
using UnityEngine;

public class MenuBackgroundMusic : MonoBehaviour
{
	private static string[] scenetsToPlayMusicOn = new string[]
	{
		"ConnectScene",
		"SettingScene",
		"NewArmoryScene",
		"PromScene",
		"NewMapList",
		//"ArmoryScene",
		//"PrimaryScene",
		//"BackupScene",
		//"MeleeScene",
		//"SpecialScene",
		//"PremiumScene",
		//"GearScene"
	};

	public static MenuBackgroundMusic Instance;
	public static AudioSource myAudioSOurce;

	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Update()
	{
		Instance = base.GetComponent<MenuBackgroundMusic>();
		myAudioSOurce = base.GetComponent<AudioSource>();
	}

	private void OnLevelWasLoaded(int idx)
	{
		if (Array.IndexOf(scenetsToPlayMusicOn, Application.loadedLevelName) >= 0 || Application.loadedLevelName.StartsWith("Menu_"))
		{
			if (!base.GetComponent<AudioSource>().isPlaying && (Array.IndexOf(Defs.MainMenuScenes, Application.loadedLevelName) >= 0))
			{
				base.GetComponent<AudioSource>().clip = MenuSettings.GetCurrentMenu.song;
				base.GetComponent<AudioSource>().Play();
			}
			if (!base.GetComponent<AudioSource>().isPlaying && !(Array.IndexOf(Defs.MainMenuScenes, Application.loadedLevelName) >= 0))
			{
				base.GetComponent<AudioSource>().Play();
			}
		}
		else
		{
			base.GetComponent<AudioSource>().Stop();
		}
	}
}
