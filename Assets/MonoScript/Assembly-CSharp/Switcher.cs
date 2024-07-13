using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public sealed class Switcher : MonoBehaviour
{
	public static string LoadingInResourcesPath = "LevelLoadings";

	public static string[] loadingNames = new string[11]
	{
		"Loading_Training", "loading_Cementery", "Loading_Maze", "Loading_City", "Loading_Hospital", "Loading_Jail", "Loading_Gluk", "Loading_Arena", "Loading_Area52", "Loading_Slender",
		"Loading_Hell"
	};

	public bool NoWait;

	public bool isGameOver;

	public GameObject coinsShopPrefab;

	private Texture fonToDraw;

	public GameObject skinsManagerPrefab;

	public GameObject weaponManagerPrefab;

	public GameObject flurryPrefab;

	public GameObject backgroundMusicPrefab;

	public GameObject guiHelperPrefab;

	public Texture plashkaCoins;

	private Rect plashkaCoinsRect;

	public UITexture loadingScreen;
	
	private void Update()
	{
		loadingScreen.mainTexture = fonToDraw;
	}

	private void Start()
	{
		//add this back when it's time, but we must do this LATER!!!
		//int random = (int)UnityEngine.Random.Range(0, Defs.MainMenuScenes.Length);
		//Defs.CurrentMainMenuScene = Defs.MainMenuScenes[random];
		Defs.CurrentMainMenuScene = "Menu_DesertTemple";
		Debug.Log("0 GlobalGameController.currentLevel " + GlobalGameController.currentLevel);
		AudioListener.volume = (PlayerPrefsX.GetBool(PlayerPrefsX.SndSetting, true) ? prefs.GetFloat("setVolm", 1.0f) : 0);
		if (GlobalGameController._currentIndexInMapping >= GlobalGameController.NumOfLevels - 1 && GlobalGameController.currentLevel != 101 && !isGameOver)
		{
			GlobalGameController.reGenerateLevelMapping();
			GlobalGameController.setLevelToFirstInMapping();
			GlobalGameController.AllLevelsCompleted++;
		}
		if (GlobalGameController.currentLevel != 101 && GlobalGameController.currentLevel != -1 && !isGameOver)
		{
			GlobalGameController.numOfCompletedLevels++;
			GlobalGameController.totalNumOfCompletedLevels++;
		}
		int levelsToGetCoins = GlobalGameController.levelsToGetCoins;
		int coinsBase = GlobalGameController.coinsBase;
		int coinsBaseAdding = GlobalGameController.coinsBaseAdding;
		if (GlobalGameController.totalNumOfCompletedLevels % (GlobalGameController.NumOfLevels - 1) == 0 && GlobalGameController.AllLevelsCompleted > 0)
		{
			int val = 45;
			if (!Storager.hasKey(Defs.EarnedCoins))
			{
				Storager.setInt(Defs.EarnedCoins, val, false);
			}
			Storager.setInt(Defs.EarnedCoins, val, false);
			GlobalGameController.numOfCompletedLevels = 0;
		}
		else if (GlobalGameController.numOfCompletedLevels >= levelsToGetCoins)
		{
			int val2 = 3;
			if (!Storager.hasKey(Defs.EarnedCoins))
			{
				Storager.setInt(Defs.EarnedCoins, val2, false);
			}
			Storager.setInt(Defs.EarnedCoins, val2, false);
			Debug.Log(val2 + " coins earned");
			GlobalGameController.numOfCompletedLevels = 0;
		}
		Debug.Log("1 GlobalGameController.currentLevel " + GlobalGameController.currentLevel);
		if (isGameOver)
		{
			fonToDraw = Resources.Load("dead") as Texture;
			return;
		}
		if (GlobalGameController.currentLevel != -1 && GlobalGameController.currentLevel != 101 && (GlobalGameController.AllLevelsCompleted <= 0 || GlobalGameController.currentLevel != GlobalGameController.levelMapping[0]))
		{
			plashkaCoins = Resources.Load(Path.Combine("CoinsIndicationSystem", "lev" + (GlobalGameController._currentIndexInMapping + 1))) as Texture;
		}
		float num = ((GlobalGameController.currentLevel != GlobalGameController.levelMapping[GlobalGameController.levelMapping.Count - 1]) ? 360f : 510f) * Defs.Coef;
		float num2 = ((GlobalGameController.currentLevel != GlobalGameController.levelMapping[GlobalGameController.levelMapping.Count - 1]) ? 171f : 102f) * Defs.Coef;
		plashkaCoinsRect = new Rect(((float)Screen.width - num) / 2f, ((float)Screen.height - num2) / 2f, num, num2);
		if (!NoWait)
		{
			if (GlobalGameController.currentLevel == GlobalGameController.levelMapping[0])
			{
				fonToDraw = Resources.Load(Path.Combine(LoadingInResourcesPath, (GlobalGameController.AllLevelsCompleted != 0) ? "NextLoopFon" : Defs.GetLoadingNameFromRandomByIndex(GlobalGameController.levelMapping[0]))) as Texture;
			}
			else if (GlobalGameController.currentLevel == -1)
			{
			fonToDraw = MenuSettings.GetCurrentMenu.loading;
			}
			else if (GlobalGameController.currentLevel == 101)
			{
				fonToDraw = Resources.Load(Path.Combine(LoadingInResourcesPath, loadingNames[0])) as Texture;
			}
			else
			{
				fonToDraw = Resources.Load(Path.Combine(LoadingInResourcesPath, Defs.GetLoadingNameFromRandomByIndex(GlobalGameController.levelMapping[GlobalGameController._currentIndexInMapping]))) as Texture;
			}
		}
		else
		{
			fonToDraw = Resources.Load(Path.Combine(LoadingInResourcesPath, Defs.GetLoadingNameFromRandomByIndex((GlobalGameController.currentLevel == GlobalGameController.levelMapping[0]) ? (GlobalGameController.levelMapping[0] + 1) : 0))) as Texture;
		}
		Debug.Log("2 GlobalGameController.currentLevel " + GlobalGameController.currentLevel);
		if (!GameObject.FindGameObjectWithTag("SkinsManager") && (bool)skinsManagerPrefab)
		{
			Object.Instantiate(skinsManagerPrefab, Vector3.zero, Quaternion.identity);
		}
		if (!GameObject.FindGameObjectWithTag("WeaponManager") && (bool)weaponManagerPrefab)
		{
			Object.Instantiate(weaponManagerPrefab, Vector3.zero, Quaternion.identity);
		}
		if (!GameObject.FindGameObjectWithTag("Flurry") && (bool)flurryPrefab)
		{
			Object.Instantiate(flurryPrefab, Vector3.zero, Quaternion.identity);
		}
		if (!GameObject.FindGameObjectWithTag("MenuBackgroundMusic") && (bool)backgroundMusicPrefab)
		{
			Object.Instantiate(backgroundMusicPrefab, Vector3.zero, Quaternion.identity);
		}
		if (!GameObject.FindGameObjectWithTag("GUIHelper") && (bool)guiHelperPrefab)
		{
			Object.Instantiate(guiHelperPrefab, Vector3.zero, Quaternion.identity);
		}
		if ((bool)GameObject.FindGameObjectWithTag("CoinsShop") || !coinsShopPrefab)
		{
			return;
		}
		object obj = new object();
		Storager.Initialize(obj != null);
		if (!Storager.hasKey(Defs.initValsInKeychain))
		{
			Storager.setInt(Defs.initValsInKeychain, 0, false);
			Storager.setInt(Defs.SwordSett, 0, false);
			Storager.setInt(Defs.MinerWeaponSett, 0, false);
			Storager.setInt(Defs.InAppBoughtSett, 0, false);
			Storager.setInt(Defs.CombatRifleSett, 0, false);
			Storager.setInt(Defs.GoldenEagleSett, 0, false);
			Storager.setInt(Defs.MagicBowSett, 0, false);
			Storager.setInt(Defs.SPASSett, 0, false);
			Storager.setInt(Defs.GoldenAxeSett, 0, false);
			foreach (KeyValuePair<string, string> value in InAppData.inAppData.Values)
			{
				if (!Storager.hasKey(value.Value))
				{
					Storager.setInt(value.Value, 0, false);
				}
			}
			if (prefs.GetInt(Defs.SwordSett, 0) > 0)
			{
				Storager.setInt(Defs.SwordSett, prefs.GetInt(Defs.SwordSett, 0), true);
			}
			if (prefs.GetInt(Defs.MinerWeaponSett, 0) > 0)
			{
				Storager.setInt(Defs.MinerWeaponSett, prefs.GetInt(Defs.MinerWeaponSett, 0), true);
			}
			if (prefs.GetInt(Defs.CombatRifleSett, 0) > 0)
			{
				Storager.setInt(Defs.CombatRifleSett, prefs.GetInt(Defs.CombatRifleSett, 0), true);
			}
			if (prefs.GetInt(Defs.GoldenEagleSett, 0) > 0)
			{
				Storager.setInt(Defs.GoldenEagleSett, prefs.GetInt(Defs.GoldenEagleSett, 0), true);
			}
			if (prefs.GetInt(Defs.MagicBowSett, 0) > 0)
			{
				Storager.setInt(Defs.MagicBowSett, prefs.GetInt(Defs.MagicBowSett, 0), true);
			}
			if (prefs.GetInt(Defs.SPASSett, 0) > 0)
			{
				Storager.setInt(Defs.SPASSett, prefs.GetInt(Defs.SPASSett, 0), true);
			}
			if (prefs.GetInt(Defs.GoldenAxeSett, 0) > 0)
			{
				Storager.setInt(Defs.GoldenAxeSett, prefs.GetInt(Defs.GoldenAxeSett, 0), true);
			}
			foreach (KeyValuePair<string, string> value2 in InAppData.inAppData.Values)
			{
				if (prefs.GetInt(value2.Value, 0) > 0)
				{
					Storager.setInt(value2.Value, prefs.GetInt(value2.Value, 0), true);
				}
			}
		}
		if (!Storager.hasKey(Defs.initValsInKeychain2))
		{
			Storager.setInt(Defs.initValsInKeychain2, 0, false);
			Storager.setInt(Defs.ChainsawS, 0, false);
			Storager.setInt(Defs.FAMASS, 0, false);
			Storager.setInt(Defs.GlockSett, 0, false);
		}
		if (!Storager.hasKey(Defs.initValsInKeychain3))
		{
			Storager.setInt(Defs.initValsInKeychain3, 0, false);
			Storager.setInt(Defs.ScytheSN, 0, false);
			Storager.setInt(Defs.ShovelSN, 0, false);
		}
		if (!Storager.hasKey(Defs.initValsInKeychain4))
		{
			Storager.setInt(Defs.initValsInKeychain4, 0, false);
			Storager.setInt(Defs.Sword_2_SN, 0, false);
			Storager.setInt(Defs.HammerSN, 0, false);
		}
		if (!Storager.hasKey(Defs.initValsInKeychain5))
		{
			Storager.setInt(Defs.initValsInKeychain5, 0, false);
			Storager.setInt(Defs.StaffSN, 0, false);
		}
		if (!Storager.hasKey(Defs.initValsInKeychain6))
		{
			Storager.setInt(Defs.initValsInKeychain6, 0, false);
			Storager.setInt(Defs.LaserRifleSN, 0, false);
		}
		if (!Storager.hasKey(Defs.initValsInKeychain7))
		{
			Storager.setInt(Defs.initValsInKeychain7, 0, false);
			Storager.setInt(Defs.SkinsMakerInProfileBought, 0, false);
		}
		if (!Storager.hasKey(Defs.initValsInKeychain8))
		{
			Storager.setInt(Defs.initValsInKeychain8, 0, false);
			Storager.setInt(Defs.LightSwordSN, 0, false);
			Storager.setInt(Defs.BerettaSN, 0, false);
		}
		if (!Storager.hasKey(Defs.initValsInKeychain9))
		{
			Storager.setInt(Defs.initValsInKeychain9, 0, false);
			Storager.setInt(Defs.MaceSN, 0, false);
			Storager.setInt(Defs.CrossbowSN, 0, false);
			Storager.setInt(Defs.MinigunSN, 0, false);
		}
		float num3 = 2f;
		int num4 = 5;
		string text = "640111933";
		if (!Storager.hasKey(Defs.Coins))
		{
			int val3 = ((!Defs.IsProEdition) ? 15 : 30);
			Storager.setInt(Defs.Coins, val3, false);
		}
		if (Storager.getInt(Defs.SwordSett, true) > 0)
		{
			Storager.setInt(Defs.SwordSett, Storager.getInt(Defs.SwordSett, true), true);
		}
		if (Storager.getInt(Defs.MinerWeaponSett, true) > 0)
		{
			Storager.setInt(Defs.MinerWeaponSett, Storager.getInt(Defs.MinerWeaponSett, true), true);
		}
		if (Storager.getInt(Defs.CombatRifleSett, true) > 0)
		{
			Storager.setInt(Defs.CombatRifleSett, Storager.getInt(Defs.CombatRifleSett, true), true);
		}
		if (Storager.getInt(Defs.GoldenEagleSett, true) > 0)
		{
			Storager.setInt(Defs.GoldenEagleSett, Storager.getInt(Defs.GoldenEagleSett, true), true);
		}
		if (Storager.getInt(Defs.MagicBowSett, true) > 0)
		{
			Storager.setInt(Defs.MagicBowSett, Storager.getInt(Defs.MagicBowSett, true), true);
		}
		if (Storager.getInt(Defs.SPASSett, true) > 0)
		{
			Storager.setInt(Defs.SPASSett, Storager.getInt(Defs.SPASSett, true), true);
		}
		if (Storager.getInt(Defs.GoldenAxeSett, true) > 0)
		{
			Storager.setInt(Defs.GoldenAxeSett, Storager.getInt(Defs.GoldenAxeSett, true), true);
		}
		if (Storager.getInt(Defs.ChainsawS, true) > 0)
		{
			Storager.setInt(Defs.ChainsawS, Storager.getInt(Defs.ChainsawS, true), true);
		}
		if (Storager.getInt(Defs.GlockSett, true) > 0)
		{
			Storager.setInt(Defs.GlockSett, Storager.getInt(Defs.GlockSett, true), true);
		}
		if (Storager.getInt(Defs.FAMASS, true) > 0)
		{
			Storager.setInt(Defs.FAMASS, Storager.getInt(Defs.FAMASS, true), true);
		}
		if (Storager.getInt(Defs.ScytheSN, true) > 0)
		{
			Storager.setInt(Defs.ScytheSN, Storager.getInt(Defs.ScytheSN, true), true);
		}
		if (Storager.getInt(Defs.ShovelSN, true) > 0)
		{
			Storager.setInt(Defs.ShovelSN, Storager.getInt(Defs.ShovelSN, true), true);
		}
		if (Storager.getInt(Defs.HammerSN, true) > 0)
		{
			Storager.setInt(Defs.HammerSN, Storager.getInt(Defs.HammerSN, true), true);
		}
		if (Storager.getInt(Defs.Sword_2_SN, true) > 0)
		{
			Storager.setInt(Defs.Sword_2_SN, Storager.getInt(Defs.Sword_2_SN, true), true);
		}
		if (Storager.getInt(Defs.StaffSN, true) > 0)
		{
			Storager.setInt(Defs.StaffSN, Storager.getInt(Defs.StaffSN, true), true);
		}
		if (Storager.getInt(Defs.LaserRifleSN, true) > 0)
		{
			Storager.setInt(Defs.LaserRifleSN, Storager.getInt(Defs.LaserRifleSN, true), true);
		}
		if (Storager.getInt(Defs.SkinsMakerInProfileBought, true) > 0)
		{
			Storager.setInt(Defs.SkinsMakerInProfileBought, Storager.getInt(Defs.SkinsMakerInProfileBought, true), true);
		}
		if (Storager.getInt(Defs.LightSwordSN, true) > 0)
		{
			Storager.setInt(Defs.LightSwordSN, Storager.getInt(Defs.LightSwordSN, true), true);
		}
		if (Storager.getInt(Defs.BerettaSN, true) > 0)
		{
			Storager.setInt(Defs.BerettaSN, Storager.getInt(Defs.BerettaSN, true), true);
		}
		if (Storager.getInt(Defs.MaceSN, true) > 0)
		{
			Storager.setInt(Defs.MaceSN, Storager.getInt(Defs.MaceSN, true), true);
		}
		if (Storager.getInt(Defs.CrossbowSN, true) > 0)
		{
			Storager.setInt(Defs.CrossbowSN, Storager.getInt(Defs.CrossbowSN, true), true);
		}
		if (Storager.getInt(Defs.MinigunSN, true) > 0)
		{
			Storager.setInt(Defs.MinigunSN, Storager.getInt(Defs.MinigunSN, true), true);
		}
		foreach (KeyValuePair<string, string> value3 in InAppData.inAppData.Values)
		{
			if (Storager.getInt(value3.Value, true) > 0)
			{
				Storager.setInt(value3.Value, Storager.getInt(value3.Value, true), true);
			}
		}
		Object.Instantiate(coinsShopPrefab);
		if (NoWait)
		{
			LoadMenu();
		}
		else
		{
			Invoke("LoadMenu", Time.unscaledDeltaTime);
		}
	}

	private void Method()
	{
	}

	private void OnGUI()
	{
		//int depth = GUI.depth;
		//if (isGameOver)
		//{
		//	GUI.depth = 4;
		//}
		//Rect position = new Rect(((float)Screen.width - 2048f * (float)Screen.height / 1154f) / 2f, 0f, 2048f * (float)Screen.height / 1154f, Screen.height);
		//GUI.DrawTexture(position, fonToDraw, ScaleMode.StretchToFill);
		//if (plashkaCoins != null)
		//{
		//	GUI.DrawTexture(plashkaCoinsRect, plashkaCoins, ScaleMode.StretchToFill);
		//}
	}

	[Beebyte.Obfuscator.SkipRename]
	private void LoadMenu()
	{
		StartCoroutine(a());
	}
	private IEnumerator a()
	{
		string text;
		switch (GlobalGameController.currentLevel)
		{
		case -1:
			text = Defs.CurrentMainMenuScene;
			break;
		case 0:
			text = Defs.GetSceneNameFromRandomByIndex(0);
			break;
		case 1:
			text = Defs.GetSceneNameFromRandomByIndex(1);
			break;
		case 2:
			text = Defs.GetSceneNameFromRandomByIndex(2);
			break;
		case 3:
			text = Defs.GetSceneNameFromRandomByIndex(3);
			break;
		case 4:
			text = Defs.GetSceneNameFromRandomByIndex(4);
			break;
		case 5:
			text = Defs.GetSceneNameFromRandomByIndex(5);
			break;
		case 6:
			text = Defs.GetSceneNameFromRandomByIndex(6);
			break;
		case 7:
			text = Defs.GetSceneNameFromRandomByIndex(7);
			break;
		case 101:
			text = "Training";
			break;
		case 8:
			text = Defs.GetSceneNameFromRandomByIndex(8);
			break;
		case 9:
			text = Defs.GetSceneNameFromRandomByIndex(9);
			break;
		default:
			text = Defs.CurrentMainMenuScene;
			break;
		}
		if (GlobalGameController.currentLevel == -1)
		{
			int @int = prefs.GetInt(Defs.TrainingComplSett, 0);
			GlobalGameController.currentLevel = ((@int != 1) ? 101 : GlobalGameController.levelMapping[0]);
		}
		else if (GlobalGameController.currentLevel == 101)
		{
			GlobalGameController.currentLevel = GlobalGameController.levelMapping[0];
		}
		else
		{
			prefs.SetInt(Defs.TrainingComplSett, 1);
			prefs.Save();
			GlobalGameController.incrementLevel();
		}
		Debug.Log("3 GlobalGameController.currentLevel " + GlobalGameController.currentLevel);
		yield return Application.LoadLevelAsync(text);
		yield break;
	}
}
