using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

public class NewArmory : MonoBehaviour
{
	private GameObject gunButtonTemplate;

	public UIGrid weaponList;

	public GameObject scrollThing;

	private int currentCategoryIndex;

	public List<GameObject> primaries = new List<GameObject>();

	public List<GameObject> backup = new List<GameObject>();

	public List<GameObject> melee = new List<GameObject>();

	public List<GameObject> special = new List<GameObject>();

	public List<GameObject> heavy = new List<GameObject>();

	public List<ButtonHandler> categoryButtons;

	public ButtonHandler equipButton;

	public string notApplicableText;

	public List<GameObject> getCategoryWeaponList(int index)
	{
		switch (index)
		{
			case 0:
			return primaries;
			case 1:
			return backup;
			case 2:
			return melee;
			case 3:
			return special;
			case 4:
			return heavy;
		}
		return primaries;
	}

	void Start()
	{
		if (!Debug.isDebugBuild)
		{
			Destroy(DeveloperButton);
		}
		gunButtonTemplate = Resources.Load<GameObject>("WeaponButtonTemplate");
		foreach (UnityEngine.Object weapon in WeaponManager.WeaponPrefabs)
		{
			GameObject weaponObject = weapon as GameObject;
			CreateButton(weaponObject.GetComponent<WeaponSounds>());
		}
		ChangeCategory(0, true);
		equipButton.Clicked += (object sender2, EventArgs args2) =>
		{
			BuyCurrentWeapon();
		};
		categoryButtons[0].Clicked += (object sender2, EventArgs args2) =>
		{
			GetComponent<AudioSource>().PlayOneShot(normalPress);
			ChangeCategory(0);
		};
		categoryButtons[1].Clicked += (object sender2, EventArgs args2) =>
		{
			GetComponent<AudioSource>().PlayOneShot(normalPress);
			ChangeCategory(1);
		};
		categoryButtons[2].Clicked += (object sender2, EventArgs args2) =>
		{
			GetComponent<AudioSource>().PlayOneShot(normalPress);
			ChangeCategory(2);
		};
		categoryButtons[3].Clicked += (object sender2, EventArgs args2) =>
		{
			GetComponent<AudioSource>().PlayOneShot(normalPress);
			ChangeCategory(3);
		};
		categoryButtons[4].Clicked += (object sender2, EventArgs args2) =>
		{
			GetComponent<AudioSource>().PlayOneShot(normalPress);
			ChangeCategory(4);
		};
	}

	public void Exit()
	{
		LoadConnectScene.loading = null;
		LoadConnectScene.sceneToLoad = Defs.CurrentMainMenuScene;
		Application.LoadLevel("PromScene");
	}
	
	void Update()
	{
		//another placeholder, please help
		scrollThing.transform.localPosition = new Vector3(scrollThing.transform.localPosition.x, scrollThing.transform.localPosition.y + mouseScrollThing, scrollThing.transform.localPosition.z);
	}

	public float GetGridSize
	{
		get
		{
			int count = 0;
			foreach (Transform trf in weaponList.gameObject.GetComponentsInChildren<Transform>())
			{
				if (trf.name.StartsWith("WeaponButton"))
				{
					count++;
				}
			}
			int itemsToCount = count / 5;
			if (itemsToCount < 5)
			{
				return -1680f;
			}
			float minusCount = weaponList.cellHeight * itemsToCount;
			float final = -1680f + minusCount - 35f;
			return final * 2;
		}
	}

	public void ChangeCategory(int categoryIndex, bool first = false)
	{
		if (categoryIndex == currentCategoryIndex && !first)
		{
			return;
		}
		foreach (GameObject weaponButton in getCategoryWeaponList(categoryIndex))
		{
			weaponButton.SetActive(true);
			weaponButton.transform.parent = weaponList.transform;
			weaponButton.transform.localScale = Vector3.one;
			weaponButton.transform.localPosition = new Vector3(weaponButton.transform.localPosition.x, weaponButton.transform.localPosition.y, 0f);
		}
		if (!first)
		{
			foreach (GameObject weaponButton in getCategoryWeaponList(currentCategoryIndex))
			{
				weaponButton.SetActive(false);
				weaponButton.transform.parent = null;
				weaponButton.transform.localScale = Vector3.one;
				weaponButton.transform.localPosition = new Vector3(weaponButton.transform.localPosition.x, weaponButton.transform.localPosition.y, 0f);
			}
		}
		currentCategoryIndex = categoryIndex;
		scrollThing.transform.localPosition = new Vector3(scrollThing.transform.localPosition.x, -1680f, scrollThing.transform.localPosition.z);
		weaponList.Reposition();
	}

	public int mouseScrollThing
	{
		get
		{
			return (Input.GetAxis("Mouse ScrollWheel") == 0 ? 0 : Input.GetAxis("Mouse ScrollWheel") < 0 ? 25 : -25);
		}
	}

	public UILabel entityDamage, playerDamage, clip, reserve, capacity, movementSpeed, attackSpeed, recoilPower, recoilCooldown, impulse, radius, radiusSelf, rocketSpeed, nameLabel, purchaseButtonLabel; 
	
	public GameObject weaponActive;

	public WeaponSounds currentWeapon;

	public GameObject DeveloperButton;

	public void SelectWeapon(WeaponSounds weapon)
	{
		purchaseButtonLabel.text = (Bought(weapon) ? WeaponEquipped(weapon) ? "Equipped" : "Equip" : "Purchase");
		nameLabel.text = weapon.weaponName.Replace("newline", "\n");
		weaponActive.SetActive(true);
		entityDamage.text = weapon.damage + "";
		playerDamage.text = weapon.multiplayerDamage + "";
		clip.text = weapon.ammoInClip + "";
		reserve.text = weapon.InitialAmmo + "";
		capacity.text = weapon.maxAmmo + "";
		movementSpeed.text = weapon.speedModifier + "";
		if (weapon.isMelee)
		{
			attackSpeed.text = weapon.meleeAttackTimeModifier + "";
		}
		else
		{
			attackSpeed.text = notApplicableText;
		}
		recoilPower.text = weapon.upKoofFire + "";
		recoilCooldown.text = weapon.downKoof + "";
		if (weapon.bazooka)
		{
			impulse.text = weapon.bazookaImpulseRadius + "";
			radius.text = weapon.bazookaExplosionRadius + "";
			radiusSelf.text = weapon.bazookaExplosionRadiusSelf + "";
			rocketSpeed.text = weapon.bazookaSpeed + "";
		}
		else
		{
			impulse.text = notApplicableText;
			radius.text = notApplicableText;
			radiusSelf.text = notApplicableText;
			rocketSpeed.text = notApplicableText;
		}
		currentWeapon = weapon;
	}

	public bool Buy(WeaponSounds weapon)
	{
		#if USES_WEBSOCKET
		bool toreturn = false;
		// TODO: copy all important weapon stats to database, like price
		// just for extra security
		handler.networking.WebsocketHandler.CallAction("buy_weapon", (string data) => {
			Dictionary<string, object> resultDictionary = handler.networking.WebsocketHandler.Decrypt(JsonConvert.DeserializeObject<Dictionary<string, object>>(data));
			if ((string)resultDictionary["response"] == "success")
			{
				toreturn = true;
			}
		}, new Dictionary<string, object>(){
			{"uid", handler.data.UserController.Instance.ID},
			{"ak", handler.data.UserController.Instance.AuthKey},
			{"wn", weapon.name},
			{"wp", weapon.price},
			{"wc", (int)weapon.category},
		});
		return toreturn;
		#else
		if (Defs.CoinsAmount >= weapon.price)
		{
			Defs.CoinsAmount -= weapon.price;
			Storager.setInt(weapon.name + "shopbuy", 1, false);
			return true;
		}
		return false;
		#endif
	}

	public bool Bought(WeaponSounds weapon)
	{
		#if USES_WEBSOCKET
		return handler.data.UserController.Instance.BoughtGuns.Contains(weapon.name);
		#else
		return Storager.getInt(weapon.name + "shopbuy", false) == 1;
		#endif
	}

	public AudioClip buy, normalPress;

	public void BuyCurrentWeapon()
	{
		if (!Bought(currentWeapon))
		{
			if (Buy(currentWeapon))
			{
				GetComponent<AudioSource>().PlayOneShot(buy);
				EquipWeapon(currentWeapon);
				getCategoryWeaponList(currentCategoryIndex).Find(x => x.name == "WeaponButtonTemplate_" + currentWeapon.name).transform.Find("unboughtOnly").gameObject.SetActive(false);
				return;
			}
			GetComponent<AudioSource>().PlayOneShot(normalPress);
			return;
		}
		GetComponent<AudioSource>().PlayOneShot(normalPress);
		EquipWeapon(currentWeapon);
	}

	public void EquipWeapon(WeaponSounds weapon)
	{
		#if USES_WEBSOCKET
		handler.networking.WebsocketHandler.CallAction("equip_weapon", (string data) => {
			Dictionary<string, object> resultDictionary = handler.networking.WebsocketHandler.Decrypt(JsonConvert.DeserializeObject<Dictionary<string, object>>(data));
			Debug.Log(JsonConvert.SerializeObject(resultDictionary));
		}, new Dictionary<string, object>(){
			{"uid", handler.data.UserController.Instance.ID},
			{"ak", handler.data.UserController.Instance.AuthKey},
			{"wn", weapon.name},
			{"wc", (int)weapon.category},
		});
		#else
		prefs.SetString(GetCatStringFromCategoryType(weapon.category), weapon.name);
		#endif
	}

	public bool WeaponEquipped(WeaponSounds weapon)
	{
		return prefs.GetString(GetCatStringFromCategoryType(weapon.category)) == weapon.name;
	}

	public string GetCatStringFromCategoryType(CategoryType category)
	{
		switch (category)
		{
			case CategoryType.Backup:
			return "cat2";
			case CategoryType.Melee:
			return "cat3";
			case CategoryType.Special:
			return "cat4";
			case CategoryType.Heavy:
			return "cat5";
			default:
			return "cat1";
		}
	}

	public void DeveloperFunnies()
	{
		#if USES_WEBSOCKET
		handler.networking.WebsocketHandler.CallAction("admin_give_coins", null, new Dictionary<string, object>(){
			{"uid", handler.data.UserController.Instance.ID},
			{"ak", handler.data.UserController.Instance.AuthKey},
		});
		#else
		Storager.setInt(Defs.Coins, Storager.getInt(Defs.Coins, false) + 99999, false);
		#endif
	}

	public void CreateButton(WeaponSounds weapon)
	{
		bool needsBuy = !Bought(weapon) && weapon.price != 0;
		GameObject newButton = Instantiate(gunButtonTemplate);
		newButton.tag = "WeaponButton";
		newButton.transform.Find("EquippedLabel").gameObject.SetActive(WeaponEquipped(weapon));
		newButton.transform.Find("unboughtOnly").gameObject.SetActive(needsBuy);
		newButton.GetComponent<ButtonHandler>().Clicked += (object sender2, EventArgs args2) =>
		{
			GetComponent<AudioSource>().PlayOneShot(normalPress);
			SelectWeapon(weapon);
		};
		newButton.name = newButton.name.Replace("(Clone)", "") + "_" + weapon.name;
		newButton.transform.Find("GunInstantiation").localScale = weapon.armoryScale;
		newButton.transform.Find("unboughtOnly").Find("PriceLabel").GetComponent<UILabel>().text = weapon.price + "";
		newButton.transform.Find("NameLabel").GetComponent<UILabel>().text = weapon.weaponName.Replace("newline", "\n");
		try
		{
			GameObject gunPrefab = Instantiate(weapon.bonusPrefab, newButton.transform.Find("GunInstantiation"));
			if (gunPrefab.GetComponent<SkinnedMeshRenderer>())
			{
				GameObject newModel = new GameObject("NewModel_" + gunPrefab.name);
				newModel.AddComponent<MeshFilter>().sharedMesh = gunPrefab.GetComponent<SkinnedMeshRenderer>().sharedMesh;
				newModel.AddComponent<MeshRenderer>().materials = gunPrefab.GetComponent<SkinnedMeshRenderer>().materials;
				newModel.transform.parent = gunPrefab.transform;
				newModel.transform.localScale = Vector3.one;
				newModel.transform.localPosition = Vector3.zero;
				newModel.transform.localRotation = Quaternion.Euler(weapon.armoryRotation);
			}
			gunPrefab.transform.localRotation = Quaternion.Euler(weapon.armoryRotation);
			gunPrefab.transform.localPosition = weapon.armoryPosition;
			gunPrefab.gameObject.layer = 10;
			foreach (Transform trf in gunPrefab.gameObject.GetComponentsInChildren<Transform>())
			{
				trf.gameObject.layer = 10;
			}
		}
		catch
		{
			Debug.LogError("BONUS PREFAB IN " + weapon.name  +  " MISSING. SKIPPING GUN UNTIL YOU FIX IT YOU LAZY LITTL-");
		}
		newButton.transform.Find("GunInstantiation").localScale = weapon.armoryScale;
		newButton.SetActive(false);
		getCategoryWeaponList((int)weapon.category).Add(newButton);
	}
}
