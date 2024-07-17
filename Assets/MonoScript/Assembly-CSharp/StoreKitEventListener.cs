using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Rilisoft;
using UnityEngine;

public sealed class StoreKitEventListener : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CqueryInventorySucceededEvent_003Ec__AnonStorey1E
	{
		internal string[] productIds;

		internal bool _003C_003Em__14(GoogleSkuInfo s)
		{
			return productIds.Contains(s.productId);
		}
	}

	public const string coin1 = "coin1";

	public const string coin2 = "coin2";

	public const string coin3 = "coin3.";

	public const string coin4 = "coin4";

	public const string coin5 = "coin5";

	public const string coin6 = "coin6";

	public const string coin7 = "coin7";

	public const string bigAmmoPackID = "bigammopack";

	public const string crystalswordID = "crystalsword";

	public const string fullHealthID = "Fullhealth";

	public const string minerWeaponID = "MinerWeapon";

	internal readonly ICollection<IMarketProduct> _products = new List<IMarketProduct>();

	public readonly ICollection<GoogleSkuInfo> _skinProducts = new GoogleSkuInfo[0];

	public static bool billingSupported;

	public static readonly string[] coinIds;

	private static string[] _productIds;

	private bool _hasCoins;

	public static string elixirSettName;

	public static string fullSettName;

	public static bool purchaseInProcess;

	public static bool restoreInProcess;

	public static GameObject purchaseActivityInd;

	public static string elixirID;

	public static string endmanskin;

	public static string chief;

	public static string spaceengineer;

	public static string nanosoldier;

	public static string steelman;

	public static string CaptainSkin;

	public static string HawkSkin;

	public static string GreenGuySkin;

	public static string TunderGodSkin;

	public static string GordonSkin;

	public static string animeGirl;

	public static string EMOGirl;

	public static string Nurse;

	public static string magicGirl;

	public static string braveGirl;

	public static string glamDoll;

	public static string kittyGirl;

	public static string famosBoy;

	public static string combatrifle;

	public static string goldeneagle;

	public static string magicbow;

	public static string fullVersion;

	public static string axe;

	public static string spas;

	public static string chainsaw;

	public static string famas;

	public static string glock;

	public static string scythe;

	public static string shovel;

	public static string hammer;

	public static string sword_2;

	public static string staff;

	public static string laser;

	public static string armor;

	public static string lightSword;

	public static string beretta;

	public static string mace;

	public static string minigun;

	public static string crossbow;

	public static readonly string[] skinIDs;

	public static readonly string[] idsForSingle;

	public static readonly string[] idsForMulti;

	public static readonly string[] idsForFull;

	public static readonly string[][] categoriesSingle;

	public static readonly string[][] categoriesMulti;

	public GameObject messagePrefab;

	public static string[] categoryNames;

	[CompilerGenerated]
	private static Func<GoogleSkuInfo, bool> _003C_003Ef__am_0024cache3B;

	[CompilerGenerated]
	private static Func<GoogleSkuInfo, string> _003C_003Ef__am_0024cache3C;

	[CompilerGenerated]
	private static Func<GooglePurchase, string> _003C_003Ef__am_0024cache3D;

	[CompilerGenerated]
	private static Func<GoogleSkuInfo, bool> _003C_003Ef__am_0024cache3E;

	[CompilerGenerated]
	private static Func<AmazonItem, string> _003C_003Ef__am_0024cache3F;

	[CompilerGenerated]
	private static Func<AmazonItem, bool> _003C_003Ef__am_0024cache40;

	[CompilerGenerated]
	private static Func<AmazonReceipt, string> _003C_003Ef__am_0024cache41;

	static StoreKitEventListener()
	{
		elixirSettName = Defs.NumberOfElixirsSett;
		fullSettName = "FullVersion";
		purchaseInProcess = false;
		restoreInProcess = false;
		elixirID = ((!GlobalGameController.isFullVersion) ? "elixirlite" : "elixir");
		endmanskin = ((!GlobalGameController.isFullVersion) ? "endmanskinlite" : "endmanskin");
		chief = ((!GlobalGameController.isFullVersion) ? "chiefskinlite" : "chief");
		spaceengineer = ((!GlobalGameController.isFullVersion) ? "spaceengineerskinlite" : "spaceengineer");
		nanosoldier = ((!GlobalGameController.isFullVersion) ? "nanosoldierlite" : "nanosoldier");
		steelman = ((!GlobalGameController.isFullVersion) ? "steelmanlite" : "steelman");
		CaptainSkin = "captainskin";
		HawkSkin = "hawkskin";
		GreenGuySkin = "greenguyskin";
		TunderGodSkin = "thundergodskin";
		GordonSkin = "gordonskin";
		animeGirl = "animeGirl";
		EMOGirl = "EMOGirl";
		Nurse = "Nurse";
		magicGirl = "magicGirl";
		braveGirl = "braveGirl";
		glamDoll = "glamDoll";
		kittyGirl = "kittyGirl";
		famosBoy = "famosBoy";
		combatrifle = ((!GlobalGameController.isFullVersion) ? "combatriflelite" : "combatrifle");
		goldeneagle = ((!GlobalGameController.isFullVersion) ? "goldeneaglelite" : "goldeneagle");
		magicbow = ((!GlobalGameController.isFullVersion) ? "magicbowlite" : "magicbow");
		fullVersion = "extendedversion";
		axe = "axe";
		spas = "spas";
		chainsaw = "chainsaw";
		famas = "famas";
		glock = "glock";
		scythe = "scythe";
		shovel = "shovel";
		hammer = "hammer";
		sword_2 = "sword_2";
		staff = "staff";
		laser = "laser";
		armor = "armor";
		lightSword = "lightsword";
		beretta = "beretta";
		mace = "mace";
		minigun = "minigun";
		crossbow = "crossbow";
		categoryNames = new string[4] { "Premium", "Guns", "Melee", "Gear" };
		billingSupported = false;
		coinIds = new string[7] { "coin1", "coin6", "coin7", "coin2", "coin3.", "coin4", "coin5" };
		_productIds = new string[5] { "bigammopack", "Fullhealth", "crystalsword", "MinerWeapon", elixirID };
		skinIDs = new string[18]
		{
			endmanskin, chief, spaceengineer, nanosoldier, steelman, CaptainSkin, HawkSkin, GreenGuySkin, TunderGodSkin, GordonSkin,
			animeGirl, EMOGirl, Nurse, magicGirl, braveGirl, glamDoll, kittyGirl, famosBoy
		};
		idsForSingle = new string[11]
		{
			"bigammopack", "Fullhealth", "crystalsword", "MinerWeapon", axe, spas, elixirID, glock, chainsaw, scythe,
			shovel
		};
		idsForMulti = new string[10]
		{
			idsForSingle[2],
			idsForSingle[3],
			axe,
			magicbow,
			combatrifle,
			spas,
			goldeneagle,
			idsForSingle[7],
			idsForSingle[8],
			famas
		};
		idsForFull = new string[1] { fullVersion };
		categoriesMulti = new string[4][]
		{
			new string[6] { sword_2, staff, laser, lightSword, hammer, beretta },
			new string[6]
			{
				idsForMulti[6],
				minigun,
				idsForMulti[3],
				idsForSingle[5],
				idsForSingle[7],
				idsForMulti[9]
			},
			new string[6]
			{
				idsForSingle[3],
				idsForSingle[4],
				idsForSingle[2],
				idsForSingle[8],
				mace,
				crossbow
			},
			new string[3]
			{
				idsForSingle[0],
				idsForSingle[1],
				armor
			}
		};
		categoriesSingle = new string[4][]
		{
			new string[6] { sword_2, staff, laser, lightSword, hammer, beretta },
			new string[6]
			{
				idsForMulti[6],
				minigun,
				idsForMulti[3],
				idsForSingle[5],
				idsForSingle[7],
				idsForMulti[9]
			},
			new string[6]
			{
				idsForSingle[3],
				idsForSingle[4],
				idsForSingle[2],
				idsForSingle[8],
				mace,
				crossbow
			},
			new string[4]
			{
				idsForSingle[0],
				idsForSingle[1],
				armor,
				idsForSingle[6]
			}
		};
	}

	private void Start()
	{
		GameObject gameObject = Resources.Load("ActivityIndicator") as GameObject;
		if (gameObject == null)
		{
			Debug.LogWarning("activityIndicatorPrefab == null");
		}
		else
		{
			purchaseActivityInd = UnityEngine.Object.Instantiate(gameObject) as GameObject;
		}
		string publicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA2fddncCpVwPU3m4ZzG8MfQTrxf3LBdjWwOV4LBRy2q4Kp/gPYi5QQaNJjsiQAbpIR51qSEJv9EomOi8+JZ4rWO52zOaLeumnKzpv++QVOllbGxaSwwSPDEZ0++eKmdsl5r+xzVvd20ey4n5tYotrRdYQfypZKYuHiMGvpsiIXf0rwv3yMNhVU7MDtbDgAs8zriBvPqCtkrRLnZdG+2dQEZ+hDPns0gO+N8y1V7odOHg4bDUceaK8al9DHcVKNItCMnOFyLHx++vKzHSLiXw2ojSUR1cfSbTkyyOTw9r9emHxxuGmc2/qWp7n/Qin1ksuAhyYFGOC9RClxxu1ygXKTQIDAQAB";
		GoogleIAB.init(publicKey);
		AmazonIAP.initiateItemDataRequest(coinIds);
	}

	private void OnEnable()
	{
		AmazonIAPManager.onSdkAvailableEvent += HandleAmazonSdkAvailableEvent;
		AmazonIAPManager.onGetUserIdResponseEvent += HandleGetUserIdResponseEvent;
		AmazonIAPManager.itemDataRequestFinishedEvent += HandleItemDataRequestFinishedEvent;
		AmazonIAPManager.itemDataRequestFailedEvent += HandleItemDataRequestFailedEvent;
		AmazonIAPManager.purchaseSuccessfulEvent += HandlePurchaseSuccessfulEvent;
		AmazonIAPManager.purchaseFailedEvent += purchaseFailedEvent;
		AmazonIAPManager.purchaseUpdatesRequestSuccessfulEvent += HandlePurchaseUpdatesRequestSuccessfulEvent;
		AmazonIAPManager.purchaseUpdatesRequestFailedEvent += HandlePurchaseUpdatesRequestFailedEvent;
		GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent += billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent += purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
	}

	private void OnDisable()
	{
		AmazonIAPManager.onSdkAvailableEvent -= HandleAmazonSdkAvailableEvent;
		AmazonIAPManager.onGetUserIdResponseEvent -= HandleGetUserIdResponseEvent;
		AmazonIAPManager.itemDataRequestFinishedEvent -= HandleItemDataRequestFinishedEvent;
		AmazonIAPManager.itemDataRequestFailedEvent -= HandleItemDataRequestFailedEvent;
		AmazonIAPManager.purchaseSuccessfulEvent -= HandlePurchaseSuccessfulEvent;
		AmazonIAPManager.purchaseFailedEvent -= purchaseFailedEvent;
		AmazonIAPManager.purchaseUpdatesRequestSuccessfulEvent -= HandlePurchaseUpdatesRequestSuccessfulEvent;
		AmazonIAPManager.purchaseUpdatesRequestFailedEvent -= HandlePurchaseUpdatesRequestFailedEvent;
		GoogleIABManager.billingSupportedEvent -= billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent -= billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent -= purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent -= purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
	}

	private void billingSupportedEvent()
	{
		billingSupported = true;
		Debug.Log("billingSupportedEvent");
		string[] array = _productIds.Concat(coinIds).ToArray();
		GoogleIAB.queryInventory(array);
		AmazonIAP.initiateItemDataRequest(array);
	}

	private void billingNotSupportedEvent(string error)
	{
		billingSupported = false;
		Debug.LogWarning("billingNotSupportedEvent: " + error);
	}

	private void HandleAmazonSdkAvailableEvent(bool available)
	{
		billingSupported = available;
		Debug.Log("Amazon: IAP SDK available: " + available);
		if (available)
		{
			AmazonIAPManager.onSdkAvailableEvent -= HandleAmazonSdkAvailableEvent;
			string[] items = _productIds.Concat(coinIds).ToArray();
			AmazonIAP.initiateItemDataRequest(items);
		}
	}

	private void HandleGetUserIdResponseEvent(string id)
	{
		Debug.Log("Amazon: User: " + id);
	}

	private void queryInventorySucceededEvent(List<GooglePurchase> purchases, List<GoogleSkuInfo> skus)
	{
		_003CqueryInventorySucceededEvent_003Ec__AnonStorey1E _003CqueryInventorySucceededEvent_003Ec__AnonStorey1E = new _003CqueryInventorySucceededEvent_003Ec__AnonStorey1E();
		if (_003C_003Ef__am_0024cache3B == null)
		{
			_003C_003Ef__am_0024cache3B = _003CqueryInventorySucceededEvent_003Em__10;
		}
		if (skus.Any(_003C_003Ef__am_0024cache3B))
		{
			return;
		}
		if (_003C_003Ef__am_0024cache3C == null)
		{
			_003C_003Ef__am_0024cache3C = _003CqueryInventorySucceededEvent_003Em__11;
		}
		_003CqueryInventorySucceededEvent_003Ec__AnonStorey1E.productIds = skus.Select(_003C_003Ef__am_0024cache3C).ToArray();
		string arg = string.Join(", ", _003CqueryInventorySucceededEvent_003Ec__AnonStorey1E.productIds);
		if (_003C_003Ef__am_0024cache3D == null)
		{
			_003C_003Ef__am_0024cache3D = _003CqueryInventorySucceededEvent_003Em__12;
		}
		string[] value = purchases.Select(_003C_003Ef__am_0024cache3D).ToArray();
		string arg2 = string.Join(", ", value);
		string message = string.Format("queryInventorySucceededEvent;    Purchases: [{0}], Skus: [{1}]", arg2, arg);
		Debug.Log(message);
		if (_003C_003Ef__am_0024cache3E == null)
		{
			_003C_003Ef__am_0024cache3E = _003CqueryInventorySucceededEvent_003Em__13;
		}
		IEnumerable<GoogleSkuInfo> source = skus.Where(_003C_003Ef__am_0024cache3E);
		_hasCoins = source.Any();
		IEnumerable<GoogleMarketProduct> enumerable = skus.Where(_003CqueryInventorySucceededEvent_003Ec__AnonStorey1E._003C_003Em__14).Select(MarketProductFactory.CreateGoogleMarketProduct);
		foreach (GoogleMarketProduct item in enumerable)
		{
			if (!_products.Contains(item))
			{
				_products.Add(item);
			}
		}
		foreach (GooglePurchase purchase in purchases)
		{
			if (purchase.productId == "MinerWeapon" || purchase.productId == "MinerWeapon".ToLower())
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag("WeaponManager");
				if ((bool)gameObject)
				{
					gameObject.SendMessage("AddMinerWeaponToInventoryAndSaveInApp");
				}
			}
			else if (purchase.productId == "crystalsword")
			{
				GameObject gameObject2 = GameObject.FindGameObjectWithTag("WeaponManager");
				if ((bool)gameObject2)
				{
					gameObject2.SendMessage("AddSwordToInventoryAndSaveInApp");
				}
			}
			else
			{
				GoogleIAB.consumeProduct(purchase.productId);
			}
		}
		purchaseInProcess = false;
		restoreInProcess = false;
	}

	private void queryInventoryFailedEvent(string error)
	{
		Debug.LogWarning("Google: queryInventoryFailedEvent: " + error);
	}

	private void HandleItemDataRequestFinishedEvent(List<string> unavailableSkus, List<AmazonItem> availableItems)
	{
		try
		{
			if (_003C_003Ef__am_0024cache3F == null)
			{
				_003C_003Ef__am_0024cache3F = _003CHandleItemDataRequestFinishedEvent_003Em__15;
			}
			string[] value = availableItems.Select(_003C_003Ef__am_0024cache3F).ToArray();
			string arg = string.Join(", ", value);
			string arg2 = string.Join(", ", unavailableSkus.ToArray());
			string message = string.Format("Item data request finished;    Unavailable skus: [{0}], Available skus: [{1}]", arg2, arg);
			Debug.Log(message);
			if (_003C_003Ef__am_0024cache40 == null)
			{
				_003C_003Ef__am_0024cache40 = _003CHandleItemDataRequestFinishedEvent_003Em__16;
			}
			IEnumerable<AmazonItem> source = availableItems.Where(_003C_003Ef__am_0024cache40);
			_hasCoins = source.Any();
			IEnumerable<AmazonMarketProduct> enumerable = availableItems.Select(MarketProductFactory.CreateAmazonMarketProduct);
			foreach (AmazonMarketProduct item in enumerable)
			{
				if (!_products.Contains(item))
				{
					_products.Add(item);
				}
			}
		}
		finally
		{
			purchaseInProcess = false;
			restoreInProcess = false;
		}
	}

	private void HandleItemDataRequestFailedEvent()
	{
		Debug.LogWarning("Amamzon: Item data request failed.");
	}

	private void purchaseCompleteAwaitingVerificationEvent(string purchaseData, string signature)
	{
		Debug.Log("purchaseCompleteAwaitingVerificationEvent. purchaseData: " + purchaseData + ", signature: " + signature);
	}

	private void purchaseSucceededEvent(GooglePurchase purchase)
	{
		Debug.Log("Google: purchaseSucceededEvent: " + purchase);
		if (purchase.productId.Equals(elixirID))
		{
			PlayerPrefs.SetInt(elixirSettName, PlayerPrefs.GetInt(elixirSettName, 1) + 1);
			PlayerPrefs.Save();
		}
		else
		{
			int num = Array.IndexOf(coinIds, purchase.productId);
			if (num >= coinIds.GetLowerBound(0))
			{
				string message = string.Format("Process purchase {0}, coinInappsQuantity[{1}]", purchase.productId, num);
				Debug.Log(message);
				int val = Storager.getInt(Defs.Coins, false) + VirtualCurrencyHelper.coinInappsQuantity[num];
				Storager.setInt(Defs.Coins, val, false);
			}
		}
		string[] source = new string[3]
		{
			"MinerWeapon",
			"MinerWeapon".ToLower(),
			"crystalsword"
		};
		if (!source.Contains(purchase.productId))
		{
			GoogleIAB.consumeProduct(purchase.productId);
		}
		purchaseInProcess = false;
		restoreInProcess = false;
	}

	private void purchaseFailedEvent(string error)
	{
		purchaseInProcess = false;
		Debug.LogWarning("purchaseFailedEvent: " + error);
	}

	private void HandlePurchaseSuccessfulEvent(AmazonReceipt receipt)
	{
		Debug.Log("Amazon: purchaseSuccessfulEvent: " + receipt);
		try
		{
			int num = Array.IndexOf(coinIds, receipt.sku);
			if (num >= coinIds.GetLowerBound(0))
			{
				string message = string.Format("Process purchase {0}, coinInappsQuantity[{1}]", receipt.sku, num);
				Debug.Log(message);
				int val = Storager.getInt(Defs.Coins, false) + VirtualCurrencyHelper.coinInappsQuantity[num];
				Storager.setInt(Defs.Coins, val, false);
			}
		}
		finally
		{
			purchaseInProcess = false;
			restoreInProcess = false;
		}
	}

	private void consumePurchaseSucceededEvent(GooglePurchase purchase)
	{
		Debug.Log("consumePurchaseSucceededEvent: " + purchase);
	}

	private void consumePurchaseFailedEvent(string error)
	{
		Debug.LogWarning("consumePurchaseFailedEvent: " + error);
	}

	private void HandlePurchaseUpdatesRequestSuccessfulEvent(List<string> revokedSkus, List<AmazonReceipt> receipts)
	{
		string arg = string.Join(", ", revokedSkus.ToArray());
		if (_003C_003Ef__am_0024cache41 == null)
		{
			_003C_003Ef__am_0024cache41 = _003CHandlePurchaseUpdatesRequestSuccessfulEvent_003Em__17;
		}
		string message = string.Format("Amazon: Purchase updates request successful.\nRevoked skus: [{0}]\nReceipts: [{1}]", arg, string.Join(", ", receipts.Select(_003C_003Ef__am_0024cache41).ToArray()));
		Debug.Log(message);
	}

	private void HandlePurchaseUpdatesRequestFailedEvent()
	{
		Debug.LogWarning("Amazon: Purchase updates request failed.");
	}

	public bool HasCoins()
	{
		return _hasCoins;
	}

	public void ProvideContent()
	{
	}

	[CompilerGenerated]
	private static bool _003CqueryInventorySucceededEvent_003Em__10(GoogleSkuInfo s)
	{
		return s.productId == "skinsmaker";
	}

	[CompilerGenerated]
	private static string _003CqueryInventorySucceededEvent_003Em__11(GoogleSkuInfo sku)
	{
		return sku.productId;
	}

	[CompilerGenerated]
	private static string _003CqueryInventorySucceededEvent_003Em__12(GooglePurchase p)
	{
		return p.productId;
	}

	[CompilerGenerated]
	private static bool _003CqueryInventorySucceededEvent_003Em__13(GoogleSkuInfo s)
	{
		return coinIds.Contains(s.productId);
	}

	[CompilerGenerated]
	private static string _003CHandleItemDataRequestFinishedEvent_003Em__15(AmazonItem item)
	{
		return item.sku;
	}

	[CompilerGenerated]
	private static bool _003CHandleItemDataRequestFinishedEvent_003Em__16(AmazonItem item)
	{
		return coinIds.Contains(item.sku);
	}

	[CompilerGenerated]
	private static string _003CHandlePurchaseUpdatesRequestSuccessfulEvent_003Em__17(AmazonReceipt r)
	{
		return r.sku;
	}
}
