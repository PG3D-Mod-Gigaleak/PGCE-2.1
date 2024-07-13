using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Prime31;
using Rilisoft;
using UnityEngine;

public sealed class MainMenu : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDrawGameModeButtons_003Ec__AnonStorey1B
	{
		internal float buttonsLeft;

		internal float buttonsTop;

		internal float rowHeight;

		internal float buttonWidth;

		internal float buttonHeight;

		internal Rect _003C_003Em__5(int rowIndex)
		{
			return new Rect(buttonsLeft, buttonsTop + (float)rowIndex * rowHeight, buttonWidth, buttonHeight);
		}
	}

	[CompilerGenerated]
	private sealed class _003CDrawToolbar_003Ec__AnonStorey1C
	{
		internal float toolbarLeft;

		internal float columnWidth;

		internal float toolbarTop;

		internal float smallButtonWidth;

		internal float smallButtonHeight;

		internal Rect _003C_003Em__6(int columnIndex)
		{
			return new Rect(toolbarLeft + (float)columnIndex * columnWidth, toolbarTop, smallButtonWidth, smallButtonHeight);
		}
	}

	private const string SkinsMakerSku = "skinsmaker";

	public GUIStyle coopStyle;

	public GUIStyle facebookStyle;

	public GUIStyle gamecenterStyle;

	public GUIStyle playMultyStyle;

	public GUIStyle playStyle;

	public GUIStyle skinsMakerStyle;

	public GUIStyle skinsMakerOffStyle;

	public GUIStyle soundStyle;

	public GUIStyle twitterStyle;

	public GUIStyle ArmoryStyle;

	public Texture fon;

	public Texture bottomShadow;

	private bool showFreeCoins;

	private bool _skinsMakerQuerySucceeded;

	private GameObject _inAppGameObject;

	private StoreKitEventListener _listener;

	public Texture inAppFon;

	public GUIStyle puliInApp;

	public GUIStyle healthInApp;

	public GUIStyle pulemetInApp;

	public GUIStyle crystalSwordInapp;

	public GUIStyle elixirInapp;

	public bool isFirstFrame = true;

	public bool isInappWinOpen;

	private bool productPurchased;

	private Dictionary<string, KeyValuePair<Action, GUIStyle>> _actionsForPurchasedItems = new Dictionary<string, KeyValuePair<Action, GUIStyle>>();

	private bool showUnlockDialog;

	private bool isPressFullOnMulty;

	private string[] productIdentifiers = StoreKitEventListener.idsForFull;

	private float _timeWhenPurchShown;

	public Texture plashkaPodScore;

	public Texture playMultyStyleNO;

	public Texture fonFull;

	public Texture fonFree;

	public Texture head_Free;

	public GameObject skinsManagerPrefab;

	public GameObject weaponManagerPrefab;

	public GUIStyle unlockStyle;

	public GUIStyle noStyle;

	public GUIStyle fullVerStyle;

	public GUIStyle bestScoreStyle;

	public GUIStyle labelStyle;

	public GUIStyle f_Free;

	public GUIStyle t_Free;

	public GUIStyle rate_Free;

	public GUIStyle coins_Free;

	public GUIStyle tube_free;

	public GUIStyle backBut;

	private bool showMessagFacebook;

	private bool showMessagTiwtter;

	private GameObject _purchaseActivityIndicator;

	public Texture freeFaceOff;

	public Texture freeTwitterOff;

	public Texture freeTubeOff;

	private bool clickButtonFacebook;

	private bool _canUserUseFacebookComposer;

	private bool _hasPublishPermission;

	private bool _hasPublishActions;

	[CompilerGenerated]
	private static Func<GooglePurchase, string> _003C_003Ef__am_0024cache39;

	[CompilerGenerated]
	private static Func<GoogleSkuInfo, string> _003C_003Ef__am_0024cache3A;

	[CompilerGenerated]
	private static Func<GooglePurchase, bool> _003C_003Ef__am_0024cache3B;

	public static int FontSizeForMessages
	{
		get
		{
			return Mathf.RoundToInt((float)Screen.height * 0.03f);
		}
	}

	public static float iOSVersion
	{
		get
		{
			float result = -1f;
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				string text = SystemInfo.operatingSystem.Replace("iPhone OS ", string.Empty);
				float.TryParse(text.Substring(0, 1), out result);
			}
			return result;
		}
	}

	private static string ReadPrefsFileToString()
	{
		//Discarded unreachable code: IL_0023, IL_0035
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	try
		//	{
		//		using (StreamReader streamReader = File.OpenText("/data/data/com.P3D.Pixlgun/shared_prefs/com.P3D.Pixlgun.xml"))
		//		{
		//			return streamReader.ReadToEnd();
		//		}
		//	}
		//	catch (Exception exception)
		//	{
		//		Debug.LogException(exception);
		//	}
		//}
		return string.Empty;
	}

	private void DrawGameModeButtons(float rightBorder)
	{
		_003CDrawGameModeButtons_003Ec__AnonStorey1B _003CDrawGameModeButtons_003Ec__AnonStorey1B = new _003CDrawGameModeButtons_003Ec__AnonStorey1B();
		float num = (float)Screen.height / 768f;
		_003CDrawGameModeButtons_003Ec__AnonStorey1B.buttonWidth = (float)playStyle.normal.background.width * num;
		_003CDrawGameModeButtons_003Ec__AnonStorey1B.buttonHeight = (float)playStyle.normal.background.height * num;
		_003CDrawGameModeButtons_003Ec__AnonStorey1B.buttonsTop = 222f * num;
		_003CDrawGameModeButtons_003Ec__AnonStorey1B.buttonsLeft = rightBorder - _003CDrawGameModeButtons_003Ec__AnonStorey1B.buttonWidth;
		float num2 = 18f * num;
		_003CDrawGameModeButtons_003Ec__AnonStorey1B.rowHeight = _003CDrawGameModeButtons_003Ec__AnonStorey1B.buttonHeight + num2;
		Func<int, Rect> func = _003CDrawGameModeButtons_003Ec__AnonStorey1B._003C_003Em__5;
		int arg = 0;
		if (GUI.RepeatButton(func(arg), string.Empty, playStyle))
		{
			GUIHelper.DrawLoading();
			prefs.SetInt("MultyPlayer", 0);
			prefs.SetInt("COOP", 0);
			GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().Reset();
			prefs.SetFloat(Defs.CurrentHealthSett, Player_move_c.MaxPlayerHealth);
			prefs.SetFloat(Defs.CurrentArmorSett, 0f);
			FlurryPluginWrapper.LogSurvivalModePress();
			Application.LoadLevel("LoadingNoWait");
		}
		int arg2 = 1;
		if (GUI.RepeatButton(func(arg2), string.Empty, playMultyStyle))
		{
			GUIHelper.DrawLoading();
			prefs.SetInt("MultyPlayer", 1);
			prefs.SetInt("COOP", 0);
			GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().Reset();
			FlurryPluginWrapper.LogDeathmatchModePress();
			Application.LoadLevel("NewMapList");
		}
		int arg3 = 2;
		if (GUI.RepeatButton(func(arg3), string.Empty, coopStyle))
		{
			GUIHelper.DrawLoading();
			prefs.SetString("TypeConnect", "inet");
			prefs.SetInt("COOP", 1);
			prefs.SetInt("MultyPlayer", 1);
			GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().Reset();
			FlurryPluginWrapper.LogCOOPModePress();
			Application.LoadLevel("NewMapList");
		}
		int arg4 = 3;
		if (GUI.RepeatButton(func(arg4), string.Empty, ArmoryStyle))
		{
			GUIHelper.DrawLoading();
			GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().Reset();
			Application.LoadLevel("NewArmoryScene");
		}
	}

	private void DrawToolbar(float rightBorder)
	{
		_003CDrawToolbar_003Ec__AnonStorey1C _003CDrawToolbar_003Ec__AnonStorey1C = new _003CDrawToolbar_003Ec__AnonStorey1C();
		float num = (float)Screen.height / 768f;
		_003CDrawToolbar_003Ec__AnonStorey1C.toolbarLeft = 0.5f * (float)Screen.width - 423f * num;
		_003CDrawToolbar_003Ec__AnonStorey1C.toolbarTop = 682f * num;
		_003CDrawToolbar_003Ec__AnonStorey1C.smallButtonWidth = (float)soundStyle.normal.background.width * num;
		_003CDrawToolbar_003Ec__AnonStorey1C.smallButtonHeight = (float)soundStyle.normal.background.height * num;
		float num2 = 30f * num;
		_003CDrawToolbar_003Ec__AnonStorey1C.columnWidth = _003CDrawToolbar_003Ec__AnonStorey1C.smallButtonWidth + num2;
		Func<int, Rect> func = _003CDrawToolbar_003Ec__AnonStorey1C._003C_003Em__6;
		int arg = 0;
		if (GUI.RepeatButton(func(arg), string.Empty, soundStyle) && !isFirstFrame)
		{
			GUIHelper.DrawLoading();
			Application.LoadLevel("SettingScene");
		}
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			int arg2 = 1;
			if (GUI.Button(func(arg2), string.Empty, gamecenterStyle))
			{
				FlurryPluginWrapper.LogGamecenter();
				if (Application.isEditor)
				{
				}
			}
		}
		float num3 = (float)skinsMakerStyle.normal.background.width * num;
		if (true)
		{
			Rect position = new Rect(rightBorder - num3, _003CDrawToolbar_003Ec__AnonStorey1C.toolbarTop, num3, (float)skinsMakerStyle.normal.background.height * num);
			bool flag = Defs.IsProEdition || prefs.GetInt(Defs.SkinsMakerInMainMenuPurchased) > 0;
			GUIStyle style = skinsMakerStyle;
			if ((Application.platform != RuntimePlatform.Android) ? GUI.RepeatButton(position, string.Empty, skinsMakerStyle) : GUI.Button(position, string.Empty, style))
			{
				if (flag)
				{
					GUIHelper.DrawLoading();
					prefs.SetInt(Defs.SkinEditorMode, 0);
					FlurryPluginWrapper.LogSkinsMakerModePress();
					FlurryPluginWrapper.LogSkinsMakerEnteredEvent();
					Application.LoadLevel("SkinEditor");
				}
				else if (_skinsMakerQuerySucceeded)
				{
					GoogleIABManager.purchaseSucceededEvent += HandleSkinsMakerPurchaseSucceededEvent;
					GoogleIABManager.purchaseFailedEvent += HandleSkinsMakerPurchasePurchaseFailedEvent;
					GoogleIAB.purchaseProduct("skinsmaker");
				}
				else
				{
					Debug.Log("Skins Maker query not performed yet.");
				}
			}
		}
		float num4 = (float)coins_Free.normal.background.width * num;
		if (true)
		{
			Rect position2 = new Rect(rightBorder - num3 - num4 - _003CDrawToolbar_003Ec__AnonStorey1C.columnWidth, _003CDrawToolbar_003Ec__AnonStorey1C.toolbarTop, num4, _003CDrawToolbar_003Ec__AnonStorey1C.smallButtonHeight);
			if (GUI.Button(position2, string.Empty, coins_Free))
			{
				showFreeCoins = true;
			}
		}
	}

	private void PerformSkinsMakerQueryIfLiteEdition()
	{
		if (!Defs.IsProEdition && prefs.GetInt(Defs.SkinsMakerInMainMenuPurchased) <= 0)
		{
			GoogleIABManager.queryInventorySucceededEvent += HandleSkinsMakerQueryInventorySucceededEvent;
			GoogleIABManager.queryInventoryFailedEvent += HandleSkinsMakerQueryInventoryFailedEvent;
			GoogleIAB.queryInventory(new string[1] { "skinsmaker" });
		}
	}

	private void HandleSkinsMakerQueryInventorySucceededEvent(List<GooglePurchase> purchases, List<GoogleSkuInfo> skus)
	{
		if (_003C_003Ef__am_0024cache39 == null)
		{
			_003C_003Ef__am_0024cache39 = _003CHandleSkinsMakerQueryInventorySucceededEvent_003Em__7;
		}
		string arg = string.Join(", ", purchases.Select(_003C_003Ef__am_0024cache39).ToArray());
		if (_003C_003Ef__am_0024cache3A == null)
		{
			_003C_003Ef__am_0024cache3A = _003CHandleSkinsMakerQueryInventorySucceededEvent_003Em__8;
		}
		string arg2 = string.Join(", ", skus.Select(_003C_003Ef__am_0024cache3A).ToArray());
		if (skus.Count != 1 || skus.Single().productId != "skinsmaker")
		{
			string message = string.Format("Skins Maker query inventory not performed yet.\n\t[ {0} ]\n\t[ {1} ]", arg, arg2);
			Debug.Log(message);
			return;
		}
		if (_003C_003Ef__am_0024cache3B == null)
		{
			_003C_003Ef__am_0024cache3B = _003CHandleSkinsMakerQueryInventorySucceededEvent_003Em__9;
		}
		if (purchases.Any(_003C_003Ef__am_0024cache3B))
		{
			prefs.SetInt(Defs.SkinsMakerInMainMenuPurchased, 1);
		}
		_skinsMakerQuerySucceeded = true;
		string message2 = string.Format("Skins Maker query inventory succeeded.\n\t[ {0} ]\n\t[ {1} ]", arg, arg2);
		Debug.Log(message2);
		GoogleIABManager.queryInventorySucceededEvent -= HandleSkinsMakerQueryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent -= HandleSkinsMakerQueryInventoryFailedEvent;
	}

	private void HandleSkinsMakerQueryInventoryFailedEvent(string message)
	{
		GoogleIABManager.queryInventorySucceededEvent -= HandleSkinsMakerQueryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent -= HandleSkinsMakerQueryInventoryFailedEvent;
		Debug.LogWarning("Skins Maker query failed.\n\t" + message);
	}

	private void HandleSkinsMakerPurchaseSucceededEvent(GooglePurchase purchase)
	{
		if (!(purchase.productId != "skinsmaker"))
		{
			prefs.SetInt(Defs.SkinsMakerInMainMenuPurchased, 1);
			Debug.Log("Skins Maker purchase succeeded: " + purchase.productId);
			GoogleIABManager.purchaseSucceededEvent -= HandleSkinsMakerPurchaseSucceededEvent;
			GoogleIABManager.purchaseFailedEvent -= HandleSkinsMakerPurchasePurchaseFailedEvent;
		}
	}

	private void HandleSkinsMakerPurchasePurchaseFailedEvent(string message)
	{
		GoogleIABManager.purchaseSucceededEvent -= HandleSkinsMakerPurchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent -= HandleSkinsMakerPurchasePurchaseFailedEvent;
		Debug.LogWarning("Skins Maker purchase failed.\n\t" + message);
	}

	private void DrawFreeCoins()
	{
		float num = (float)Screen.height / 768f;
		float num2 = 30f * num;
		float num3 = 682f * num;
		Rect rect = new Rect(0.5f * ((float)Screen.width - num * 1366f), 0f, num * 1366f, num * 768f);
		GUI.DrawTexture(new Rect(0.5f * ((float)Screen.width - (float)head_Free.width * num), 0f, (float)head_Free.width * num, (float)head_Free.height * num), head_Free);
		if (true)
		{
			if (prefs.GetInt("freeFacebook", 0) == 0)
			{
				if (GUI.Button(new Rect((float)Screen.width * 0.7f - (float)f_Free.normal.background.width * 0.38f * num, (float)Screen.height * 0.5f - (float)f_Free.normal.background.height * num * 1.14f - num2, (float)f_Free.normal.background.width * num * 0.76f, (float)f_Free.normal.background.height * num * 0.76f), string.Empty, f_Free))
				{
					FlurryPluginWrapper.LogFacebook();
					FlurryPluginWrapper.LogFreeCoinsFacebook();
					prefs.SetInt("freeFacebook", 1);
					if (!Application.isEditor && FacebookSupported())
					{
						int @int = Storager.getInt(Defs.Coins, false);
						Storager.setInt(Defs.Coins, @int + 5, false);
						InitFacebook();
					}
				}
			}
			else
			{
				GUI.DrawTexture(new Rect((float)Screen.width * 0.7f - (float)f_Free.normal.background.width * 0.38f * num, (float)Screen.height * 0.5f - (float)f_Free.normal.background.height * num * 1.14f - num2, (float)f_Free.normal.background.width * num * 0.76f, (float)f_Free.normal.background.height * num * 0.76f), freeFaceOff);
			}
		}
		if (true)
		{
			if (prefs.GetInt("freeTwitter", 0) == 0)
			{
				if (GUI.Button(new Rect((float)Screen.width * 0.7f - (float)f_Free.normal.background.width * 0.38f * num, (float)Screen.height * 0.5f - (float)f_Free.normal.background.height * num * 0.38f, (float)f_Free.normal.background.width * num * 0.76f, (float)f_Free.normal.background.height * num * 0.76f), string.Empty, t_Free))
				{
					prefs.SetInt("freeTwitter", 1);
					FlurryPluginWrapper.LogTwitter();
					FlurryPluginWrapper.LogFreeCoinsTwitter();
					if (!Application.isEditor)
					{
						int int2 = Storager.getInt(Defs.Coins, false);
						Storager.setInt(Defs.Coins, int2 + 5, false);
						InitTwitter();
					}
				}
			}
			else
			{
				GUI.DrawTexture(new Rect((float)Screen.width * 0.7f - (float)f_Free.normal.background.width * 0.38f * num, (float)Screen.height * 0.5f - (float)f_Free.normal.background.height * num * 0.38f, (float)f_Free.normal.background.width * num * 0.76f, (float)f_Free.normal.background.height * num * 0.76f), freeTwitterOff);
			}
		}
		if (prefs.GetInt("freeTube", 0) == 0)
		{
			Rect position = new Rect((float)Screen.width * 0.7f - (float)f_Free.normal.background.width * 0.38f * num, (float)Screen.height * 0.5f + (float)f_Free.normal.background.height * num * 0.38f + num2, (float)f_Free.normal.background.width * num * 0.76f, (float)f_Free.normal.background.height * num * 0.76f);
			if (GUI.Button(position, string.Empty, tube_free))
			{
				FlurryPluginWrapper.LogFreeCoinsYoutube();
				prefs.SetInt("freeTube", 1);
				int int3 = Storager.getInt(Defs.Coins, false);
				Storager.setInt(Defs.Coins, int3 + 100, false);
			}
		}
		else
		{
			GUI.DrawTexture(new Rect((float)Screen.width * 0.7f - (float)f_Free.normal.background.width * 0.38f * num, (float)Screen.height * 0.5f + (float)f_Free.normal.background.height * num * 0.38f + num2, (float)f_Free.normal.background.width * num * 0.76f, (float)f_Free.normal.background.height * num * 0.76f), freeTubeOff);
		}
		if (GUI.Button(new Rect((float)Screen.width * 0.7f - (float)rate_Free.normal.background.width * 0.5f * num, (float)Screen.height * 0.9f - (float)backBut.normal.background.height * num * 0.5f, (float)rate_Free.normal.background.width * num, (float)rate_Free.normal.background.height * num), string.Empty, rate_Free))
		{
			FlurryPluginWrapper.LogFreeCoinsRateUs();
			string applicationUrl = Defs.ApplicationUrl;
			Application.OpenURL(applicationUrl);
		}
		if (GUI.Button(new Rect((float)backBut.normal.background.width * num * 0.5f, (float)Screen.height * 0.9f - (float)backBut.normal.background.height * num * 0.5f, (float)backBut.normal.background.width * num, (float)backBut.normal.background.height * num), string.Empty, backBut))
		{
			showFreeCoins = false;
		}
	}

	private void OnGUI()
	{
		float num = (float)Screen.height / 768f;
		if (showFreeCoins)
		{
			DrawFreeCoins();
		}
		else
		{
			Rect position = new Rect(0.5f * ((float)Screen.width - num * (float)fon.width), 0f, num * (float)fon.width, num * (float)fon.height);
			GUI.DrawTexture(position, fon, ScaleMode.StretchToFill);
			float num2 = (float)Screen.height - 662f * num;
			Rect position2 = new Rect(0f, (float)Screen.height - num2, Screen.width, num2);
			GUI.DrawTexture(position2, bottomShadow, ScaleMode.StretchToFill);
			float rightBorder = 0.5f * (float)Screen.width + 423f * num;
			DrawGameModeButtons(rightBorder);
			DrawToolbar(rightBorder);
		}
		bestScoreStyle.fontSize = Mathf.RoundToInt((float)Screen.height * 0.04f);
		labelStyle.fontSize = Mathf.RoundToInt((float)Screen.height * 0.04f);
		if (showMessagFacebook)
		{
			labelStyle.fontSize = Player_move_c.FontSizeForMessages;
			GUI.Label(Player_move_c.SuccessMessageRect(), _SocialSentSuccess("Facebook"), labelStyle);
		}
		if (showMessagTiwtter)
		{
			labelStyle.fontSize = Player_move_c.FontSizeForMessages;
			GUI.Label(Player_move_c.SuccessMessageRect(), _SocialSentSuccess("Twitter"), labelStyle);
		}
		if (Time.realtimeSinceStartup - _timeWhenPurchShown >= 2f)
		{
			productPurchased = false;
		}
		if (productPurchased)
		{
			labelStyle.fontSize = FontSizeForMessages;
			GUI.Label(Player_move_c.SuccessMessageRect(), "Purchase was successful", labelStyle);
		}
	}

	private string _SocialMessage()
	{
		string applicationUrl = Defs.ApplicationUrl;
		return "I scored " + prefs.GetInt(Defs.BestScoreSett, 0) + " points in Pixel Gun 3D! Try right now! " + applicationUrl;
	}

	private string _SocialSentSuccess(string SocialName)
	{
		return "Your best score was sent to " + SocialName;
	}

	private void completionHandler(string error, object result)
	{
		if (error != null)
		{
			Debug.LogError(error);
		}
		else
		{
			Utils.logObject(result);
		}
	}

	private void facebookGraphReqCompl(object result)
	{
		Utils.logObject(result);
	}

	private void facebookSessionOpened()
	{
		_hasPublishPermission = ServiceLocator.FacebookFacade.GetSessionPermissions().Contains("publish_stream");
		_hasPublishActions = ServiceLocator.FacebookFacade.GetSessionPermissions().Contains("publish_actions");
	}

	private void facebookreauthorizationSucceededEvent()
	{
		_hasPublishPermission = ServiceLocator.FacebookFacade.GetSessionPermissions().Contains("publish_stream");
		_hasPublishActions = ServiceLocator.FacebookFacade.GetSessionPermissions().Contains("publish_actions");
	}

	private void Start()
	{
		Application.targetFrameRate = 240;
		PhotonNetwork.PhotonServerSettings.UseCloud("57aa4ce3-57e7-4092-a71f-416d847507f2", 0);
		PerformSkinsMakerQueryIfLiteEdition();
		string @string = prefs.GetString(Defs.ShouldReoeatActionSett, string.Empty);
		if (@string.Equals(Defs.GoToProfileAction))
		{
			prefs.SetString(Defs.ShouldReoeatActionSett, string.Empty);
			prefs.Save();
		}
		else if (!@string.Equals(Defs.GoToSkinsMakerAction))
		{
		}
		InitFacebookEvents();
		Storager.setInt(Defs.EarnedCoins, 0, false);
		if (!Application.isEditor)
		{
			ServiceLocator.FacebookFacade.Init();
			FacebookSessionLoginBehavior sessionLoginBehavior = FacebookSessionLoginBehavior.SSO_WITH_FALLBACK;
			ServiceLocator.FacebookFacade.SetSessionLoginBehavior(sessionLoginBehavior);
		}
		Invoke("setEnabledGUI", 0.1f);
		if (GlobalGameController.isFullVersion)
		{
			prefs.SetInt("FullVersion", 1);
			Debug.Log("FULL VERSION");
		}
		else
		{
			Debug.Log("LITE VERSION");
		}
		_purchaseActivityIndicator = StoreKitEventListener.purchaseActivityInd;
		if (_purchaseActivityIndicator == null)
		{
			Debug.LogWarning("_purchaseActivityIndicator is null.");
		}
		else
		{
			_purchaseActivityIndicator.SetActive(false);
		}
		prefs.SetInt("typeConnect__", 0);
		productIdentifiers = StoreKitEventListener.idsForFull;
		Debug.Log("productIdentifiers = " + productIdentifiers);
		if (!GameObject.FindGameObjectWithTag("SkinsManager") && (bool)skinsManagerPrefab)
		{
			UnityEngine.Object.Instantiate(skinsManagerPrefab, Vector3.zero, Quaternion.identity);
		}
		if (!GameObject.FindGameObjectWithTag("WeaponManager") && (bool)weaponManagerPrefab)
		{
			UnityEngine.Object.Instantiate(weaponManagerPrefab, Vector3.zero, Quaternion.identity);
		}
		GlobalGameController.ResetParameters();
		GlobalGameController.Score = 0;
		_inAppGameObject = GameObject.FindGameObjectWithTag("InAppGameObject");
		_listener = _inAppGameObject.GetComponent<StoreKitEventListener>();
		GoogleIABManager.purchaseSucceededEvent += purchaseSuccessful;
		if (!Application.isEditor && iOSVersion > 5f)
		{
			FacebookManager.graphRequestCompletedEvent += facebookGraphReqCompl;
			FacebookManager.sessionOpenedEvent += facebookSessionOpened;
			FacebookManager.reauthorizationSucceededEvent += facebookreauthorizationSucceededEvent;
			_canUserUseFacebookComposer = ServiceLocator.FacebookFacade.CanUserUseFacebookComposer();
		}
	}

	private void InitTwitter()
	{
		Debug.Log("InitTwitter(): init");
		string empty = string.Empty;
		string empty2 = string.Empty;
		if (GlobalGameController.isFullVersion)
		{
			empty = "cuMbTHM8izr9Mb3bIfcTxA";
			empty2 = "mpTLWIku4kIaQq7sTTi91wRLlvAxADhalhlEresnuI";
		}
		else
		{
			empty = "Jb7CwCaMgCQQiMViQRNHw";
			empty2 = "zGVrax4vqgs3CYf04O7glsoRbNT3vhIafte6lfm8w";
		}
		ServiceLocator.TwitterFacade.Init(empty, empty2);
		if (!ServiceLocator.TwitterFacade.IsLoggedIn())
		{
			TwitterLogin();
		}
		else
		{
			TwitterPost();
		}
	}

	private void TwitterLogin()
	{
		TwitterManager.loginSucceededEvent += OnTwitterLogin;
		TwitterManager.loginFailedEvent += OnTwitterLoginFailed;
		ServiceLocator.TwitterFacade.ShowLoginDialog();
	}

	private void OnTwitterLogin(string s)
	{
		TwitterManager.loginSucceededEvent -= OnTwitterLogin;
		TwitterManager.loginFailedEvent -= OnTwitterLoginFailed;
		TwitterPost();
	}

	private void OnTwitterLoginFailed(string error)
	{
		TwitterManager.loginSucceededEvent -= OnTwitterLogin;
		TwitterManager.loginFailedEvent -= OnTwitterLoginFailed;
	}

	private void TwitterPost()
	{
		TwitterManager.requestDidFinishEvent += OnTwitterPost;
		ServiceLocator.TwitterFacade.PostStatusUpdate(_SocialMessage());
	}

	private void OnTwitterPost(object result)
	{
		if (result != null)
		{
			TwitterManager.requestDidFinishEvent -= OnTwitterPost;
			showMessagTiwtter = true;
			Invoke("hideMessagTwitter", 3f);
		}
	}

	private void OnTwitterPostFailed(string _error)
	{
		TwitterManager.requestDidFinishEvent -= OnTwitterPost;
	}

	private void Update()
	{
		if (Application.platform == RuntimePlatform.Android && Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	[Beebyte.Obfuscator.SkipRename]
	private void setEnabledGUI()
	{
		isFirstFrame = false;
	}

	private bool FacebookSupported()
	{
		//return (Application.platform != RuntimePlatform.IPhonePlayer) ? (Application.platform == RuntimePlatform.Android) : (iOSVersion > 5f);
		return false;
	}

	private void InitFacebook()
	{
		if (!Application.isEditor)
		{
			clickButtonFacebook = true;
			if (!ServiceLocator.FacebookFacade.IsSessionValid())
			{
				Debug.Log("Facebook: !isSessionValid");
				string[] permissions = new string[1] { "email" };
				ServiceLocator.FacebookFacade.LoginWithReadPermissions(permissions);
			}
			else
			{
				Debug.Log("Facebook: isSessionValid");
				OnEventFacebookLogin();
			}
		}
	}

	private void InitFacebookEvents()
	{
		FacebookManager.reauthorizationSucceededEvent += OnEventFacebookLogin;
		FacebookManager.loginFailedEvent += OnEventFacebookLoginFailed;
		FacebookManager.sessionOpenedEvent += OnEventFacebookLogin;
	}

	private void CleanFacebookEvents()
	{
		FacebookManager.reauthorizationSucceededEvent -= OnEventFacebookLogin;
		FacebookManager.loginFailedEvent -= OnEventFacebookLoginFailed;
		FacebookManager.sessionOpenedEvent -= OnEventFacebookLogin;
	}

	private void OnEventFacebookLogin()
	{
		if (Application.isEditor || !clickButtonFacebook)
		{
			return;
		}
		Debug.Log("OnEventFacebookLogin");
		if (!ServiceLocator.FacebookFacade.IsSessionValid())
		{
			return;
		}
		if (_hasPublishPermission)
		{
			Debug.Log("sendMessag");
			clickButtonFacebook = false;
			showMessagFacebook = true;
			Invoke("hideMessag", 3f);
			Facebook.instance.postMessage(_SocialMessage(), completionHandler);
			return;
		}
		Debug.Log("poluchau permissions");
		string[] permissions = new string[2] { "publish_actions", "publish_stream" };
		ServiceLocator.FacebookFacade.ReauthorizeWithPublishPermissions(permissions, FacebookSessionDefaultAudience.Everyone);
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	_hasPublishPermission = true;
		//	_hasPublishActions = true;
		//}
	}

	private void OnEventFacebookLoginFailed(P31Error error)
	{
		clickButtonFacebook = false;
		Debug.Log("OnEventFacebookLoginFailed=" + error);
	}

	private void purchaseSuccessful(GooglePurchase purchase)
	{
	}

	private void OnDestroy()
	{
		FacebookManager.graphRequestCompletedEvent -= facebookGraphReqCompl;
		FacebookManager.sessionOpenedEvent -= facebookSessionOpened;
		FacebookManager.reauthorizationSucceededEvent -= facebookreauthorizationSucceededEvent;
		GoogleIABManager.purchaseSucceededEvent -= purchaseSuccessful;
		CleanFacebookEvents();
	}

	[Beebyte.Obfuscator.SkipRename]
	private void hideMessag()
	{
		showMessagFacebook = false;
	}

	[Beebyte.Obfuscator.SkipRename]
	private void hideMessagTwitter()
	{
		showMessagTiwtter = false;
	}

	[CompilerGenerated]
	private static string _003CHandleSkinsMakerQueryInventorySucceededEvent_003Em__7(GooglePurchase p)
	{
		return p.productId;
	}

	[CompilerGenerated]
	private static string _003CHandleSkinsMakerQueryInventorySucceededEvent_003Em__8(GoogleSkuInfo s)
	{
		return s.productId;
	}

	[CompilerGenerated]
	private static bool _003CHandleSkinsMakerQueryInventorySucceededEvent_003Em__9(GooglePurchase p)
	{
		return p.productId == "skinsmaker";
	}
}
