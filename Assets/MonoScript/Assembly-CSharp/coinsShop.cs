using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public sealed class coinsShop : MonoBehaviour
{
	public static coinsShop thisScript;

	public static bool showPlashkuPriExit;

	public Action onReturnAction;

	private float kfSize = (float)Screen.height / 768f;

	private float reSizeWidth;

	public GameObject _purchaseActivityIndicator;

	private bool productPurchased;

	private float _timeWhenPurchShown;

	public GUIStyle labelStyle;

	public GUIStyle notEnoughCoinsStyle;

	public bool notEnoughCoins;

	public Texture txFon;

	public Texture txTitle;

	public GUIStyle stBut1;

	public GUIStyle stBut2;

	public GUIStyle stBut3;

	public GUIStyle stBut4;

	public GUIStyle stBut5;

	public GUIStyle stBut6;

	public GUIStyle stBut7;

	public GUIStyle stButBack;

	public GUIStyle noInternetStyle;

	private Rect[] rects = new Rect[7];

	private GUIStyle[] styles = new GUIStyle[7];

	private bool coinsBought;

	private Rect rectFon;

	private Rect rectBut1;

	private Rect rectBut2;

	private Rect rectBut3;

	private Rect rectBut4;

	private Rect rectBut5;

	private Rect rectButBack;

	private Rect rectTitle;

	private bool productsReceived;

	[CompilerGenerated]
	private static Func<GoogleSkuInfo, bool> _003C_003Ef__am_0024cache22;

	[CompilerGenerated]
	private static Func<GoogleSkuInfo, string> _003C_003Ef__am_0024cache23;

	[CompilerGenerated]
	private static Func<AmazonItem, string> _003C_003Ef__am_0024cache24;

	private void HandleQueryInventorySucceededEvent(List<GooglePurchase> purchases, List<GoogleSkuInfo> skus)
	{
		if (_003C_003Ef__am_0024cache22 == null)
		{
			_003C_003Ef__am_0024cache22 = _003CHandleQueryInventorySucceededEvent_003Em__18;
		}
		if (!skus.Any(_003C_003Ef__am_0024cache22))
		{
			if (_003C_003Ef__am_0024cache23 == null)
			{
				_003C_003Ef__am_0024cache23 = _003CHandleQueryInventorySucceededEvent_003Em__19;
			}
			string[] value = skus.Select(_003C_003Ef__am_0024cache23).ToArray();
			string arg = string.Join(", ", value);
			string message = string.Format("Google: Query inventory succeeded;\tPurchases count: {0}, Skus: [{1}]", purchases.Count, arg);
			Debug.Log(message);
			productsReceived = true;
		}
	}

	private void HandleItemDataRequestFinishedEvent(List<string> unavailableSkus, List<AmazonItem> availableItems)
	{
		string arg = string.Join(", ", unavailableSkus.ToArray());
		if (_003C_003Ef__am_0024cache24 == null)
		{
			_003C_003Ef__am_0024cache24 = _003CHandleItemDataRequestFinishedEvent_003Em__1A;
		}
		string message = string.Format("Amazon: Item data request finished;\n\tUnavailable skus: [{0}]\n\tAvailableSkus: [{1}]", arg, string.Join(", ", availableItems.Select(_003C_003Ef__am_0024cache24).ToArray()));
		Debug.Log(message);
		productsReceived = true;
	}

	private void OnEnable()
	{
		GoogleIABManager.queryInventorySucceededEvent += HandleQueryInventorySucceededEvent;
		GoogleIABManager.purchaseSucceededEvent += HandlePurchaseSucceededEvent;
		AmazonIAPManager.itemDataRequestFinishedEvent += HandleItemDataRequestFinishedEvent;
		AmazonIAPManager.purchaseSuccessfulEvent += HandlePurchaseSuccessfulEvent;
		GameObject gameObject = GameObject.FindGameObjectWithTag("InAppGameObject");
		StoreKitEventListener component = gameObject.GetComponent<StoreKitEventListener>();
		if (component.HasCoins())
		{
			productsReceived = true;
			Debug.Log(string.Format("productsReceived == {0}", productsReceived));
		}
		_purchaseActivityIndicator = StoreKitEventListener.purchaseActivityInd;
		if (_purchaseActivityIndicator == null)
		{
			Debug.LogWarning("_purchaseActivityIndicator == null");
		}
		else
		{
			_purchaseActivityIndicator.SetActive(false);
		}
		coinsBought = false;
	}

	private void OnDisable()
	{
		Debug.Log("Removed subscription to queryInventorySucceededEvent.");
		GoogleIABManager.queryInventorySucceededEvent -= HandleQueryInventorySucceededEvent;
		GoogleIABManager.purchaseSucceededEvent -= HandlePurchaseSucceededEvent;
		AmazonIAPManager.itemDataRequestFinishedEvent -= HandleItemDataRequestFinishedEvent;
		AmazonIAPManager.purchaseSuccessfulEvent -= HandlePurchaseSuccessfulEvent;
		if (_purchaseActivityIndicator != null)
		{
			_purchaseActivityIndicator.SetActive(false);
		}
		coinsBought = false;
	}

	private void HandlePurchaseSuccessfullCore()
	{
		if (coinsBought)
		{
			productPurchased = true;
			_timeWhenPurchShown = Time.realtimeSinceStartup;
		}
	}

	private void HandlePurchaseSucceededEvent(GooglePurchase purchase)
	{
		Debug.Log("Google: Purchase succeeded.");
		HandlePurchaseSuccessfullCore();
	}

	private void HandlePurchaseSuccessfulEvent(AmazonReceipt receipt)
	{
		Debug.Log("Amazon: Purchase succeessful.");
		HandlePurchaseSuccessfullCore();
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		thisScript = base.gameObject.GetComponent<coinsShop>();
		hideCoinsShop();
		rectFon = new Rect((float)Screen.width * 0.5f - 683f * kfSize, 0f, 1366f * kfSize, 768f * kfSize);
		float num = (float)Screen.width / 45f;
		float num2 = ((float)Screen.width - ((float)StoreKitEventListener.coinIds.Length + 1f) * num) / (float)StoreKitEventListener.coinIds.Length;
		for (int i = 0; i < StoreKitEventListener.coinIds.Length; i++)
		{
			rects[i] = new Rect((float)(i + 1) * num + (float)i * num2, 288f * kfSize, num2, num2 * 2.1073298f);
		}
		rectButBack = new Rect((float)Screen.width * 0.5f - (float)stButBack.normal.background.width * kfSize * 0.5f, (float)Screen.height - 108f * kfSize, (float)stButBack.normal.background.width * kfSize, (float)stButBack.normal.background.height * kfSize);
		rectTitle = new Rect((float)Screen.width * 0.5f - (float)txTitle.width * kfSize * 0.5f, 137f * kfSize, (float)txTitle.width * kfSize, (float)txTitle.height * kfSize);
		styles[0] = stBut1;
		styles[1] = stBut6;
		styles[2] = stBut7;
		styles[3] = stBut2;
		styles[4] = stBut3;
		styles[5] = stBut4;
		styles[6] = stBut5;
	}

	public static void showCoinsShop()
	{
		thisScript.enabled = true;
		coinsPlashka.hideButtonCoins = true;
		coinsPlashka.showPlashka();
	}

	public static void hideCoinsShop()
	{
		if (thisScript != null)
		{
			thisScript.enabled = false;
		}
	}

	private void OnGUI()
	{
		GUI.depth = -2;
		GUI.enabled = !StoreKitEventListener.purchaseInProcess;
		if (notEnoughCoins)
		{
			float num = 2f * (float)Screen.width / 5f;
			float num2 = 2f * (float)Screen.height / 10f;
			Rect position = new Rect((float)Screen.width / 2f - num / 2f, coinsPlashka.thisScript.rectLabelCoins.y + coinsPlashka.thisScript.rectLabelCoins.height / 2f - num2 / 2f, num, num2);
			notEnoughCoinsStyle.fontSize = Mathf.RoundToInt(30f * Defs.Coef);
			GUI.Box(position, "Not enough coins...", notEnoughCoinsStyle);
		}
		if (Time.realtimeSinceStartup - _timeWhenPurchShown >= GUIHelper.Int)
		{
			productPurchased = false;
		}
		if (productPurchased)
		{
			labelStyle.fontSize = Player_move_c.FontSizeForMessages;
			GUI.Label(Player_move_c.SuccessMessageRect(), "Purchase was successful", labelStyle);
		}
		_purchaseActivityIndicator.SetActive(StoreKitEventListener.purchaseInProcess);
		GUI.DrawTexture(rectFon, txFon);
		GUI.DrawTexture(rectTitle, txTitle);
		guiButtonCoins();
		if (GUI.Button(rectButBack, string.Empty, stButBack))
		{
			hideCoinsShop();
			if (showPlashkuPriExit)
			{
				coinsPlashka.hidePlashka();
			}
			coinsPlashka.hideButtonCoins = false;
			if (onReturnAction != null && coinsBought)
			{
				coinsBought = false;
				onReturnAction();
			}
			else
			{
				onReturnAction = null;
			}
		}
		GUI.enabled = true;
	}

	private void guiButtonCoins()
	{
		if (!productsReceived)
		{
			float num = (float)Screen.width / 2f;
			noInternetStyle.fontSize = Mathf.RoundToInt(30f * (float)Screen.height / 768f);
			string text = ((Application.platform == RuntimePlatform.IPhonePlayer) ? "No connection to App Store..." : ((Application.platform != RuntimePlatform.Android) ? "No connection to store..." : "No connection to Market..."));
			GUI.Box(new Rect((float)Screen.width / 2f - num / 2f, (float)Screen.height / 4f, num, (float)Screen.height / 2f), text, noInternetStyle);
			return;
		}
		for (int i = 0; i < StoreKitEventListener.coinIds.Length; i++)
		{
			if (GUI.Button(rects[i], string.Empty, styles[i]))
			{
				coinsBought = true;
				StoreKitEventListener.purchaseInProcess = true;
				string sku = StoreKitEventListener.coinIds[i];
				GoogleIAB.purchaseProduct(sku);
				AmazonIAP.initiatePurchaseRequest(sku);
			}
		}
	}

	[CompilerGenerated]
	private static bool _003CHandleQueryInventorySucceededEvent_003Em__18(GoogleSkuInfo s)
	{
		return s.productId == "skinsmaker";
	}

	[CompilerGenerated]
	private static string _003CHandleQueryInventorySucceededEvent_003Em__19(GoogleSkuInfo sku)
	{
		return sku.productId;
	}

	[CompilerGenerated]
	private static string _003CHandleItemDataRequestFinishedEvent_003Em__1A(AmazonItem item)
	{
		return item.sku;
	}
}
