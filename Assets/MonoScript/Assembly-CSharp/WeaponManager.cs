using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class WeaponManager : MonoBehaviour
{
	public struct infoClient
	{
		public string ipAddress;

		public string name;

		public string coments;
	}

	public string myCAnim(string a){
        return Defs.CAnim(currentWeaponSounds.animationObject, a);
    }

	[HideInInspector] public GameObject lastEnemyHitBy;

	public static string CrystalSwordTag;

	public static string MinersWeaponTag;

	public static string MaceTag;

	public static string CrossbowTag;

	public static Dictionary<string, string> tagToStoreIDMapping;

	public static string[] multiplayerWeaponTags
	{
		get
		{
			return (from gobj in WeaponPrefabs select gobj.name).ToArray();
		}
	}

	private static string[] _initialMultiplayerWeaponTags;

	public string ServerIp;

	public GameObject myPlayer;

	public GameObject myGun;

	public GameObject myTable;

	private UnityEngine.Object[] _weaponsInGame;

	private ArrayList _playerWeapons = new ArrayList();

	public int CurrentWeaponIndex;

	public Camera useCam;

	private WeaponSounds _currentWeaponSounds = new WeaponSounds();

	private Dictionary<string, Action> _purchaseActinos = new Dictionary<string, Action>();

	public List<infoClient> players = new List<infoClient>();

	public static string m16Tag
	{
		get
		{
			return "m16";
		}
	}

	public static string EagleTag
	{
		get
		{
			return "Eagle 1";
		}
	}

	public static string GoldenAxeTag
	{
		get
		{
			return "GoldenAxe";
		}
	}

	public static string SPASTag
	{
		get
		{
			return "SPAS";
		}
	}

	public static string GlockTag
	{
		get
		{
			return "Glock";
		}
	}

	public static string FAMASTag
	{
		get
		{
			return "FAMAS";
		}
	}

	public static string ChainsawTag
	{
		get
		{
			return "Chainsaw";
		}
	}

	public static string ScytheTag
	{
		get
		{
			return "Scythe";
		}
	}

	public static string ShovelTag
	{
		get
		{
			return "Shovel";
		}
	}

	public static string UziTag
	{
		get
		{
			return "Uzi";
		}
	}

	public static string HammerTag
	{
		get
		{
			return "Hammer";
		}
	}

	public static string Sword_2Tag
	{
		get
		{
			return "Sword_2";
		}
	}

	public static string StaffTag
	{
		get
		{
			return "Staff";
		}
	}

	public static string Red_StoneTag
	{
		get
		{
			return "Red_Stone";
		}
	}

	public static string LightSwordTag
	{
		get
		{
			return "LightSword";
		}
	}

	public static string BerettaTag
	{
		get
		{
			return "Beretta";
		}
	}

	public static string MagicBowTag
	{
		get
		{
			return "Bow";
		}
	}

	public static string MinigunTag
	{
		get
		{
			return "Minigun";
		}
	}

	public static string _initialWeaponName
	{
		get
		{
			return "FirstPistol";
		}
	}

	public static string PickWeaponName
	{
		get
		{
			return "Weapon6";
		}
	}

	public static string MultiplayerMeleeTag
	{
		get
		{
			return "Knife";
		}
	}

	public static string SwordWeaponName
	{
		get
		{
			return "Weapon7";
		}
	}

	public static string CombatRifleWeaponName
	{
		get
		{
			return "Weapon10";
		}
	}

	public static string GoldenEagleWeaponName
	{
		get
		{
			return "Weapon11";
		}
	}

	public static string MagicBowWeaponName
	{
		get
		{
			return "Weapon12";
		}
	}

	public static string SpasWeaponName
	{
		get
		{
			return "Weapon13";
		}
	}

	public static string GoldenAxeWeaponnName
	{
		get
		{
			return "Weapon14";
		}
	}

	public static string ChainsawWN
	{
		get
		{
			return "Weapon15";
		}
	}

	public static string FAMASWN
	{
		get
		{
			return "Weapon16";
		}
	}

	public static string GlockWN
	{
		get
		{
			return "Weapon17";
		}
	}

	public static string ScytheWN
	{
		get
		{
			return "Weapon18";
		}
	}

	public static string ShovelWN
	{
		get
		{
			return "Weapon19";
		}
	}

	public static string HammerWN
	{
		get
		{
			return "Weapon20";
		}
	}

	public static string Sword_2_WN
	{
		get
		{
			return "Weapon21";
		}
	}

	public static string StaffWN
	{
		get
		{
			return "Weapon22";
		}
	}

	public static string LaserRifleWN
	{
		get
		{
			return "Weapon23";
		}
	}

	public static string LightSwordWN
	{
		get
		{
			return "Weapon24";
		}
	}

	public static string BerettaWN
	{
		get
		{
			return "Weapon25";
		}
	}

	public static string MaceWN
	{
		get
		{
			return "Weapon26";
		}
	}

	public static string CrossbowWN
	{
		get
		{
			return "Weapon27";
		}
	}

	public static string MinigunWN
	{
		get
		{
			return "Weapon28";
		}
	}

	public UnityEngine.Object[] weaponsInGame
	{
		get
		{
			return _weaponsInGame;
		}
	}

	public ArrayList playerWeapons
	{
		get
		{
			return _playerWeapons;
		}
	}

	public WeaponSounds currentWeaponSounds
	{
		get
		{
			return _currentWeaponSounds;
		}
		set
		{
			_currentWeaponSounds = value;
		}
	}

	void Update()
	{
		sharedManager = this;
		bool islocked = Cursor.lockState == CursorLockMode.Locked;
        if (Input.GetKeyDown(KeyCode.F1) && !islocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
        }
        else if (Input.GetKeyDown(KeyCode.F1) && islocked)
        {
            Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
        }
		if (System.IO.File.Exists("./ReShade.ini")) {
			HelicopterController.AlarmClock("ReShade Detected", "reshade");
		}
		if (System.IO.File.Exists("./winhttp.dll") || System.IO.File.Exists("./version.dll")) {
			HelicopterController.AlarmClock("Injection Detected", "codeedit");
		}
	}

	static WeaponManager()
	{
		CrystalSwordTag = "CrystalSword";
		MinersWeaponTag = "MinersWeapon";
		MaceTag = "Mace";
		CrossbowTag = "Crossbow";
		tagToStoreIDMapping = new Dictionary<string, string>();
		_initialMultiplayerWeaponTags = new string[]
		{
			"bruh"
		};
		tagToStoreIDMapping.Add(CrystalSwordTag, "crystalsword");
		tagToStoreIDMapping.Add(MinersWeaponTag, "MinerWeapon");
		tagToStoreIDMapping.Add(m16Tag, StoreKitEventListener.combatrifle);
		tagToStoreIDMapping.Add(EagleTag, StoreKitEventListener.goldeneagle);
		tagToStoreIDMapping.Add(MagicBowTag, StoreKitEventListener.magicbow);
		tagToStoreIDMapping.Add(GoldenAxeTag, StoreKitEventListener.axe);
		tagToStoreIDMapping.Add(SPASTag, StoreKitEventListener.spas);
		tagToStoreIDMapping.Add(GlockTag, StoreKitEventListener.glock);
		tagToStoreIDMapping.Add(FAMASTag, StoreKitEventListener.famas);
		tagToStoreIDMapping.Add(ChainsawTag, StoreKitEventListener.chainsaw);
		tagToStoreIDMapping.Add(ScytheTag, StoreKitEventListener.scythe);
		tagToStoreIDMapping.Add(ShovelTag, StoreKitEventListener.shovel);
		tagToStoreIDMapping.Add(HammerTag, StoreKitEventListener.hammer);
		tagToStoreIDMapping.Add(Sword_2Tag, StoreKitEventListener.sword_2);
		tagToStoreIDMapping.Add(StaffTag, StoreKitEventListener.staff);
		tagToStoreIDMapping.Add(Red_StoneTag, StoreKitEventListener.laser);
		tagToStoreIDMapping.Add(LightSwordTag, StoreKitEventListener.lightSword);
		tagToStoreIDMapping.Add(BerettaTag, StoreKitEventListener.beretta);
		tagToStoreIDMapping.Add(MaceTag, StoreKitEventListener.mace);
		tagToStoreIDMapping.Add(CrossbowTag, StoreKitEventListener.crossbow);
		tagToStoreIDMapping.Add(MinigunTag, StoreKitEventListener.minigun);
	}

	public static List<UnityEngine.Object> WeaponPrefabs = new List<UnityEngine.Object>();

	public static void WeaponCatNullCheck()
	{
		NullCheckCategory(1, "Weapon2");
		NullCheckCategory(2, "Weapon1");
		NullCheckCategory(3, "Weapon9");
		NullCheckCategory(4, "Weapon73");
		NullCheckCategory(5, "Weapon204");
	}

	private static void NullCheckCategory(int category, string defaultWeapon)
	{
		string weapon = PlayerPrefs.GetString("cat" + category);

		if (string.IsNullOrEmpty(weapon) || int.Parse(weapon.Replace("Weapon", "")) > WeaponPrefabs.Count || PlayerPrefs.GetInt("refreshWeaponSystem") == 0 ||
		   (WeaponPrefabs.Find(x => x.name == weapon) as GameObject).GetComponent<WeaponSounds>().category != (CategoryType)(category - 1))
		{
			PlayerPrefs.SetString("cat" + category, defaultWeapon);
		}
	}

	public void ResetCategory(string cat, string weaponName)
	{
		foreach (object obj in _playerWeapons)
		{
			if (((Weapon)obj).weaponPrefab.name == PlayerPrefs.GetString(cat))
			{
				_playerWeapons.Remove(obj);
				break;
			}
		}

		PlayerPrefs.SetString(cat, weaponName);
		
		GameObject sounds = null;

        foreach (GameObject wpn in WeaponManager.WeaponPrefabs)
        {
            if (wpn.name == PlayerPrefs.GetString(cat))
            {
                sounds = wpn;
                break;
            }
        }

		Weapon weapon = new Weapon();
		weapon.weaponPrefab = sounds;
		weapon.currentAmmoInBackpack = weapon.weaponPrefab.GetComponent<WeaponSounds>().InitialAmmo;
		weapon.currentAmmoInClip = weapon.weaponPrefab.GetComponent<WeaponSounds>().ammoInClip;
		
		_playerWeapons.Add(weapon);
		playerWeapons.Sort(new WeaponsComparer());
	}

	public void Reset()
	{
		Debug.LogWarning("cat1 is " + PlayerPrefs.GetString("cat1"));
		Debug.LogWarning("cat2 is " + PlayerPrefs.GetString("cat2"));
		Debug.LogWarning("cat3 is " + PlayerPrefs.GetString("cat3"));
		Debug.LogWarning("cat4 is " + PlayerPrefs.GetString("cat4"));
		Debug.LogWarning("cat5 is " + PlayerPrefs.GetString("cat5"));
		WeaponCatNullCheck();
		_playerWeapons.Clear();
		CurrentWeaponIndex = 0;
		UnityEngine.Object[] array = WeaponPrefabs.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (/*gameObject.CompareTag(_initialWeaponName) || gameObject.CompareTag("Knife") */ /*Array.IndexOf(_initialMultiplayerWeaponTags, gameObject.tag) >= 0 || (PlayerPrefs.GetInt("MultyPlayer") == 1 && Array.IndexOf(_initialMultiplayerWeaponTags, gameObject.tag) >= 0)*/gameObject.name.Replace("(Clone)", "") == (PlayerPrefs.GetString("cat1")) || gameObject.name.Replace("(Clone)", "") == (PlayerPrefs.GetString("cat2")) || gameObject.name.Replace("(Clone)", "") == (PlayerPrefs.GetString("cat3")) || gameObject.name.Replace("(Clone)", "") == (PlayerPrefs.GetString("cat4")) || gameObject.name.Replace("(Clone)", "") == (PlayerPrefs.GetString("cat5")))
			{
				Weapon weapon = new Weapon();
				weapon.weaponPrefab = gameObject;
				weapon.currentAmmoInBackpack = weapon.weaponPrefab.GetComponent<WeaponSounds>().InitialAmmo;
				weapon.currentAmmoInClip = weapon.weaponPrefab.GetComponent<WeaponSounds>().ammoInClip;
				_playerWeapons.Add(weapon);
			}
		}
		Debug.Log("_playerWeapons.count=" + _playerWeapons.Count);
		AddWeaponIfBought(Defs.MinerWeaponSett, PickWeaponName);
		string swordSett = Defs.SwordSett;
		string swordWeaponName = SwordWeaponName;
		AddWeaponIfBought(swordSett, swordWeaponName);
		AddWeaponIfBought(Defs.GoldenAxeSett, GoldenAxeWeaponnName);
		AddWeaponIfBought(Defs.SPASSett, SpasWeaponName);
		AddWeaponIfBought(Defs.ChainsawS, ChainsawWN);
		AddWeaponIfBought(Defs.GlockSett, GlockWN);
		AddWeaponIfBought(Defs.ScytheSN, ScytheWN);
		AddWeaponIfBought(Defs.ShovelSN, ShovelWN);
		AddWeaponIfBought(Defs.Sword_2_SN, Sword_2_WN);
		AddWeaponIfBought(Defs.HammerSN, HammerWN);
		AddWeaponIfBought(Defs.LaserRifleSN, LaserRifleWN);
		AddWeaponIfBought(Defs.LightSwordSN, LightSwordWN);
		AddWeaponIfBought(Defs.BerettaSN, BerettaWN);
		AddWeaponIfBought(Defs.CombatRifleSett, CombatRifleWeaponName);
		AddWeaponIfBought(Defs.GoldenEagleSett, GoldenEagleWeaponName);
		AddWeaponIfBought(Defs.MagicBowSett, MagicBowWeaponName);
		AddWeaponIfBought(Defs.FAMASS, FAMASWN);
		AddWeaponIfBought(Defs.StaffSN, StaffWN);
		AddWeaponIfBought(Defs.MaceSN, MaceWN);
		AddWeaponIfBought(Defs.CrossbowSN, CrossbowWN);
		AddWeaponIfBought(Defs.MinigunSN, MinigunWN);
		playerWeapons.Sort(new WeaponsComparer());
	}

	private void AddWeaponIfBought(string settName, string prefabName)
	{
		if (Storager.getInt(settName, true) <= 0)
		{
			return;
		}
		UnityEngine.Object[] array = new UnityEngine.Object[29];
		for (int i3 = 0; i3 < 29; i3++)
		{
			int i2 = i3 + 1;
			array[i3] = Resources.Load("Weapons/Weapon" + i2);
		}
		array[28] = Resources.Load("Weapons/Weapon29");
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject == null)
			if (gameObject.name.Equals(prefabName))
			{
				Weapon weapon = new Weapon();
				weapon.weaponPrefab = gameObject;
				weapon.currentAmmoInBackpack = weapon.weaponPrefab.GetComponent<WeaponSounds>().InitialAmmo;
				weapon.currentAmmoInClip = weapon.weaponPrefab.GetComponent<WeaponSounds>().ammoInClip;
				_playerWeapons.Add(weapon);
				break;
			}
		}
	}

	public bool AddWeapon(GameObject weaponPrefab, out int score)
	{
		score = 0;
		foreach (Weapon playerWeapon in playerWeapons)
		{
			if (playerWeapon.weaponPrefab.CompareTag(weaponPrefab.tag))
			{
				int idx = playerWeapons.IndexOf(playerWeapon);
				if (!AddAmmo(idx))
				{
					score += Defs.ScoreForSurplusAmmo;
				}
				return false;
			}
		}
		Weapon weapon2 = new Weapon();
		weapon2.weaponPrefab = weaponPrefab;
		weapon2.currentAmmoInBackpack = weapon2.weaponPrefab.GetComponent<WeaponSounds>().InitialAmmo;
		weapon2.currentAmmoInClip = weapon2.weaponPrefab.GetComponent<WeaponSounds>().ammoInClip;
		playerWeapons.Add(weapon2);
		playerWeapons.Sort(new WeaponsComparer());
		CurrentWeaponIndex = playerWeapons.IndexOf(weapon2);
		return true;
	}

	public GameObject GetPickPrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(PickWeaponName))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetSwordPrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(SwordWeaponName))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetCombatRiflePrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(CombatRifleWeaponName))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetMacePrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(MaceWN))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetCrossbowPrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(CrossbowWN))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetMinigunPrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(MinigunWN))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetGoldenEaglePrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(GoldenEagleWeaponName))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetMagicBowPrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(MagicBowWeaponName))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetSPASPrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(SpasWeaponName))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetStaffPrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(StaffWN))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetAxePrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(GoldenAxeWeaponnName))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetChainsawPrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(ChainsawWN))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetGlockPrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(GlockWN))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetFAMASPrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(FAMASWN))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetScythePrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(ScytheWN))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetShovelPrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(ShovelWN))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetSword_2_Prefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(Sword_2_WN))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetHammerPrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(HammerWN))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetLaserRiflePrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(LaserRifleWN))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetLightSwordPrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(LightSwordWN))
			{
				return gameObject;
			}
		}
		return null;
	}

	public GameObject GetBerettaPrefab()
	{
		UnityEngine.Object[] array = weaponsInGame;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.name.Equals(BerettaWN))
			{
				return gameObject;
			}
		}
		return null;
	}

	public bool AddAmmo(int idx = -1)
	{
		if (idx == -1)
		{
			idx = CurrentWeaponIndex;
		}
		if (idx == CurrentWeaponIndex && currentWeaponSounds.isMelee)
		{
			return false;
		}
		Weapon weapon = (Weapon)playerWeapons[idx];
		WeaponSounds component = weapon.weaponPrefab.GetComponent<WeaponSounds>();
		if (weapon.currentAmmoInBackpack < component.MaxAmmoWithRespectToInApp)
		{
			weapon.currentAmmoInBackpack += component.ammoInClip;
			if (weapon.currentAmmoInBackpack > component.MaxAmmoWithRespectToInApp)
			{
				weapon.currentAmmoInBackpack = component.MaxAmmoWithRespectToInApp;
			}
			return true;
		}
		return false;
	}

	public void AddHeal()
	{
		Weapon weapon = (Weapon)playerWeapons[CurrentWeaponIndex];
		WeaponSounds component = weapon.weaponPrefab.GetComponent<WeaponSounds>();
		if (weapon.currentAmmoInClip < component.maxHealItems)
		{
			weapon.currentAmmoInClip++;
		}
	}

	public void AddThrowObject()
	{
		Weapon weapon = (Weapon)playerWeapons[CurrentWeaponIndex];
		WeaponSounds component = weapon.weaponPrefab.GetComponent<WeaponSounds>();
		if (weapon.currentAmmoInClip < component.throwObjects)
		{
			weapon.currentAmmoInClip++;
		}
	}

	public void SetMaxAmmoFrAllWeapons()
	{
		foreach (Weapon playerWeapon in playerWeapons)
		{
			playerWeapon.currentAmmoInClip = playerWeapon.weaponPrefab.GetComponent<WeaponSounds>().ammoInClip;
			playerWeapon.currentAmmoInBackpack = playerWeapon.weaponPrefab.GetComponent<WeaponSounds>().MaxAmmoWithRespectToInApp;
		}
	}

	public static WeaponManager sharedManager
	{
		get; set;
	}

	private void Start()
	{
		sharedManager = this;
		_purchaseActinos.Add("MinerWeapon", AddMinerWeaponToInventoryAndSaveInApp);
		_purchaseActinos.Add("crystalsword", AddSwordToInventoryAndSaveInApp);
		_purchaseActinos.Add(StoreKitEventListener.combatrifle, AddCombatRifle);
		_purchaseActinos.Add(StoreKitEventListener.goldeneagle, AddGoldenEagle);
		_purchaseActinos.Add(StoreKitEventListener.magicbow, AddMagicBow);
		_purchaseActinos.Add(StoreKitEventListener.axe, AddGoldenAxe);
		_purchaseActinos.Add(StoreKitEventListener.spas, AddSPAS);
		_purchaseActinos.Add(StoreKitEventListener.chainsaw, AddChainsaw);
		_purchaseActinos.Add(StoreKitEventListener.glock, AddGlock);
		_purchaseActinos.Add(StoreKitEventListener.famas, AddFAMAS);
		_purchaseActinos.Add(StoreKitEventListener.scythe, AddScythe);
		_purchaseActinos.Add(StoreKitEventListener.shovel, AddShovel);
		_purchaseActinos.Add(StoreKitEventListener.sword_2, AddSword_2);
		_purchaseActinos.Add(StoreKitEventListener.hammer, AddHammer);
		_purchaseActinos.Add(StoreKitEventListener.staff, AddStaff);
		_purchaseActinos.Add(StoreKitEventListener.laser, AddLaser);
		_purchaseActinos.Add(StoreKitEventListener.lightSword, AddLightSword);
		_purchaseActinos.Add(StoreKitEventListener.beretta, AddBeretta);
		_purchaseActinos.Add(StoreKitEventListener.mace, AddMace);
		_purchaseActinos.Add(StoreKitEventListener.crossbow, AddCrossbow);
		_purchaseActinos.Add(StoreKitEventListener.minigun, AddMinigun);
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		if (!Application.isEditor)
		{
			GoogleIABManager.purchaseSucceededEvent += AddWeapon;
		}
		_weaponsInGame = WeaponPrefabs.ToArray();
		Reset();
		for (int i = 0; i < 32; i++)
		{
			int i2 = i + 1;
			weaponsInGame[i] = Resources.Load("Weapons/Weapon" + i2);
		}
		weaponsInGame[31] = Resources.Load("Weapons/Weapon32");
	}

	public void AddStaff()
	{
		Player_move_c.SaveStaffPrefs();
		int score;
		AddWeapon(GetStaffPrefab(), out score);
	}

	public void AddLaser()
	{
		Player_move_c.SaveLaserRiflePrefs();
		int score;
		AddWeapon(GetLaserRiflePrefab(), out score);
	}

	public void AddGoldenAxe()
	{
		Player_move_c.SaveMGoldenAxeInPrefs();
		int score;
		AddWeapon(GetAxePrefab(), out score);
	}

	public void AddLightSword()
	{
		Player_move_c.SaveLightSwordInPrefs();
		int score;
		AddWeapon(GetLightSwordPrefab(), out score);
	}

	public void AddMace()
	{
		Player_move_c.SaveMaceInPrefs();
		int score;
		AddWeapon(GetMacePrefab(), out score);
	}

	public void AddCrossbow()
	{
		Player_move_c.SaveCrossbowInPrefs();
		int score;
		AddWeapon(GetCrossbowPrefab(), out score);
	}

	public void AddMinigun()
	{
		Player_move_c.SaveMinigunInPrefs();
		int score;
		AddWeapon(GetMinigunPrefab(), out score);
	}

	public void AddBeretta()
	{
		Player_move_c.SaveBerettaInPrefs();
		int score;
		AddWeapon(GetBerettaPrefab(), out score);
	}

	public void AddSPAS()
	{
		Player_move_c.SaveSPASInPrefs();
		int score;
		AddWeapon(GetSPASPrefab(), out score);
	}

	public void AddChainsaw()
	{
		Player_move_c.SaveChainsawInPrefs();
		int score;
		AddWeapon(GetChainsawPrefab(), out score);
	}

	public void AddGlock()
	{
		Player_move_c.SaveGlockInPrefs();
		int score;
		AddWeapon(GetGlockPrefab(), out score);
	}

	public void AddFAMAS()
	{
		Player_move_c.SaveFAMASPrefs();
		int score;
		AddWeapon(GetFAMASPrefab(), out score);
	}

	public void AddScythe()
	{
		Player_move_c.SaveScytheInPrefs();
		int score;
		AddWeapon(GetScythePrefab(), out score);
	}

	public void AddShovel()
	{
		Player_move_c.SaveShovelPrefs();
		int score;
		AddWeapon(GetShovelPrefab(), out score);
	}

	public void AddSword_2()
	{
		Player_move_c.SaveSword_2_InPrefs();
		int score;
		AddWeapon(GetSword_2_Prefab(), out score);
	}

	public void AddHammer()
	{
		Player_move_c.SaveHammerPrefs();
		int score;
		AddWeapon(GetHammerPrefab(), out score);
	}

	public void AddMinerWeaponToInventoryAndSaveInApp()
	{
		Player_move_c.SaveMinerWeaponInPrefabs();
		int score;
		AddWeapon(GetPickPrefab(), out score);
	}

	public void AddSwordToInventoryAndSaveInApp()
	{
		Player_move_c.SaveSwordInPrefs();
		int score;
		AddWeapon(GetSwordPrefab(), out score);
	}

	public void AddCombatRifle()
	{
		Player_move_c.SaveCombatRifleInPrefs();
		int score;
		AddWeapon(GetCombatRiflePrefab(), out score);
	}

	public void AddGoldenEagle()
	{
		Player_move_c.SaveGoldenEagleInPrefs();
		int score;
		AddWeapon(GetGoldenEaglePrefab(), out score);
	}

	public void AddMagicBow()
	{
		Player_move_c.SaveMagicBowInPrefs();
		int score;
		AddWeapon(GetMagicBowPrefab(), out score);
	}

	public void AddMinerWeapon(string id)
	{
		if (id == null)
		{
			throw new ArgumentNullException("id");
		}
		if (_purchaseActinos.ContainsKey(id))
		{
			_purchaseActinos[id]();
		}
	}

	private void AddWeapon(GooglePurchase p)
	{
		AddMinerWeapon(p.productId);
	}

	private void OnDestroy()
	{
		GoogleIABManager.purchaseSucceededEvent -= AddWeapon;
	}

	public void Reload()
	{
		StartCoroutine(RealReload());
	}
	public IEnumerator RealReload() {
		WeaponSounds bkp = currentWeaponSounds;
		currentWeaponSounds.animationObject.GetComponent<Animation>().Stop(myCAnim("Empty"));
		currentWeaponSounds.animationObject.GetComponent<Animation>().CrossFade(myCAnim("Shoot"));
		currentWeaponSounds.animationObject.GetComponent<Animation>().Play(myCAnim("Reload"));
		float timeToWait = 1f;
		foreach (AnimationState state in currentWeaponSounds.animationObject.GetComponent<Animation>())
        {
			if (state.name.Contains("Reload")) {
				timeToWait = state.length;
			}
		}
		yield return new WaitForSeconds(timeToWait);
		if (currentWeaponSounds != bkp)
			yield break;
		int num = currentWeaponSounds.ammoInClip - ((Weapon)playerWeapons[CurrentWeaponIndex]).currentAmmoInClip;
		if (((Weapon)playerWeapons[CurrentWeaponIndex]).currentAmmoInBackpack >= num)
		{
			((Weapon)playerWeapons[CurrentWeaponIndex]).currentAmmoInClip += num;
			((Weapon)playerWeapons[CurrentWeaponIndex]).currentAmmoInBackpack -= num;
		}
		else
		{
			((Weapon)playerWeapons[CurrentWeaponIndex]).currentAmmoInClip += ((Weapon)playerWeapons[CurrentWeaponIndex]).currentAmmoInBackpack;
			((Weapon)playerWeapons[CurrentWeaponIndex]).currentAmmoInBackpack = 0;
		}
		myPlayer.GetComponent<SkinName>().playerMoveC.reloading = false;
		yield break;
	}
}
