using UnityEngine;

public class FlurryEventListener : MonoBehaviour
{
	public static string ModeEnteredEvent = "ModeEnteredEvent";

	public static string MapEnteredEvent = "MapEnteredEvent";

	public static string MapNameParameter = "MapName";

	public static string ModeParameter = "Mode";

	public static string ModePressedEvent = "ModePressed";

	public static string SocialEventName = "Post to Social";

	public static string SocialParName = "Service";

	public static string AppVersionParameter = "App_version";

	public static string MultiplayerWayEvent = "Way_to_start_multiplayer";

	public static string WayName = "Button";

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

	public static void LogEnteringMap(int typeConnect, string mapName)
	{
	}

	private void Start()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}
}
