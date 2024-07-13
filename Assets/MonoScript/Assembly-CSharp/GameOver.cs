using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Rilisoft;
using UnityEngine;

public sealed class GameOver : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003COnGUI_003Ec__AnonStorey1A
	{
		private sealed class _003COnGUI_003Ec__AnonStorey19
		{
			internal int newCoins;

			internal Action<string> showShop;

			internal _003COnGUI_003Ec__AnonStorey1A _003C_003Ef__ref_002426;

			internal void _003C_003Em__1()
			{
				Storager.setInt(Defs.Coins, newCoins, false);
				_003C_003Ef__ref_002426._003C_003Ef__this._Buy();
			}

			internal void _003C_003Em__2(string pressedbutton)
			{
				if (!pressedbutton.Equals("Cancel"))
				{
					coinsShop.thisScript.notEnoughCoins = true;
					coinsShop.thisScript.onReturnAction = _003C_003Ef__ref_002426.act;
					coinsShop.showCoinsShop();
				}
			}

			internal void _003C_003Em__3()
			{
				showShop("Yes!");
			}
		}

		internal Action act;

		internal GameOver _003C_003Ef__this;

		internal void _003C_003Em__0()
		{
			_003COnGUI_003Ec__AnonStorey19 _003COnGUI_003Ec__AnonStorey = new _003COnGUI_003Ec__AnonStorey19();
			_003COnGUI_003Ec__AnonStorey._003C_003Ef__ref_002426 = this;
			coinsShop.thisScript.notEnoughCoins = false;
			coinsShop.thisScript.onReturnAction = null;
			int num = ((!VirtualCurrencyHelper.prices.ContainsKey(StoreKitEventListener.elixirID)) ? 10 : VirtualCurrencyHelper.prices[StoreKitEventListener.elixirID]);
			int @int = Storager.getInt(Defs.Coins, false);
			_003COnGUI_003Ec__AnonStorey.newCoins = @int - num;
			Action action = _003COnGUI_003Ec__AnonStorey._003C_003Em__1;
			_003COnGUI_003Ec__AnonStorey.showShop = _003COnGUI_003Ec__AnonStorey._003C_003Em__2;
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
			component.yesPressed = _003COnGUI_003Ec__AnonStorey._003C_003Em__3;
		}
	}

	private GameObject _purchaseActivityIndicator;

	public Texture elixir;

	public Texture res_tag;

	public Texture noElixir;

	public Texture noElixirNOinet;

	public GUIStyle resurrect;

	public GUIStyle retry;

	public GUIStyle quit;

	public GUIStyle decline;

	public GUIStyle buy;

	public GUIStyle ok;

	private bool haveNoElixirSh;

	private float coef = (float)Screen.height / 768f;

	private GameObject _inAppGameObject;

	public StoreKitEventListener _listener;

	internal ICollection<IMarketProduct> _products = new IMarketProduct[0];

	public bool activeInicator;

	private void Start()
	{
		_inAppGameObject = GameObject.FindGameObjectWithTag("InAppGameObject");
		_listener = _inAppGameObject.GetComponent<StoreKitEventListener>();
		if (_listener == null)
		{
			Debug.LogWarning("_listener is null.");
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
		Invoke("setAppropriateProducts", 0.01f);
		coinsPlashka.thisScript.enabled = true;
	}

	[Beebyte.Obfuscator.SkipRename]
	private void setAppropriateProducts()
	{
		_products = _listener._products;
	}

	private void hideActiveInd(string error)
	{
		activeInicator = false;
		Debug.Log("activeInicator=false; " + error);
	}

	private void OnEnable()
	{
		GoogleIABManager.purchaseSucceededEvent += ElixirBuyAndr;
	}

	private void OnDisable()
	{
		GoogleIABManager.purchaseSucceededEvent -= ElixirBuyAndr;
	}

	private void OnDestroy()
	{
		coinsPlashka.thisScript.enabled = false;
	}

	public void ElixirBuy()
	{
		activeInicator = false;
		_purchaseActivityIndicator.SetActive(activeInicator);
		_Resurrect();
	}

	private void ElixirBuyAndr(GooglePurchase purchase)
	{
		if (purchase == null)
		{
			Debug.LogWarning("purchase is null.");
		}
		else if (purchase.productId == null)
		{
			Debug.LogWarning("purchase.productId is null.");
		}
		else if (purchase.productId.Equals(StoreKitEventListener.elixirID))
		{
			ElixirBuy();
		}
	}

	private void _Resurrect()
	{
		Defs.NumberOfElixirs--;
		WeaponManager component = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
		foreach (Weapon playerWeapon in component.playerWeapons)
		{
			WeaponSounds component2 = playerWeapon.weaponPrefab.GetComponent<WeaponSounds>();
			if (playerWeapon.currentAmmoInClip + playerWeapon.currentAmmoInBackpack < component2.InitialAmmo + component2.ammoInClip)
			{
				playerWeapon.currentAmmoInClip = component2.ammoInClip;
				playerWeapon.currentAmmoInBackpack = component2.InitialAmmo;
			}
		}
		if (GlobalGameController.currentLevel == GlobalGameController.levelMapping[0])
		{
			GlobalGameController.currentLevel = 101;
		}
		else
		{
			GlobalGameController.decrementLevel();
			GlobalGameController.numOfCompletedLevels--;
			GlobalGameController.totalNumOfCompletedLevels--;
		}
		prefs.SetFloat(Defs.CurrentHealthSett, Player_move_c.MaxPlayerHealth);
		prefs.SetFloat(Defs.CurrentArmorSett, 0f);
		Application.LoadLevel("Loading");
	}

	private void _Retry()
	{
	}

	private void _Buy()
	{
		Defs.NumberOfElixirs++;
		ElixirBuy();
	}

	private void OnGUI()
	{
		int depth = GUI.depth;
		if (_purchaseActivityIndicator == null)
		{
			Debug.LogWarning("_purchaseActivityIndicator is null.");
		}
		else
		{
			_purchaseActivityIndicator.SetActive(activeInicator);
		}
		float num = (float)Screen.width * 0.31f;
		float num2 = num * ((float)resurrect.normal.background.height / (float)resurrect.normal.background.width);
		float num3 = num2 * 0.2f;
		Rect position = new Rect((float)(Screen.width / 2) - num / 2f, (float)Screen.height - num2 * 3f - num3 * 3f, num, num2);
		GUI.enabled = !haveNoElixirSh && !activeInicator;
		if (GUI.Button(position, string.Empty, resurrect))
		{
			if (Defs.NumberOfElixirs > 0)
			{
				_Resurrect();
			}
			else
			{
				haveNoElixirSh = true;
			}
		}
		GUI.enabled = !haveNoElixirSh && !activeInicator;
		if (GUI.Button(new Rect((float)(Screen.width / 2) - num / 2f, (float)Screen.height - num2 * 2f - num3 * 2f, num, num2), string.Empty, retry))
		{
			GUI.enabled = true;
			GlobalGameController.ResetParameters();
			GlobalGameController.Score = 0;
			GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().Reset();
			prefs.SetFloat(Defs.CurrentHealthSett, Player_move_c.MaxPlayerHealth);
			prefs.SetFloat(Defs.CurrentArmorSett, 0f);
			Application.LoadLevel("LoadingNoWait");
		}
		float num4 = num * ((float)quit.normal.background.width / (float)resurrect.normal.background.width);
		float height = num4 * ((float)quit.normal.background.height / (float)quit.normal.background.width);
		if (GUI.Button(new Rect((float)Screen.width * 0.5f - num4 / 2f, (float)Screen.height - num2 - num3 * 1f, num4, height), string.Empty, quit))
		{
			GUI.enabled = true;
			Application.LoadLevel(Defs.CurrentMainMenuScene);
		}
		float width = (float)(elixir.width * Screen.height) / 768f;
		float num5 = (float)(elixir.height * Screen.height) / 768f;
		GUI.DrawTexture(new Rect(position.x + position.width + num3, position.y + position.height / 2f - num5 / 2f, width, num5), elixir);
		GUI.DrawTexture(new Rect(position.x - (float)(res_tag.width * Screen.height) / 768f - num3, position.y + position.height / 2f - (float)(res_tag.height * Screen.height) / 768f / 2f, (float)(res_tag.width * Screen.height) / 768f, (float)(res_tag.height * Screen.height) / 768f), res_tag);
		if (haveNoElixirSh)
		{
			GUI.enabled = !activeInicator;
			float num6 = (float)Screen.width * 0.45f * 1.5f;
			Texture texture = noElixir;
			float num7 = num6 * ((float)texture.height / (float)texture.width);
			float num8 = num6 * 0.27f;
			float num9 = num8 * ((float)buy.normal.background.height / (float)buy.normal.background.width);
			float num10 = num8 / 10f;
			float num11 = num9 * 3f;
			float num12 = Defs.Coef;
			Rect position2 = new Rect(0.5f * ((float)Screen.width - num12 * (float)texture.width), 0.5f * ((float)Screen.height - num12 * (float)texture.height), num12 * (float)texture.width, num12 * (float)texture.height);
			GUI.DrawTexture(position2, texture, ScaleMode.StretchToFill);
			GUI.BeginGroup(position2);
			try
			{
				Rect position3 = new Rect(0.5f * num12 * (float)(texture.width - buy.normal.background.width), (float)(texture.height - buy.normal.background.height - 96) * num12, (float)buy.normal.background.width * num12, (float)buy.normal.background.height * num12);
				if (GUI.Button(position3, string.Empty, buy))
				{
					_003COnGUI_003Ec__AnonStorey1A _003COnGUI_003Ec__AnonStorey1A = new _003COnGUI_003Ec__AnonStorey1A();
					_003COnGUI_003Ec__AnonStorey1A._003C_003Ef__this = this;
					_003COnGUI_003Ec__AnonStorey1A.act = null;
					_003COnGUI_003Ec__AnonStorey1A.act = _003COnGUI_003Ec__AnonStorey1A._003C_003Em__0;
					_003COnGUI_003Ec__AnonStorey1A.act();
				}
				if (haveNoElixirSh && _products.Count > 0)
				{
					Rect position4 = new Rect(num12 * (float)(texture.width - decline.normal.background.width), 0f, (float)decline.normal.background.width * num12, (float)decline.normal.background.height * num12);
					if (GUI.Button(position4, string.Empty, decline))
					{
						haveNoElixirSh = false;
					}
				}
			}
			finally
			{
				GUI.EndGroup();
			}
			GUI.enabled = false;
		}
		GUI.depth = depth;
	}
}
