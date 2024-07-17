using UnityEngine;

public class FlurryPluginWrapper : MonoBehaviour
{
	public static string ModeEnteredEvent = "ModeEnteredEvent";

	public static string MapEnteredEvent = "MapEnteredEvent";

	public static string MapNameParameter = "MapName";

	public static string ModeParameter = "Mode";

	public static string ModePressedEvent = "ModePressed";

	public static string SocialEventName = "Post to Social";

	public static string SocialParName = "Service";

	public static string AppVersionParameter = "App_version";

	public static string MultiplayerWayDeaathmatchEvent = "Way_to_start_multiplayer_DEATHMATCH";

	public static string MultiplayerWayCOOPEvent = "Way_to_start_multiplayer_COOP";

	public static string WayName = "Button";

	public static string FreeCoinsEv = "FreeCoins";

	public static string FreeCoinsParName = "type";

	public static string RateUsEv = "Rate_Us";

	public static string MultiplayerWayEvent
	{
		get
		{
			return (PlayerPrefs.GetInt("COOP", 0) != 1) ? MultiplayerWayDeaathmatchEvent : MultiplayerWayCOOPEvent;
		}
	}

	public static void LogEventWithParameterAndValue(string ev, string pat, string val)
	{
	}

	public static void LogModeEventWithValue(string val)
	{
		LogEventWithParameterAndValue(ModePressedEvent, ModeParameter, val);
	}

	public static void LogMultiplayerWayStart()
	{
		LogEventWithParameterAndValue(MultiplayerWayEvent, WayName, "Start");
	}

	public static void LogMultiplayerWayQuckRandGame()
	{
		LogEventWithParameterAndValue(MultiplayerWayEvent, WayName, "Quick_rand_game");
	}

	public static void LogMultiplayerWayCustom()
	{
		LogEventWithParameterAndValue(MultiplayerWayEvent, WayName, "Custom");
	}

	public static void LogDeathmatchModePress()
	{
		LogModeEventWithValue("Deathmatch");
	}

	public static void LogSurvivalModePress()
	{
		LogModeEventWithValue("Survival");
	}

	public static void LogCOOPModePress()
	{
		LogModeEventWithValue("COOP");
	}

	public static void LogSkinsMakerModePress()
	{
		LogModeEventWithValue("Skins_Maker");
	}

	public static void LogTwitter()
	{
		LogEventWithParameterAndValue(SocialEventName, SocialParName, "Twitter");
	}

	public static void LogFacebook()
	{
		LogEventWithParameterAndValue(SocialEventName, SocialParName, "Facebook");
	}

	public static void LogGamecenter()
	{
		LogEventWithParameterAndValue(SocialEventName, SocialParName, "Gamecenter");
	}

	public static void LogFreeCoinsFacebook()
	{
		LogEventWithParameterAndValue(FreeCoinsEv, FreeCoinsParName, "Facebook");
	}

	public static void LogFreeCoinsTwitter()
	{
		LogEventWithParameterAndValue(FreeCoinsEv, FreeCoinsParName, "Twitter");
	}

	public static void LogFreeCoinsYoutube()
	{
		LogEventWithParameterAndValue(FreeCoinsEv, FreeCoinsParName, "Youtube");
	}

	public static void LogCoinEarned()
	{
	}

	public static void LogCoinEarned_COOP()
	{
	}

	public static void LogCoinEarned_Deathmatch()
	{
	}

	public static void LogFreeCoinsRateUs()
	{
	}

	public static void LogSkinsMakerEnteredEvent()
	{
	}

	public static void LogAddYourSkinBoughtEvent()
	{
	}

	public static void LogAddYourSkinTriedToBoughtEvent()
	{
	}

	public static void LogAddYourSkinUsedEvent()
	{
	}

	public static void LogCategoryEnteredEvent(string catName)
	{
		LogEventWithParameterAndValue("Dhop_Category", "Category_name", catName);
	}

	public static void LogEnteringMap(int typeConnect, string mapName)
	{
	}

	private void Start()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}
	
	public void Update() {
		AudioListener.volume = PlayerPrefs.GetFloat("setVolm", 1.0f);
	}
}
