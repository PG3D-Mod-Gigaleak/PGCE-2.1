using System;
using System.Runtime.CompilerServices;
using Prime31;
using Rilisoft;
using UnityEngine;

public sealed class SpisokSkinov : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003COnGUI_003Ec__AnonStorey1F
	{
		private sealed class _003COnGUI_003Ec__AnonStorey20
		{
			internal int newCoins;

			internal Action<string>[] showShop;

			internal _003COnGUI_003Ec__AnonStorey1F _003C_003Ef__ref_002431;

			internal void _003C_003Em__1F()
			{
				Storager.setInt(Defs.Coins, newCoins, false);
				_003C_003Ef__ref_002431._003C_003Ef__this.mainController.previewControl.PurchaseSuccessful(_003C_003Ef__ref_002431.id);
				_003C_003Ef__ref_002431._003C_003Ef__this.SetCurrent();
			}

			internal void _003C_003Em__20(string pressedbutton)
			{
				if (!pressedbutton.Equals("Cancel"))
				{
					coinsShop.thisScript.notEnoughCoins = true;
					coinsShop.thisScript.onReturnAction = _003C_003Ef__ref_002431.act;
					coinsShop.showCoinsShop();
				}
			}

			internal void _003C_003Em__21()
			{
				showShop[0]("Yes!");
			}
		}

		internal string id;

		internal Action act;

		internal SpisokSkinov _003C_003Ef__this;

		internal void _003C_003Em__1C()
		{
			_003COnGUI_003Ec__AnonStorey20 _003COnGUI_003Ec__AnonStorey = new _003COnGUI_003Ec__AnonStorey20();
			_003COnGUI_003Ec__AnonStorey._003C_003Ef__ref_002431 = this;
			coinsShop.thisScript.notEnoughCoins = false;
			coinsShop.thisScript.onReturnAction = null;
			int num = ((!VirtualCurrencyHelper.prices.ContainsKey(id)) ? 10 : VirtualCurrencyHelper.prices[id]);
			_003COnGUI_003Ec__AnonStorey.newCoins = Storager.getInt(Defs.Coins, false) - num;
			Action action = _003COnGUI_003Ec__AnonStorey._003C_003Em__1F;
			_003COnGUI_003Ec__AnonStorey.showShop = null;
			_003COnGUI_003Ec__AnonStorey.showShop = new Action<string>[1] { _003COnGUI_003Ec__AnonStorey._003C_003Em__20 };
			if (_003COnGUI_003Ec__AnonStorey.newCoins >= 0)
			{
				action();
				return;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("CustomDialog")) as GameObject;
			if (gameObject == null)
			{
				Debug.LogError("customDialogEntity == null");
			}
			CustomDialog component = gameObject.GetComponent<CustomDialog>();
			component.yesPressed = _003COnGUI_003Ec__AnonStorey._003C_003Em__21;
		}
	}

	[CompilerGenerated]
	private sealed class _003COnGUI_003Ec__AnonStorey22
	{
		private sealed class _003COnGUI_003Ec__AnonStorey21
		{
			internal int newCoins;

			internal Action<string> showShop;

			internal _003COnGUI_003Ec__AnonStorey22 _003C_003Ef__ref_002434;

			internal void _003C_003Em__22()
			{
				Storager.setInt(Defs.Coins, newCoins, false);
				FlurryPluginWrapper.LogAddYourSkinBoughtEvent();
				Storager.setInt(Defs.SkinsMakerInProfileBought, 1, true);
				_003C_003Ef__ref_002434.goToSM();
			}

			internal void _003C_003Em__23(string pressedbutton)
			{
				EtceteraAndroidManager.alertButtonClickedEvent -= showShop;
				if (!pressedbutton.Equals(Defs.CancelButtonTitle))
				{
					coinsShop.thisScript.notEnoughCoins = true;
					coinsShop.thisScript.onReturnAction = _003C_003Ef__ref_002434.act;
					coinsShop.showCoinsShop();
				}
			}

			internal void _003C_003Em__24()
			{
				showShop("Yes!");
			}
		}

		internal Action goToSM;

		internal Action act;

		internal SpisokSkinov _003C_003Ef__this;

		internal void _003C_003Em__1D()
		{
			coinsPlashka.thisScript.enabled = false;
			_003C_003Ef__this.HideThisController();
			_003C_003Ef__this.mainController.GoToCreateNew();
		}

		internal void _003C_003Em__1E()
		{
			_003COnGUI_003Ec__AnonStorey21 _003COnGUI_003Ec__AnonStorey = new _003COnGUI_003Ec__AnonStorey21();
			_003COnGUI_003Ec__AnonStorey._003C_003Ef__ref_002434 = this;
			coinsShop.thisScript.notEnoughCoins = false;
			coinsShop.thisScript.onReturnAction = null;
			int skinsMakerPrice = Defs.skinsMakerPrice;
			int @int = Storager.getInt(Defs.Coins, false);
			_003COnGUI_003Ec__AnonStorey.newCoins = @int - skinsMakerPrice;
			Action action = _003COnGUI_003Ec__AnonStorey._003C_003Em__22;
			_003COnGUI_003Ec__AnonStorey.showShop = null;
			_003COnGUI_003Ec__AnonStorey.showShop = _003COnGUI_003Ec__AnonStorey._003C_003Em__23;
			if (_003COnGUI_003Ec__AnonStorey.newCoins >= 0)
			{
				action();
				return;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("CustomDialog")) as GameObject;
			if (gameObject == null)
			{
				Debug.LogError("customDialogEntity == null");
				return;
			}
			CustomDialog component = gameObject.GetComponent<CustomDialog>();
			component.yesPressed = _003COnGUI_003Ec__AnonStorey._003C_003Em__24;
		}
	}

	public bool showEnabled;

	private static float koefMashtab = (float)Screen.height / 768f;

	public Controller mainController;

	private ViborChastiTela viborChastiTelaController;

	public ArrayListWrapper arrNameSkin;

	public ArrayListWrapper arrTitleSkin;

	public Texture2D fonTitle;

	public Texture2D plashkaNiz;

	public Texture2D oknoDelSkin;

	public Texture2D head_Prof;

	public GUIStyle butBack;

	public GUIStyle butBackProfile;

	public GUIStyle labelTitle;

	public GUIStyle butDel;

	public GUIStyle nameStyle;

	public GUIStyle setStyle;

	public GUIStyle labelTitleSkin;

	public GUIStyle butDlgOk;

	public GUIStyle butDlgCancel;

	private bool dialogDelNeActiv = true;

	private bool msgSaveShow;

	private Rect rectDialogDel;

	private string namePlayer;

	public Texture leftArr;

	public Texture rightArr;

	public Texture swipeToChange;

	public Texture equipped;

	public Texture locked;

	public Texture cup;

	public GUIStyle buyStyle;

	public GUIStyle winsLabelStyle;

	public GUIStyle restoreStyle;

	public Texture restoreWindowTexture;

	public GUIStyle restoreWindButStyle;

	public GUIStyle cancelEindButStyle;

	private Font f;

	public Texture lockedSkinPriceTexture;

	private bool _canUserUseFacebookComposer;

	private bool _hasPublishPermission;

	private bool _hasPublishActions;

	private bool showMessagFacebook;

	private bool showMessagTiwtter;

	private bool clickButtonFacebook;

	public GUIStyle facebookStyle;

	public GUIStyle twitterStyle;

	public GUIStyle gamecenterStyle;

	public GUIStyle labelStyle;

	public GUIStyle scoresStyle;

	public GUIStyle leftBut;

	public GUIStyle rightBut;

	public GUIStyle skinsMakerStyle;

	public GUIStyle editSkinStyle;

	public GUIStyle deleteStyle;

	public GUIStyle addToProfileStyle;

	public Texture skinsMakerPriceTexture;

	public Texture skinsMakerProPriceTexture;

	public Texture skinsMakerNormal;

	public Texture skinsMakerActive;

	private int COOPScore;

	public GameObject skinSavedToProfileMsg;

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

	public static int EquippedSkinIndexToPreviewControllerIndex()
	{
		int num = prefs.GetInt(Defs.SkinIndexMultiplayer, 0);
		if (num >= Controller.IndexBaseForUserMultiSkins)
		{
			num -= Controller.IndexBaseForUserMultiSkins;
		}
		return num;
	}

	public void HideThisController()
	{
		mainController.showEnabled = true;
		showEnabled = false;
		mainController.objPeople.active = false;
	}

	public static void GoFromProfileToConnect()
	{
		prefs.SetInt("typeConnect__", 0);
		Application.LoadLevel("NewMapList");
	}

	private void Start()
	{
		f = labelTitle.font;
		if (prefs.GetInt(Defs.SkinEditorMode, 0) == 1)
		{
			InitFacebookEvents();
			if (!Application.isEditor)
			{
				ServiceLocator.FacebookFacade.Init();
				FacebookSessionLoginBehavior sessionLoginBehavior = FacebookSessionLoginBehavior.SSO_WITH_FALLBACK;
				ServiceLocator.FacebookFacade.SetSessionLoginBehavior(sessionLoginBehavior);
			}
			if (!Application.isEditor)
			{
				FacebookManager.graphRequestCompletedEvent += facebookGraphReqCompl;
				FacebookManager.sessionOpenedEvent += facebookSessionOpened;
				FacebookManager.reauthorizationSucceededEvent += facebookreauthorizationSucceededEvent;
				_canUserUseFacebookComposer = ServiceLocator.FacebookFacade.CanUserUseFacebookComposer();
			}
		}
		mainController = GetComponent<Controller>();
		viborChastiTelaController = GetComponent<ViborChastiTela>();
		mainController.previewControl.editModeEnteredDelegate = shooseSkin;
		rectDialogDel = new Rect((float)Screen.width * 0.5f - (float)oknoDelSkin.width * 0.5f * koefMashtab, (float)Screen.height * 0.5f - (float)oknoDelSkin.height * 0.5f * koefMashtab, (float)oknoDelSkin.width * koefMashtab, (float)oknoDelSkin.height * koefMashtab);
		namePlayer = prefs.GetString("NamePlayer", Defs.defaultPlayerName);
		nameStyle.fontSize = Mathf.RoundToInt(30f * koefMashtab);
		if (prefs.GetInt(Defs.SkinEditorMode, 0) == 1)
		{
			Camera.main.transform.position += new Vector3(0f, 0.37f, 0f);
			if (!Storager.hasKey(Defs.COOPScore))
			{
				Storager.setInt(Defs.COOPScore, 0, false);
			}
			COOPScore = Storager.getInt(Defs.COOPScore, false);
		}
	}

	private void Update()
	{
		if (!showEnabled)
		{
		}
	}

	public void SetCurrent()
	{
		int num = mainController.previewControl.CurrentTextureIndex;
		if (num >= prefs.GetInt(Defs.NumOfMultSkinsSett, 0))
		{
			num += Controller.IndexBaseForUserMultiSkins;
		}
		string value = (string)arrNameSkin[mainController.previewControl.CurrentTextureIndex];
		prefs.SetString(Defs.SkinNameMultiplayer, value);
		Debug.LogError(value);
		prefs.SetInt(Defs.SkinIndexMultiplayer, num);
		Debug.LogError(num + string.Empty);
		prefs.SetString("NamePlayer", namePlayer);
		prefs.Save();
	}

	private void OnGUI()
	{
		if (!showEnabled || coinsShop.thisScript.enabled)
		{
			return;
		}
		if (prefs.GetInt(Defs.SkinEditorMode, 0) == 0)
		{
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, (float)fonTitle.height * koefMashtab), fonTitle);
			labelTitle.fontSize = Mathf.RoundToInt(25f * koefMashtab);
			GUI.Label(new Rect(0f, 0f, Screen.width, (float)fonTitle.height * koefMashtab), "CHOOSE THE SKIN", labelTitle);
			GUI.DrawTexture(new Rect(0f, (float)Screen.height - (float)plashkaNiz.height * koefMashtab, Screen.width, (float)plashkaNiz.height * koefMashtab), plashkaNiz);
		}
		bool flag = false || !dialogDelNeActiv;
		int depth = GUI.depth;
		if (prefs.GetInt(Defs.SkinEditorMode, 0) == 1)
		{
		}
		if (prefs.GetInt(Defs.SkinEditorMode, 0) == 1)
		{
			coinsPlashka.thisScript.enabled = true && !flag;
		}
		float left = 55f * koefMashtab;
		float width = (float)butBack.normal.background.width * koefMashtab;
		if (prefs.GetInt(Defs.SkinEditorMode, 0) == 1)
		{
			GUI.enabled = !StoreKitEventListener.restoreInProcess && !flag;
		}
		Rect position = ((prefs.GetInt(Defs.SkinEditorMode, 0) != 0) ? ConnectGUI.LeftButtonRect : new Rect(left, (float)Screen.height - (9f + (float)butBack.normal.background.height) * koefMashtab, width, (float)butBack.normal.background.height * koefMashtab));
		if (prefs.GetInt(Defs.SkinEditorMode, 0) == 0)
		{
			if (GUI.Button(position, string.Empty, butBack) && dialogDelNeActiv)
			{
				HideThisController();
			}
		}
		else if (GUI.RepeatButton(position, string.Empty, butBackProfile) && dialogDelNeActiv)
		{
			GUIHelper.DrawLoading();
			if (prefs.GetInt(Defs.ProfileEnteredFromMenu, 0) == 1)
			{
				Application.LoadLevel(Defs.CurrentMainMenuScene);
			}
			else
			{
				GoFromProfileToConnect();
			}
		}
		if (prefs.GetInt(Defs.SkinEditorMode, 0) == 1)
		{
			GUI.enabled = true && !flag;
		}
		float num = 115f * Defs.Coef;
		Rect position2 = new Rect((float)Screen.width * 0.5f - (float)(leftBut.normal.background.width * Screen.height) / 768f - num, (float)Screen.height * ((prefs.GetInt(Defs.SkinEditorMode, 0) != 1) ? 0.5f : 0.6f) - (float)(leftBut.normal.background.height * Screen.height) / 768f * 0.5f, (float)(leftBut.normal.background.width * Screen.height) / 768f, (float)(leftBut.normal.background.height * Screen.height) / 768f);
		Rect position3 = new Rect((float)Screen.width * 0.5f + num, (float)Screen.height * ((prefs.GetInt(Defs.SkinEditorMode, 0) != 1) ? 0.5f : 0.6f) - (float)(leftBut.normal.background.height * Screen.height) / 768f * 0.5f, (float)(leftBut.normal.background.width * Screen.height) / 768f, (float)(leftBut.normal.background.height * Screen.height) / 768f);
		if (!flag && dialogDelNeActiv && (!viborChastiTelaController.showEnabled || prefs.GetInt(Defs.SkinEditorMode, 0) == 1))
		{
			if (GUI.Button(position2, string.Empty, leftBut))
			{
				mainController.previewControl.move(1);
			}
			if (GUI.Button(position3, string.Empty, rightBut))
			{
				mainController.previewControl.move(-1);
			}
		}
		if (prefs.GetInt(Defs.SkinEditorMode, 0) == 1)
		{
			GUI.DrawTexture(new Rect((float)Screen.width / 2f - (float)head_Prof.width / 2f * koefMashtab, (float)Screen.height * 0.1f - (float)head_Prof.height / 2f * koefMashtab, (float)head_Prof.width * koefMashtab, (float)head_Prof.height * koefMashtab), head_Prof);
			if (StoreKitEventListener.purchaseActivityInd == null)
			{
				Debug.LogWarning("StoreKitEventListener.purchaseActivityInd == null");
			}
			else
			{
				StoreKitEventListener.purchaseActivityInd.SetActive(StoreKitEventListener.restoreInProcess);
			}
			GUI.enabled = !StoreKitEventListener.purchaseInProcess && !flag;
			Rect symmetricRect = coinsPlashka.symmetricRect;
			symmetricRect.y += symmetricRect.height / 2f;
			symmetricRect.y -= (float)restoreStyle.normal.background.height / 2f * koefMashtab;
			symmetricRect.width = (float)restoreStyle.normal.background.width * koefMashtab;
			symmetricRect.height = (float)restoreStyle.normal.background.height * koefMashtab;
			symmetricRect.x = ConnectGUI.RightButtonRect.x + ConnectGUI.RightButtonRect.width / 2f - symmetricRect.width / 2f;
			if (Application.platform == RuntimePlatform.IPhonePlayer && GUI.Button(symmetricRect, string.Empty, restoreStyle))
			{
				StoreKitEventListener.purchaseInProcess = true;
				StoreKitEventListener.restoreInProcess = true;
			}
			GUI.enabled = true && !flag;
			int num2 = 0;
			Rect position4 = new Rect((float)Screen.width * 0.5f - (float)nameStyle.normal.background.width * 0.5f * koefMashtab, (float)Screen.height * 0.23f - (float)nameStyle.normal.background.height * 0.5f * koefMashtab, (float)nameStyle.normal.background.width * koefMashtab, (float)nameStyle.normal.background.height * koefMashtab);
			namePlayer = GUI.TextField(position4, namePlayer, nameStyle);
			bool flag2 = true;
			if (!string.IsNullOrEmpty(namePlayer))
			{
				for (int i = 0; i < namePlayer.Length; i++)
				{
					if (namePlayer.Substring(i, 1) != " ")
					{
						flag2 = false;
						break;
					}
				}
			}
			if (flag2)
			{
				prefs.SetString("NamePlayer", "Unnamed");
			}
			else
			{
				prefs.SetString("NamePlayer", namePlayer);
			}
			if (namePlayer.Length > 20)
			{
				namePlayer = namePlayer.Substring(0, 20);
			}
			float num3 = (float)cup.width * koefMashtab * 2f;
			Rect position5 = new Rect(coinsPlashka.thisScript.rectButCoins.x + num3 * 0.5f - (float)cup.width / 2f * koefMashtab, position4.y + position4.height / 2f - (float)cup.height / 2f * koefMashtab, num3 * 0.5f, (float)cup.height * koefMashtab);
			if (dialogDelNeActiv)
			{
				GUI.DrawTexture(position5, cup);
			}
			winsLabelStyle.fontSize = Mathf.RoundToInt(30f * koefMashtab);
			Rect rect = new Rect(coinsPlashka.thisScript.rectButCoins.x, position5.y + position5.height, num3, num3 / 3f);
			if (!flag)
			{
				GUI.Box(rect, " Wins: " + prefs.GetInt("Rating", 0), winsLabelStyle);
			}
			scoresStyle.fontSize = winsLabelStyle.fontSize;
			Rect position6 = rect;
			bool flag3 = Defs.screenRation > 1.3666667f;
			position6.height = (float)scoresStyle.fontSize * 8f;
			position6.y = position2.y + position2.height * 0.5f - position6.height / 2f;
			if (!flag)
			{
				GUI.Label(position6, "CO-OP\nMAX SCORE\n" + COOPScore + "\n\nSURVIVAL\nMAX SCORE\n" + prefs.GetInt(Defs.BestScoreSett, 0), scoresStyle);
			}
			Rect rightButtonRect = ConnectGUI.RightButtonRect;
			rightButtonRect.y = ConnectGUI.LeftButtonRect.y;
			rightButtonRect.height = ConnectGUI.LeftButtonRect.height;
			if (InAppData.inAppData.ContainsKey(mainController.previewControl.CurrentTextureIndex) && Storager.getInt(InAppData.inAppData[mainController.previewControl.CurrentTextureIndex].Value, true) < 1)
			{
				GUI.enabled = !StoreKitEventListener.restoreInProcess && !flag;
				try
				{
					if (!mainController.previewControl.Locked && GUI.Button(rightButtonRect, string.Empty, buyStyle))
					{
						_003COnGUI_003Ec__AnonStorey1F _003COnGUI_003Ec__AnonStorey1F = new _003COnGUI_003Ec__AnonStorey1F();
						_003COnGUI_003Ec__AnonStorey1F._003C_003Ef__this = this;
						_003COnGUI_003Ec__AnonStorey1F.id = InAppData.inAppData[mainController.previewControl.CurrentTextureIndex].Key;
						_003COnGUI_003Ec__AnonStorey1F.act = null;
						_003COnGUI_003Ec__AnonStorey1F.act = _003COnGUI_003Ec__AnonStorey1F._003C_003Em__1C;
						_003COnGUI_003Ec__AnonStorey1F.act();
					}
				}
				finally
				{
					GUI.enabled = !flag;
				}
			}
			else if (!mainController.previewControl.Locked && GUI.Button(rightButtonRect, string.Empty, setStyle) && dialogDelNeActiv)
			{
				SetCurrent();
			}
			float num4 = (float)Screen.height / 768f;
			float num5 = (float)Screen.width / 4f;
			Rect rect2 = new Rect((float)Screen.width / 2f + num5 - (float)leftArr.width / 2f * num4, Screen.height / 2, (float)leftArr.width * num4, (float)leftArr.height * num4);
			float num6 = (float)facebookStyle.normal.background.width * Defs.Coef / 3f;
			float left2 = position.x + position.width * 0.5f - (float)twitterStyle.normal.background.width * Defs.Coef * 0.5f;
			float num7 = (float)Screen.height / 20f;
			Rect rect3 = new Rect(left2, position2.y + position2.height * 0.5f - (float)twitterStyle.normal.background.height * Defs.Coef * 0.5f, (float)twitterStyle.normal.background.width * Defs.Coef, (float)twitterStyle.normal.background.height * Defs.Coef);
			Rect rect4 = new Rect(left2, position2.y + position2.height * 0.5f - (float)twitterStyle.normal.background.height * Defs.Coef * 1.5f - num7, (float)twitterStyle.normal.background.width * Defs.Coef, (float)twitterStyle.normal.background.height * Defs.Coef);
			float num8 = (float)Screen.width / 2f - rect4.width / 2f;
			float num9 = (float)Screen.height * 0.105f;
			Rect rect5 = new Rect(left2, position2.y + position2.height * 0.5f + (float)twitterStyle.normal.background.height * Defs.Coef * 0.5f + num7, (float)twitterStyle.normal.background.width * Defs.Coef, (float)twitterStyle.normal.background.height * Defs.Coef);
			int num10 = EquippedSkinIndexToPreviewControllerIndex();
			if (!mainController.previewControl.Locked && prefs.GetInt(Defs.SkinEditorMode, 0) == 1 && num10 == mainController.previewControl.CurrentTextureIndex)
			{
				GUI.DrawTexture(new Rect(position3.x + position3.width * 0.5f - (float)equipped.width * 0.5f * num4, rect2.y - rect2.height * 0.5f - (float)equipped.height * 0.67f * num4 - (float)equipped.height * 0.16f * num4, (float)equipped.width * num4, (float)equipped.height * num4), equipped);
			}
			if (InAppData.inAppData.ContainsKey(mainController.previewControl.CurrentTextureIndex) && Storager.getInt(InAppData.inAppData[mainController.previewControl.CurrentTextureIndex].Value, true) < 1)
			{
				Rect position7 = new Rect((float)Screen.width / 2f - (float)locked.width / 2f * num4, position2.y + position2.height / 2f - (float)locked.height / 2f * num4, (float)locked.width * num4, (float)locked.height * num4);
				GUI.DrawTexture(position7, locked);
				if (lockedSkinPriceTexture != null)
				{
					Rect position8 = new Rect(position7.x + position7.width - (float)lockedSkinPriceTexture.width * num4 + 10f * num4, position7.y + position7.height * 2f, (float)lockedSkinPriceTexture.width * num4, (float)lockedSkinPriceTexture.height * num4);
					GUI.DrawTexture(position8, lockedSkinPriceTexture);
				}
			}
			float num11 = (float)skinsMakerStyle.normal.background.width * Defs.Coef;
			float num12 = (float)skinsMakerStyle.normal.background.height * Defs.Coef;
			float num13 = 36f * Defs.Coef;
			Rect position9 = new Rect((float)Screen.width * 0.5f - num13 / 2f - (float)editSkinStyle.normal.background.width * Defs.Coef, position.y + position.height * 0.5f - (float)editSkinStyle.normal.background.height * Defs.Coef * 0.5f, (float)editSkinStyle.normal.background.width * Defs.Coef, (float)editSkinStyle.normal.background.height * Defs.Coef);
			Rect position10 = new Rect((float)Screen.width * 0.5f + num13 / 2f, position.y + position.height * 0.5f - (float)deleteStyle.normal.background.height * Defs.Coef * 0.5f, (float)deleteStyle.normal.background.width * Defs.Coef, (float)deleteStyle.normal.background.height * Defs.Coef);
			if (mainController.previewControl.CurrentTextureIndex >= prefs.GetInt(Defs.NumOfMultSkinsSett, 0))
			{
				if (GUI.Button(position9, string.Empty, editSkinStyle))
				{
					shooseSkin();
				}
				if (GUI.Button(position10, string.Empty, deleteStyle))
				{
					ShoeDeleteDialog();
				}
			}
			Rect rect6 = new Rect(rightButtonRect.x + rightButtonRect.width / 2f - num11 / 2f, position2.y + position2.height * 0.5f - num12 * 0.5f, num11, num12);
			Rect position11 = rect6;
			position11.x = position11.x + position11.width / 2f - (float)skinsMakerPriceTexture.width * 0.5f * Defs.Coef;
			position11.y -= 1.15f * (float)skinsMakerPriceTexture.height * Defs.Coef;
			position11.width = (float)skinsMakerPriceTexture.width * Defs.Coef;
			position11.height = (float)skinsMakerPriceTexture.height * Defs.Coef;
			bool flag4 = Application.platform != RuntimePlatform.Android || Defs.IsProEdition;
			if (Storager.getInt(Defs.SkinsMakerInProfileBought, true) == 1)
			{
				skinsMakerStyle.normal.background = skinsMakerNormal as Texture2D;
				skinsMakerStyle.active.background = skinsMakerActive as Texture2D;
			}
			else if (flag4)
			{
				Texture image = ((!Defs.IsProEdition) ? skinsMakerPriceTexture : skinsMakerProPriceTexture);
				GUI.DrawTexture(position11, image);
			}
			if (flag4 && !flag && GUI.Button(rect6, string.Empty, skinsMakerStyle))
			{
				_003COnGUI_003Ec__AnonStorey22 _003COnGUI_003Ec__AnonStorey = new _003COnGUI_003Ec__AnonStorey22();
				_003COnGUI_003Ec__AnonStorey._003C_003Ef__this = this;
				_003COnGUI_003Ec__AnonStorey.goToSM = _003COnGUI_003Ec__AnonStorey._003C_003Em__1D;
				_003COnGUI_003Ec__AnonStorey.act = null;
				_003COnGUI_003Ec__AnonStorey.act = _003COnGUI_003Ec__AnonStorey._003C_003Em__1E;
				if (Storager.getInt(Defs.SkinsMakerInProfileBought, true) == 0)
				{
					_003COnGUI_003Ec__AnonStorey.act();
					FlurryPluginWrapper.LogAddYourSkinTriedToBoughtEvent();
				}
				else
				{
					_003COnGUI_003Ec__AnonStorey.goToSM();
					FlurryPluginWrapper.LogAddYourSkinUsedEvent();
				}
			}
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
		}
		if (prefs.GetInt(Defs.SkinEditorMode, 0) == 0)
		{
			labelTitleSkin.fontSize = Mathf.RoundToInt(20f * koefMashtab);
			GUI.Label(new Rect(0f, 120f * koefMashtab, Screen.width, 50f * koefMashtab), (string)arrTitleSkin[mainController.previewControl.CurrentTextureIndex], labelTitleSkin);
		}
		if (prefs.GetInt(Defs.SkinEditorMode, 0) == 0 && arrNameSkin != null && mainController.previewControl.CurrentTextureIndex > Controller.SkinMaker_arrVremTitle.Length - 1)
		{
			Rect position12 = new Rect((float)Screen.width - 55f * koefMashtab - (float)butDel.normal.background.width * koefMashtab, (float)Screen.height - (9f + (float)butDel.normal.background.height) * koefMashtab, (float)butDel.normal.background.width * koefMashtab, (float)butDel.normal.background.height * koefMashtab);
			if (GUI.Button(position12, string.Empty, butDel) && dialogDelNeActiv)
			{
				ShoeDeleteDialog();
			}
			float height = position12.height;
			float num14 = height * ((float)addToProfileStyle.normal.background.width / (float)addToProfileStyle.normal.background.height);
			Rect position13 = new Rect((float)Screen.width * 0.5f - num14 * 0.5f, position12.y + position12.height * 0.5f - height * 0.5f, num14, height);
			if ((Application.isEditor || Storager.getInt(Defs.SkinsMakerInProfileBought, true) == 1) && GUI.Button(position13, string.Empty, addToProfileStyle))
			{
				viborChastiTelaController.cutSkin(mainController.previewControl.CurrentTextureIndex);
				prefs.SetInt(Defs.SkinEditorMode, 1);
				string[] array = Load.LoadStringArray(Controller.arrNameSkin_sett);
				string[] array2 = Load.LoadStringArray(Controller.arrTitleSkin_sett);
				ArrayListWrapper arrayListWrapper = new ArrayListWrapper();
				ArrayListWrapper arrayListWrapper2 = new ArrayListWrapper();
				if (array != null)
				{
					string[] array3 = array;
					foreach (string text in array3)
					{
						if (!arrayListWrapper.Contains(text))
						{
							Debug.Log("ADDED: " + text);
							arrayListWrapper.Add(text);
						}
						else
						{
							Debug.LogWarning("DUPLICATE: " + text);
						}
					}
				}
				if (array2 != null)
				{
					string[] array4 = array2;
					foreach (string item in array4)
					{
						arrayListWrapper2.Add(item);
					}
				}
				viborChastiTelaController.AddSkinToArrs(arrayListWrapper, arrayListWrapper2);
				prefs.SetInt(Defs.SkinEditorMode, 0);
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(skinSavedToProfileMsg);
				gameObject.GetComponent<Message>().message = "Skin was added to profile";
			}
		}
		if (!dialogDelNeActiv)
		{
			GUI.enabled = true;
			GUI.DrawTexture(rectDialogDel, oknoDelSkin);
			if (GUI.Button(new Rect(rectDialogDel.x + 55f * koefMashtab, rectDialogDel.y + rectDialogDel.height - 125f * koefMashtab, (float)butDlgCancel.normal.background.width * koefMashtab, (float)butDlgCancel.normal.background.height * koefMashtab), string.Empty, butDlgCancel))
			{
				dialogDelNeActiv = true;
				mainController.previewControl.Locked = false;
			}
			if (GUI.Button(new Rect(rectDialogDel.x + rectDialogDel.width - 55f * koefMashtab - (float)butDlgOk.normal.background.width * koefMashtab, rectDialogDel.y + rectDialogDel.height - 125f * koefMashtab, (float)butDlgOk.normal.background.width * koefMashtab, (float)butDlgOk.normal.background.height * koefMashtab), string.Empty, butDlgOk))
			{
				bool flag5 = false;
				if (prefs.GetInt(Defs.SkinEditorMode, 0) == 1)
				{
					flag5 = EquippedSkinIndexToPreviewControllerIndex() == mainController.previewControl.CurrentTextureIndex;
					if (EquippedSkinIndexToPreviewControllerIndex() > mainController.previewControl.CurrentTextureIndex)
					{
						prefs.SetInt(Defs.SkinIndexMultiplayer, prefs.GetInt(Defs.SkinIndexMultiplayer, 0) - 1);
					}
				}
				SkinsManager.DeleteTexture((string)arrNameSkin[mainController.previewControl.CurrentTextureIndex]);
				arrNameSkin.RemoveAt(mainController.previewControl.CurrentTextureIndex);
				arrTitleSkin.RemoveAt(mainController.previewControl.CurrentTextureIndex);
				string[] variable = arrNameSkin.ToArray(typeof(string)) as string[];
				string[] variable2 = arrTitleSkin.ToArray(typeof(string)) as string[];
				Save.SaveStringArray(Controller.arrNameSkin_sett, variable);
				Save.SaveStringArray(Controller.arrTitleSkin_sett, variable2);
				mainController.previewControl.updateSpisok();
				mainController.previewControl.ShowSkin(mainController.previewControl.CurrentTextureIndex - 1);
				mainController.previewControl.Locked = false;
				dialogDelNeActiv = true;
				if (flag5)
				{
					mainController.previewControl.ShowSkin(0);
					SetCurrent();
				}
			}
		}
		if (msgSaveShow)
		{
			labelTitle.fontSize = Mathf.RoundToInt(25f * koefMashtab);
			GUI.Label(new Rect(0f, (float)Screen.height - 200f * koefMashtab, Screen.width, 100f * koefMashtab), "The Skin has been  saved to gallery", labelTitle);
		}
	}

	private void ShoeDeleteDialog()
	{
		if (prefs.GetInt(Defs.SkinEditorMode, 0) == 1)
		{
		}
		dialogDelNeActiv = false;
		mainController.previewControl.Locked = true;
	}

	private void shooseSkin()
	{
		int currentTextureIndex = mainController.previewControl.CurrentTextureIndex;
		if (prefs.GetInt(Defs.SkinEditorMode, 0) == 1)
		{
			coinsPlashka.thisScript.enabled = false;
		}
		viborChastiTelaController.cutSkin(currentTextureIndex);
		showEnabled = false;
		viborChastiTelaController.showEnabled = true;
		ViborChastiTela.skinIzm = false;
	}

	[Beebyte.Obfuscator.SkipRename]
	public void hideMsg()
	{
		msgSaveShow = false;
	}

	public void showMsg()
	{
		msgSaveShow = true;
		Invoke("hideMsg", 2f);
	}

	private void OnDestroy()
	{
		coinsPlashka.thisScript.enabled = false;
		if (StoreKitEventListener.purchaseActivityInd == null)
		{
			Debug.LogWarning("StoreKitEventListener.purchaseActivityInd == null");
		}
		else
		{
			StoreKitEventListener.purchaseActivityInd.SetActive(false);
		}
		if (prefs.GetInt(Defs.SkinEditorMode, 0) == 1)
		{
			CleanFacebookEvents();
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

	private void OnTwitterLoginFailed(string _error)
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

	private void InitFacebook()
	{
		if (!Application.isEditor)
		{
			clickButtonFacebook = true;
			if (!ServiceLocator.FacebookFacade.IsSessionValid())
			{
				Debug.Log("!isSessionValid");
				string[] permissions = new string[1] { "email" };
				ServiceLocator.FacebookFacade.LoginWithReadPermissions(permissions);
			}
			else
			{
				Debug.Log("isSessionValid");
				OnEventFacebookLogin();
			}
		}
	}

	private void InitFacebookEvents()
	{
		if (!Application.isEditor)
		{
			FacebookManager.reauthorizationSucceededEvent += OnEventFacebookLogin;
			FacebookManager.loginFailedEvent += OnEventFacebookLoginFailed;
			FacebookManager.sessionOpenedEvent += OnEventFacebookLogin;
		}
	}

	private void CleanFacebookEvents()
	{
		if (!Application.isEditor)
		{
			FacebookManager.reauthorizationSucceededEvent -= OnEventFacebookLogin;
			FacebookManager.loginFailedEvent -= OnEventFacebookLoginFailed;
			FacebookManager.sessionOpenedEvent -= OnEventFacebookLogin;
		}
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

	private void facebookGraphReqCompl(object result)
	{
		if (!Application.isEditor)
		{
			Utils.logObject(result);
		}
	}

	private void facebookSessionOpened()
	{
		if (!Application.isEditor)
		{
			_hasPublishPermission = ServiceLocator.FacebookFacade.GetSessionPermissions().Contains("publish_stream");
			_hasPublishActions = ServiceLocator.FacebookFacade.GetSessionPermissions().Contains("publish_actions");
		}
	}

	private void facebookreauthorizationSucceededEvent()
	{
		if (!Application.isEditor)
		{
			_hasPublishPermission = ServiceLocator.FacebookFacade.GetSessionPermissions().Contains("publish_stream");
			_hasPublishActions = ServiceLocator.FacebookFacade.GetSessionPermissions().Contains("publish_actions");
		}
	}

	private void OnEventFacebookLoginFailed(P31Error error)
	{
		clickButtonFacebook = false;
		Debug.Log("OnEventFacebookLoginFailed=" + error);
	}

	private string _SocialMessage()
	{
		if (!Storager.hasKey(Defs.COOPScore))
		{
			Storager.setInt(Defs.COOPScore, 0, false);
		}
		int @int = Storager.getInt(Defs.COOPScore, false);
		return "I have " + prefs.GetInt("Rating", 0) + " wins and " + @int + " points in Coop mode! Join now! " + Defs.ApplicationUrl;
	}

	private string _SocialSentSuccess(string SocialName)
	{
		return "Message was sent to " + SocialName;
	}
}
