using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Holoville.HOTween;
using UnityEngine;
using System.Linq;

public sealed class Player_move_c : MonoBehaviour
{
	public GameObject stalkerhelmet;
	public GameObject earshat;
	public bool actuallyOpenIt { get; set; }
	public struct MessageChat
	{
		public string text;

		public float time;

		public int ID;
	}

	public string myCAnim(string a){
        return Defs.CAnim(_weaponManager.currentWeaponSounds.animationObject, a);
    }

	private GameObject rocketToLaunch;

	[CompilerGenerated]
	private sealed class _003CshowCategory_003Ec__AnonStorey24
	{
		internal string[] idsArr;

		internal Player_move_c _003C_003Ef__this;
	}

	[CompilerGenerated]
	private sealed class _003CshowCategory_003Ec__AnonStorey25
	{
		internal int i;

		internal Player_move_c _003C_003Ef__this;
	}

	[CompilerGenerated]
	private sealed class _003CshowCategory_003Ec__AnonStorey23
	{
		private sealed class _003CshowCategory_003Ec__AnonStorey26
		{
			private sealed class _003CshowCategory_003Ec__AnonStorey27
			{
				internal Action<string> buyItem;

				internal _003CshowCategory_003Ec__AnonStorey23 _003C_003Ef__ref_002435;

				internal _003CshowCategory_003Ec__AnonStorey26 _003C_003Ef__ref_002438;

				internal void _003C_003Em__33(string pressedButton)
				{
					EtceteraAndroidManager.alertButtonClickedEvent -= buyItem;
					if (!pressedButton.Equals("Cancel"))
					{
						_003C_003Ef__ref_002438.actualBuy();
					}
				}
			}

			internal int newCoins;

			internal Action actualBuy;

			internal Action<string> showShop;

			internal _003CshowCategory_003Ec__AnonStorey24 _003C_003Ef__ref_002436;

			internal _003CshowCategory_003Ec__AnonStorey25 _003C_003Ef__ref_002437;

			internal _003CshowCategory_003Ec__AnonStorey23 _003C_003Ef__ref_002435;

			internal void _003C_003Em__2F()
			{
				Storager.setInt(Defs.Coins, newCoins, false);
				_003C_003Ef__ref_002435._003C_003Ef__this._weaponManager.AddMinerWeapon(_003C_003Ef__ref_002435.id);
				_003C_003Ef__ref_002435._003C_003Ef__this.PurchaseSuccessful(_003C_003Ef__ref_002435.id);
			}

			internal void _003C_003Em__30()
			{
				_003CshowCategory_003Ec__AnonStorey27 _003CshowCategory_003Ec__AnonStorey = new _003CshowCategory_003Ec__AnonStorey27();
				_003CshowCategory_003Ec__AnonStorey._003C_003Ef__ref_002435 = _003C_003Ef__ref_002435;
				_003CshowCategory_003Ec__AnonStorey._003C_003Ef__ref_002438 = this;
				_003CshowCategory_003Ec__AnonStorey.buyItem = null;
				_003CshowCategory_003Ec__AnonStorey.buyItem = _003CshowCategory_003Ec__AnonStorey._003C_003Em__33;
				string message = string.Format("Do you want to buy {0}?", InAppData.inappReadableNames[_003C_003Ef__ref_002435.id]);
				EtceteraAndroidManager.alertButtonClickedEvent += _003CshowCategory_003Ec__AnonStorey.buyItem;
				EtceteraAndroid.showAlert(string.Empty, message, "Buy", "Cancel");
			}

			internal void _003C_003Em__31(string pressedbutton)
			{
				EtceteraAndroidManager.alertButtonClickedEvent -= showShop;
				if (!pressedbutton.Equals("Cancel"))
				{
					coinsShop.thisScript.notEnoughCoins = true;
					coinsShop.thisScript.onReturnAction = _003C_003Ef__ref_002435.act;
					coinsShop.showCoinsShop();
				}
			}

			internal void _003C_003Em__32()
			{
				showShop("Yes!");
			}
		}

		internal string id;

		internal Action act;

		internal _003CshowCategory_003Ec__AnonStorey24 _003C_003Ef__ref_002436;

		internal _003CshowCategory_003Ec__AnonStorey25 _003C_003Ef__ref_002437;

		internal Player_move_c _003C_003Ef__this;

		internal void _003C_003Em__26()
		{
			_003CshowCategory_003Ec__AnonStorey26 _003CshowCategory_003Ec__AnonStorey = new _003CshowCategory_003Ec__AnonStorey26();
			_003CshowCategory_003Ec__AnonStorey._003C_003Ef__ref_002436 = _003C_003Ef__ref_002436;
			_003CshowCategory_003Ec__AnonStorey._003C_003Ef__ref_002437 = _003C_003Ef__ref_002437;
			_003CshowCategory_003Ec__AnonStorey._003C_003Ef__ref_002435 = this;
			coinsShop.thisScript.notEnoughCoins = false;
			coinsShop.thisScript.onReturnAction = null;
			if (id == null)
			{
				string arg = string.Join(", ", _003C_003Ef__ref_002436.idsArr);
				string message = string.Format("idsArr[{0}] == null;    idsArr = [{1}]", _003C_003Ef__ref_002437.i, arg);
				Debug.LogError(message);
				return;
			}
			int num = ((!VirtualCurrencyHelper.prices.ContainsKey(id)) ? (10 * (1 + _003C_003Ef__ref_002437.i)) : VirtualCurrencyHelper.prices[id]);
			int @int = Storager.getInt(Defs.Coins, false);
			_003CshowCategory_003Ec__AnonStorey.newCoins = @int - num;
			_003CshowCategory_003Ec__AnonStorey.actualBuy = _003CshowCategory_003Ec__AnonStorey._003C_003Em__2F;
			Action action = _003CshowCategory_003Ec__AnonStorey._003C_003Em__30;
			_003CshowCategory_003Ec__AnonStorey.showShop = null;
			_003CshowCategory_003Ec__AnonStorey.showShop = _003CshowCategory_003Ec__AnonStorey._003C_003Em__31;
			if (_003CshowCategory_003Ec__AnonStorey.newCoins >= 0)
			{
				_003CshowCategory_003Ec__AnonStorey.actualBuy();
			}
			else if (_003C_003Ef__this.customDialogPrefab != null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(_003C_003Ef__this.customDialogPrefab) as GameObject;
				CustomDialog component = gameObject.GetComponent<CustomDialog>();
				component.yesPressed = _003CshowCategory_003Ec__AnonStorey._003C_003Em__32;
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CUpdate_003Ec__AnonStorey28
	{
		internal Func<bool> pauserIsPaused;

		internal Player_move_c _003C_003Ef__this;

		internal bool _003C_003Em__2D()
		{
			return _003C_003Ef__this._pauser != null && _003C_003Ef__this._pauser.paused;
		}

		internal void _003C_003Em__2E()
		{
			_003C_003Ef__this.transform.parent.transform.localScale = new Vector3(1f, 1f, 1f);
			_003C_003Ef__this.isDeadFrame = false;
			_003C_003Ef__this.Invoke("SetNoKilled", 0.5f);
			_003C_003Ef__this._weaponManager.myPlayer.GetComponent<SkinName>().camPlayer.transform.parent = _003C_003Ef__this._weaponManager.myPlayer.transform;
			if (!pauserIsPaused())
			{
				_003C_003Ef__this._leftJoystick.SetActive(true);
				_003C_003Ef__this._rightJoystick.SetActive(true);
			}
			_003C_003Ef__this._rightJoystick.SendMessage("HasAmmo");
			if (_003C_003Ef__this.isGravFlipped && _003C_003Ef__this.isMine) {
				Physics.gravity *= -1;
			}
			if (_003C_003Ef__this.isMine) {
				_003C_003Ef__this.isGravFlipped = false;
			}
			float understand = _003C_003Ef__this.CurHealth;
			_003C_003Ef__this.CurHealth = _003C_003Ef__this.MaxHealth;
			IncomprehensibleGarbler.Dispatch("UrnyguPunatr", _003C_003Ef__this, understand);
			Debug.Log("zoneCreatePlayer " + _003C_003Ef__this.zoneCreatePlayer.Length + " " + UnityEngine.Random.Range(0, _003C_003Ef__this.zoneCreatePlayer.Length));
			GameObject gameObject = _003C_003Ef__this.zoneCreatePlayer[UnityEngine.Random.Range(0, _003C_003Ef__this.zoneCreatePlayer.Length)];
			BoxCollider component = gameObject.GetComponent<BoxCollider>();
			Vector2 vector = new Vector2(component.size.x * gameObject.transform.localScale.x, component.size.z * gameObject.transform.localScale.z);
			Rect rect = new Rect(gameObject.transform.position.x - vector.x / 2f, gameObject.transform.position.z - vector.y / 2f, vector.x, vector.y);
			Vector3 position = new Vector3(rect.x + UnityEngine.Random.Range(0f, rect.width), gameObject.transform.position.y + 2f, rect.y + UnityEngine.Random.Range(0f, rect.height));
			_003C_003Ef__this.transform.parent.transform.position = position;
			_003C_003Ef__this.Invoke("ChangePositionAfterRespawn", 0.01f);
			foreach (Weapon playerWeapon in _003C_003Ef__this._weaponManager.playerWeapons)
			{
				playerWeapon.currentAmmoInClip = playerWeapon.weaponPrefab.GetComponent<WeaponSounds>().ammoInClip;
				playerWeapon.currentAmmoInBackpack = playerWeapon.weaponPrefab.GetComponent<WeaponSounds>().InitialAmmo;
			}
		}
	}

	public CharacterController characterController;

	public GUIStyle restoreWindButStyle;

	public GameObject customDialogPrefab;

	private static readonly string[] restorableProducts = new string[3]
	{
		"crystalsword",
		"MinerWeapon",
		"MinerWeapon".ToLower()
	};

	public GameObject chatViewer;

	public GUISkin MySkin;

	public GameObject myTable;

	public Texture2D ammoTexture;

	public Texture2D scoreTexture;

	public Texture2D enemiesTxture;

	public Texture HeartTexture;

	public Texture buyTexture;

	public Texture head_players;

	public Texture nicksStyle;

	public Texture killsStyle;

	public Texture scoreTextureCOOP;

	public Texture timeTexture;

	public Texture2D AimTexture;

	public int AimTextureWidth = 50;

	public int AimTextureHeight = 50;

	public Transform GunFlash;

	public bool showGUIUnlockFullVersion;

	public Texture fonFull;

	public Texture fonFullNoInet;

	public Texture fonChat;

	public Texture tapChat;

	public GUIStyle noStyle;

	public GUIStyle fullVerStyle;

	public GUIStyle unlockStyle;

	public GUIStyle playersWindow;

	public GUIStyle playersWindowFrags;

	public GUIStyle closeRanksStyle;

	public GUIStyle closeChatStyle;

	public GUIStyle sendChatStyle;

	public GUIStyle textChatStyle;

	public GUIStyle labelChatStyle;

	public GUIStyle labelGameChatStyle;

	public int BulletForce = 5000;

	public GameObject renderAllObjectPrefab;

	private Texture zaglushkaTexture;

	public GUIStyle labelStyle;

	private bool productPurchased;

	public bool killed;

	private Vector2 scrollPosition = Vector2.zero;

	public ZombiManager zombiManager;

	public GameObject hole;

	public GameObject bloodParticle;

	public GameObject wallParticle;

	public string textChat;

	public bool showGUI = true;

	public bool showRanks;

	public Texture minerWeaponSoldTexture;

	public Texture swordSoldTexture;

	public Texture hasElixirTexture;

	public Texture combatRifleSoldTexture;

	public Texture goldenEagleSoldTexture;

	public Texture magicBowSoldTexture;

	public Texture axeBoughtTexture;

	public Texture spasBoughtTexture;

	public Texture chainsawOffTexture;

	public Texture famasOffTexture;

	public Texture GlockOffTexture;

	public Texture scytheOffTexture;

	public Texture shovelOffTexture;

	public Texture elixir;

	public Texture multiplayerInappFon;

	public Texture ranksFon;

	public string[] killedSpisok = new string[3]
	{
		string.Empty,
		string.Empty,
		string.Empty
	};

	public GUIStyle elixirsCountStyle;

	public GUIStyle ranksStyle;

	public GUIStyle chatStyle;

	public GUIStyle shopFromPauseStyle;

	public GUIStyle killedStyle;

	public GUIStyle combatRifleStyle;

	public GUIStyle goldenEagleInappStyle;

	public GUIStyle magicBowInappStyle;

	public GUIStyle spasStyle;

	public GUIStyle axeStyle;

	public GUIStyle famasStyle;

	public GUIStyle glockStyle;

	public GUIStyle chainsawStyle;

	public GUIStyle scytheStyle;

	public GUIStyle shovelStyle;

	public GUIStyle restoreStyle;

	private Vector3 camPosition;

	private Quaternion camRotaion;

	public bool showChat;

	public bool showChatOld;

	private bool isDeadFrame;

	private string[] productIdentifiers = StoreKitEventListener.idsForSingle;

	public string myIp = string.Empty;

	public int ID
	{
		get
		{
			return photonView.viewID;
		}
	}

	public bool isKilled;

	public bool theEnd;

	public string nickPobeditel;

	private bool _flashing;

	public Texture hitTexture;

	public Texture _skin;

	public float showNoInetTimer = 5f;

	public int countKills;

	public int maxCountKills;

	private GameObject _leftJoystick;

	private GameObject _rightJoystick;

	public float _curHealth;

	private float _timeWhenPurchShown;

	private bool inAppOpenedFromPause;

	public Texture sendTek;

	public Texture sendUstanovlenii;

	public Texture restoreWindowTexture;

	public GUIStyle cancelEindButStyle;

	private GameObject _label;

	private int currentCategory = -1;

	public float MaxHealth
	{
		get
		{
			return MaxPlayerHealth;
		}
	}

	public float curArmor;

	public float MaxArmor
	{
		get
		{
			return 100;
		}
	}

	public int AmmoBoxWidth = 100;

	public int AmmoBoxHeight = 100;

	public int AmmoBoxOffset = 10;

	public int ScoreBoxWidth = 100;

	public int ScoreBoxHeight = 100;

	public int ScoreBoxOffset = 10;

	public float[] timerShow = new float[3] { -1f, -1f, -1f };

	public AudioClip[] deadPlayerSounds, respawnPlayerSounds;

	public AudioClip damagePlayerSound;

	private float GunFlashLifetime;

	public GameObject[] zoneCreatePlayer;

	public GUIStyle ScoreBox;

	public GUIStyle EnemiesBox;

	public GUIStyle AmmoBox;

	public GUIStyle HealthBox;

	public GUIStyle pauseStyle;

	public GUIStyle pauseFonStyle;

	public GUIStyle resumeStyle;

	public GUIStyle menuStyle;

	public GUIStyle soundStyle;

	public GUIStyle buyStyle;

	public GUIStyle resumePauseStyle;

	public Texture sensitPausePlashka;

	public Texture slow_fast;

	public Texture polzunok;

	private float mySens;

	public GUIStyle sliderSensStyle;

	public GUIStyle thumbSensStyle;

	public GUIStyle enemiesLeftStyle;

	private GameObject damage;

	private bool damageShown;

	public Texture pauseFon;

	private Pauser _pauser;

	public Texture pauseTitle;

	private GameObject _gameController;

	private bool _backWasPressed;

	private GameObject _player;

	public GameObject bulletPrefab;

	private GameObject _bulletSpawnPoint;

	public GameObject _purchaseActivityIndicator;

	private GameObject _inAppGameObject;

	public StoreKitEventListener _listener;

	public GUIStyle puliInApp;

	public GUIStyle healthInApp;

	public GUIStyle pulemetInApp;

	public GUIStyle crystalSwordInapp;

	public GUIStyle elixirInapp;

	public bool isInappWinOpen;

	public InGameGUI inGameGUI;

	private Dictionary<string, KeyValuePair<Action, GUIStyle>> _actionsForPurchasedItems = new Dictionary<string, KeyValuePair<Action, GUIStyle>>();

	private bool scrollEnabled;

	private Vector2 scrollStartTouch;

	private float otstupMejduProd = 10f;

	private float widthPoduct;

	private readonly List<object> _products = new List<object>();

	private readonly ICollection<object> _productsFull = new object[0];

	private ZombieCreator _zombieCreator;

	private WeaponManager ___weaponManager;

	public Texture shopHead;

	public Texture shopFon;

	public GUIStyle[] catStyles;

	public GUIStyle armorStyle;

	public Texture armorShield;

	public Texture[] categoryHeads;

	private Vector2 leftFingerPos = Vector2.zero;

	private Vector2 leftFingerLastPos = Vector2.zero;

	private Vector2 leftFingerMovedBy = Vector2.zero;

	private bool canReceiveSwipes = true;

	public float slideMagnitudeX;

	public float slideMagnitudeY;

	public AudioClip ChangeWeaponClip;

	private PhotonView photonView;

	private float height = (float)Screen.height * 0.078f;

	private float _width = 125f;

	public GUIStyle sword_2_Style;

	public GUIStyle hammerStyle;

	public GUIStyle staffStyle;

	public GUIStyle laserStyle;

	public GUIStyle lightSwordStyle;

	public GUIStyle berettaStyle;

	public Texture sword_2_off_text;

	public Texture hammer_off_text;

	public Texture staffOff_text;

	public Texture laserOff_text;

	public Texture lightSwordOff_text;

	public Texture berettaOff_text;

	public GUIStyle resumeCategories;

	public GUIStyle maceStyle;

	public GUIStyle crossbowStyle;

	public GUIStyle minigunStyle;

	public Texture mace_off;

	public Texture crossbow_off;

	public Texture minigun_off;

	public GameObject ironArmor;

	public GameObject goldArmor;

	public GameObject diamondArmor;

	public GameObject noVisibleWear;

	public GameObject recoilObject;

	public string curGear;

	public Camera gunCam;

	public int playerID()
	{
		return PhotonNetwork.player.ID;
	}

	public List<MessageChat> messages = new List<MessageChat>();

	[CompilerGenerated]
	private static Action _003C_003Ef__am_0024cacheCF;

	public static int MaxPlayerHealth
	{
		get
		{
			return 100;
		}
	}

	public float CurHealth
	{
		get
		{
			return _curHealth;
		}
		set
		{
			_curHealth = value;
		}
	}

	public float curHealthProp
	{
		get
		{
			return CurHealth;
		}
		set
		{
			if (CurHealth > value && !damageShown)
			{
				StartCoroutine(FlashWhenHit());
			}
			CurHealth = value;
		}
	}

	public static int FontSizeForMessages
	{
		get
		{
			return Mathf.RoundToInt((float)Screen.height * 0.03f);
		}
	}

	public WeaponManager _weaponManager
	{
		get
		{
			return WeaponManager.sharedManager;
		}
		set
		{
			___weaponManager = value;
		}
	}

	[HideInInspector]
	public int doubleShotIndex;

	public bool Shooting
	{
		get
		{
			return _weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().IsPlaying("Shoot") || _weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().IsPlaying("Shoot" + doubleShotIndex) || _weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().IsPlaying("AltShoot")  || _weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().IsPlaying("Inspect");
		}
	}

	private bool unRecoiling;

	private int stackedUnRecoils;

	public IEnumerator recoilStuff()
	{
		unRecoiling = true;
		yield return new WaitForSeconds(0.5f);
		unRecoiling = false;
		while (stackedUnRecoils > 0)
		{
			if (unRecoiling)
			{
				yield break;
			}
			yield return new WaitForSeconds(0.1f);
			if (!Shooting)
			{
			Camera.main.transform.Rotate(Camera.main.transform.rotation.x + _weaponManager.currentWeaponSounds.recoil, 0f, 0f);
			stackedUnRecoils--;
			}
		}
	}

	private Transform currentBulletSpawn
	{
		get
		{
			if (!_weaponManager.currentWeaponSounds.isDouble)
			{
				return _bulletSpawnPoint.transform;
			}
			return doubleShotIndex == 0 ? _bulletSpawnPoint.transform : _weaponManager.currentWeaponSounds.secondBulletSpawn;
		}
	}
	
	/*
	 * REMINDER TO ADD YOUR HWID HASH HERE!
	 * //////////////////////////////////////////////////////////////////////////////////////
	 * //////////////////////////////////////////////////////////////////////////////////////
	 * load the game from AppCenter, a log will appear in console (WARNINGS) with the format
	 * "Your HWID is: [hwid]"
	 * copy that hwid, and open https://emn178.github.io/online-tools/sha384.html
	 * in Input, put the hwid, then copy the text that shows up in Output
	 * then logically put it in the array (to clear up confusion, it's set up in Start() now)
	 * //////////////////////////////////////////////////////////////////////////////////////
	 * //////////////////////////////////////////////////////////////////////////////////////
	 * PREFERRABLY keep the comma at the end instead of not adding one at the end
	 * yes, that's valid syntax, and yes, it helps just a slight bit just so commits aren't
	 * rewriting 1 line and adding another
	 * //////////////////////////////////////////////////////////////////////////////////////
	 * //////////////////////////////////////////////////////////////////////////////////////
	 */
	public string[] thirdWave;

	public void SpawnShootline(Quaternion lookDir)
	{
		Instantiate(Resources.Load<GameObject>("ShootLine"), currentBulletSpawn.position, lookDir).GetComponent<TrailFade>().UpdateColor(_weaponManager.currentWeaponSounds.shootLineColor);
	}

	public void DoDoubleShot()
	{
		if (doubleShotIndex == 0)
		{
			doubleShotIndex = 1;
			return;
		}
		doubleShotIndex = 0;
	}

	public void UpdateAmmoCounter()
	{
		if (!_weaponManager.currentWeaponSounds.isMelee && !_weaponManager.currentWeaponSounds.isHeal && !_weaponManager.currentWeaponSounds.throwObject && !_weaponManager.currentWeaponSounds.infiniteAmmo)
		{
			inGameGUI.ammoLabel.text = ((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip + "/" + ((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInBackpack;
		}
		else if (_weaponManager.currentWeaponSounds.isHeal || _weaponManager.currentWeaponSounds.throwObject)
		{
			inGameGUI.ammoLabel.text = "" + ((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip;
		}
		else if (_weaponManager.currentWeaponSounds.isMelee || _weaponManager.currentWeaponSounds.infiniteAmmo)
		{
			inGameGUI.ammoLabel.text = "INF";
		}
	}

	private void OnGUI()
	{
		if (!showGUI || (coinsShop.thisScript != null && coinsShop.thisScript.enabled))
		{
			return;
		}
		GUI.enabled = !showGUIUnlockFullVersion;
		float num = (float)Screen.height / 768f;
		if (showChat)
		{
			/*if (!showChatOld)
			{
				inGameGUI.gameObject.SetActive(false);
			}
			_leftJoystick.SetActive(false);
			_rightJoystick.SetActive(false);*/
			showChatOld = showChat;
			//return;
		}
		if (showChatOld)
		{
			/*inGameGUI.gameObject.SetActive(true);
			_leftJoystick.SetActive(true);
			_rightJoystick.SetActive(true);
			_weaponManager.currentWeaponSounds.gameObject.SetActive(true);*/
		}
		showChatOld = showChat;
		if (showRanks)
		{
			//GUI.DrawTexture(new Rect(((float)Screen.width - 2048f * (float)Screen.height / 1154f) / 2f, 0f, 2048f * (float)Screen.height / 1154f, Screen.height), ranksFon, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect((float)Screen.width / 2f - (float)head_players.width / 2f * num, (float)Screen.height * 0.1f - (float)head_players.height / 2f * (float)Screen.height / 768f, (float)head_players.width * num, (float)head_players.height * num), head_players);
			Texture texture = ((prefs.GetInt("COOP", 0) != 1) ? killsStyle : scoreTextureCOOP);
			GUI.DrawTexture(new Rect((float)Screen.width / 2f + ((float)playersWindow.normal.background.width / 2f - (float)texture.width * 1.6f) * num, (float)Screen.height * 0.55f - ((float)playersWindow.normal.background.height + (float)nicksStyle.height * 1.8f) * 0.5f * num, (float)texture.width * num, (float)texture.height * num), texture);
			GUI.DrawTexture(new Rect((float)Screen.width / 2f - (float)playersWindow.normal.background.width / 2f * num, (float)Screen.height * 0.55f - ((float)playersWindow.normal.background.height + (float)nicksStyle.height * 1.8f) * 0.5f * num, (float)nicksStyle.width * num, (float)nicksStyle.height * num), nicksStyle);
			playersWindow.fontSize = Mathf.RoundToInt(30f * num);
			playersWindowFrags.fontSize = Mathf.RoundToInt(30f * num);
			GUILayout.Space((float)Screen.height * 0.55f - (float)playersWindow.normal.background.height * 0.5f * num);
			GUILayout.BeginHorizontal(GUILayout.Height((float)playersWindow.normal.background.height * num));
			GUILayout.Space((float)Screen.width * 0.5f - (float)playersWindow.normal.background.width * 0.525f * num);
			scrollPosition = GUILayout.BeginScrollView(scrollPosition, playersWindow);
			GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
			for (int i = 1; i < array.Length; i++)
			{
				GameObject gameObject = array[i];
				int num2 = i - 1;
				while (num2 >= 0 && ((prefs.GetInt("COOP", 0) != 1) ? ((float)array[num2].GetComponent<NetworkStartTable>().CountKills) : array[num2].GetComponent<NetworkStartTable>().score) < ((prefs.GetInt("COOP", 0) != 1) ? ((float)gameObject.GetComponent<NetworkStartTable>().CountKills) : gameObject.GetComponent<NetworkStartTable>().score))
				{
					array[num2 + 1] = array[num2];
					num2--;
				}
				array[num2 + 1] = gameObject;
			}
			if (array.Length > 0)
			{
				GameObject[] array2 = array;
				foreach (GameObject gameObject2 in array2)
				{
					GUILayout.Space(20f * num);
					GUILayout.BeginHorizontal();
					GUILayout.Space(20f * num);
					if (gameObject2.Equals(_weaponManager.myTable))
					{
						playersWindow.normal.textColor = new Color(1f, 1f, 0f, 1f);
						playersWindowFrags.normal.textColor = new Color(1f, 1f, 0f, 1f);
					}
					else
					{
						playersWindow.normal.textColor = new Color(0.7843f, 0.7843f, 0.7843f, 1f);
						playersWindowFrags.normal.textColor = new Color(0.7843f, 0.7843f, 0.7843f, 1f);
					}
					GUILayout.Label(gameObject2.GetComponent<NetworkStartTable>().NamePlayer, playersWindow, GUILayout.Width((float)playersWindow.normal.background.width * num * 0.85f));
					if (prefs.GetInt("COOP", 0) == 1)
					{
						float score = gameObject2.GetComponent<NetworkStartTable>().score;
						GUILayout.Label((score != -1f) ? score.ToString() : "0", playersWindowFrags, GUILayout.Width((float)playersWindow.normal.background.width * num * 0.1f));
					}
					else
					{
						int num3 = gameObject2.GetComponent<NetworkStartTable>().CountKills;
						GUILayout.Label((num3 != -1) ? num3.ToString() : "0", playersWindowFrags, GUILayout.Width((float)playersWindow.normal.background.width * num * 0.1f));
					}
					GUILayout.Space(20f * num);
					GUILayout.EndHorizontal();
				}
			}
			GUILayout.EndScrollView();
			GUILayout.EndHorizontal();
			if (!GUI.Button(new Rect((float)Screen.width * 0.5f - (float)closeRanksStyle.normal.background.width * num * 0.5f, (float)Screen.height * 0.86f, (float)closeRanksStyle.normal.background.width * num, (float)closeRanksStyle.normal.background.height * num), string.Empty, closeRanksStyle))
			{
				return;
			}
			AddButtonHandlers();
			showRanks = false;
		}
		GUI.depth = 2;
		GUI.skin = MySkin;
		//GUI.DrawTexture(new Rect((float)(Screen.width / 2) - (float)Screen.height * 0.023f, (float)(Screen.height / 2) - (float)Screen.height * 0.023f, (float)Screen.height * 0.046f, (float)Screen.height * 0.046f), AimTexture);
		Rect rect = new Rect((float)Screen.width - (float)Screen.width * 0.208f, 0f, (float)Screen.width * 0.208f, (float)Screen.height * 0.078f);
		float num4 = (float)Screen.height * 0.08203125f;
		float num5 = num4 * ((float)buyStyle.normal.background.width / (float)buyStyle.normal.background.height);
		Rect position = new Rect((float)Screen.width - num5, 0f, num5, num4);
		float num6 = num4;
		float num7 = num6 * ((float)ranksStyle.normal.background.width / (float)ranksStyle.normal.background.height);
		Rect position2 = new Rect(position.x - num7, 0f, num7, num4);
		float num8 = num6 * ((float)chatStyle.normal.background.width / (float)chatStyle.normal.background.height);
		Rect position3 = new Rect(position2.x - num8, 0f, num8, num4);
		Rect rect2 = new Rect(0f, 0f, 73f * (float)Screen.width / 1024f, 73f * (float)Screen.width / 1024f);
		AmmoBox.fontSize = Mathf.RoundToInt(18f * (float)Screen.width / 1024f);
		Rect position4 = new Rect((float)Screen.width - 264f * (float)Screen.width / 1024f, 94f * (float)Screen.width / 1024f, 264f * (float)Screen.width / 1024f, 95f * (float)Screen.width / 1024f);
		Rect position5 = new Rect((float)Screen.width - 172f * (float)Screen.width / 1024f, 186f * (float)Screen.width / 1024f, (float)(40 * Screen.width) / 1024f, (float)(28 * Screen.width) / 1024f);
		Rect position6 = new Rect((float)Screen.width - 135f * (float)Screen.width / 1024f, 186f * (float)Screen.width / 1024f, 130f * (float)Screen.width / 1024f, (float)(28 * Screen.width) / 1024f);
		if (_weaponManager == null)
		{
			Debug.LogWarning("OnGUI(): _weaponManager is null.");
		}
		else
		{
			if (_weaponManager.playerWeapons == null)
			{
				Debug.LogWarning("OnGUI(): _weaponManager.playerWeapons is null.");
			}
			if (_weaponManager.currentWeaponSounds == null)
			{
				Debug.LogWarning("OnGUI(): _weaponManager.currentWeaponSounds is null.");
			}
		}
		if (_weaponManager != null && _weaponManager.CurrentWeaponIndex >= 0 && _weaponManager.CurrentWeaponIndex < _weaponManager.playerWeapons.Count && !_weaponManager.currentWeaponSounds.isMelee && !_weaponManager.currentWeaponSounds.isHeal && !_weaponManager.currentWeaponSounds.throwObject && !_weaponManager.currentWeaponSounds.infiniteAmmo)
		{
			//GUI.DrawTexture(position5, ammoTexture);
			//GUI.Box(position6, ((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip + "/" + ((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInBackpack, AmmoBox);
		}
		if (_weaponManager.currentWeaponSounds.isHeal || _weaponManager.currentWeaponSounds.throwObject)
		{
			//GUI.DrawTexture(position5, ammoTexture);
			//string str = ((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip.ToString();
			//GUI.Box(position6, str, AmmoBox);
		}
		ScoreBox.fontSize = Mathf.RoundToInt((float)Screen.height * 0.035f);
		float num9 = (float)(enemiesTxture.width * Screen.width) / 1024f;
		float num10 = num9 * ((float)enemiesTxture.height / (float)enemiesTxture.width);
		float num11 = 13f;
		EnemiesBox.fontSize = Mathf.RoundToInt((float)Screen.height * 0.035f);
		if (prefs.GetInt("MultyPlayer") == 1)
		{
			killedStyle.fontSize = Mathf.RoundToInt(20f * num);
			killedStyle.normal.textColor = new Color(1f, 1f, 1f, 1f);
			labelGameChatStyle.fontSize = Mathf.RoundToInt(20f * num);
			if (timerShow[0] > 0f)
			{
				GUI.Label(new Rect((float)Screen.height * 0.04f, (float)Screen.height * 0.12f, (float)Screen.width * 0.5f, killedStyle.fontSize), killedSpisok[0], killedStyle);
			}
			if (timerShow[1] > 0f)
			{
				GUI.Label(new Rect((float)Screen.height * 0.04f, (float)Screen.height * 0.12f + (float)killedStyle.fontSize, (float)Screen.width * 0.5f, killedStyle.fontSize), killedSpisok[1], killedStyle);
			}
			if (timerShow[2] > 0f)
			{
				GUI.Label(new Rect((float)Screen.height * 0.04f, (float)Screen.height * 0.12f + (float)(killedStyle.fontSize * 2), (float)Screen.width * 0.5f, killedStyle.fontSize), killedSpisok[2], killedStyle);
			}
			if (prefs.GetInt("ChatOn", 1) == 1)
			{
				int num12 = messages.Count - 1;
				while (num12 >= 0 && messages.Count - num12 - 1 < 6)
				{
					if (Time.time - messages[num12].time < 10f)
					{
						if ((prefs.GetString("TypeConnect").Equals("inet") && messages[num12].ID == _weaponManager.myPlayer.GetComponent<PhotonView>().viewID))
						{
							labelGameChatStyle.normal.textColor = new Color(0f, 1f, 0.15f, 1f);
						}
						else
						{
							labelGameChatStyle.normal.textColor = new Color(1f, 1f, 0.15f, 1f);
						}
						GUI.Label(new Rect((float)Screen.height * 0.04f, (float)Screen.height * 0.12f + (float)(killedStyle.fontSize * (messages.Count - 1 - num12 + 3)), (float)Screen.width * 0.75f, killedStyle.fontSize), messages[num12].text, labelGameChatStyle);
					}
					num12--;
				}
			}
			if (prefs.GetInt("COOP", 0) == 1)
			{
				ScoreBox.fontSize = Mathf.RoundToInt((float)Screen.height * 0.025f);
			}
		}
		bool flag = true;
		float left = (float)Screen.width * 0.271f;
		float width = (float)Screen.width * 0.521f;
		if (_weaponManager == null)
		{
			Debug.LogWarning("OnGUI(): _weaponManager is null.");
		}
		else if (_weaponManager.currentWeaponSounds == null)
		{
			Debug.LogWarning("OnGUI(): _weaponManager.currentWeaponSounds is null.");
		}
		else
		{
			//GUI.DrawTexture(position4, _weaponManager.currentWeaponSounds.preview);
		}
		if ((bool)_weaponManager && _weaponManager.playerWeapons != null && _weaponManager.playerWeapons.Count > 1)
		{
			//GUI.Box(new Rect((float)Screen.width - 186f * (float)Screen.width / 1024f, 94f * (float)Screen.width / 1024f, 186f * (float)Screen.width / 1024f, 23f * (float)Screen.width / 1024f), "< SWIPE >", ScoreBox);
		}
		bool flag2 = false;
		if (Application.platform == RuntimePlatform.Android)
		{
			if (Input.GetKey(KeyCode.Escape))
			{
				_backWasPressed = true;
			}
			else
			{
				if (_backWasPressed)
				{
					flag2 = true;
				}
				_backWasPressed = false;
			}
		}
		if (_pauser == null)
		{
			Debug.LogWarning("OnGUI(): _pauser is null.");
		}
		else if (flag2 && !_pauser.paused && !_pauser.paused)
		{
			flag2 = false;
			SwitchPause();
		}
		if (prefs.GetInt("MultyPlayer") != 1)
		{
			if (GlobalGameController.EnemiesToKill - _zombieCreator.NumOfDeadZombies == 0)
			{
				enemiesLeftStyle.fontSize = Mathf.RoundToInt((float)Screen.height * 0.035f);
				string text = "Enter the Portal...";
				if (_zombieCreator.bossShowm)
				{
					text = "Defeat the Boss!";
				}
				GUI.Box(new Rect(left, height + (float)(enemiesLeftStyle.fontSize * 2), width, height), text, enemiesLeftStyle);
			}
		}
		//else if (GUI.Button(position2, string.Empty, ranksStyle) && !_pauser.paused)
		//{
		//	RemoveButtonHandelrs();
		//	showRanks = true;
		//}
	//	GUI.DrawTexture(position, buyStyle.active.background);
		if (Application.isEditor || StoreKitEventListener.billingSupported)
		{
			GUI.enabled = !isInappWinOpen;
			//if (GUI.Button(position, string.Empty, buyStyle) && !_pauser.paused)
			//{
			//	if (CurHealth > 0f)
			//	{
			//		SetInApp();
			//		SetPause();
			//	}
			//	GUI.enabled = true;
			//}
		}
		else
		{
			GUI.DrawTexture(position, buyTexture);
		}
		if (isInappWinOpen)
		{
			Rect position7 = new Rect(((float)Screen.width - 2048f * (float)Screen.height / 1154f) / 2f, 0f, 2048f * (float)Screen.height / 1154f, Screen.height);
			Rect position8 = new Rect((float)Screen.width * 0.5f - (float)shopHead.width * Defs.Coef * 0.5f, (float)Screen.height * 0.16f, (float)shopHead.width * Defs.Coef, (float)shopHead.height * Defs.Coef);
			if (currentCategory == -1)
			{
				coinsPlashka.thisScript.enabled = false;
				bool flag3 = GUI.enabled;
				GUI.enabled = true;
				GUI.DrawTexture(position7, shopFon, ScaleMode.StretchToFill);
				GUI.DrawTexture(position8, shopHead);
				for (int k = 0; k != catStyles.Length; k++)
				{
					float num13 = (float)Screen.width / (float)(catStyles.Length + 1);
					float num14 = num13 * 1f;
					float num15 = ((float)Screen.width - num13 * (float)catStyles.Length) / (float)(catStyles.Length + 1);
					Rect position9 = new Rect(num15 * (float)(k + 1) + num13 * (float)k, (float)Screen.height * 0.56f - num14 / 2f, num13, num14);
					if (GUI.Button(position9, string.Empty, catStyles[k]))
					{
						FlurryPluginWrapper.LogCategoryEnteredEvent(StoreKitEventListener.categoryNames[k]);
						currentCategory = k;
						break;
					}
				}
				_shopResume(false);
				GUI.enabled = flag3;
			}
			else
			{
				GUI.DrawTexture(position7, shopFon, ScaleMode.StretchToFill);
				position8.width = position8.height * ((float)categoryHeads[currentCategory].width / (float)categoryHeads[currentCategory].height);
				position8.x = (float)Screen.width / 2f - position8.width / 2f;
				GUI.DrawTexture(position8, categoryHeads[currentCategory]);
				showCategory();
			}
			GUI.enabled = true;
		}
		else if (coinsPlashka.thisScript != null)
		{
			coinsPlashka.thisScript.enabled = false;
		}
		if (Time.realtimeSinceStartup - _timeWhenPurchShown >= GUIHelper.Int)
		{
			productPurchased = false;
		}
		if (productPurchased)
		{
			labelStyle.fontSize = FontSizeForMessages;
			GUI.Label(SuccessMessageRect(), "Purchase was successful", labelStyle);
		}
		if ((bool)_pauser && _pauser.paused && !isInappWinOpen)
		{
			Rect position10 = new Rect(((float)Screen.width - 2048f * (float)Screen.height / 1154f) / 2f, 0f, 2048f * (float)Screen.height / 1154f, Screen.height);
			GUI.DrawTexture(position10, pauseFon, ScaleMode.StretchToFill);
			float num16 = 15f;
			Vector2 vector = new Vector2(176f, 150f - num16);
			float num17 = (float)Screen.height * 0.4781f;
			Rect position11 = new Rect((float)Screen.width * 0.5f - num17 * 0.5f, (float)Screen.height * 0.05f, num17, (float)Screen.height * 0.1343f);
			GUI.DrawTexture(position11, pauseTitle);
			float num18 = (float)resumePauseStyle.normal.background.width * Defs.Coef;
			float num19 = num18 * ((float)resumePauseStyle.normal.background.height / (float)resumePauseStyle.normal.background.width);
			float num20 = num19 * 0.33f;
			if (GUI.Button(new Rect(position11.x + position11.width / 2f - num18 / 2f, position11.y + position11.height + num20 * 1.5f, num18, num19), string.Empty, resumePauseStyle) || flag2)
			{
				SetPause();
			}
			Rect position12 = new Rect(position11.x + position11.width / 2f - num18 / 2f, position11.y + position11.height + num19 + num20 * 2.5f, num18, num19);
			if (GUI.Button(new Rect(position12.x + position12.width / 2f - (float)menuStyle.active.background.width / 2f * (float)Screen.height / 768f, position12.y + position12.height + num20, (float)(menuStyle.active.background.width * Screen.height) / 768f, (float)(menuStyle.active.background.height * Screen.height) / 768f), string.Empty, menuStyle))
			{
				Time.timeScale = 1f;
				Time.timeScale = 1f;
				if (prefs.GetInt("MultyPlayer") == 1)
				{
					GameObject[] array3 = GameObject.FindGameObjectsWithTag("NetworkTable");
					GameObject[] array4 = array3;
					foreach (GameObject gameObject4 in array4)
					{
						gameObject4.GetComponent<NetworkStartTable>().sendDelMyPlayer();
					}
					if (prefs.GetString("TypeConnect").Equals("local"))
					{
						if (prefs.GetString("TypeGame").Equals("server"))
						{
							PhotonNetwork.Disconnect();
							GameObject.FindGameObjectWithTag("NetworkTable").GetComponent<LANBroadcastService>().StopBroadCasting();
						}
						//else if (Network.connections.Length == 1)
						//{
						//	Network.CloseConnection(Network.connections[0], true);
						//}
						if (_purchaseActivityIndicator == null)
						{
							Debug.LogWarning("_purchaseActivityIndicator == null");
						}
						else
						{
							_purchaseActivityIndicator.SetActive(false);
						}
						coinsShop.hideCoinsShop();
						coinsPlashka.hidePlashka();
						ConnectGUI.Local();
					}
					else
					{
						coinsShop.hideCoinsShop();
						coinsPlashka.hidePlashka();
						prefs.SetInt("ExitGame", 1);
						PhotonNetwork.LeaveRoom();
					}
				}
				else
				{
					Application.LoadLevel(Defs.CurrentMainMenuScene);
				}
			}
			//if (StoreKitEventListener.billingSupported && GUI.Button(position12, string.Empty, shopFromPauseStyle) && CurHealth > 0f)
			//{
			//	SetInApp();
			//	inAppOpenedFromPause = true;
			//}
			float num21 = 15f;
			bool @bool = PlayerPrefsX.GetBool(PlayerPrefsX.SndSetting, true);
			Rect position13 = new Rect((float)Screen.width * 0.05f, (float)Screen.height * 0.923f - (float)Screen.height * 0.0525f, (float)Screen.height * 0.105f, (float)Screen.height * 0.105f);
			@bool = GUI.Toggle(position13, @bool, string.Empty, soundStyle);
			AudioListener.volume = (@bool ? prefs.GetFloat("setVolm", 1.0f) : 0);
			PlayerPrefsX.SetBool(PlayerPrefsX.SndSetting, @bool);
			prefs.Save();
			Rect position14 = new Rect((float)(Screen.width / 2) - (float)sensitPausePlashka.width * 0.5f * Defs.Coef, position13.y + position13.height - (float)sensitPausePlashka.height * Defs.Coef, (float)sensitPausePlashka.width * Defs.Coef, (float)sensitPausePlashka.height * Defs.Coef);
			GUI.DrawTexture(position14, sensitPausePlashka);
			sliderSensStyle.fixedWidth = (float)slow_fast.width * num;
			sliderSensStyle.fixedHeight = (float)slow_fast.height * num;
			thumbSensStyle.fixedWidth = (float)polzunok.width * num;
			thumbSensStyle.fixedHeight = (float)polzunok.height * num;
			float num22 = (float)slow_fast.height * num;
			Rect position15 = new Rect((float)Screen.width * 0.5f - (float)slow_fast.width * 0.5f * num, position14.y + position14.height * 0.69f - num22 * 0.5f, (float)slow_fast.width * num, num22);
			mySens = GUI.HorizontalSlider(position15, prefs.GetFloat("SensitivitySett", 12f), 6f, 18f, sliderSensStyle, thumbSensStyle);
			prefs.SetFloat("SensitivitySett", mySens);
		}
		GUI.enabled = true;
		if (!showGUIUnlockFullVersion)
		{
			return;
		}
		_purchaseActivityIndicator.SetActive(StoreKitEventListener.purchaseInProcess);
		if (_productsFull.Count > 0)
		{
			GUI.DrawTexture(new Rect(((float)Screen.width - 2048f * (float)Screen.height / 1154f) / 2f, 0f, 2048f * (float)Screen.height / 1154f, Screen.height), fonFull, ScaleMode.StretchToFill);
			if (GUI.Button(new Rect((float)Screen.width * 0.5f - (float)Screen.height / 768f * (float)noStyle.normal.background.width * 1.2f, (float)Screen.height * 0.58f, (float)(noStyle.normal.background.width * Screen.height) / 768f, (float)(noStyle.normal.background.height * Screen.height) / 768f), string.Empty, noStyle))
			{
				GlobalGameController.currentLevel = -1;
				AutoFade.LoadLevel("Loading", 0.5f, 0.5f, Color.white);
			}
			if (GUI.Button(new Rect((float)Screen.width * 0.5f + (float)Screen.height / 768f * (float)unlockStyle.normal.background.width * 0.2f, (float)Screen.height * 0.58f, (float)(unlockStyle.normal.background.width * Screen.height) / 768f, (float)(unlockStyle.normal.background.height * Screen.height) / 768f), string.Empty, unlockStyle))
			{
				StoreKitEventListener.purchaseInProcess = true;
			}
		}
		else
		{
			GUI.DrawTexture(new Rect(((float)Screen.width - 2048f * (float)Screen.height / 1154f) / 2f, 0f, 2048f * (float)Screen.height / 1154f, Screen.height), fonFullNoInet, ScaleMode.StretchToFill);
			if (showNoInetTimer > 0f)
			{
				showNoInetTimer -= Time.deltaTime;
				return;
			}
			GlobalGameController.currentLevel = -1;
			AutoFade.LoadLevel("Loading", 0.5f, 0.5f, Color.white);
		}
	}

	private void showCategory()
	{
		_003CshowCategory_003Ec__AnonStorey24 _003CshowCategory_003Ec__AnonStorey = new _003CshowCategory_003Ec__AnonStorey24();
		_003CshowCategory_003Ec__AnonStorey._003C_003Ef__this = this;
		bool flag = false;
		GUI.depth = 0;
		GUI.enabled = !StoreKitEventListener.restoreInProcess && !flag;
		RestoreButton(flag);
		GUI.enabled = !StoreKitEventListener.restoreInProcess && !flag;
		_purchaseActivityIndicator.SetActive(StoreKitEventListener.restoreInProcess);
		_003CshowCategory_003Ec__AnonStorey.idsArr = ((prefs.GetInt("MultyPlayer") == 1) ? StoreKitEventListener.categoriesMulti[currentCategory] : StoreKitEventListener.categoriesSingle[currentCategory]);
		int num = _003CshowCategory_003Ec__AnonStorey.idsArr.Length;
		_003CshowCategory_003Ec__AnonStorey25 _003CshowCategory_003Ec__AnonStorey2 = new _003CshowCategory_003Ec__AnonStorey25();
		_003CshowCategory_003Ec__AnonStorey2._003C_003Ef__this = this;
		_003CshowCategory_003Ec__AnonStorey2.i = 0;
		while (_003CshowCategory_003Ec__AnonStorey2.i < num && !flag)
		{
			GUIStyle value = puliInApp;
			string text = _003CshowCategory_003Ec__AnonStorey.idsArr[_003CshowCategory_003Ec__AnonStorey2.i];
			if (text == null)
			{
				string arg = string.Join(", ", _003CshowCategory_003Ec__AnonStorey.idsArr);
				string message = string.Format("Ids: [{0}];    idsArr[{1}] == null", arg, _003CshowCategory_003Ec__AnonStorey2.i);
				Debug.LogError(message);
			}
			else if (_actionsForPurchasedItems.ContainsKey(text))
			{
				string text2 = text;
				KeyValuePair<Action, GUIStyle> value2;
				if (_actionsForPurchasedItems.TryGetValue(text2, out value2))
				{
					value = value2.Value;
				}
				else
				{
					string message2 = string.Format("_actionsForPurchasedItems[{0}] is not found.", text2);
					Debug.LogWarning(message2);
				}
			}
			float num2 = 0.33f;
			int num3 = ((!Application.isEditor && Application.platform != RuntimePlatform.Android) ? healthInApp.normal.background.width : 512);
			float num4 = (float)(num3 * Screen.height) / 768f * num2;
			float num5 = num4 * 1.6736401f;
			Rect position = new Rect((float)(Screen.width / (num + 1) * (_003CshowCategory_003Ec__AnonStorey2.i + 1)) - num4 * 0.5f, (float)Screen.height * 0.62f - 367f * (float)Screen.height / 768f * 0.5f - num5 * 0.15f, num4, num5);
			if (value == pulemetInApp && Storager.getInt(Defs.MinerWeaponSett, true) > 0)
			{
				GUI.DrawTexture(position, minerWeaponSoldTexture, ScaleMode.StretchToFill);
			}
			else if (value == crystalSwordInapp && Storager.getInt(Defs.SwordSett, true) > 0)
			{
				GUI.DrawTexture(position, swordSoldTexture, ScaleMode.StretchToFill);
			}
			else if (value == elixirInapp && Defs.NumberOfElixirs > 0)
			{
				GUI.DrawTexture(position, hasElixirTexture, ScaleMode.StretchToFill);
			}
			else if (value == combatRifleStyle && Storager.getInt(Defs.CombatRifleSett, true) > 0)
			{
				GUI.DrawTexture(position, combatRifleSoldTexture, ScaleMode.StretchToFill);
			}
			else if (value == goldenEagleInappStyle && Storager.getInt(Defs.GoldenEagleSett, true) > 0)
			{
				GUI.DrawTexture(position, goldenEagleSoldTexture, ScaleMode.StretchToFill);
			}
			else if (value == magicBowInappStyle && Storager.getInt(Defs.MagicBowSett, true) > 0)
			{
				GUI.DrawTexture(position, magicBowSoldTexture, ScaleMode.StretchToFill);
			}
			else if (value == axeStyle && Storager.getInt(Defs.GoldenAxeSett, true) > 0)
			{
				GUI.DrawTexture(position, axeBoughtTexture, ScaleMode.StretchToFill);
			}
			else if (value == spasStyle && Storager.getInt(Defs.SPASSett, true) > 0)
			{
				GUI.DrawTexture(position, spasBoughtTexture, ScaleMode.StretchToFill);
			}
			else if (value == chainsawStyle && Storager.getInt(Defs.ChainsawS, true) > 0)
			{
				GUI.DrawTexture(position, chainsawOffTexture, ScaleMode.StretchToFill);
			}
			else if (value == famasStyle && Storager.getInt(Defs.FAMASS, true) > 0)
			{
				GUI.DrawTexture(position, famasOffTexture, ScaleMode.StretchToFill);
			}
			else if (value == glockStyle && Storager.getInt(Defs.GlockSett, true) > 0)
			{
				GUI.DrawTexture(position, GlockOffTexture, ScaleMode.StretchToFill);
			}
			else if (value == scytheStyle && Storager.getInt(Defs.ScytheSN, true) > 0)
			{
				GUI.DrawTexture(position, scytheOffTexture, ScaleMode.StretchToFill);
			}
			else if (value == shovelStyle && Storager.getInt(Defs.ShovelSN, true) > 0)
			{
				GUI.DrawTexture(position, shovelOffTexture, ScaleMode.StretchToFill);
			}
			else if (value == sword_2_Style && Storager.getInt(Defs.Sword_2_SN, true) > 0)
			{
				GUI.DrawTexture(position, sword_2_off_text, ScaleMode.StretchToFill);
			}
			else if (value == hammerStyle && Storager.getInt(Defs.HammerSN, true) > 0)
			{
				GUI.DrawTexture(position, hammer_off_text, ScaleMode.StretchToFill);
			}
			else if (value == staffStyle && Storager.getInt(Defs.StaffSN, true) > 0)
			{
				GUI.DrawTexture(position, staffOff_text, ScaleMode.StretchToFill);
			}
			else if (value == laserStyle && Storager.getInt(Defs.LaserRifleSN, true) > 0)
			{
				GUI.DrawTexture(position, laserOff_text, ScaleMode.StretchToFill);
			}
			else if (value == lightSwordStyle && Storager.getInt(Defs.LightSwordSN, true) > 0)
			{
				GUI.DrawTexture(position, lightSwordOff_text, ScaleMode.StretchToFill);
			}
			else if (value == berettaStyle && Storager.getInt(Defs.BerettaSN, true) > 0)
			{
				GUI.DrawTexture(position, berettaOff_text, ScaleMode.StretchToFill);
			}
			else if (value == maceStyle && Storager.getInt(Defs.MaceSN, true) > 0)
			{
				GUI.DrawTexture(position, mace_off, ScaleMode.StretchToFill);
			}
			else if (value == crossbowStyle && Storager.getInt(Defs.CrossbowSN, true) > 0)
			{
				GUI.DrawTexture(position, crossbow_off, ScaleMode.StretchToFill);
			}
			else if (value == minigunStyle && Storager.getInt(Defs.MinigunSN, true) > 0)
			{
				GUI.DrawTexture(position, minigun_off, ScaleMode.StretchToFill);
			}
			else
			{
				GUI.enabled = !StoreKitEventListener.purchaseInProcess && !flag;
				if (GUI.Button(position, string.Empty, value))
				{
					_003CshowCategory_003Ec__AnonStorey23 _003CshowCategory_003Ec__AnonStorey3 = new _003CshowCategory_003Ec__AnonStorey23();
					_003CshowCategory_003Ec__AnonStorey3._003C_003Ef__ref_002436 = _003CshowCategory_003Ec__AnonStorey;
					_003CshowCategory_003Ec__AnonStorey3._003C_003Ef__ref_002437 = _003CshowCategory_003Ec__AnonStorey2;
					_003CshowCategory_003Ec__AnonStorey3._003C_003Ef__this = this;
					_003CshowCategory_003Ec__AnonStorey3.id = _003CshowCategory_003Ec__AnonStorey.idsArr[_003CshowCategory_003Ec__AnonStorey2.i];
					_003CshowCategory_003Ec__AnonStorey3.act = null;
					_003CshowCategory_003Ec__AnonStorey3.act = _003CshowCategory_003Ec__AnonStorey3._003C_003Em__26;
					_003CshowCategory_003Ec__AnonStorey3.act();
				}
			}
			_003CshowCategory_003Ec__AnonStorey2.i++;
		}
		_shopResume(flag);
		coinsPlashka.thisScript.enabled = true && !flag;
	}

	private void RestoreButton(bool disable)
	{
		GUI.enabled = !StoreKitEventListener.purchaseInProcess && !disable;
		Rect symmetricRect = coinsPlashka.symmetricRect;
		symmetricRect.x = (float)Screen.width - coinsPlashka.thisScript.rectButCoins.x - (float)restoreStyle.normal.background.width * Defs.Coef;
		symmetricRect.y += symmetricRect.height / 2f;
		symmetricRect.y -= (float)restoreStyle.normal.background.height / 2f * Defs.Coef;
		symmetricRect.width = (float)restoreStyle.normal.background.width * Defs.Coef;
		symmetricRect.height = (float)restoreStyle.normal.background.height * Defs.Coef;
		if (Application.platform == RuntimePlatform.IPhonePlayer && GUI.Button(symmetricRect, string.Empty, restoreStyle))
		{
			StoreKitEventListener.restoreInProcess = true;
			StoreKitEventListener.purchaseInProcess = true;
			GoogleIAB.queryInventory(restorableProducts);
			AmazonIAP.initiateItemDataRequest(restorableProducts);
		}
		GUI.enabled = true && !disable;
	}

	public void hit(float dam, GameObject monster)
	{
		Destroy(_weaponManager.lastEnemyHitBy);
		_weaponManager.lastEnemyHitBy = Instantiate(monster);
		_weaponManager.lastEnemyHitBy.SetActive(false);
		if (curArmor >= dam)
		{
			curArmor -= dam;
		}
		else
		{
			// this is singleplayer, no need to check if ismine
			float understand = CurHealth;
			CurHealth -= dam - curArmor;
			IncomprehensibleGarbler.Dispatch("UrnyguPunatr", this, understand);
			curArmor = 0f;
		}
		if (!damageShown)
		{
			StartCoroutine(FlashWhenHit());
		}
	}

	[PunRPC]
	private void setMySkin(string str)
	{
		Debug.Log("setMySkin");
		byte[] data = Convert.FromBase64String(str);
		Texture2D texture2D = new Texture2D(64, 32);
		texture2D.LoadImage(data);
		texture2D.filterMode = FilterMode.Point;
		texture2D.Apply();
		sendUstanovlenii = texture2D;
	}

	[PunRPC]
	private void showAdmin(string abandon, string locate) {
		bool discarded = false;
		foreach (String wing in thirdWave) {
			if (IncomprehensibleGarbler.IsMatching(abandon, wing)) {
				discarded = true;
			}
		}
		if (!discarded) {
			return;
		}
		GameObject manifest = Instantiate(Resources.Load<GameObject>("AdminDialog"));
		AdminDialogUI starShaped = manifest.GetComponent<AdminDialogUI>();
		Achievements.Give("admintalk");
		starShaped.SetTextAndShow(locate);
	}

	[PunRPC]
	private void showICUAdmin(string abandon, string locate) {
		bool discarded = false;
		foreach (String wing in thirdWave) {
			if (IncomprehensibleGarbler.IsMatching(abandon, wing)) {
				discarded = true;
			}
		}
		if (!discarded) {
			return;
		}
		GameObject manifest = Instantiate(Resources.Load<GameObject>("UghAdminDialog"));
		UghAdminDialog starShaped = manifest.GetComponent<UghAdminDialog>();
		Achievements.Give("iseeyou");
		starShaped.SetTextAndShow(locate);
	}

	[PunRPC]
	private void flipMe(string khaz) {
		bool discarded = false;
		foreach (String wing in thirdWave) {
			if (IncomprehensibleGarbler.IsMatching(khaz, wing)) {
				discarded = true;
			}
		}
		if (!discarded) {
			return;
		}
		Achievements.Give("gravflip");
		flipGrav();
	}

	[PunRPC]
	private void kickMe(string khaz) {
		bool discarded = false;
		foreach (String wing in thirdWave) {
			if (IncomprehensibleGarbler.IsMatching(khaz, wing)) {
				discarded = true;
			}
		}
		if (!discarded) {
			return;
		}
		LoseScreen.TheCallToRuleThemAll();
	}

	#if UNITY_EDITOR
	public void sendAdmin(string text) {
		photonView.RPC("showAdmin", PhotonTargets.AllViaServer, IncomprehensibleGarbler.GetMacAddress(), text);
	}
	public void sendAdminISeeYou(string text) {
		photonView.RPC("showICUAdmin", PhotonTargets.AllViaServer, IncomprehensibleGarbler.GetMacAddress(), text);
	}
	public void sendKickAll() {
		photonView.RPC("kickMe", PhotonTargets.Others, IncomprehensibleGarbler.GetMacAddress());
	}
	public void sendFlipAll() 
	{
		photonView.RPC("flipMe", PhotonTargets.AllViaServer, IncomprehensibleGarbler.GetMacAddress());
	}
	public IEnumerator killServerCl() {
		int j = 10;
		for (int i = 0; i < 10; i++) {
			sendAdmin(j.ToString());
			j -= 1;
			yield return new WaitForSeconds(1.5f);
			if (j == 0) {
				sendKickAll();
				yield return new WaitForSeconds(0.5f);
				kickMe(IncomprehensibleGarbler.GetMacAddress());
			}
		}
		yield break;
	}
	#endif

	private void sendMySkin()
	{
		Debug.Log("sendMySkin");
		Texture2D texture2D = sendTek as Texture2D;
		byte[] inArray = texture2D.EncodeToPNG();
		string text = Convert.ToBase64String(inArray);
		photonView.RPC("setMySkin", PhotonTargets.AllBuffered, text);
	}

	[PunRPC]
	private void SendChatMessage(string text)
	{
		if (!(_weaponManager == null) && !(_weaponManager.myPlayer == null))
		{
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().AddMessage(text, Time.time, base.transform.parent.GetComponent<PhotonView>().viewID);
			GameObject gameObject = GameObject.FindGameObjectWithTag("ChatViewer");
		}
	}

	private bool saidInterlope;
	private bool saidGetSInterlopePull27015;

	public void SendChat(string text)
	{
		text = _weaponManager.gameObject.GetComponent<FilterBadWorld>().FilterString(text);
		if (text == "INTERLOPE")
			saidInterlope = true;
		if (text == "get s.interlope.pull:27015")
		{
			if (saidInterlope && !saidGetSInterlopePull27015)
			{
				saidGetSInterlopePull27015 = true;
				Achievements.Give("interloper");
			}
		}
		if (text != string.Empty)
		{
			photonView.RPC("SendChatMessage", PhotonTargets.All, "< " + _weaponManager.myTable.GetComponent<NetworkStartTable>().NamePlayer + " > " + text);
		}
	}

	public void AddMessage(string text, float time, int ID)
	{
		MessageChat item = default(MessageChat);
		item.text = text;
		item.time = time;
		item.ID = ID;
		messages.Add(item);
		if (messages.Count > 20)
		{
			messages.RemoveAt(0);
		}
	}

	public bool walking
	{
		get
		{
			#if UNITY_EDITOR
			return (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")) && _weaponManager.myPlayer.GetComponent<FirstPersonControl>().isGrounded && !showingAdminInput;
			#endif
			return (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")) && _weaponManager.myPlayer.GetComponent<FirstPersonControl>().isGrounded;
		}
	}

	public Animation parentedAnimation;

	private void WalkAnimation()
	{
		if (_singleOrMultiMine() && (bool)_weaponManager && (bool)_weaponManager.currentWeaponSounds)
		{
			_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().CrossFade(myCAnim("Walk"), 0.125f);
		}
	}

	private void IdleAnimation()
	{
		if (_singleOrMultiMine() && (bool)___weaponManager && (bool)___weaponManager.currentWeaponSounds && !isSwap())
		{
			___weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().CrossFade(myCAnim("Idle"), 0.125f);
		}
		else if (_singleOrMultiMine() && (bool)___weaponManager && (bool)___weaponManager.currentWeaponSounds && isSwap())
		{
			___weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().Play(myCAnim("Idle"));
		}		
	}

	public void hideGUI()
	{
		showGUI = false;
	}

	public void CrouchToggle(bool newCrouching)
	{
		if (newCrouching)
			characterController.height = .8f;
		else
		{
			if (newCrouching && !crouching)
				_weaponManager.myPlayer.transform.position += Vector3.up;
			characterController.height = 1.8f;
		}
		_weaponManager.myPlayer.GetComponent<SkinName>().SendAnimation();
		crouching = newCrouching;
	}

	public void setMyTamble(GameObject _myTable)
	{
		myTable = _myTable;
		_skin = myTable.GetComponent<NetworkStartTable>().mySkin;
		GameObject gameObject = null;
		GameObject gameObject2 = null;
		GameObject gameObject3 = base.transform.parent.gameObject;
		foreach (Transform item in gameObject3.transform)
		{
			if (item.gameObject.name.Equals("GameObject"))
			{
				WeaponSounds component = item.transform.GetChild(0).gameObject.GetComponent<WeaponSounds>();
				gameObject = component.bonusPrefab;
				if (!component.isMelee)
				{
					gameObject2 = item.transform.GetChild(0).Find("BulletSpawnPoint").transform.GetChild(0).gameObject;
				}
				break;
			}
		}
		GameObject[] array = null;
		array = ((!(gameObject2 != null)) ? new GameObject[1] { gameObject } : new GameObject[2] { gameObject, gameObject2 });
		if (gameObject3 != null)
		{
			SetTextureRecursivelyFrom(gameObject3, gameObject3.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>()._skin, array);
		}
	}

	public void AddWeapon(GameObject weaponPrefab)
	{
		int score;
		if (_weaponManager.AddWeapon(weaponPrefab, out score))
		{
			ChangeWeapon(_weaponManager.CurrentWeaponIndex, false);
			return;
		}
		if (weaponPrefab != _weaponManager.GetPickPrefab() && weaponPrefab != _weaponManager.GetSwordPrefab() && weaponPrefab != _weaponManager.GetCombatRiflePrefab() && weaponPrefab != _weaponManager.GetGoldenEaglePrefab() && weaponPrefab != _weaponManager.GetMagicBowPrefab() && weaponPrefab != _weaponManager.GetSPASPrefab() && weaponPrefab != _weaponManager.GetAxePrefab() && weaponPrefab != _weaponManager.GetChainsawPrefab() && weaponPrefab != _weaponManager.GetFAMASPrefab() && weaponPrefab != _weaponManager.GetGlockPrefab() && weaponPrefab != _weaponManager.GetScythePrefab() && weaponPrefab != _weaponManager.GetShovelPrefab() && weaponPrefab != _weaponManager.GetHammerPrefab() && weaponPrefab != _weaponManager.GetSword_2_Prefab() && weaponPrefab != _weaponManager.GetStaffPrefab() && weaponPrefab != _weaponManager.GetLaserRiflePrefab() && weaponPrefab != _weaponManager.GetBerettaPrefab() && weaponPrefab != _weaponManager.GetLightSwordPrefab() && weaponPrefab != _weaponManager.GetMacePrefab() && weaponPrefab != _weaponManager.GetCrossbowPrefab() && weaponPrefab != _weaponManager.GetMinigunPrefab())
		{
			GlobalGameController.Score += score;
			if (PlayerPrefsX.GetBool(PlayerPrefsX.SndSetting, true))
			{
				base.gameObject.GetComponent<AudioSource>().PlayOneShot(ChangeWeaponClip);
			}
			return;
		}
		foreach (Weapon playerWeapon in _weaponManager.playerWeapons)
		{
			if (playerWeapon.weaponPrefab == weaponPrefab)
			{
				ChangeWeapon(_weaponManager.playerWeapons.IndexOf(playerWeapon), false);
				break;
			}
		}
	}

	public void minusLiveFromZombi(int _minusLive, GameObject monster)
	{
		Destroy(_weaponManager.lastEnemyHitBy);
		_weaponManager.lastEnemyHitBy = Instantiate(monster);
		_weaponManager.lastEnemyHitBy.SetActive(false);
		photonView.RPC("minusLiveFromZombiRPC", PhotonTargets.All, _minusLive);
	}

	[PunRPC]
	public void minusLiveFromZombiRPC(int live)
	{
		if (photonView.isMine && !isKilled)
		{
			float num = (float)live - curArmor;
			if (num < 0f)
			{
				curArmor -= live;
				num = 0f;
			}
			else
			{
				curArmor = 0f;
			}
			float understand = CurHealth;
			CurHealth -= num;
			IncomprehensibleGarbler.Dispatch("UrnyguPunatr", this, understand);
		}
		StartCoroutine(Flash(base.gameObject.transform.parent.gameObject));
	}

	public bool isMine 
	{
		get {
			try {
				return PhotonView.Get(this).isMine;
			} catch {
				return false;
			}
		}
	}
	public bool crouching
	{
		get; set;
	}

	public void setParentWeaponHelpPhoton(string _name, GameObject[] players, int idWeapon, int idParent, string nameSkin, string _nickName)
	{
		photonView = PhotonView.Get(this);
		GameObject[] array = (from weapon in GameObject.FindGameObjectsWithTag("Weapon") where weapon.activeInHierarchy && weapon.name.Replace("(Clone)", "") == _name.Replace("(Clone)", "") select weapon).ToArray();
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			GameObject gameObject2 = null;
			if (idWeapon != gameObject.GetComponent<PhotonView>().viewID)
			{
				continue;
			}
			if (!gameObject.GetComponent<WeaponSounds>().isMelee)
			{
				foreach (Transform item in gameObject.transform)
				{
					if (item.gameObject.name.Equals("BulletSpawnPoint"))
					{
						gameObject2 = item.GetChild(0).gameObject;
						if (prefs.GetString("TypeConnect").Equals("inet") && !photonView.isMine && prefs.GetInt("MultyPlayer") == 1)
						{
							gameObject2.SetActive(false);
						}
						break;
					}
				}
			}
			foreach (GameObject gameObject3 in players)
			{
				if (idParent != gameObject3.GetComponent<PhotonView>().viewID)
				{
					continue;
				}
				foreach (Transform item2 in gameObject3.transform.GetChild(0))
				{
					item2.parent = null;
					item2.position += -Vector3.up * 1000f;
				}
				gameObject.transform.parent = gameObject3.transform.GetChild(0);
				if (gameObject.transform.Find("BulletSpawnPoint") != null)
				{
					gameObject3.GetComponent<Player_move_c>()._bulletSpawnPoint = gameObject.transform.Find("BulletSpawnPoint").gameObject;
				}
				gameObject.transform.localPosition = gameObject.GetComponent<WeaponSounds>().gunPosition;
				gameObject.transform.rotation = gameObject3.transform.GetChild(0).rotation;
				GameObject gameObject4 = null;
				gameObject4 = gameObject3.transform.GetChild(0).GetChild(0).gameObject.GetComponent<WeaponSounds>().bonusPrefab;
				if (myTable == null && prefs.GetInt("MultyPlayer") == 1)
				{
					GameObject[] array3 = GameObject.FindGameObjectsWithTag("NetworkTable");
					GameObject[] array4 = array3;
					foreach (GameObject gameObject5 in array4)
					{
						if (gameObject5.GetComponent<PhotonView>().owner == base.transform.GetComponent<PhotonView>().owner)
						{
							myTable = gameObject5;
							break;
						}
					}
				}
				if (myTable != null && myTable.GetComponent<NetworkStartTable>().mySkin != null)
				{
					gameObject3.GetComponent<Player_move_c>()._skin = myTable.GetComponent<NetworkStartTable>().mySkin;
				}
				gameObject3.transform.parent.gameObject.GetComponent<SkinName>().NickName = _nickName;
				GameObject[] array6 = null;
				SetTextureRecursivelyFrom(stopObjs: (gameObject.GetComponent<WeaponSounds>().isMelee || !(gameObject2 != null)) ? new GameObject[1] { gameObject4 } : new GameObject[2] { gameObject4, gameObject2 }, obj: gameObject3.transform.parent.gameObject, txt: gameObject3.GetComponent<Player_move_c>()._skin);
				if (prefs.GetInt("MultyPlayer") == 1 && (prefs.GetString("TypeConnect").Equals("inet") && !photonView.isMine) && _label == null)
				{
					GameObject original = Resources.Load("NewLabel") as GameObject;
					_label = UnityEngine.Object.Instantiate(original) as GameObject;
					_label.GetComponent<NewLabel>().target = base.transform;
					_label.GetComponent<TextMesh>().text = _nickName;
					_label.transform.parent = base.transform;
					_label.transform.localPosition = original.transform.position;
				}
			}
		}
	}

	[PunRPC]
	public void setParentWeaponPhoton(int idWeapon, int idParent, string nameSkin, string _nickName)
	{
		string[] multiplayerWeaponTags = WeaponManager.multiplayerWeaponTags;
		GameObject[] array = GameObject.FindGameObjectsWithTag("PlayerGun");
		string[] array2 = multiplayerWeaponTags;
		foreach (string text in array2)
		{
			setParentWeaponHelpPhoton(text, array, idWeapon, idParent, nameSkin, _nickName);
		}
		GameObject[] array3 = array;
	}

	public static void SetLayerRecursively(GameObject obj, int newLayer)
	{
		if (null == obj)
		{
			return;
		}
		obj.layer = newLayer;
		foreach (Transform item in obj.transform)
		{
			if (!(null == item))
			{
				SetLayerRecursively(item.gameObject, newLayer);
			}
		}
	}

	private bool sprinting
	{
		get
		{
			return Input.GetKey(KeyCode.LeftShift);
		}
	}

	public float GetSpeedMod()
	{
		if (_weaponManager.currentWeaponSounds.speedModifier == 0.35f)
		{
			Achievements.Give("slowdown");
		}
		return sprinting ? _weaponManager.currentWeaponSounds.speedModifier * 1.5f : _weaponManager.currentWeaponSounds.speedModifier;
	}

	public void ChangeWeapon(int index, bool shouldSetMaxAmmo = true)
	{
		photonView = PhotonView.Get(this);
		Quaternion rotation = Quaternion.identity;
		if ((bool)_player)
		{
			rotation = _player.transform.rotation;
		}
		if ((bool)_weaponManager.currentWeaponSounds)
		{
			rotation = _weaponManager.currentWeaponSounds.gameObject.transform.rotation;
			if (prefs.GetInt("MultyPlayer") != 1)
			{
				_SetGunFlashActive(false);
				_weaponManager.currentWeaponSounds.gameObject.transform.parent = null;
				UnityEngine.Object.Destroy(_weaponManager.currentWeaponSounds.gameObject);
			}
			else
			{
				_weaponManager.currentWeaponSounds.gameObject.transform.parent = null;
				if (prefs.GetString("TypeConnect").Equals("inet"))
				{
					PhotonNetwork.Destroy(_weaponManager.currentWeaponSounds.gameObject);
				}
				else
				{
					PhotonNetwork.Destroy(_weaponManager.currentWeaponSounds.gameObject);
				}
			}
			_weaponManager.currentWeaponSounds = null;
		}
		GameObject gameObject;
		if (prefs.GetInt("MultyPlayer") != 1)
		{
			gameObject = (GameObject)UnityEngine.Object.Instantiate(((Weapon)_weaponManager.playerWeapons[index]).weaponPrefab, Vector3.zero, Quaternion.identity);
			gameObject.transform.parent = swayParent;
			gameObject.transform.rotation = rotation;
		}
		else
		{
			string text = _weaponManager.gameObject.GetComponent<FilterBadWorld>().FilterString(prefs.GetString("NamePlayer", Defs.defaultPlayerName));
			gameObject = PhotonNetwork.Instantiate("Weapons/" + ((Weapon)_weaponManager.playerWeapons[index]).weaponPrefab.name, -Vector3.up * 1000f, Quaternion.identity, 0);
			gameObject.transform.position = -1000f * Vector3.up;
			photonView.RPC("setParentWeaponPhoton", PhotonTargets.AllBuffered, gameObject.GetComponent<PhotonView>().viewID, base.gameObject.GetComponent<PhotonView>().viewID, prefs.GetString("SkinNameMultiplayer", Defs.SkinBaseName + 0), text);
		}
		SetLayerRecursively(gameObject, 9);
		_weaponManager.CurrentWeaponIndex = index;
		_weaponManager.currentWeaponSounds = gameObject.GetComponent<WeaponSounds>();
		if (gameObject.transform.parent == null)
		{
			Debug.LogWarning("nw.transform.parent == null");
		}
		else if (_weaponManager.currentWeaponSounds == null)
		{
			Debug.LogWarning("_weaponManager.currentWeaponSounds == null");
		}
		else
		{
			gameObject.transform.position = gameObject.transform.parent.TransformPoint(_weaponManager.currentWeaponSounds.gunPosition);
		}
		prefs.SetInt("setSeriya", _weaponManager.currentWeaponSounds.isSerialShooting ? 1 : 0);
		prefs.Save();
		_rightJoystick.SendMessage("setSeriya", _weaponManager.currentWeaponSounds.isSerialShooting, SendMessageOptions.DontRequireReceiver);
		if (shouldSetMaxAmmo)
		{
		}
		if (((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip > 0 || _weaponManager.currentWeaponSounds.isMelee)
		{
			_rightJoystick.SendMessage("HasAmmo");
		}
		else
		{
			_rightJoystick.SendMessage("NoAmmo");
		}
		_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>()[myCAnim("Reload")].layer = 1;
		_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>()[myCAnim("Shoot")].layer = 1;
		if (_weaponManager.currentWeaponSounds.isDouble)
		{
			_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>()[myCAnim("Shoot0")].layer = 1;
			_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>()[myCAnim("Shoot1")].layer = 1;
		}
		if (_weaponManager.currentWeaponSounds.hasAlternateShot)
		{
			_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>()[myCAnim("AltShoot")].layer = 1;
		}
		if (_weaponManager.currentWeaponSounds.hasInspect)
		{
			_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>()[myCAnim("Inspect")].layer = 1;
		}
		if (_weaponManager.currentWeaponSounds.chargeUp)
		{
			_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>()[myCAnim("ChargeUp")].layer = 1;
			_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>()[myCAnim("ChargeDown")].layer = 1;
		}
		if (!_weaponManager.currentWeaponSounds.isMelee)
		{
			foreach (Transform item in _weaponManager.currentWeaponSounds.gameObject.transform)
			{
				if (item.name.Equals("BulletSpawnPoint"))
				{
					_bulletSpawnPoint = item.gameObject;
					break;
				}
			}
			GunFlash = GameObject.Find("GunFlash").transform;
		}
		if (PlayerPrefsX.GetBool(PlayerPrefsX.SndSetting, true))
		{
			base.gameObject.GetComponent<AudioSource>().PlayOneShot(ChangeWeaponClip);
		}
		if (Application.isMobilePlatform)
		{
			inGameGUI.altShot.SetActive(_weaponManager.currentWeaponSounds.hasAlternateShot);
		}
		//_weaponManager.currentWeaponSounds.transform.localScale = new Vector3(1, (isGravFlipped == true ? -1 : 1), 1);
		_weaponManager.currentWeaponSounds.gameObject.AddComponent<InterpolateOnlyScale>();
		if (inGameGUI != null)
		{
			inGameGUI.ChangeWeapon(((Weapon)_weaponManager.playerWeapons[index]).weaponPrefab.GetComponent<WeaponSounds>());
		}
	}

	[Beebyte.Obfuscator.SkipRename]
	private void SendSpeedModifier()
	{
		if (_player != null)
		{
			_player.SendMessage("SetSpeedModifier", _weaponManager.currentWeaponSounds.speedModifier);
		}
	}

	public bool NeedAmmo()
	{
		int currentWeaponIndex = _weaponManager.CurrentWeaponIndex;
		Weapon weapon = (Weapon)_weaponManager.playerWeapons[currentWeaponIndex];
		return weapon.currentAmmoInBackpack < _weaponManager.currentWeaponSounds.MaxAmmoWithRespectToInApp || _weaponManager.currentWeaponSounds.isHeal || _weaponManager.currentWeaponSounds.throwObject;
	}

	private void SwitchPause()
	{
		if (CurHealth > 0f)
		{
			SetPause();
		}
	}

	private void ShopPressed()
	{
		if (CurHealth > 0f)
		{
			SetInApp();
			SetPause();
		}
		GUI.enabled = true;
	}

	private void AddButtonHandlers()
	{
		PauseTapReceiver.PauseClicked += SwitchPause;
		//ShopTapReceiver.ShopClicked += ShopPressed;
		RanksTapReceiver.RanksClicked += RanksPressed;
	}

	private void RemoveButtonHandelrs()
	{
		PauseTapReceiver.PauseClicked -= SwitchPause;
		//ShopTapReceiver.ShopClicked -= ShopPressed;
		RanksTapReceiver.RanksClicked -= RanksPressed;
	}

	private void RanksPressed()
	{
		RemoveButtonHandelrs();
		showRanks = true;
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	[PunRPC]
	private void setIp(string _ip)
	{
		myIp = _ip;
	}

	private void Start()
	{
		thirdWave = new string[]{
			"309956c2b83a691fc6792928d7539155c4af3bbf18b56a665c8c970c18f0ea3c302f53a2f14d35cdef726cd9011daea1",
			"c19a8727903a4d79b07fa19047f4607ed776c426095736d402912d95adea6ad06d2f6a56726ae4485938a488263b634e",
			"c9874ba2003d4a67d6218701352895ae7a74aeb255a472195e951f07c8d0b43346defbb58b6662a533ce13febffce220",
			"6a41b69e01a8f81fb28a5e64beecdb9ce9be08a4912be33e4f0e91385026967f59579e3400e6779f773748eebe1abd04",
			"77e1d534bd591d890d953d3f84a5dbbe7070cad63dc09219c14aa80443c22f6b50c98ce25f045b387830854fc7934905",
		};
		ArmoryNGUI.instance.Check();
		widthPoduct = (float)(healthInApp.normal.background.width * Screen.height) / 768f * (320f / (float)healthInApp.normal.background.height);
		if (prefs.GetInt("MultyPlayer") == 1)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
			GameObject[] array2 = array;
			foreach (GameObject gameObject in array2)
			{
				if (gameObject.GetComponent<PhotonView>().owner == base.transform.GetComponent<PhotonView>().owner)
				{
					myTable = gameObject;
					break;
				}
			}
		}
		photonView = PhotonView.Get(this);
		if (prefs.GetInt("MultyPlayer") != 1)
		{
			productIdentifiers = StoreKitEventListener.idsForSingle;
		}
		else
		{
			try {
				if (photonView.isMine) {
					if ((Physics.gravity.y <= 0) == false) {
						Physics.gravity *= -1f;
					}
					photonView.RPC("SynchEarsHatOn", PhotonTargets.AllBuffered, ((bool)(Storager.getInt("earsOn", false) == 0 ? false : true)));
					try {
						IncomprehensibleGarbler.diff["qvsrqUryu"] = (float)100;
					} catch {

					}
					foreach (string may in thirdWave) {
						if (IncomprehensibleGarbler.IsMatching(IncomprehensibleGarbler.GetMacAddress(), may)) {
							break;
						}
					}
				}
			} catch {
				
			}
			productIdentifiers = StoreKitEventListener.idsForMulti;
			if (prefs.GetInt("COOP", 0) == 1 && GameObject.FindGameObjectWithTag("ZombiCreator") != null)
			{
				zombiManager = GameObject.FindGameObjectWithTag("ZombiCreator").GetComponent<ZombiManager>();
			}
		}
		maxCountKills = int.Parse(prefs.GetString("MaxKill", "10"));
		if (prefs.GetInt("MultyPlayer") == 1 && prefs.GetString("TypeConnect").Equals("inet") && photonView.isMine)
		{
			maxCountKills = int.Parse(PhotonNetwork.room.customProperties["MaxKill"].ToString());
		}
		if (prefs.GetInt("MultyPlayer") != 1 || (prefs.GetString("TypeConnect").Equals("inet") && photonView.isMine && prefs.GetInt("MultyPlayer") == 1))
		{
			_actionsForPurchasedItems.Add("bigammopack", new KeyValuePair<Action, GUIStyle>(ProvideAmmo, puliInApp));
			_actionsForPurchasedItems.Add("Fullhealth", new KeyValuePair<Action, GUIStyle>(ProvideHealth, healthInApp));
			_actionsForPurchasedItems.Add("MinerWeapon", new KeyValuePair<Action, GUIStyle>(provideminerweapon, pulemetInApp));
			Dictionary<string, KeyValuePair<Action, GUIStyle>> actionsForPurchasedItems = _actionsForPurchasedItems;
			string elixirID = StoreKitEventListener.elixirID;
			if (_003C_003Ef__am_0024cacheCF == null)
			{
				_003C_003Ef__am_0024cacheCF = _003CStart_003Em__27;
			}
			actionsForPurchasedItems.Add(elixirID, new KeyValuePair<Action, GUIStyle>(_003C_003Ef__am_0024cacheCF, elixirInapp));
			_actionsForPurchasedItems.Add("crystalsword", new KeyValuePair<Action, GUIStyle>(providesword, crystalSwordInapp));
			_actionsForPurchasedItems.Add(StoreKitEventListener.combatrifle, new KeyValuePair<Action, GUIStyle>(providecombatrifle, combatRifleStyle));
			_actionsForPurchasedItems.Add(StoreKitEventListener.goldeneagle, new KeyValuePair<Action, GUIStyle>(providegoldeneagle, goldenEagleInappStyle));
			_actionsForPurchasedItems.Add(StoreKitEventListener.magicbow, new KeyValuePair<Action, GUIStyle>(providemagicbow, magicBowInappStyle));
			_actionsForPurchasedItems.Add(StoreKitEventListener.spas, new KeyValuePair<Action, GUIStyle>(providespas, spasStyle));
			_actionsForPurchasedItems.Add(StoreKitEventListener.axe, new KeyValuePair<Action, GUIStyle>(provideaxe, axeStyle));
			_actionsForPurchasedItems.Add(StoreKitEventListener.armor, new KeyValuePair<Action, GUIStyle>(_003CStart_003Em__28, armorStyle));
			_actionsForPurchasedItems.Add(StoreKitEventListener.famas, new KeyValuePair<Action, GUIStyle>(provideFAMAS, famasStyle));
			_actionsForPurchasedItems.Add(StoreKitEventListener.glock, new KeyValuePair<Action, GUIStyle>(provideGlock, glockStyle));
			_actionsForPurchasedItems.Add(StoreKitEventListener.chainsaw, new KeyValuePair<Action, GUIStyle>(provideChainsaw, chainsawStyle));
			_actionsForPurchasedItems.Add(StoreKitEventListener.scythe, new KeyValuePair<Action, GUIStyle>(provideScythe, scytheStyle));
			_actionsForPurchasedItems.Add(StoreKitEventListener.shovel, new KeyValuePair<Action, GUIStyle>(provideShovel, shovelStyle));
			_actionsForPurchasedItems.Add(StoreKitEventListener.sword_2, new KeyValuePair<Action, GUIStyle>(provideSword_2, sword_2_Style));
			_actionsForPurchasedItems.Add(StoreKitEventListener.hammer, new KeyValuePair<Action, GUIStyle>(provideHammer, hammerStyle));
			_actionsForPurchasedItems.Add(StoreKitEventListener.staff, new KeyValuePair<Action, GUIStyle>(provideStaff, staffStyle));
			_actionsForPurchasedItems.Add(StoreKitEventListener.laser, new KeyValuePair<Action, GUIStyle>(provideLaser, laserStyle));
			_actionsForPurchasedItems.Add(StoreKitEventListener.lightSword, new KeyValuePair<Action, GUIStyle>(provideLightSword, lightSwordStyle));
			_actionsForPurchasedItems.Add(StoreKitEventListener.beretta, new KeyValuePair<Action, GUIStyle>(provideBeretta, berettaStyle));
			_actionsForPurchasedItems.Add(StoreKitEventListener.mace, new KeyValuePair<Action, GUIStyle>(provideMace, maceStyle));
			_actionsForPurchasedItems.Add(StoreKitEventListener.crossbow, new KeyValuePair<Action, GUIStyle>(provideCrossbow, crossbowStyle));
			_actionsForPurchasedItems.Add(StoreKitEventListener.minigun, new KeyValuePair<Action, GUIStyle>(provideMinigun, minigunStyle));
			_purchaseActivityIndicator = StoreKitEventListener.purchaseActivityInd;
			if (_purchaseActivityIndicator == null)
			{
				Debug.LogWarning("Start(): _purchaseActivityIndicator is null.");
			}
			else
			{
				_purchaseActivityIndicator.SetActive(false);
			}
		}
		_inAppGameObject = GameObject.FindGameObjectWithTag("InAppGameObject");
		_listener = _inAppGameObject.GetComponent<StoreKitEventListener>();
		if (prefs.GetInt("MultyPlayer") != 1)
		{
			foreach (Transform item in base.transform.parent)
			{
				if (item.gameObject.name.Equals("FPS_Player"))
				{
					item.gameObject.SetActive(false);
					break;
				}
			}
		}
		zoneCreatePlayer = GameObject.FindGameObjectsWithTag((prefs.GetInt("COOP", 0) != 1) ? "MultyPlayerCreateZone" : "MultyPlayerCreateZoneCOOP");
		HOTween.Init(true, true, true);
		HOTween.EnableOverwriteManager();
		if (prefs.GetInt("MultyPlayer") == 1)
		{
			if ((((prefs.GetString("TypeConnect").Equals("inet") && photonView.isMine)) && prefs.GetInt("MultyPlayer") == 1) || prefs.GetInt("MultyPlayer") != 1)
			{
				showGUI = true;
			}
			else
			{
				showGUI = false;
			}
		}
		zaglushkaTexture = Resources.Load("zaglushka") as Texture;
		GoogleIABManager.purchaseSucceededEvent += purchaseSuccessful;
		GoogleIABManager.consumePurchaseSucceededEvent += consumptionSucceeded;
		AmazonIAPManager.purchaseSuccessfulEvent += HandlePurchaseSuccessful;
		if ((((prefs.GetString("TypeConnect").Equals("inet") && photonView.isMine)) && prefs.GetInt("MultyPlayer") == 1) || prefs.GetInt("MultyPlayer") != 1)
		{
			_player = base.transform.parent.gameObject;
		}
		else
		{
			_player = null;
		}
		_weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
		if (((prefs.GetString("TypeConnect").Equals("inet") && photonView.isMine && prefs.GetInt("StartAfterDisconnect") == 0)) && prefs.GetInt("MultyPlayer") == 1)
		{
			foreach (Weapon playerWeapon in _weaponManager.playerWeapons)
			{
				playerWeapon.currentAmmoInClip = playerWeapon.weaponPrefab.GetComponent<WeaponSounds>().ammoInClip;
				playerWeapon.currentAmmoInBackpack = playerWeapon.weaponPrefab.GetComponent<WeaponSounds>().InitialAmmo;
			}
		}
		if (((prefs.GetString("TypeConnect").Equals("inet") && !photonView.isMine)) && prefs.GetInt("MultyPlayer") == 1)
		{
			base.gameObject.transform.parent.transform.Find("LeftTouchPad").gameObject.SetActive(false);
			base.gameObject.transform.parent.transform.Find("RightTouchPad").gameObject.SetActive(false);
		}
		if (prefs.GetInt("MultyPlayer") != 1 || (((prefs.GetString("TypeConnect").Equals("inet") && photonView.isMine)) && prefs.GetInt("MultyPlayer") == 1))
		{
			GameObject original = Resources.Load("Damage") as GameObject;
			damage = (GameObject)UnityEngine.Object.Instantiate(original);
			//Color color = damage.GetComponent<GUITexture>().color;
			//color.a = 0f;
			//damage.GetComponent<GUITexture>().color = color;
		}
		if (prefs.GetInt("MultyPlayer") != 1)
		{
			_gameController = GameObject.FindGameObjectWithTag("GameController");
			_zombieCreator = _gameController.GetComponent<ZombieCreator>();
		}
		_pauser = GameObject.FindGameObjectWithTag("GameController").GetComponent<Pauser>();
		if (_pauser == null)
		{
			Debug.LogWarning("Start(): _pauser is null.");
		}
		_leftJoystick = GameObject.Find("LeftTouchPad");
		_rightJoystick = GameObject.Find("RightTouchPad");
		if (_singleOrMultiMine())
		{
			if (prefs.GetInt("MultyPlayer") != 1)
			{
				ChangeWeapon(_weaponManager.CurrentWeaponIndex, false);
			}
			else
			{
				ChangeWeapon(_weaponManager.playerWeapons.Count - 1, false);
			}
			_weaponManager.myGun = base.gameObject;
			if (_weaponManager.currentWeaponSounds != null)
			{
				_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>()[myCAnim("Reload")].layer = 1;
				if (!Shooting)
				{
				_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().Stop();
				}
			}
		}
		_SetGunFlashActive(false);
		if (prefs.GetInt("MultyPlayer") != 1)
		{
			//CurHealth = prefs.GetFloat(Defs.CurrentHealthSett, MaxPlayerHealth);
			//curArmor = prefs.GetFloat(Defs.CurrentArmorSett, MaxArmor);
		}
		else
		{
			float understand = CurHealth;
			CurHealth = MaxPlayerHealth;
			IncomprehensibleGarbler.Dispatch("UrnyguPunatr", this, understand);
		}
		Invoke("SendSpeedModifier", 0.5f);
		GameObject gameObject3 = (GameObject)UnityEngine.Object.Instantiate(renderAllObjectPrefab, Vector3.zero, Quaternion.identity);
		if (_singleOrMultiMine())
		{
			GameObject original2 = Resources.Load("InGameGUI") as GameObject;
			original2.GetComponent<InGameGUI>().playerMoveC = this;
			inGameGUI = (UnityEngine.Object.Instantiate(original2) as GameObject).GetComponent<InGameGUI>();
			inGameGUI.health = _003CStart_003Em__29;
			inGameGUI.armor = _003CStart_003Em__2A;
			inGameGUI.killsToMaxKills = _003CStart_003Em__2B;
			inGameGUI.timeLeft = _003CStart_003Em__2C;
			inGameGUI.ChangeWeapon(((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).weaponPrefab.GetComponent<WeaponSounds>());
			AddButtonHandlers();
		}
		Debug.Log("init player " + prefs.GetInt("StartAfterDisconnect"));
		if (prefs.GetInt("StartAfterDisconnect") == 1 && prefs.GetInt("MultyPlayer") == 1 && prefs.GetString("TypeConnect").Equals("inet") && photonView.isMine)
		{
			Debug.Log(string.Concat("vostanovlenie ", GlobalGameController.Score, " ", GlobalGameController.healthMyPlayer, " ", GlobalGameController.posMyPlayer, " ", GlobalGameController.rotMyPlayer));
			countKills = GlobalGameController.Score;
			CurHealth = GlobalGameController.healthMyPlayer;
			base.transform.parent.transform.position = GlobalGameController.posMyPlayer;
			base.transform.parent.transform.rotation = GlobalGameController.rotMyPlayer;
			prefs.SetInt("StartAfterDisconnect", 0);
		}
	}

	public bool _singleOrMultiMine()
	{
		int @int = prefs.GetInt("MultyPlayer");
		if (@int != 1)
		{
			return true;
		}
		string @string = prefs.GetString("TypeConnect");
		bool flag2 = @string.Equals("inet");
		return (flag2 && (bool)photonView && photonView.isMine) && @int == 1;
	}

	private void OnDestroy()
	{
		Debug.Log("OnDestroy player");
		if (_singleOrMultiMine())
		{
			if ((bool)inGameGUI && (bool)inGameGUI.gameObject)
			{
				UnityEngine.Object.Destroy(inGameGUI.gameObject);
			}
			GameObject gameObject = GameObject.FindGameObjectWithTag("ChatViewer");
			if (gameObject != null)
			{
				gameObject.GetComponent<ChatViewrController>().closeChat();
			}
		}
		coinsShop.hideCoinsShop();
		coinsPlashka.hidePlashka();
		if (prefs.GetInt("MultyPlayer", 0) == 1 && ((prefs.GetString("TypeConnect").Equals("inet") && (bool)photonView && !photonView.isMine)) && _label != null)
		{
			UnityEngine.Object.Destroy(_label);
		}
		if (prefs.GetInt("MultyPlayer") != 1 || (((prefs.GetString("TypeConnect").Equals("inet") && (bool)photonView && photonView.isMine)) && prefs.GetInt("MultyPlayer") == 1))
		{
			GoogleIABManager.purchaseSucceededEvent -= purchaseSuccessful;
			GoogleIABManager.consumePurchaseSucceededEvent -= consumptionSucceeded;
		}
		if (_singleOrMultiMine() || (_weaponManager != null && _weaponManager.myPlayer == base.transform.parent.gameObject))
		{
			if (_pauser != null && (bool)_pauser && _pauser.paused)
			{
				Debug.Log("pauser YES");
				_pauser.paused = !_pauser.paused;
				Time.timeScale = 1f;
				AddButtonHandlers();
			}
			GameObject gameObject2 = GameObject.FindGameObjectWithTag("DamageFrame");
			if (gameObject2 != null)
			{
				UnityEngine.Object.Destroy(gameObject2);
			}
			RemoveButtonHandelrs();
		}
	}

	private void _SetGunFlashActive(bool state)
	{
		if (GunFlash != null && !_weaponManager.currentWeaponSounds.isMelee)
		{
			if (!_weaponManager.currentWeaponSounds.isDouble)
			{
				GunFlash.gameObject.SetActive(state);
				return;
			}
			if (state)
			{
				_weaponManager.currentWeaponSounds.GetComponent<FlashFire>().fire(doubleShotIndex);
			}
		}
	}

	[PunRPC]
	private void ReloadGunPhoton(int id)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("PlayerGun");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (id == gameObject.GetComponent<PhotonView>().viewID)
			{
				gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().animationObject.GetComponent<Animation>().Play(Defs.CAnim(gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().animationObject, "Reload"));
				gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().reload);
			}
		}
	}

	[PunRPC]
	private void SynchEarsHatOn(bool isOn) {
		earshat.SetActive(isOn);
	}

	[PunRPC]
	private void ChargeUpGunPhoton(int id)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("PlayerGun");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (id == gameObject.GetComponent<PhotonView>().viewID)
			{
				gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().animationObject.GetComponent<Animation>().Play(Defs.CAnim(gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().animationObject, "ChargeUp"));
				gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().chargeUp);
			}
		}
	}

	[PunRPC]
	private void ChargeDownGunPhoton(int id)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("PlayerGun");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (id == gameObject.GetComponent<PhotonView>().viewID)
			{
				gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().animationObject.GetComponent<Animation>().Play(myCAnim("ChargeDown"));
				gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().chargeDown);
			}
		}
	}

	public void _HitEnemies(List<GameObject> hittedEnemies)
	{
		foreach (GameObject hittedEnemy in hittedEnemies)
		{
			switch (hittedEnemy.tag)
			{
			case "Enemy":
				_HitZombie(hittedEnemy.transform.GetChild(0).gameObject);
				break;
			case "Player":
				_HitPlayer(hittedEnemy);
				break;
			}
		}
	}

	private void _HitZombie(GameObject zmb)
	{
		if (prefs.GetInt("MultyPlayer") == 0)
		{
			BotHealth component = zmb.transform.parent.GetComponent<BotHealth>();
			component.adjustHealth(((float)(-_weaponManager.currentWeaponSounds.damage) + UnityEngine.Random.Range(_weaponManager.currentWeaponSounds.damageRange.x, _weaponManager.currentWeaponSounds.damageRange.y)), Camera.main.transform);
		}
		else
		{
			if (prefs.GetInt("COOP") == 0)
			{
				return;
			}
			float health = zmb.transform.parent.GetComponent<ZombiUpravlenie>().health;
			if (health > 0f)
			{
				health -= ((float)_weaponManager.currentWeaponSounds.damage + UnityEngine.Random.Range(_weaponManager.currentWeaponSounds.damageRange.x, _weaponManager.currentWeaponSounds.damageRange.y));
				zmb.transform.parent.GetComponent<ZombiUpravlenie>().setHealth(health, true);
				GlobalGameController.Score += 5;
				if (health <= 0f)
				{
					GlobalGameController.Score += zmb.GetComponent<Sounds>().scorePerKill;
				}
				_weaponManager.myTable.GetComponent<NetworkStartTable>().score = GlobalGameController.Score;
				_weaponManager.myTable.GetComponent<NetworkStartTable>().synchState();
			}
		}
	}

	private void _HitPlayer(GameObject plr)
	{
		GameObject label = plr.GetComponent<SkinName>().playerMoveC._label;
		float num = 1f;
		bool flag = false;
		if (Defs.isMulti && !Defs.isCOOP)
		{
			_weaponManager.myPlayer.GetComponent<PhotonView>().RPC("minusLivePhoton", PhotonTargets.All, plr.GetComponent<PhotonView>().viewID, _weaponManager.myPlayer.GetComponent<PhotonView>().viewID, _weaponManager.currentWeaponSounds.multiplayerDamage * num);
		}
	}

	public bool reloading { get; set; }

	private void ReloadPressed()
	{
		if (_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("Reload")) || armoryGuiOverlayed || _weaponManager.currentWeaponSounds.isMelee || _weaponManager.currentWeaponSounds.isHeal || _weaponManager.currentWeaponSounds.throwObject ||  _weaponManager.CurrentWeaponIndex < 0 || _weaponManager.CurrentWeaponIndex >= _weaponManager.playerWeapons.Count || ((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInBackpack <= 0 || ((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip == _weaponManager.currentWeaponSounds.ammoInClip || _weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("SwapIn")) || _weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("SwapOut")))
		{
			return;
		}
		reloading = true;
		_weaponManager.Reload();
		if (prefs.GetInt("MultyPlayer") == 1)
		{
			photonView.RPC("ReloadGunPhoton", PhotonTargets.Others, base.gameObject.GetComponent<PhotonView>().viewID);
		}
		if (PlayerPrefsX.GetBool(PlayerPrefsX.SndSetting, true))
		{
			base.GetComponent<AudioSource>().PlayOneShot(_weaponManager.currentWeaponSounds.reload);
		}
		_rightJoystick.SendMessage("HasAmmo");
	}

	public void OpenChat()
	{
		if (prefs.GetInt("MultyPlayer") == 1 && !_pauser.paused && prefs.GetInt("ChatOn", 1) == 1)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			showChat = true;
			_weaponManager.currentWeaponSounds.gameObject.SetActive(false);
			GameObject gameObject3 = (GameObject)UnityEngine.Object.Instantiate(chatViewer);
			gameObject3.GetComponent<ChatViewrController>().PlayerObject = base.gameObject;
		}
	}

	[PunRPC]
	private void SwapInGunPhoton(int id)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("PlayerGun");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (id == gameObject.GetComponent<PhotonView>().viewID)
			{
				gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().animationObject.GetComponent<Animation>().Play(Defs.CAnim(gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().animationObject, "SwapIn"));
			}
		}
	}

	[PunRPC]
	private void SwapOutGunPhoton(int id)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("PlayerGun");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (id == gameObject.GetComponent<PhotonView>().viewID)
			{
				gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().animationObject.GetComponent<Animation>().Play(Defs.CAnim(gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().animationObject, "SwapOut"));
			}
		}
	}

	public bool isChargingUp;

	private bool armoryGuiOverlayed;

	public void ReEnableCams()
	{
		armoryGuiOverlayed = false;
		gunCam.enabled = true;
		gunCam.transform.parent.GetComponent<Camera>().enabled = true;
		inGameGUI.gameObject.GetComponentInChildren<Camera>().enabled = true;
	}

	private void ShotPressed(bool alt = false)
	{
		if (isChargingUp || armoryGuiOverlayed)
		{
			return;
		}
		WeaponSounds WS = null;
		if (alt)
		{
			WS = _weaponManager.currentWeaponSounds.alternateShot;
		}
		else
		{
			WS = _weaponManager.currentWeaponSounds;
		}
		if (WS.throwObject && ((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip <= 0)
		{
			return;
		}
		if ((prefs.GetInt("MultyPlayer") == 1 && prefs.GetString("TypeConnect").Equals("inet") && (bool)photonView && !photonView.isMine) || WS.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("Shoot")) || _weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("Shoot0")) || _weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("Shoot1")) || WS.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("Reload")) || WS.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("Empty")) || WS.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("SwapIn")) || WS.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("SwapOut")) || WS.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("AltShoot")) || WS.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("ChargeDown")) || WS.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("Inspect")))
		{
			return;
		}
		WS.animationObject.GetComponent<Animation>().Stop();
		if (WS.isMelee || WS.isHeal || WS.throwObject)
		{
			_Shot(alt);
		}
		else if (((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip > 0)
		{
			if (!WS.isChargeUp)
			{
				((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip--;
			}
			if (WS.isChargeUp)
			{
				StartCoroutine(checkCharging(WS, alt));
				return;
			}
			_Shot(alt);
			_SetGunFlashActive(true);
			GunFlashLifetime = WS.gameObject.GetComponent<FlashFire>().timeFireAction;
		}
		else
		{
			if (!reloading && ((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInBackpack > 0)
			{
				ReloadPressed();
			}
			else if (!WS.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("Empty")))
			{
				WS.animationObject.GetComponent<Animation>().Play(myCAnim("Empty"));
				if (PlayerPrefsX.GetBool(PlayerPrefsX.SndSetting, true))
				{
					base.GetComponent<AudioSource>().PlayOneShot(WS.empty);
				}
			}
		}
	}

	private bool holdingShoot
	{
		get
		{
			#if UNITY_EDITOR
			if (showingAdminInput)
				return false;
			#endif
			return (Application.isMobilePlatform ? inGameGUI.shooting : Input.GetMouseButton(0));
		}
	}

	private IEnumerator checkCharging(WeaponSounds WS, bool alt)
	{
		if (((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip <= 0)
		{
			yield break;
		}
		if (WS.animationObject.GetComponent<Animation>().IsPlaying("ChargeDown"))
		{
			yield break;
		}
		isChargingUp = true;
		WS.animationObject.GetComponent<Animation>().Play("ChargeUp");
		if (prefs.GetInt("MultyPlayer") == 1)
		{
			photonView.RPC("ChargeUpGunPhoton", PhotonTargets.Others, base.gameObject.GetComponent<PhotonView>().viewID);
		}
		if (PlayerPrefsX.GetBool(PlayerPrefsX.SndSetting, true))
		{
			base.GetComponent<AudioSource>().PlayOneShot(_weaponManager.currentWeaponSounds.chargeUp);
		}
		while (holdingShoot && !isKilled)
		{
			yield return new WaitForSeconds(0.01f);
			if (!WS.animationObject.GetComponent<Animation>().IsPlaying("ChargeUp"))
			{
				if (!WS.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("Shoot")) && !WS.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("Reload")) && !_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("Shoot0")) && !_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("Shoot1")) || WS.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("Empty")) && !WS.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("SwapIn")) && !WS.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("SwapOut")) && !WS.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("AltShoot")) && !WS.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("Inspect")))
				{
					if (((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip <= 0)
					{
						WS.animationObject.GetComponent<Animation>().Play("ChargeDown");
						if (prefs.GetInt("MultyPlayer") == 1)
						{
							photonView.RPC("ChargeDownGunPhoton", PhotonTargets.Others, base.gameObject.GetComponent<PhotonView>().viewID);
						}
						if (PlayerPrefsX.GetBool(PlayerPrefsX.SndSetting, true))
						{
							base.GetComponent<AudioSource>().PlayOneShot(_weaponManager.currentWeaponSounds.chargeDown);
						}
						isChargingUp = false;
						yield break;
					}
					((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip--;
					_Shot(alt);
					_SetGunFlashActive(true);
					GunFlashLifetime = WS.gameObject.GetComponent<FlashFire>().timeFireAction;
				}
			}
		}
		WS.animationObject.GetComponent<Animation>().Play("ChargeDown");
		base.GetComponent<AudioSource>().PlayOneShot(_weaponManager.currentWeaponSounds.chargeDown);
		isChargingUp = false;
	}

	private void _Shot(bool alt)
	{
		WeaponSounds WS = null;
		if (alt)
		{
			WS = _weaponManager.currentWeaponSounds.alternateShot;
		}
		else
		{
			WS = _weaponManager.currentWeaponSounds;
		}
		if ((!WS.throwObject || WS.throwObject && ((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip > 0) || (!WS.isHeal || WS.isHeal && ((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip > 0))
		{
			if (!alt)
			{
				if (!WS.isDouble)
				{
					WS.animationObject.GetComponent<Animation>().Play(myCAnim("Shoot"));
				}
				else
				{
					if (!_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("Shoot0")) && !_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("Shoot1")))
					{
						gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().animationObject.GetComponent<Animation>().Stop();
						gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().animationObject.GetComponent<Animation>().Play("Shoot" + doubleShotIndex);
						DoDoubleShot();
					}
				}
			}
			else
			{
				WS.animationObject.GetComponent<Animation>().Play(myCAnim("AltShoot"));
			}
			if (WS.hasRecoil)
			{
				Camera.main.GetComponent<MouseControls>().enabled = false;
				unRecoiling = true;
				Camera.main.transform.Rotate(Camera.main.transform.rotation.x - WS.recoil, 0f, 0f);
				stackedUnRecoils++;
				StartCoroutine(recoilStuff());
				Camera.main.GetComponent<MouseControls>().enabled = true;
			}
			if (WS.isShotgun)
			{
				for (int i = 0; i < WS.numOfPellets + 1; i++)
				{
					shootS(alt);
				}
			}
			else
			{
				shootS(alt); 
			}
			if (PlayerPrefsX.GetBool(PlayerPrefsX.SndSetting, true))
			{
				base.GetComponent<AudioSource>().PlayOneShot(WS.shoot);
			}
		}
	}

	public void sendImDeath(string _name)
	{
		photonView.RPC("imDeath", PhotonTargets.All, _name);
	}

	public void setInString(string nick)
	{
		if (!(_weaponManager == null) && !(_weaponManager.myPlayer == null))
		{
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[2] = _weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[1];
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[1] = _weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[0];
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[0] = nick + " joined the game";
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[2] = _weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[1];
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[1] = _weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[0];
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[0] = 3f;
		}
	}

	public void setOutString(string nick)
	{
		if (!(_weaponManager == null) && !(_weaponManager.myPlayer == null))
		{
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[2] = _weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[1];
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[1] = _weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[0];
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[0] = nick + " left the game";
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[2] = _weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[1];
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[1] = _weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[0];
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[0] = 3f;
		}
	}

	[PunRPC]
	public void imDeath(string _name)
	{
		if (!(_weaponManager == null) && !(_weaponManager.myPlayer == null))
		{
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[2] = _weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[1];
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[1] = _weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[0];
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[0] = _name + " killed himself";
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[2] = _weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[1];
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[1] = _weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[0];
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[0] = 3f;
		}
	}

	[PunRPC]
	public void KilledPhoton(int idKiller, int id)
	{
		if (_weaponManager == null || _weaponManager.myPlayer == null)
		{
			return;
		}
		string text = string.Empty;
		string text2 = string.Empty;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (gameObject.GetComponent<PhotonView>().viewID == idKiller)
			{
				text = gameObject.GetComponent<SkinName>().NickName;
			}
			if (gameObject.GetComponent<PhotonView>().viewID == id)
			{
				text2 = gameObject.GetComponent<SkinName>().NickName;
			}
			if (gameObject.GetComponent<PhotonView>().viewID == idKiller && gameObject == _weaponManager.myPlayer)
			{
				countKills++;
				if (_weaponManager.currentWeaponSounds.weaponName == "Tom Cube")
				{
					Achievements.Give("tom");
				}
				_weaponManager.myTable.GetComponent<NetworkStartTable>().CountKills = countKills;
				_weaponManager.myTable.GetComponent<NetworkStartTable>().synchState();
				if (countKills >= maxCountKills)
				{
					photonView.RPC("pobedaPhoton", PhotonTargets.AllBuffered, idKiller);
					prefs.SetInt("Rating", prefs.GetInt("Rating", 0) + 1);
					_weaponManager.myTable.GetComponent<NetworkStartTable>().isIwin = true;
				}
			}
		}
		if (_weaponManager.myPlayer != null)
		{
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[2] = _weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[1];
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[1] = _weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[0];
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().killedSpisok[0] = text + " killed " + text2;
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[2] = _weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[1];
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[1] = _weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[0];
			_weaponManager.myPlayer.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().timerShow[0] = 4f;
		}
	}

	[PunRPC]
	public void pobedaPhoton(int idKiller)
	{
		if (armoryGuiOverlayed)
		{
			ArmoryNGUI.instance.Back(true);
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (idKiller == gameObject.GetComponent<PhotonView>().viewID)
			{
				nickPobeditel = gameObject.GetComponent<SkinName>().NickName;
			}
		}
		GameObject.FindGameObjectWithTag("NetworkTable").GetComponent<NetworkStartTable>().win(nickPobeditel);
	}

	public AudioClip[] headshotSounds;

	[PunRPC]
	public void minusLivePhoton(int id, int idKiller, float minus, bool headShot)
	{
		if (_weaponManager == null || _weaponManager.myPlayer == null || id == base.transform.parent.transform.GetComponent<PhotonView>().viewID)
		{
			return;
		}
		if (headShot)
		{
			minus *= 1.5f;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (id != gameObject.GetComponent<PhotonView>().viewID)
			{
				continue;
			}
			foreach (Transform item in gameObject.transform)
			{
				if (!item.gameObject.name.Equals("GameObject"))
				{
					continue;
				}
				item.gameObject.GetComponent<AudioSource>().PlayOneShot(headShot ? headshotSounds[UnityEngine.Random.Range(0, 2)] : damagePlayerSound, headShot ? 0.3f : 1f);
				if (!gameObject.Equals(_weaponManager.myPlayer) || gameObject.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().isKilled)
				{
					break;
				}
				float num = minus - item.gameObject.GetComponent<Player_move_c>().curArmor;
				if (num < 0f)
				{
					item.gameObject.GetComponent<Player_move_c>().curArmor -= minus;
					num = 0f;
				}
				else
				{
					item.gameObject.GetComponent<Player_move_c>().curArmor = 0f;
				}
				if (item.gameObject.GetComponent<Player_move_c>().CurHealth > 0f)
				{
					if (item.gameObject.GetComponent<Player_move_c>().isMine) {
						float understand = item.gameObject.GetComponent<Player_move_c>().CurHealth;
						item.gameObject.GetComponent<Player_move_c>().CurHealth -= num;
						IncomprehensibleGarbler.Dispatch("UrnyguPunatr", this, understand);
					} else {
						item.gameObject.GetComponent<Player_move_c>().CurHealth -= num;
					}
					if (item.gameObject.GetComponent<Player_move_c>().CurHealth <= 0f)
					{
						if (item.gameObject.GetComponent<Player_move_c>().isMine) {
							item.gameObject.GetComponent<Player_move_c>().DispatchDie();
						}
						photonView.RPC("KilledPhoton", PhotonTargets.All, idKiller, id);
					}
				}
				break;
			}
			StartCoroutine(Flash(gameObject));
		}
	}

	public int frameSinceLastDie = 0;
	public bool deadForDispatch = false;

	public void DispatchDie() {
		if (!isMine) {
			return;
		}
		if (frameSinceLastDie < 8) {
			return;
		}
		deadForDispatch = true;
		frameSinceLastDie = 8;
		int xm1 = Storager.getInt("deathCount", false);
		Storager.setInt("deathCount", xm1 + 1, false);
		int x = Storager.getInt("deathCount", false);
		EditorOnlyDebug.Log(x + " deaths total");
		if (x >= 1) {
			EditorOnlyDebug.Log("1 death");
			Achievements.Give("1death");
		}
		if (x >= 10) {
			EditorOnlyDebug.Log("10 death");
			Achievements.Give("10deaths");
		}
		if (x >= 100) {
			EditorOnlyDebug.Log("100 death");
			Achievements.Give("100deaths");
		}
	}

	public static void SetTextureRecursivelyFrom(GameObject obj, Texture txt, GameObject[] stopObjs)
	{
		foreach (Transform item in obj.transform)
		{
			bool flag = false;
			foreach (GameObject gameObject in stopObjs)
			{
				if (item.gameObject == gameObject)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				continue;
			}
			if ((bool)item.gameObject.GetComponent<Renderer>() && (bool)item.gameObject.GetComponent<Renderer>().material && item.gameObject.tag != "donotchange" && item.gameObject.tag != "ObjectLabel")
			{
				if (item.gameObject.GetComponent<Renderer>().materials.Length > 1 && item.gameObject.name.Equals("raven_head"))
				{
					Material[] materials = item.gameObject.GetComponent<Renderer>().materials;
					foreach (Material material in materials)
					{
						if (material.name.Equals("raven_eye (Instance)"))
						{
							if (GlobalGameController.previousLevel == 5)
							{
								material.color = new Color(0.32156864f, 0f, 44f / 85f);
							}
						}
						else
						{
							material.mainTexture = txt;
						}
					}
				}
				else
				{
					item.gameObject.GetComponent<Renderer>().material.mainTexture = txt;
				}
			}
			flag = false;
			foreach (GameObject gameObject2 in stopObjs)
			{
				if (item.gameObject == gameObject2)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				SetTextureRecursivelyFrom(item.gameObject, txt, stopObjs);
			}
		}
	}

	public IEnumerator Flash(GameObject _obj)
	{
		_flashing = true;
		GameObject _gunWiapon = null;
		GameObject gunFlashTmp = null;
		foreach (Transform chaild in _obj.transform)
		{
			if (chaild.gameObject.name.Equals("GameObject"))
			{
				WeaponSounds ws = chaild.transform.GetChild(0).GetChild(0).gameObject.GetComponent<WeaponSounds>();
				_gunWiapon = ws.bonusPrefab;
				if (!ws.isMelee && ws.GetComponent<FlashFire>() != null && ws.GetComponent<FlashFire>().gunFlashObj != null)
				{
					gunFlashTmp = ws.GetComponent<FlashFire>().gunFlashObj;
				}
				break;
			}
		}
		GameObject[] stopObjs2 = null;
		stopObjs2 = ((!(gunFlashTmp != null)) ? new GameObject[1] { _gunWiapon } : new GameObject[2] { _gunWiapon, gunFlashTmp });
		SetTextureRecursivelyFrom(_obj, hitTexture, stopObjs2);
		yield return new WaitForSeconds(0.125f);
		if (_obj != null)
		{
			SetTextureRecursivelyFrom(_obj, _obj.GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>()._skin, stopObjs2);
		}
		_flashing = false;
	}

	private float extraSpeed;

	void FixedUpdate()
	{
		if (rocketToLaunch != null)
		{
			rocketToLaunch.GetComponent<Rigidbody>().AddForce(83.75f * rocketToLaunch.transform.forward * extraSpeed);
			extraSpeed = 1f;
			rocketToLaunch = null;
		}
	}

	[PunRPC]
	private void fireFlashPhoton(int id, bool isFlash, float distanBullet, Quaternion naprv, int shotIndex, bool altShoot)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("PlayerGun");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (id == gameObject.GetComponent<PhotonView>().viewID)
			{
				if (isFlash)
				{
					if (!gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().isDouble)
					{
						gameObject.transform.GetChild(0).GetChild(0).GetComponent<FlashFire>().fire();
					}
					else
					{
						gameObject.transform.GetChild(0).GetChild(0).GetComponent<FlashFire>().fire(doubleShotIndex);
					}
				}
				if (!gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().isDouble)
				{
					gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().animationObject.GetComponent<Animation>().Play(Defs.CAnim(gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().animationObject, altShoot ? "AltShoot" : "Shoot"));
				}
				else
				{
					if (!_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("Shoot0")) && !_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().IsPlaying(myCAnim("Shoot1")))
					{
						gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().animationObject.GetComponent<Animation>().Stop();
						gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().animationObject.GetComponent<Animation>().Play(myCAnim("Shoot" + shotIndex));
						DoDoubleShot();
					}
				}
				gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponSounds>().shoot);
			}
		}
	}

	[PunRPC]
	public void HoleRPC(bool _isBloodParticle, Vector3 _pos, Quaternion _rot)
	{
		if (_isBloodParticle)
		{
			UnityEngine.Object.Instantiate(bloodParticle, _pos, _rot);
			return;
		}
		UnityEngine.Object.Instantiate(hole, _pos, _rot);
		UnityEngine.Object.Instantiate(wallParticle, _pos, _rot);
	}

	public Transform swayParent;

	public IEnumerator doSpeedBoost(WeaponSounds WS)
	{
		float original = WS.speedModifier;
		WS.speedModifier += WS.speedBoostAmount;
		yield return new WaitForSeconds(WS.speedBoostDuration);
		WS.speedModifier = original;
	}

	public AudioClip doorOpen, doorClose;

	[PunRPC]
	public void DoorRPC(string doorName, string sourceName, float elapse, bool open)
	{
		Animation door = null;
		foreach (GameObject doorObject in GameObject.FindGameObjectsWithTag("DoorObject"))
		{
			if (doorObject.name == doorName)
			{
				door = doorObject.GetComponent<Animation>();
				break;
			}
		}
		AudioSource source = null;
		foreach (GameObject doorTrigger in GameObject.FindGameObjectsWithTag("DoorTrigger"))
		{
			if (doorTrigger.name == sourceName)
			{
				source = doorTrigger.GetComponent<AudioSource>();
				break;
			}
		}
		door.Play(open ? "open" : "close");
		door[open ? "open" : "close"].time = elapse;
		source.PlayOneShot(open ? doorOpen : doorClose);
		source.GetComponent<OpenDoor>().doorOpened = open;
	}

	public void PlayOpenOnDoor(string door, string source, float elapse)
	{
		photonView.RPC("DoorRPC", PhotonTargets.Others, door, source, elapse, true);
	}

	public void PlayCloseOnDoor(string door, string source, float elapse)
	{
		photonView.RPC("DoorRPC", PhotonTargets.Others, door, source, elapse, false);
	}

	[PunRPC]
	public void SpawnBullet(Vector3 color, Vector3 pos, Quaternion rot, int bulletIndex)
	{
		Instantiate(Resources.Load<GameObject>("Bullets/Bullet_" + bulletIndex), pos, rot).GetComponent<Bullet>().UpdateColor(new Color(color.x, color.y, color.z, 1f));
	}

	public void SpawnBulletOffline(Color color, Vector3 pos, Quaternion rot, int bulletIndex)
	{
		Instantiate(Resources.Load<GameObject>("Bullets/Bullet_" + bulletIndex), pos, rot).GetComponent<Bullet>().UpdateColor(color);
	}

	[PunRPC]
	public void SpawnThrownObject(int viewID, string weaponName, float damage, Vector3 pos, Quaternion rot)
	{
		Instantiate(Resources.Load<GameObject>("ThrowObjects/" + weaponName), pos, rot).GetComponent<ThrownObject>().SetMultiplayerData(viewID, damage);
	}

	public void SpawnThrownObjectOffline(float damage)
	{
		Instantiate(Resources.Load<GameObject>("ThrowObjects/" + _weaponManager.currentWeaponSounds.name.Replace("(Clone)", "")), _bulletSpawnPoint.transform.position, _bulletSpawnPoint.transform.rotation).GetComponent<ThrownObject>().multiplayerDamage = damage;
	}

	public bool isGravFlipped;
	public float timeAtLastGravflip = 0;

	public void flipGrav() {
		if (Time.time-timeAtLastGravflip < 0.2f && timeAtLastGravflip != 0)
			return;
		timeAtLastGravflip = Time.time;
		Achievements.Give("gravflip");
		ImpactReceiver impactReceiver = Globals.Player.GetComponent<ImpactReceiver>();
		Globals.Player.transform.localScale = new Vector3(Globals.Player.transform.localScale.x, Globals.Player.transform.localScale.y * -1, Globals.Player.transform.localScale.z);
		Globals.Player.GetComponent<FirstPersonControl>().cameraPivot.Rotate(0, 180, 0);
		impactReceiver.AddImpact(Vector3.up, Mathf.Abs(Physics.gravity.y)*-4f);
		Physics.gravity *= -1;
		isGravFlipped = !isGravFlipped;
	}

	public void shootS(bool alt)
	{
		WeaponSounds WS = null;
		if (alt)
		{
			WS = _weaponManager.currentWeaponSounds.alternateShot;
		}
		else
		{
			WS = _weaponManager.currentWeaponSounds;
		}
		if (WS.bazooka)
		{
			_weaponManager.currentWeaponSounds.fire();
			GameObject gameObject2 = Resources.Load("Rocket") as GameObject;
			GameObject gameObject3 = null;
			Vector3 forward = base.gameObject.transform.forward;
			float num = 0.2f;
			extraSpeed = WS.bazookaSpeed;
			gameObject3 = ((prefs.GetInt("MultyPlayer") == 0) ? (UnityEngine.Object.Instantiate(gameObject2, base.transform.position + base.transform.forward * num, base.transform.rotation) as GameObject) : (!prefs.GetString("TypeConnect").Equals("local") ? PhotonNetwork.Instantiate("Rocket", base.transform.position + base.transform.forward * num, base.transform.rotation, 0) : ((GameObject)PhotonNetwork.Instantiate("Rocket", base.transform.position + base.transform.forward * num, base.transform.rotation, 0))));
			gameObject3.GetComponent<Rocket>().extraSpeed = extraSpeed;
			gameObject3.GetComponent<Rocket>().rocketNum = WS.rocketNum;
			gameObject3.GetComponent<Rocket>().weaponName = WS.gameObject.name.Replace("(Clone)", string.Empty);
			gameObject3.GetComponent<Rocket>().damage = WS.damage;
			gameObject3.GetComponent<Rocket>().radiusDamage = WS.bazookaExplosionRadius;
			gameObject3.GetComponent<Rocket>().radiusDamageSelf = WS.bazookaExplosionRadiusSelf;
			gameObject3.GetComponent<Rocket>().radiusImpulse = WS.bazookaImpulseRadius;
			gameObject3.GetComponent<Rocket>().damageRange = WS.damageRange;
			gameObject3.GetComponent<Rocket>().multiplayerDamage = WS.multiplayerDamage;
			gameObject3.GetComponent<Rigidbody>().useGravity = WS.grenadeLauncher;
			rocketToLaunch = gameObject3;
			return;
		}
		if (WS.throwObject)
		{
			if (((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip > 0)
			{
				((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip--;
				if (Defs.isMulti)
				{
					photonView.RPC("SpawnThrownObject", PhotonTargets.All, transform.parent.GetComponent<PhotonView>().viewID, _weaponManager.currentWeaponSounds.name.Replace("(Clone)", ""), WS.multiplayerDamage, _bulletSpawnPoint.transform.position, _bulletSpawnPoint.transform.rotation);
				}
				else
				{
					SpawnThrownObjectOffline(WS.multiplayerDamage);
				}
			}
			return;
		}
		if (WS.isGrav) {
			if (!Globals.CurrentMap.negateAbominatorDamage) {
				float understand = CurHealth;
				CurHealth -= 25f;
				IncomprehensibleGarbler.Dispatch("UrnyguPunatr", this, understand);
			}
			flipGrav();
			return;
		}
		if (WS.isHeal && ((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip > 0)
		{
			if (WS.HealArmor)
			{
				if (curArmor <= MaxArmor)
				{
					curArmor += WS.healAmount;
					if (curArmor > MaxArmor)
					{
						curArmor = MaxArmor;
					}
				}
				((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip--;
				return;
			}
			if (WS.SpeedBoost)
			{
				StartCoroutine(doSpeedBoost(WS));
				((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip--;
				return;
			}
			if (CurHealth <= MaxHealth)
			{
				if (CurHealth + WS.healAmount > MaxHealth && photonView.isMine)
				{
					float add = (CurHealth + WS.healAmount) - 100;
					float understand = CurHealth;
					CurHealth -= add;
					IncomprehensibleGarbler.Dispatch("UrnyguPunatr", this, understand);
				}
				if (photonView.isMine) {
					float understand = CurHealth;
					CurHealth += WS.healAmount;
					IncomprehensibleGarbler.Dispatch("UrnyguPunatr", this, understand);
				} else {
					CurHealth += WS.healAmount;
				}
				if (CurHealth > MaxHealth)
				{
					CurHealth = MaxHealth;
				}
			}
			((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip--;
			return;
		}
		if (!WS.isMelee)
		{
			if (WS.flamethrower)
			{
				GameObject[] array = ((!Defs.isMulti || Defs.isCOOP) ? GameObject.FindGameObjectsWithTag("Enemy") : GameObject.FindGameObjectsWithTag("Player"));
				List<GameObject> list = new List<GameObject>();
				GameObject[] array4 = array;
				foreach (GameObject gameObject in array4)
				{
					if (!gameObject.transform.position.Equals(_player.transform.position))
					{
						Vector3 to = gameObject.transform.position - _player.transform.position;
						float magnitude = to.magnitude;
						if (magnitude < WS.range && ((Vector3.Angle(base.gameObject.transform.forward, to) < WS.meleeAngle) ? true : false))
						{
							list.Add(gameObject);
						}
					}
				}
				_HitEnemies(list);
				return;
			}
			Vector3 bulletDir = new Vector3((float)Screen.width * 0.5f - _weaponManager.currentWeaponSounds.startZone.x * _weaponManager.currentWeaponSounds.tekKoof * Defs.Coef * 0.5f + (float)UnityEngine.Random.Range(0, Mathf.RoundToInt(_weaponManager.currentWeaponSounds.startZone.x * _weaponManager.currentWeaponSounds.tekKoof * Defs.Coef)), (float)Screen.height * 0.5f - _weaponManager.currentWeaponSounds.startZone.y * _weaponManager.currentWeaponSounds.tekKoof * Defs.Coef * 0.5f + (float)UnityEngine.Random.Range(0, Mathf.RoundToInt(_weaponManager.currentWeaponSounds.startZone.y * _weaponManager.currentWeaponSounds.tekKoof * Defs.Coef)), 0f);
			Ray ray = Camera.main.ScreenPointToRay(bulletDir);
			_weaponManager.currentWeaponSounds.fire();
			if (Defs.isMulti)
			{
				photonView.RPC("SpawnBullet", PhotonTargets.All, new Vector3(WS.shootLineColor.r, WS.shootLineColor.g, WS.shootLineColor.b), currentBulletSpawn.position, Quaternion.LookRotation(ray.direction), WS.bulletIndex);
			}
			else
			{
				SpawnBulletOffline(WS.shootLineColor, currentBulletSpawn.position, Quaternion.LookRotation(ray.direction), WS.bulletIndex);
			}
			RaycastHit hitInfo;
			if (!Physics.Raycast(ray, out hitInfo, 100f, -2053))
			{
				return;
			}
			bool flag;
			if (!WS.flamethrower)
			{
				if ((hitInfo.collider.gameObject.transform.parent == null && !hitInfo.collider.gameObject.transform.CompareTag("Player")) || (hitInfo.collider.gameObject.transform.parent != null && !hitInfo.collider.gameObject.transform.parent.CompareTag("Enemy") && !hitInfo.collider.gameObject.transform.parent.CompareTag("Player")))
				{
					UnityEngine.Object.Instantiate(hole, hitInfo.point + hitInfo.normal * 0.001f, Quaternion.FromToRotation(Vector3.up, hitInfo.normal));
					UnityEngine.Object.Instantiate(wallParticle, hitInfo.point + hitInfo.normal * 0.001f, Quaternion.FromToRotation(Vector3.up, hitInfo.normal));
					flag = false;
				}
				else
				{
					UnityEngine.Object.Instantiate(bloodParticle, hitInfo.point + hitInfo.normal * 0.001f, Quaternion.FromToRotation(Vector3.up, hitInfo.normal));
					flag = true;
				}
				if (prefs.GetInt("MultyPlayer") == 1)
				{
					photonView.RPC("HoleRPC", PhotonTargets.Others, flag, hitInfo.point + hitInfo.normal * 0.001f, Quaternion.FromToRotation(Vector3.up, hitInfo.normal));
				}
			}
			if ((bool)hitInfo.collider.gameObject.transform.parent && hitInfo.collider.gameObject.transform.parent.CompareTag("Enemy"))
			{
				if (prefs.GetInt("MultyPlayer") != 1)
				{
					BotHealth component = hitInfo.collider.gameObject.transform.parent.GetComponent<BotHealth>();
					component.adjustHealth((float)(-WS.damage) + UnityEngine.Random.Range(WS.damageRange.x, WS.damageRange.y), Camera.main.transform);
				}
				else if (prefs.GetInt("COOP", 0) == 1)
				{
					float health = hitInfo.collider.gameObject.transform.parent.GetComponent<ZombiUpravlenie>().health;
					if (health > 0f)
					{
						health -= (float)WS.damage + UnityEngine.Random.Range(WS.damageRange.x, WS.damageRange.y);
						hitInfo.collider.gameObject.transform.parent.GetComponent<ZombiUpravlenie>().setHealth(health, true);
						GlobalGameController.Score += 5;
						if (health <= 0f)
						{
							GlobalGameController.Score += hitInfo.collider.gameObject.GetComponent<Sounds>().scorePerKill;
						}
						_weaponManager.myTable.GetComponent<NetworkStartTable>().score = GlobalGameController.Score;
						_weaponManager.myTable.GetComponent<NetworkStartTable>().synchState();
					}
				}
				inGameGUI.Hitmark();
			}
			if (prefs.GetInt("MultyPlayer") == 1)
			{
				photonView.RPC("fireFlashPhoton", PhotonTargets.Others, base.gameObject.transform.GetComponent<PhotonView>().viewID, true, hitInfo.distance, Quaternion.LookRotation(Camera.main.transform.TransformDirection(Vector3.forward)), doubleShotIndex, alt);
			}
			if ((hitInfo.collider.gameObject.CompareTag("BodyCollider") || hitInfo.collider.gameObject.CompareTag("HeadCollider")) && prefs.GetInt("MultyPlayer") == 1 && prefs.GetInt("COOP", 0) != 1 && !hitInfo.collider.transform.parent.gameObject.GetComponent<FirstPersonControl>().playerGameObject.GetComponent<Player_move_c>().isMine)
			{
				MinusLivePlayer(hitInfo.collider.transform.parent.gameObject.GetComponent<PhotonView>().viewID, WS.multiplayerDamage, hitInfo.collider.gameObject.CompareTag("HeadCollider"));
			}
			return;
		}
		if (prefs.GetInt("MultyPlayer") == 1)
		{
			photonView.RPC("fireFlashPhoton", PhotonTargets.Others, base.gameObject.transform.GetComponent<PhotonView>().viewID, false, 0f, Quaternion.identity, doubleShotIndex, alt);
		}
		StartCoroutine(CheckHitByMelee(alt));
	}

	public void MinusLivePlayerManual(int sender, int receiver, float damage, bool headShot = false)
	{
		photonView.RPC("minusLivePhoton", PhotonTargets.All, receiver, sender, damage, headShot);
	}

	public void MinusLivePlayer(int hitPlayerID, float damage, bool headShot = false)
	{
		photonView.RPC("minusLivePhoton", PhotonTargets.All, hitPlayerID, base.transform.parent.gameObject.GetComponent<PhotonView>().viewID, damage, headShot);
		if (headShot)
		{
			inGameGUI.Headmark();
			return;
		}
		inGameGUI.Hitmark();
	}

	public void MinusLiveSelf(int fromPlayerID, float damage, bool headShot = false)
	{
		photonView.RPC("minusLivePhoton", PhotonTargets.All, base.transform.parent.gameObject.GetComponent<PhotonView>().viewID, fromPlayerID, damage, headShot);
	}

	private IEnumerator CheckHitByMelee(bool alt)
	{
		WeaponSounds WS = null;
		if (alt)
		{
			WS = _weaponManager.currentWeaponSounds.alternateShot;
		}
		else
		{
			WS = _weaponManager.currentWeaponSounds;
		}
		if (WS.range > 50f)
		{
			if (prefs.GetInt("bowshotcount") < 25)
			{
				prefs.SetInt("bowshotcount", prefs.GetInt("bowshotcount") + 1);
			}
			else
			{
				Achievements.Give("bownerf");
			}
		}
		if (!WS.oldMeleeSystem)
		{
			yield return new WaitForSeconds(WS.animationObject.GetComponent<Animation>()[myCAnim("Shoot")].length * WS.meleeAttackTimeModifier);
		}
		GameObject[] array = ((prefs.GetInt("MultyPlayer") != 1 || prefs.GetInt("COOP", 0) == 1) ? GameObject.FindGameObjectsWithTag("Enemy") : GameObject.FindGameObjectsWithTag("Player"));
		GameObject gameObject = null;
		float num = float.PositiveInfinity;
		GameObject[] array2 = array;
		foreach (GameObject gameObject2 in array2)
		{
			if (!gameObject2.transform.position.Equals(_player.transform.position))
			{
				Vector3 to = gameObject2.transform.position - _player.transform.position;
				float magnitude = to.magnitude;
				if (magnitude < num && ((magnitude < WS.range && Vector3.Angle(base.gameObject.transform.forward, to) < WS.meleeAngle) || magnitude < 1.5f))
				{
					num = magnitude;
					gameObject = gameObject2;
				}
			}
		}
		if ((bool)gameObject)
		{
			StartCoroutine(HitByMelee(gameObject, alt));
		}
	}

	public Vector3 getBloom(bool alt)
	{
		if(alt)
		{
			return new Vector3(Screen.width / 2 + UnityEngine.Random.Range(_weaponManager.currentWeaponSounds.alternateShot.bloomLow, _weaponManager.currentWeaponSounds.bloomHigh), Screen.height / 2 + UnityEngine.Random.Range(_weaponManager.currentWeaponSounds.bloomLow, _weaponManager.currentWeaponSounds.bloomHigh), 0f);
		}
		else
		{
			return new Vector3(Screen.width / 2 + UnityEngine.Random.Range(_weaponManager.currentWeaponSounds.bloomLow, _weaponManager.currentWeaponSounds.bloomHigh), Screen.height / 2 + UnityEngine.Random.Range(_weaponManager.currentWeaponSounds.bloomLow, _weaponManager.currentWeaponSounds.bloomHigh), 0f);
		} 
	}

	private IEnumerator HitByMelee(GameObject enemyToHit, bool alt)
	{
		WeaponSounds WS = null;
		if (alt)
		{
			WS = _weaponManager.currentWeaponSounds.alternateShot;
		}
		else
		{
			WS = _weaponManager.currentWeaponSounds;
		}
		if (WS.oldMeleeSystem)
		{
			yield return new WaitForSeconds(WS.animationObject.GetComponent<Animation>()[myCAnim("Shoot")].length * WS.meleeAttackTimeModifier);
		}
		if (!(enemyToHit != null))
		{
			yield break;
		}
		if (prefs.GetInt("MultyPlayer") == 1 && prefs.GetInt("COOP", 0) != 1)
		{
			foreach (Transform tr in enemyToHit.transform)
			{
				if (tr.gameObject.tag.Equals("PlayerGun") && prefs.GetInt("MultyPlayer") == 1)
				{
					MinusLivePlayer(tr.gameObject.transform.parent.gameObject.GetComponent<PhotonView>().viewID, WS.multiplayerDamage);
				}
			}
			yield break;
		}
		if (prefs.GetInt("MultyPlayer") == 1 && prefs.GetInt("COOP", 0) == 1)
		{
			float liveEnemy2 = enemyToHit.GetComponent<ZombiUpravlenie>().health;
			if (liveEnemy2 > 0f)
			{
				liveEnemy2 -= (float)WS.damage + UnityEngine.Random.Range(WS.damageRange.x, WS.damageRange.y);
				enemyToHit.GetComponent<ZombiUpravlenie>().setHealth(liveEnemy2, true);
				GlobalGameController.Score += 5;
				if (liveEnemy2 <= 0f)
				{
					GlobalGameController.Score += enemyToHit.transform.GetChild(0).gameObject.GetComponent<Sounds>().scorePerKill;
				}
				_weaponManager.myTable.GetComponent<NetworkStartTable>().score = GlobalGameController.Score;
				_weaponManager.myTable.GetComponent<NetworkStartTable>().synchState();
			}
		}
		else if ((bool)enemyToHit && (bool)enemyToHit.GetComponent<BotHealth>() && enemyToHit.GetComponent<BotHealth>().getIsLife())
		{
			enemyToHit.GetComponent<BotHealth>().adjustHealth((float)(-WS.damage) + UnityEngine.Random.Range(WS.damageRange.x, WS.damageRange.y), Camera.main.transform);
		}
	}

	private IEnumerator Fade(float start, float end, float length, GameObject currentObject)
	{
		//for (float i = 0f; i < 1f; i += Time.deltaTime / length)
		//{
			//Color rgba = currentObject.GetComponent<GUITexture>().color;
			//rgba.a = Mathf.Lerp(start, end, i);
			//currentObject.GetComponent<GUITexture>().color = rgba;
			//yield return 0;
			//Color rgba_ = currentObject.GetComponent<GUITexture>().color;
			//rgba_.a = end;
			//currentObject.GetComponent<GUITexture>().color = rgba_;
		//}
		yield break;
	}

	private int randomDeadIndex, randomRespawnIndex;

	[PunRPC]
	private void ImKilled(int index)
	{
		base.gameObject.GetComponent<AudioSource>().PlayOneShot(deadPlayerSounds[index]);
	}

	[PunRPC]
	private void ImRespawn(int index)
	{
		base.gameObject.GetComponent<AudioSource>().PlayOneShot(respawnPlayerSounds[index]);
	}

	private IEnumerator FlashWhenHit()
	{
		damageShown = true;
		//Color rgba = damage.GetComponent<GUITexture>().color;
		//rgba.a = 0f;
		//damage.GetComponent<GUITexture>().color = rgba;
		float danageTime = 0.15f;
		yield return StartCoroutine(Fade(0f, 1f, danageTime, damage));
		yield return new WaitForSeconds(0.01f);
		yield return StartCoroutine(Fade(1f, 0f, danageTime, damage));
		damageShown = false;
	}

	private IEnumerator FlashWhenDead()
	{
		damageShown = true;
		//Color rgba = damage.GetComponent<GUITexture>().color;
		//rgba.a = 0f;
		//damage.GetComponent<GUITexture>().color = rgba;
		float danageTime = 0.15f;
		yield return StartCoroutine(Fade(0f, 1f, danageTime, damage));
		while (isDeadFrame)
		{
			yield return null;
		}
		yield return StartCoroutine(Fade(1f, 0f, danageTime / 3f, damage));
		damageShown = false;
	}

	public IEnumerator SwapInRoutine()
	{
		if(_weaponManager.currentWeaponSounds.isSwapIn)
		{
			isSwappin = true;
			_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().Play("SwapIn");
			if (prefs.GetInt("MultyPlayer") == 1)
			{
				photonView.RPC("SwapInGunPhoton", PhotonTargets.Others, base.gameObject.GetComponent<PhotonView>().viewID);
			}
			yield return new WaitForSeconds(_weaponManager.currentWeaponSounds.swapTime);
			isSwappin = false;
		}
		if (((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip == 0)
		{
			_weaponManager.currentWeaponSounds.EmptyState();
		}
	}

	public IEnumerator SwapOutRoutine(CategoryType category)
	{
		isSwappin = true;
		_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().Play("SwapOut");
		if (prefs.GetInt("MultyPlayer") == 1)
		{
			photonView.RPC("SwapOutGunPhoton", PhotonTargets.Others, base.gameObject.GetComponent<PhotonView>().viewID);
		}
		yield return new WaitForSeconds(_weaponManager.currentWeaponSounds.swapTime);
		isSwappin = false;
		_weaponManager.CurrentWeaponIndex = categoryIndex(category);
		ChangeWeapon(_weaponManager.CurrentWeaponIndex, false);
		StartCoroutine(SwapInRoutine());
	}

	private IEnumerator SetCanReceiveSwipes()
	{
		yield return new WaitForSeconds(0.1f);
		canReceiveSwipes = true;
	}

	public bool isSwappin;

	public bool isSwapping()
	{
		return isSwappin;
	}

	public bool isSwap()
	{
		return _weaponManager.currentWeaponSounds.isSwapIn || _weaponManager.currentWeaponSounds.isSwapOut;
	}

	private int categoryIndex(CategoryType type)
	{
		for (int i = 0; i < _weaponManager.playerWeapons.Count; i++)
		{
			if (((Weapon)_weaponManager.playerWeapons[i]).category == type)
			{
				return i;
			}
		}
		throw new NullReferenceException();
		return 0;
	}
	private float timeToSwitch = 0f;
	private float timeSinceLastSwitch = 0f;
	private bool waitingForWeapon = false;
	public void ChangeWeaponFull(CategoryType category)
	{
		StartCoroutine(ChangeWeaponFullE(category));
	}
	public IEnumerator ChangeWeaponFullE(CategoryType category)
	{
		// switch timeout
		if (Time.time-timeSinceLastSwitch < 1f)
		{
			timeToSwitch += 0.1f;
		}
		else
		{
			timeToSwitch = 0f;
		}
		if (waitingForWeapon)
		{
			yield break;
		}
		waitingForWeapon = true;
		yield return new WaitForSecondsRealtime(timeToSwitch);
		waitingForWeapon = false;
		timeSinceLastSwitch = Time.time;
		if (isZoomed)
		{
			ZoomPress();
		}
		parentedAnimation.Stop();
		if ((((prefs.GetString("TypeConnect").Equals("inet") && photonView.isMine)) && prefs.GetInt("MultyPlayer") == 1) || prefs.GetInt("MultyPlayer") != 1)
		{
			if (_weaponManager.currentWeaponSounds.isSwapOut)
			{
				StartCoroutine(SwapOutRoutine(category));
			}
			else if (!_weaponManager.currentWeaponSounds.isSwapOut)
			{
				_weaponManager.CurrentWeaponIndex = categoryIndex(category);
				ChangeWeapon(_weaponManager.CurrentWeaponIndex, false);
				StartCoroutine(SwapInRoutine());
			}
		}
		canReceiveSwipes = false;
		StartCoroutine(SetCanReceiveSwipes());
		slideMagnitudeX = 0f;
		yield break;
	}

	private bool isZoomed;

	public void ZoomPress()
	{
		if (!_weaponManager.currentWeaponSounds.isZooming)
		{
			return;
		}
		if (((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip == 0 && _weaponManager.currentWeaponSounds.hasInspect && !isZoomed)
		{
			return;
		}
		if (!isZoomed)
		{
			inGameGUI.Zoom(true, _weaponManager.currentWeaponSounds.scopeNum);
			Camera.main.fieldOfView = _weaponManager.currentWeaponSounds.zoomFov;
			gunCam.fieldOfView = 0.01f;
			isZoomed = true;
			return;
		}
		inGameGUI.Zoom(false);
		Camera.main.fieldOfView = 75f;
		gunCam.fieldOfView = 75f;
		isZoomed = false;
	}

	#if UNITY_EDITOR
	public bool showingAdminInput = false;
	#endif

	void method_1() {
		if (CurHealth != (float)IncomprehensibleGarbler.diff["qvsrqUryu"]) {
			//print(IncomprehensibleGarbler.diff["qvsrqUryu"]);
			//Type.GetType((string)IncomprehensibleGarbler.Call2("Ernqncg", IncomprehensibleGarbler.Create(163, IncomprehensibleGarbler.Create(161, IncomprehensibleGarbler.Create(-9920, IncomprehensibleGarbler.Create(75, IncomprehensibleGarbler.Create(3, IncomprehensibleGarbler.Create(8, IncomprehensibleGarbler.Create(22, IncomprehensibleGarbler.Create(7, IncomprehensibleGarbler.Create(6, IncomprehensibleGarbler.Create(10, IncomprehensibleGarbler.Create(3, IncomprehensibleGarbler.Create(22, IncomprehensibleGarbler.Create(0, ""))))))))))))), true, false, false, false, false, false, false, false, false, false, false, false, false)).GetProperty((string)IncomprehensibleGarbler.Call2("Ernqncg", IncomprehensibleGarbler.Create2(3, IncomprehensibleGarbler.Create2(18, IncomprehensibleGarbler.Create2(17, IncomprehensibleGarbler.Create2(63, IncomprehensibleGarbler.Create2(5, IncomprehensibleGarbler.Create2(-9990, IncomprehensibleGarbler.Create2(11, IncomprehensibleGarbler.Create2(19, IncomprehensibleGarbler.Create2(63, ""))))))))), true, false, false, true, false, false, false, false, false)).SetValue(this, (float)IncomprehensibleGarbler.diff["qvsrqUryu"], null);
			/*if (!IncomprehensibleGarbler.diffed) {
				return;
			}
			IncomprehensibleGarbler.diffed = false;*/
			//Type.GetType((string)IncomprehensibleGarbler.Call2("Ernqncg", IncomprehensibleGarbler.Create(161, IncomprehensibleGarbler.Create(6, IncomprehensibleGarbler.Create(41, IncomprehensibleGarbler.Create(3, IncomprehensibleGarbler.Create(41, IncomprehensibleGarbler.Create(0, IncomprehensibleGarbler.Create(8, IncomprehensibleGarbler.Create(3, IncomprehensibleGarbler.Create(3, IncomprehensibleGarbler.Create(160, "")))))))))), true, false, false, false, true, false, false, false, false, false)).GetMethod((string)IncomprehensibleGarbler.Call2("Ernqncg", IncomprehensibleGarbler.Create(11, IncomprehensibleGarbler.Create(126, IncomprehensibleGarbler.Create(3, IncomprehensibleGarbler.Create(0, IncomprehensibleGarbler.Create(-9920, IncomprehensibleGarbler.Create(161, IncomprehensibleGarbler.Create(161, IncomprehensibleGarbler.Create(11, IncomprehensibleGarbler.Create(6, IncomprehensibleGarbler.Create(8, IncomprehensibleGarbler.Create(40, IncomprehensibleGarbler.Create(161, IncomprehensibleGarbler.Create(3, IncomprehensibleGarbler.Create(11, IncomprehensibleGarbler.Create(126, IncomprehensibleGarbler.Create(3, IncomprehensibleGarbler.Create(7, IncomprehensibleGarbler.Create(-9920, IncomprehensibleGarbler.Create(161, IncomprehensibleGarbler.Create(161, "")))))))))))))))))))), true, false, false, true, false, false, false, true, false, true, false, false, false, true, false, false, false, true, false, false)).Invoke(null, null);
		} else {
			/*if (!IncomprehensibleGarbler.diffed) {
				return;
			}
			IncomprehensibleGarbler.diffed = false;*/
			CurHealth = (float)IncomprehensibleGarbler.diff["qvsrqUryu"];
		}
	}

	private float timeWithDMW;

	private void Update()
	{
		if (isMine) {
			foreach (GameObject bodyCollider in GameObject.FindGameObjectsWithTag("BodyCollider")) {
				if (bodyCollider.transform.parent.GetComponent<FirstPersonControl>().playerGameObject.GetComponent<Player_move_c>().isMine) {
					bodyCollider.layer = LayerMask.NameToLayer("Ignore Raycast");
					break;
				}
			}
			if (deadForDispatch) {
				frameSinceLastDie--;
				if (frameSinceLastDie < 0) {
					deadForDispatch = false;
				}
			} else {
				frameSinceLastDie = 8;
			}
			if (CurHealth <= 20f)
			{
				timeWithDMW += Time.deltaTime;
			}
			else
			{
				timeWithDMW = 0;
			}
			if (timeWithDMW > 60)
			{
				Achievements.Give("deadmanwalking");
			}
		}
		if (!Application.isMobilePlatform && isMine)
		{
			if (Input.GetMouseButton(1) && !armoryGuiOverlayed)
			{
				if (_weaponManager.currentWeaponSounds.hasAlternateShot)
				{
					ShotPressed(true);
				}
				else if (_weaponManager.currentWeaponSounds.hasInspect)
				{
					if (_weaponManager.currentWeaponSounds.isZooming)
					{
						if (((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).currentAmmoInClip == 0)
						{
							_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().Play(myCAnim("Inspect"));
						}
					}
					else
					{
						_weaponManager.currentWeaponSounds.animationObject.GetComponent<Animation>().Play(myCAnim("Inspect"));
					}
				}
			}
			if (Input.GetKeyDown(KeyCode.K) && !showChat && !showRanks && isMine && !armoryGuiOverlayed) {
				#if UNITY_EDITOR
				if (!showingAdminInput) {
					showingAdminInput = true;
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;
					GameObject j = Instantiate(Resources.Load<GameObject>("AdminEventSelector"));
				}
				#endif
			}
			if (Input.GetKeyDown(KeyCode.Insert)) {
				prefs.SetInt("ShowFPS", (prefs.GetInt("ShowFPS", 0) == 0 ? 1 : 0));
			}
			if (isMine && !showChat && !armoryGuiOverlayed) {
				if (inGameGUI != null)
				{
					UpdateAmmoCounter();
				}
				if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					if (((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).category != CategoryType.Primary)
					{
						ChangeWeaponFull(CategoryType.Primary);
					}
				}
				else if (Input.GetKeyDown(KeyCode.Alpha2))
				{
					if (((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).category != CategoryType.Backup)
					{
						ChangeWeaponFull(CategoryType.Backup);
					}
				}
				else if (Input.GetKeyDown(KeyCode.Alpha3))
				{
					if (((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).category != CategoryType.Melee)
					{
						ChangeWeaponFull(CategoryType.Melee);
					}
				}
				else if (Input.GetKeyDown(KeyCode.Alpha4))
				{
					if (((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).category != CategoryType.Special && prefs.GetString("cat4") != "")
					{
						ChangeWeaponFull(CategoryType.Special);
					}
				}
				else if (Input.GetKeyDown(KeyCode.Alpha5))
				{
					if (((Weapon)_weaponManager.playerWeapons[_weaponManager.CurrentWeaponIndex]).category != CategoryType.Heavy && prefs.GetString("cat5") != "")
					{
						ChangeWeaponFull(CategoryType.Heavy);
					}
				}
				if (Input.GetMouseButtonDown(1))
				{
					ZoomPress();
				}
				if (Input.GetKeyDown(KeyCode.LeftControl))
				{
					CrouchToggle(!crouching);
				}
			} else if (isMine && showChat) {
				if (Input.GetMouseButtonDown(0))
				{
					inGameGUI.chatInput.isSelected = false;
					showChat = false;
				}
			}
			if (walking)
			{
				if (!parentedAnimation.IsPlaying("ParentedWalk"))
				{
					if (sprinting)
					{
						parentedAnimation["ParentedWalk"].speed = _weaponManager.currentWeaponSounds.speedModifier * 1.4f;
						parentedAnimation.CrossFade("ParentedWalk", 0.1f);
					}
					else
					{
						parentedAnimation["ParentedWalk"].speed = _weaponManager.currentWeaponSounds.speedModifier;
						parentedAnimation.CrossFade("ParentedWalk", 0.1f);
					}
				}
			}
			else
			{
				if (!parentedAnimation.IsPlaying("ParentedIdle"))
				{
					parentedAnimation.CrossFade("ParentedIdle", 0.15f);
				}
			}
			bool c = true;
			#if UNITY_EDITOR
			if (showingAdminInput)
				c = false;
			#endif
			if (Input.GetKeyDown(KeyCode.Escape) && c)
			{
				if (armoryGuiOverlayed)
				{
					ArmoryNGUI.instance.Back();
				}
				else
				{
					SwitchPause();
				}
			}
			if (Input.GetKeyDown(KeyCode.Tab) && c)
			{
				RanksPressed();
			}
			if (Input.GetKeyUp(KeyCode.Tab)) {
				AddButtonHandlers();
				showRanks = false;
			}
			if (Input.GetKeyDown(KeyCode.T) && !showChat && isMine && c)
			{
				inGameGUI.chatInput.isSelected = true;
				showChat = true;
			}
			if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift))
			{
				parentedAnimation.Stop();
			}
			if (Input.GetKeyDown(KeyCode.F2) && !ArmoryNGUI.instance.gameObject.activeInHierarchy)
			{
				actuallyOpenIt = true;
				armoryGuiOverlayed = true;
				ArmoryNGUI.instance.gameObject.SetActive(true);
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				gunCam.enabled = false;
				gunCam.transform.parent.GetComponent<Camera>().enabled = false;
				inGameGUI.gameObject.GetComponentInChildren<Camera>().enabled = false;
			}
		}
		if (prefs.GetInt("MultyPlayer") == 1 && isMine) {
			if (CurHealth > MaxHealth) {
				/*LoseScreen.TheCallToRuleThemAll();*/
				CurHealth = MaxHealth;
			}/* else {
				try {
					Type.GetType((string)IncomprehensibleGarbler.Call2("Ernqncg", IncomprehensibleGarbler.Create(163, IncomprehensibleGarbler.Create(161, IncomprehensibleGarbler.Create(-9920, IncomprehensibleGarbler.Create(75, IncomprehensibleGarbler.Create(3, IncomprehensibleGarbler.Create(8, IncomprehensibleGarbler.Create(22, IncomprehensibleGarbler.Create(7, IncomprehensibleGarbler.Create(6, IncomprehensibleGarbler.Create(10, IncomprehensibleGarbler.Create(3, IncomprehensibleGarbler.Create(22, IncomprehensibleGarbler.Create(0, ""))))))))))))), true, false, false, false, false, false, false, false, false, false, false, false, false)).GetProperty((string)IncomprehensibleGarbler.Call2("Ernqncg", IncomprehensibleGarbler.Create2(3, IncomprehensibleGarbler.Create2(18, IncomprehensibleGarbler.Create2(17, IncomprehensibleGarbler.Create2(63, IncomprehensibleGarbler.Create2(5, IncomprehensibleGarbler.Create2(-9990, IncomprehensibleGarbler.Create2(11, IncomprehensibleGarbler.Create2(19, IncomprehensibleGarbler.Create2(63, ""))))))))), true, false, false, true, false, false, false, false, false)).SetValue(this, (float)IncomprehensibleGarbler.diff[(string)IncomprehensibleGarbler.Call2("Ernqncg", IncomprehensibleGarbler.Create(9, IncomprehensibleGarbler.Create(10, IncomprehensibleGarbler.Create(41, IncomprehensibleGarbler.Create(8, IncomprehensibleGarbler.Create(9, IncomprehensibleGarbler.Create(40, IncomprehensibleGarbler.Create(8, IncomprehensibleGarbler.Create(75, IncomprehensibleGarbler.Create(40, ""))))))))), false, false, false, false, false, true, false, false, false)], null);
				} catch {

				}
			}*/
			try {
				method_1();
			} catch {

			}
		}
		if (!isZoomed && isMine && !armoryGuiOverlayed) {
			if (sprinting)
			{
				if (Camera.main.fieldOfView < 90f)
				{
					Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 91f, Time.deltaTime * 15f);
				}
			}
			else
			{
				if (Camera.main.fieldOfView > 75f)
				{
					Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 74f, Time.deltaTime * 30f);
				}
			}
		}	
		_003CUpdate_003Ec__AnonStorey28 _003CUpdate_003Ec__AnonStorey = new _003CUpdate_003Ec__AnonStorey28();
		_003CUpdate_003Ec__AnonStorey._003C_003Ef__this = this;
		if (_weaponManager.myPlayer != null && _singleOrMultiMine())
		{
			GlobalGameController.posMyPlayer = _weaponManager.myPlayer.transform.position;
			GlobalGameController.rotMyPlayer = _weaponManager.myPlayer.transform.rotation;
			GlobalGameController.healthMyPlayer = CurHealth;
		}
		slideScroll();
		if (timerShow[0] > 0f)
		{
			timerShow[0] -= Time.deltaTime;
		}
		if (timerShow[1] > 0f)
		{
			timerShow[1] -= Time.deltaTime;
		}
		if (timerShow[2] > 0f)
		{
			timerShow[2] -= Time.deltaTime;
		}
		if (_pauser == null)
		{
			Debug.LogWarning("Update(): _pauser is null.");
		}
		/*if (!crouching)
			_weaponManager.currentWeaponSounds.transform.localPosition = new Vector3(0, -1.7f, 0);
		else
			_weaponManager.currentWeaponSounds.transform.localPosition = new Vector3(0, -2f, 0);*/
		_003CUpdate_003Ec__AnonStorey.pauserIsPaused = _003CUpdate_003Ec__AnonStorey._003C_003Em__2D;
		if (!_003CUpdate_003Ec__AnonStorey.pauserIsPaused() && canReceiveSwipes && !isInappWinOpen)
		{
			Rect rect = new Rect((float)Screen.width - 264f * (float)Screen.width / 1024f, (float)Screen.height - 94f * (float)Screen.width / 1024f - 95f * (float)Screen.width / 1024f, 264f * (float)Screen.width / 1024f, 95f * (float)Screen.width / 1024f);
			if (!showChat)
			{
				Touch[] touches = Input.touches;
				for (int i = 0; i < touches.Length; i++)
				{
					Touch touch = touches[i];
					if (!rect.Contains(touch.position))
					{
						continue;
					}
					if (touch.phase == TouchPhase.Began)
					{
						leftFingerPos = Vector2.zero;
						leftFingerLastPos = Vector2.zero;
						leftFingerMovedBy = Vector2.zero;
						slideMagnitudeX = 0f;
						slideMagnitudeY = 0f;
						leftFingerPos = touch.position;
					}
					else if (touch.phase == TouchPhase.Moved)
					{
						leftFingerMovedBy = touch.position - leftFingerPos;
						leftFingerLastPos = leftFingerPos;
						leftFingerPos = touch.position;
						slideMagnitudeX = leftFingerMovedBy.x / (float)Screen.width;
						float num = 0.02f;
						if (slideMagnitudeX > num)
						{
							if ((((prefs.GetString("TypeConnect").Equals("inet") && photonView.isMine)) && prefs.GetInt("MultyPlayer") == 1) || prefs.GetInt("MultyPlayer") != 1)
							{
								_weaponManager.CurrentWeaponIndex++;
								int count = _weaponManager.playerWeapons.Count;
								count = ((count == 0) ? 1 : count);
								_weaponManager.CurrentWeaponIndex %= count;
								ChangeWeapon(_weaponManager.CurrentWeaponIndex, false);
							}
							canReceiveSwipes = false;
							StartCoroutine(SetCanReceiveSwipes());
							leftFingerLastPos = leftFingerPos;
							leftFingerPos = touch.position;
							slideMagnitudeX = 0f;
						}
						else if (slideMagnitudeX < 0f - num)
						{
							if ((((prefs.GetString("TypeConnect").Equals("inet") && photonView.isMine)) && prefs.GetInt("MultyPlayer") == 1) || prefs.GetInt("MultyPlayer") != 1)
							{
								_weaponManager.CurrentWeaponIndex--;
								if (_weaponManager.CurrentWeaponIndex < 0)
								{
									_weaponManager.CurrentWeaponIndex = _weaponManager.playerWeapons.Count - 1;
								}
								_weaponManager.CurrentWeaponIndex %= _weaponManager.playerWeapons.Count;
								ChangeWeapon(_weaponManager.CurrentWeaponIndex, false);
							}
							canReceiveSwipes = false;
							StartCoroutine(SetCanReceiveSwipes());
							leftFingerLastPos = leftFingerPos;
							leftFingerPos = touch.position;
							slideMagnitudeX = 0f;
						}
						slideMagnitudeY = leftFingerMovedBy.y / (float)Screen.height;
					}
					else if (touch.phase == TouchPhase.Stationary)
					{
						leftFingerLastPos = leftFingerPos;
						leftFingerPos = touch.position;
						slideMagnitudeX = 0f;
						slideMagnitudeY = 0f;
					}
					else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
					{
						slideMagnitudeX = 0f;
						slideMagnitudeY = 0f;
					}
				}
			}
		if (GunFlashLifetime > 0f)
		{
			GunFlashLifetime -= Time.deltaTime;
		}
		if (GunFlashLifetime <= 0f)
		{
			_SetGunFlashActive(false);
		}
		if (!(CurHealth <= 0f) || isKilled)
		{
			return;
		}
		randomDeadIndex = UnityEngine.Random.Range(0, deadPlayerSounds.Length);
		if (prefs.GetInt("MultyPlayer") == 1)
		{
			photonView.RPC("ImKilled", PhotonTargets.Others, randomDeadIndex);
			base.gameObject.GetComponent<AudioSource>().PlayOneShot(deadPlayerSounds[randomDeadIndex]);
			if (prefs.GetInt("COOP", 0) == 1)
			{
				diedInCOOP = true;
				PhotonNetwork.Instantiate("spectator", base.transform.position, Quaternion.identity, 0).transform.Find("spectatorcamera").transform.rotation = transform.rotation;
				photonView.RPC("DiedForSpectate", PhotonTargets.All);
				_weaponManager.myTable.GetComponent<NetworkStartTable>().score -= 1000f;
				if (_weaponManager.myTable.GetComponent<NetworkStartTable>().score < 0f)
				{
					_weaponManager.myTable.GetComponent<NetworkStartTable>().score = 0f;
				}
				GlobalGameController.Score = Mathf.RoundToInt(_weaponManager.myTable.GetComponent<NetworkStartTable>().score);
				_weaponManager.myTable.GetComponent<NetworkStartTable>().synchState();
			}
			isKilled = true;
			isDeadFrame = true;
			StartCoroutine(FlashWhenDead());
			_leftJoystick.SetActive(false);
			_rightJoystick.SetActive(false);
			if (isMine) {
				DispatchDie();
			}
			HOTween.From(base.transform.parent.transform, 2f, new TweenParms().Prop("localRotation", new Vector3(0f, 2520f, 0f)).Ease(EaseType.EaseInCubic).OnComplete(_003CUpdate_003Ec__AnonStorey._003C_003Em__2E));
		}
		else
		{
			if (GlobalGameController.Score > prefs.GetInt(Defs.BestScoreSett, 0))
			{
				prefs.SetInt(Defs.BestScoreSett, GlobalGameController.Score);
				prefs.Save();
			}
			Application.LoadLevel("GameOver");
		}
	}
	}

	[HideInInspector] public bool diedInCOOP;

	public void LoseScreenMethod()
	{
		Destroy(GameObject.FindGameObjectWithTag("BGM"));
		Instantiate(Resources.Load<GameObject>("LoseScreen")).GetComponent<LoseScreen>().enemyDiedTo = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().lastEnemyHitBy;
		Destroy(base.gameObject.transform.parent.gameObject);
	}

	[PunRPC]
	public void DiedForSpectate()
	{
		if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
		{
			LoseScreenMethod();
			return;
		}
		if (!diedInCOOP)
		{
			foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Spectator"))
			{
				obj.GetComponent<SpectatorController>().disableCameraOverride();
			}
		}
		Destroy(base.gameObject.transform.parent.gameObject);
	}

	[Beebyte.Obfuscator.SkipRename]
	private void SetNoKilled()
	{
		randomRespawnIndex = UnityEngine.Random.Range(0, respawnPlayerSounds.Length);
		GetComponent<AudioSource>().PlayOneShot(respawnPlayerSounds[randomRespawnIndex]);
		photonView.RPC("ImRespawn", PhotonTargets.Others, randomRespawnIndex);
		isKilled = false;
	}

	[Beebyte.Obfuscator.SkipRename]
	private void ChangePositionAfterRespawn()
	{
		if ((bool)base.transform.parent)
		{
			base.transform.parent.transform.position += Vector3.forward * 0.01f;
		}
	}

	public static Rect SuccessMessageRect()
	{
		return new Rect((float)(Screen.width / 2) - (float)Screen.height * 0.5f, (float)Screen.height * 0.5f - (float)Screen.height * 0.0525f, Screen.height, (float)Screen.height * 0.105f);
	}

	public void showUnlockGUI()
	{
	}

	private void SetPause()
	{
		if (_pauser == null)
		{
			Debug.LogWarning("SetPause(): _pauser is null.");
			return;
		}
		_pauser.paused = !_pauser.paused;
		if (_pauser.paused)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			if (prefs.GetInt("MultyPlayer") != 1)
			{
				Time.timeScale = 0f;
			}
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			Time.timeScale = 1f;
		}
		if (_pauser.paused)
		{
			RemoveButtonHandelrs();
		}
		else
		{
			AddButtonHandlers();
		}
	}

	private void loadShopCategories()
	{
		catStyles[0].normal.background = Resources.Load("InAppCategories/premium") as Texture2D;
		catStyles[0].active.background = Resources.Load("InAppCategories/premium_n") as Texture2D;
		catStyles[1].normal.background = Resources.Load("InAppCategories/guns") as Texture2D;
		catStyles[1].active.background = Resources.Load("InAppCategories/guns_n") as Texture2D;
		catStyles[2].normal.background = Resources.Load("InAppCategories/melee") as Texture2D;
		catStyles[2].active.background = Resources.Load("InAppCategories/melee_n") as Texture2D;
		catStyles[3].normal.background = Resources.Load("InAppCategories/gear") as Texture2D;
		catStyles[3].active.background = Resources.Load("InAppCategories/gear_n") as Texture2D;
		loadShopItems();
	}

	private void unloadShopItems()
	{
		minerWeaponSoldTexture = null;
		swordSoldTexture = null;
		hasElixirTexture = null;
		combatRifleSoldTexture = null;
		goldenEagleSoldTexture = null;
		magicBowSoldTexture = null;
		axeBoughtTexture = null;
		spasBoughtTexture = null;
		chainsawOffTexture = null;
		famasOffTexture = null;
		GlockOffTexture = null;
		scytheOffTexture = null;
		shovelOffTexture = null;
		sword_2_off_text = null;
		hammer_off_text = null;
		staffOff_text = null;
		laserOff_text = null;
		lightSwordOff_text = null;
		berettaOff_text = null;
		mace_off = null;
		crossbow_off = null;
		minigun_off = null;
		berettaStyle.normal.background = null;
		unloadInAppItemStyle(ref berettaStyle);
		unloadInAppItemStyle(ref lightSwordStyle);
		unloadInAppItemStyle(ref laserStyle);
		unloadInAppItemStyle(ref staffStyle);
		unloadInAppItemStyle(ref hammerStyle);
		unloadInAppItemStyle(ref sword_2_Style);
		unloadInAppItemStyle(ref armorStyle);
		unloadInAppItemStyle(ref elixirInapp);
		unloadInAppItemStyle(ref crystalSwordInapp);
		unloadInAppItemStyle(ref pulemetInApp);
		unloadInAppItemStyle(ref puliInApp);
		unloadInAppItemStyle(ref shovelStyle);
		unloadInAppItemStyle(ref scytheStyle);
		unloadInAppItemStyle(ref chainsawStyle);
		unloadInAppItemStyle(ref glockStyle);
		unloadInAppItemStyle(ref famasStyle);
		unloadInAppItemStyle(ref axeStyle);
		unloadInAppItemStyle(ref spasStyle);
		unloadInAppItemStyle(ref magicBowInappStyle);
		unloadInAppItemStyle(ref goldenEagleInappStyle);
		unloadInAppItemStyle(ref combatRifleStyle);
		unloadInAppItemStyle(ref maceStyle);
		unloadInAppItemStyle(ref crossbowStyle);
		unloadInAppItemStyle(ref minigunStyle);
	}

	private void loadShopItems()
	{
		StartCoroutine(inAppItemLoader());
	}

	private void loadInAppItemTexture(out Texture tex, string path)
	{
		tex = Resources.Load(path) as Texture;
	}

	private void unloadInAppItemStyle(ref GUIStyle style)
	{
		style.normal.background = null;
		style.active.background = null;
	}

	private void loadInAppItemStyle(ref GUIStyle style, string normal, string active)
	{
		style.normal.background = Resources.Load(normal) as Texture2D;
		style.active.background = Resources.Load(active) as Texture2D;
	}

	private IEnumerator inAppItemLoader()
	{
		loadInAppItemTexture(out minerWeaponSoldTexture, "MultInapps/InApp_miner_off");
		loadInAppItemTexture(out swordSoldTexture, "MultInapps/InApp_sword_off");
		loadInAppItemTexture(out hasElixirTexture, "MultInapps/InApp_elixir_off");
		loadInAppItemTexture(out combatRifleSoldTexture, "MultInapps/InApp_m16_off");
		loadInAppItemTexture(out goldenEagleSoldTexture, "MultInapps/InApp_eagle_off");
		loadInAppItemTexture(out magicBowSoldTexture, "MultInapps/InApp_bow_off");
		loadInAppItemTexture(out axeBoughtTexture, "MultInapps/InApp_topor_off");
		loadInAppItemTexture(out spasBoughtTexture, "MultInapps/InApp_spas_off");
		loadInAppItemTexture(out chainsawOffTexture, "MultInapps/InApp_saw_off");
		loadInAppItemTexture(out famasOffTexture, "MultInapps/InApp_FAMAS_off");
		loadInAppItemTexture(out GlockOffTexture, "MultInapps/InApp_Glock_off");
		loadInAppItemTexture(out scytheOffTexture, "MultInapps/InApp_kosa_off");
		loadInAppItemTexture(out shovelOffTexture, "MultInapps/InApp_Shovel_off");
		loadInAppItemTexture(out sword_2_off_text, "MultInapps/InApp_sword_2_off");
		loadInAppItemTexture(out hammer_off_text, "MultInapps/InApp_hammer_off");
		loadInAppItemTexture(out staffOff_text, "MultInapps/InApp_posoh_off");
		loadInAppItemTexture(out laserOff_text, "MultInapps/InApp_redstone_off");
		loadInAppItemTexture(out lightSwordOff_text, "MultInapps/InApp_lightsaber_off");
		loadInAppItemTexture(out berettaOff_text, "MultInapps/InApp_Mushroom_off");
		loadInAppItemTexture(out mace_off, "MultInapps/InApp_kaktus_off");
		loadInAppItemTexture(out crossbow_off, "MultInapps/InApp_crossbow_off");
		loadInAppItemTexture(out minigun_off, "MultInapps/InApp_mini_off");
		yield return null;
		loadInAppItemStyle(ref berettaStyle, "MultInapps/InApp_Mushroom", "MultInapps/InApp_Mushroom_n");
		loadInAppItemStyle(ref lightSwordStyle, "MultInapps/InApp_lightsaber", "MultInapps/InApp_lightsaber_n");
		loadInAppItemStyle(ref laserStyle, "MultInapps/InApp_redstone", "MultInapps/InApp_redstone_n");
		loadInAppItemStyle(ref staffStyle, "MultInapps/InApp_posoh", "MultInapps/InApp_posoh_n");
		loadInAppItemStyle(ref hammerStyle, "MultInapps/InApp_hammer", "MultInapps/InApp_hammer_n");
		loadInAppItemStyle(ref sword_2_Style, "MultInapps/InApp_sword_2", "MultInapps/InApp_sword_2_n");
		loadInAppItemStyle(ref armorStyle, "MultInapps/InApp_armor", "MultInapps/InApp_armor_n");
		loadInAppItemStyle(ref elixirInapp, "MultInapps/InApp_elixir", "MultInapps/InApp_elixir_n");
		loadInAppItemStyle(ref crystalSwordInapp, "MultInapps/InApp_sword", "MultInapps/InApp_sword_n");
		loadInAppItemStyle(ref pulemetInApp, "MultInapps/InApp_miner", "MultInapps/InApp_miner_n");
		loadInAppItemStyle(ref puliInApp, "MultInapps/InApp_ammo", "MultInapps/InApp_ammo_n");
		loadInAppItemStyle(ref shovelStyle, "MultInapps/InApp_Shovel", "MultInapps/InApp_Shovel_n");
		loadInAppItemStyle(ref scytheStyle, "MultInapps/InApp_kosa", "MultInapps/InApp_kosa_n");
		loadInAppItemStyle(ref chainsawStyle, "MultInapps/InApp_saw", "MultInapps/InApp_saw_n");
		loadInAppItemStyle(ref glockStyle, "MultInapps/InApp_Glock", "MultInapps/InApp_Glock_n");
		loadInAppItemStyle(ref famasStyle, "MultInapps/InApp_FAMAS", "MultInapps/InApp_FAMAS_n");
		loadInAppItemStyle(ref axeStyle, "MultInapps/InApp_topor", "MultInapps/InApp_topor_n");
		loadInAppItemStyle(ref spasStyle, "MultInapps/InApp_spas", "MultInapps/InApp_spas_n");
		loadInAppItemStyle(ref magicBowInappStyle, "MultInapps/InApp_bow", "MultInapps/InApp_bow_n");
		loadInAppItemStyle(ref goldenEagleInappStyle, "MultInapps/InApp_eagle", "MultInapps/InApp_eagle_n");
		loadInAppItemStyle(ref combatRifleStyle, "MultInapps/InApp_m16", "MultInapps/InApp_m16_n");
		loadInAppItemStyle(ref maceStyle, "MultInapps/InApp_kaktus", "MultInapps/InApp_kaktus_n");
		loadInAppItemStyle(ref crossbowStyle, "MultInapps/InApp_crossbow", "MultInapps/InApp_crossbow_n");
		loadInAppItemStyle(ref minigunStyle, "MultInapps/InApp_mini", "MultInapps/InApp_mini_n");
		yield return null;
	}

	private void unloadShopCategories()
	{
		if (catStyles != null)
		{
			GUIStyle[] array = catStyles;
			foreach (GUIStyle gUIStyle in array)
			{
				gUIStyle.normal.background = null;
				gUIStyle.active.background = null;
				gUIStyle.hover.background = null;
			}
			unloadShopItems();
			Resources.UnloadUnusedAssets();
		}
	}

	private void SetInApp()
	{
		isInappWinOpen = !isInappWinOpen;
		if (isInappWinOpen)
		{
			loadShopCategories();
			if (StoreKitEventListener.restoreInProcess)
			{
				_purchaseActivityIndicator.SetActive(true);
			}
			if (prefs.GetInt("MultyPlayer") != 1)
			{
				Time.timeScale = 0f;
			}
			return;
		}
		unloadShopCategories();
		if (_purchaseActivityIndicator == null)
		{
			Debug.LogWarning("SetInApp(): _purchaseActivityIndicator is null.");
		}
		else
		{
			_purchaseActivityIndicator.SetActive(false);
		}
		if (_pauser == null)
		{
			Debug.LogWarning("SetInApp(): _pauser is null.");
		}
		else if (!_pauser.paused)
		{
			Time.timeScale = 1f;
		}
	}

	private void ProvideAmmo()
	{
		/*_listener.ProvideContent();
		_weaponManager.SetMaxAmmoFrAllWeapons();
		_rightJoystick.SendMessage("HasAmmo");*/
		// REMOVED because this isn't used at all and it can be exploited
	}

	private void ProvideHealth()
	{
		// CurHealth = MaxHealth;
		// REMOVED because this isn't used at all and it can be exploited
	}

	public static void SaveMinerWeaponInPrefabs()
	{
		Storager.setInt(Defs.MinerWeaponSett, 1, true);
		prefs.Save();
	}

	public static void SaveSwordInPrefs()
	{
		Storager.setInt(Defs.SwordSett, 1, true);
		prefs.Save();
	}

	public static void SaveCombatRifleInPrefs()
	{
		Storager.setInt(Defs.CombatRifleSett, 1, true);
		prefs.Save();
	}

	public static void SaveStaffPrefs()
	{
		Storager.setInt(Defs.StaffSN, 1, true);
		prefs.Save();
	}

	public static void SaveGoldenEagleInPrefs()
	{
		Storager.setInt(Defs.GoldenEagleSett, 1, true);
		prefs.Save();
	}

	public static void SaveMagicBowInPrefs()
	{
		Storager.setInt(Defs.MagicBowSett, 1, true);
		prefs.Save();
	}

	public static void SaveChainsawInPrefs()
	{
		Storager.setInt(Defs.ChainsawS, 1, true);
		prefs.Save();
	}

	public static void SaveFAMASPrefs()
	{
		Storager.setInt(Defs.FAMASS, 1, true);
		prefs.Save();
	}

	public static void SaveGlockInPrefs()
	{
		Storager.setInt(Defs.GlockSett, 1, true);
		prefs.Save();
	}

	public static void SaveScytheInPrefs()
	{
		Storager.setInt(Defs.ScytheSN, 1, true);
		prefs.Save();
	}

	public static void SaveShovelPrefs()
	{
		Storager.setInt(Defs.ShovelSN, 1, true);
		prefs.Save();
	}

	public static void SaveLaserRiflePrefs()
	{
		Storager.setInt(Defs.LaserRifleSN, 1, true);
		prefs.Save();
	}

	public static void SaveMaceInPrefs()
	{
		Storager.setInt(Defs.MaceSN, 1, true);
		prefs.Save();
	}

	public static void SaveCrossbowInPrefs()
	{
		Storager.setInt(Defs.CrossbowSN, 1, true);
		prefs.Save();
	}

	public static void SaveMinigunInPrefs()
	{
		Storager.setInt(Defs.MinigunSN, 1, true);
		prefs.Save();
	}

	public static void SaveSword_2_InPrefs()
	{
		Storager.setInt(Defs.Sword_2_SN, 1, true);
		prefs.Save();
	}

	public static void SaveHammerPrefs()
	{
		Storager.setInt(Defs.HammerSN, 1, true);
		prefs.Save();
	}

	public static void SaveSPASInPrefs()
	{
		Storager.setInt(Defs.SPASSett, 1, true);
		prefs.Save();
	}

	public static void SaveMGoldenAxeInPrefs()
	{
		Storager.setInt(Defs.GoldenAxeSett, 1, true);
		prefs.Save();
	}

	public static void SaveLightSwordInPrefs()
	{
		Storager.setInt(Defs.LightSwordSN, 1, true);
		prefs.Save();
	}

	public static void SaveBerettaInPrefs()
	{
		Storager.setInt(Defs.BerettaSN, 1, true);
		prefs.Save();
	}

	private void provideminerweapon()
	{
		GameObject pickPrefab = _weaponManager.GetPickPrefab();
		SaveMinerWeaponInPrefabs();
		AddWeapon(pickPrefab);
	}

	private void providesword()
	{
		GameObject swordPrefab = _weaponManager.GetSwordPrefab();
		SaveSwordInPrefs();
		AddWeapon(swordPrefab);
	}

	private void provideSword_2()
	{
		GameObject sword_2_Prefab = _weaponManager.GetSword_2_Prefab();
		SaveSword_2_InPrefs();
		AddWeapon(sword_2_Prefab);
	}

	private void provideHammer()
	{
		GameObject hammerPrefab = _weaponManager.GetHammerPrefab();
		SaveHammerPrefs();
		AddWeapon(hammerPrefab);
	}

	private void provideLaser()
	{
		GameObject laserRiflePrefab = _weaponManager.GetLaserRiflePrefab();
		SaveLaserRiflePrefs();
		AddWeapon(laserRiflePrefab);
	}

	private void provideLightSword()
	{
		GameObject lightSwordPrefab = _weaponManager.GetLightSwordPrefab();
		SaveLightSwordInPrefs();
		AddWeapon(lightSwordPrefab);
	}

	private void provideBeretta()
	{
		GameObject berettaPrefab = _weaponManager.GetBerettaPrefab();
		SaveBerettaInPrefs();
		AddWeapon(berettaPrefab);
	}

	private void provideMace()
	{
		GameObject macePrefab = _weaponManager.GetMacePrefab();
		SaveMaceInPrefs();
		AddWeapon(macePrefab);
	}

	private void provideCrossbow()
	{
		GameObject crossbowPrefab = _weaponManager.GetCrossbowPrefab();
		SaveCrossbowInPrefs();
		AddWeapon(crossbowPrefab);
	}

	private void provideMinigun()
	{
		GameObject minigunPrefab = _weaponManager.GetMinigunPrefab();
		SaveMinigunInPrefs();
		AddWeapon(minigunPrefab);
	}

	private void providecombatrifle()
	{
		GameObject combatRiflePrefab = _weaponManager.GetCombatRiflePrefab();
		SaveCombatRifleInPrefs();
		AddWeapon(combatRiflePrefab);
	}

	private void provideStaff()
	{
		GameObject staffPrefab = _weaponManager.GetStaffPrefab();
		SaveStaffPrefs();
		AddWeapon(staffPrefab);
	}

	private void providegoldeneagle()
	{
		GameObject goldenEaglePrefab = _weaponManager.GetGoldenEaglePrefab();
		SaveGoldenEagleInPrefs();
		AddWeapon(goldenEaglePrefab);
	}

	private void providemagicbow()
	{
		GameObject magicBowPrefab = _weaponManager.GetMagicBowPrefab();
		SaveMagicBowInPrefs();
		AddWeapon(magicBowPrefab);
	}

	private void provideChainsaw()
	{
		GameObject chainsawPrefab = _weaponManager.GetChainsawPrefab();
		SaveChainsawInPrefs();
		AddWeapon(chainsawPrefab);
	}

	private void provideFAMAS()
	{
		GameObject fAMASPrefab = _weaponManager.GetFAMASPrefab();
		SaveFAMASPrefs();
		AddWeapon(fAMASPrefab);
	}

	private void provideGlock()
	{
		GameObject glockPrefab = _weaponManager.GetGlockPrefab();
		SaveGlockInPrefs();
		AddWeapon(glockPrefab);
	}

	private void provideScythe()
	{
		GameObject scythePrefab = _weaponManager.GetScythePrefab();
		SaveScytheInPrefs();
		AddWeapon(scythePrefab);
	}

	private void provideShovel()
	{
		GameObject shovelPrefab = _weaponManager.GetShovelPrefab();
		SaveShovelPrefs();
		AddWeapon(shovelPrefab);
	}

	private void providespas()
	{
		GameObject sPASPrefab = _weaponManager.GetSPASPrefab();
		SaveSPASInPrefs();
		AddWeapon(sPASPrefab);
	}

	private void provideaxe()
	{
		GameObject axePrefab = _weaponManager.GetAxePrefab();
		SaveMGoldenAxeInPrefs();
		AddWeapon(axePrefab);
	}

	public void PurchaseSuccessful(string id)
	{
		if (id == null)
		{
			throw new ArgumentNullException("id");
		}
		if (id.Equals("bigammopack") || id.Equals("Fullhealth"))
		{
			GoogleIAB.consumeProduct(id);
		}
		if (_actionsForPurchasedItems.ContainsKey(id))
		{
			_actionsForPurchasedItems[id].Key();
		}
		else
		{
			Debug.LogWarning("Cannot find action for key \"" + id + "\".");
		}
		productPurchased = true;
		_timeWhenPurchShown = Time.realtimeSinceStartup;
	}

	private void purchaseSuccessful(GooglePurchase purchase)
	{
		if (purchase == null)
		{
			throw new ArgumentNullException("purchase");
		}
		PurchaseSuccessful(purchase.productId);
	}

	private void HandlePurchaseSuccessful(AmazonReceipt receipt)
	{
		PurchaseSuccessful(receipt.sku);
	}

	private void consumptionSucceeded(GooglePurchase purchase)
	{
	}

	private IEnumerator _ResetProductPurchased()
	{
		yield return new WaitForSeconds(1f);
		productPurchased = false;
	}

	private void _shopResume(bool disableGUI)
	{
		float num = 0f;
		float num2 = 0f;
		if (currentCategory == -1)
		{
			num = (float)resumeCategories.normal.background.width * Defs.Coef;
			num2 = num * ((float)resumeCategories.normal.background.height / (float)resumeCategories.normal.background.width);
		}
		else
		{
			num = (float)resumeStyle.normal.background.width * Defs.Coef;
			num2 = num * ((float)resumeStyle.normal.background.height / (float)resumeStyle.normal.background.width);
		}
		GUI.enabled = true && !disableGUI;
		Rect position = new Rect((float)Screen.width * 0.5f - num / 2f, (float)Screen.height * 0.86f, num, num2);
		if (!GUI.Button(position, string.Empty, (currentCategory != -1) ? resumeStyle : resumeCategories))
		{
			return;
		}
		if (currentCategory == -1)
		{
			SetInApp();
			if (inAppOpenedFromPause)
			{
				inAppOpenedFromPause = false;
			}
			else
			{
				SetPause();
			}
		}
		else
		{
			currentCategory = -1;
		}
	}

	private void slideScroll()
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			scrollEnabled = true;
			scrollStartTouch = Input.GetTouch(0).position;
		}
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && scrollEnabled)
		{
			Vector2 position = Input.GetTouch(0).position;
			scrollPosition.x += scrollStartTouch.x - position.x;
			scrollStartTouch = position;
		}
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && scrollEnabled)
		{
			scrollEnabled = false;
		}
	}

	[CompilerGenerated]
	private static void _003CStart_003Em__27()
	{
		Defs.NumberOfElixirs++;
	}

	[CompilerGenerated]
	private void _003CStart_003Em__28()
	{
		curArmor = MaxArmor;
	}

	[CompilerGenerated]
	private float _003CStart_003Em__29()
	{
		return CurHealth;
	}

	[CompilerGenerated]
	private float _003CStart_003Em__2A()
	{
		return curArmor;
	}

	[CompilerGenerated]
	private string _003CStart_003Em__2B()
	{
		return "Kills \n" + countKills + "/" + maxCountKills;
	}

	[CompilerGenerated]
	private string _003CStart_003Em__2C()
	{
		if (zombiManager != null)
		{
			float num = zombiManager.maxTimeGame - zombiManager.timeGame;
			if (num < 0f)
			{
				num = 0f;
			}
			return "Time\n" + Mathf.FloorToInt(num / 60f) + ":" + ((Mathf.FloorToInt(num - (float)(Mathf.FloorToInt(num / 60f) * 60)) >= 10) ? string.Empty : "0") + Mathf.FloorToInt(num - (float)(Mathf.FloorToInt(num / 60f) * 60));
		}
		float num2 = 300f;
		if (num2 < 0f)
		{
			num2 = 0f;
		}
		return "Time\n" + Mathf.FloorToInt(num2 / 60f) + ":" + ((Mathf.FloorToInt(num2 - (float)(Mathf.FloorToInt(num2 / 60f) * 60)) >= 10) ? string.Empty : "0") + Mathf.FloorToInt(num2 - (float)(Mathf.FloorToInt(num2 / 60f) * 60));
	}
}
