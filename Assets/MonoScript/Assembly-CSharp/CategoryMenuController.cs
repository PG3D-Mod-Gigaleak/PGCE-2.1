using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryMenuController : MonoBehaviour {

	[System.Serializable]
	public class WeaponList
	{
		public string[] weaponNames = new string[10];

		public int[] weaponCosts = new int[10];

		public string[] weaponComments = new string[10];

		public string[] weaponStats = new string[10];
	}

	[SerializeField]
	public List<CategoryMenuController.WeaponList> WeaponLists = new List<CategoryMenuController.WeaponList>();

	public bool isGear;

	private bool popup;

	public string popupWeapon;

	private int popupIndex;

	public string catType;

	public Texture2D catFon;

	public GUIStyle back;

	public GUIStyle nameStyle;

	public GUIStyle setButton;

	public GUIStyle arrow1;

	public GUIStyle arrow2;

	public GUIStyle labelThingy;

	public GUIStyle commentLabelThingy;

	public GUIStyle closeButton;

	public GUIStyle next;

	public Texture popupBG;

	public Texture menuBG;

	public Texture coinsLabel;

	public GUIStyle biggerLabelThingy;

	public GUIStyle evenBiggerLabelThingy;

	public Texture coin;

	public GUIStyle blankButton;

	public void BS()
	{
		GetComponent<AudioSource>().Play();
	}

	public void Buy(int price, string key)
	{
		if (Defs.CoinsAmount >= price)
		{
			Defs.CoinsAmount -= price;
			Storager.setInt(key + "shopbuy", 1, false);
		}
	}

	public bool Bought(string key)
	{
		return Storager.getInt(key + "shopbuy", false) == 1;
	}

	public Texture[] currentWeaponTextures()
	{
		string[] weaponthe = WeaponLists[weaponIndex].weaponNames;
		Texture[] texs = new Texture[weaponsInThisPage()];
		for (int i = 0; i < weaponsInThisPage(); i++)
		{
			if (!isGear)
			{
				texs[i] = Utilities.LoadObject("Weapons/" + weaponthe[i]).GetComponent<WeaponSounds>().preview;
			}
			else
			{
				texs[i] = Utilities.LoadObject("Gear/" + weaponthe[i]).GetComponent<GearStats>().preview;
			}
		}
		return texs;
	}

	public Texture textureToDraw()
	{
		if (!currentWeaponHasUpgrades())
		{
			return currentWeaponTextures()[popupIndex];
		}
		else
		{
			return Utilities.LoadObject("Weapons/" + Utilities.LoadObject("upgrades/" + WeaponLists[weaponIndex].weaponNames[popupIndex]).GetComponent<UpgradeInfo>().weaponNames[currentUpgradeIndex()]).GetComponent<WeaponSounds>().preview;
		}
	}

	public bool currentWeaponHasUpgrades()
	{
		if (isGear)
		{
			return false;
		}
		return Utilities.LoadObject("Weapons/" + WeaponLists[weaponIndex].weaponNames[popupIndex]).GetComponent<WeaponSounds>().hasUpgrades;
	}

	public int currentWeaponPrice
	{
		get
		{
			if (isGear)
			{
				return Utilities.LoadObject("gear/" + WeaponLists[weaponIndex].weaponNames[popupIndex]).GetComponent<GearStats>().price;
			}
			if (!currentWeaponHasUpgrades())
			{
				return Utilities.LoadObject("Weapons/" + WeaponLists[weaponIndex].weaponNames[popupIndex]).GetComponent<WeaponSounds>().price;
			}
			return Utilities.LoadObject("Weapons/" + Utilities.LoadObject("upgrades/" + WeaponLists[weaponIndex].weaponNames[popupIndex]).GetComponent<UpgradeInfo>().weaponNames[currentUpgradeIndex()]).GetComponent<WeaponSounds>().price;
		}
	}

	public bool currentWeaponNeedsBuy
	{
		get
		{
			return currentWeaponPrice != 0;
		}
	}

	public int currentUpgradeIndex()
	{
		for (int i = 0; i < Utilities.LoadObject("upgrades/" + WeaponLists[weaponIndex].weaponNames[popupIndex]).GetComponent<UpgradeInfo>().weaponNames.Length; i++)
		{
			if(Utilities.LoadObject("Weapons/" + Utilities.LoadObject("upgrades/" + WeaponLists[weaponIndex].weaponNames[popupIndex]).GetComponent<UpgradeInfo>().weaponNames[i]).tag == popupWeapon)
			{
				return i;
			}
		}
		return 0;
	}

	public string[] currentWeaponNames()
	{
		string[] weaponthe =  WeaponLists[weaponIndex].weaponNames;
		string[] names = new string[weaponsInThisPage()];
		for (int i = 0; i < weaponsInThisPage(); i++)
		{
			if (!isGear)
			{
				names[i] = Utilities.LoadObject("Weapons/" + weaponthe[i]).tag;
			}
			else
			{
				names[i] = Utilities.LoadObject("Gear/" + weaponthe[i]).GetComponent<GearStats>().name;
			}
		}
		return names;
	}

	public string[] currentWeaponNames2()
	{
		string[] weaponthe =  WeaponLists[weaponIndex].weaponNames;
		string[] names = new string[weaponsInThisPage()];
		for (int i = 0; i < weaponsInThisPage(); i++)
		{
			if (!isGear)
			{
				names[i] = Utilities.LoadObject("Weapons/" + weaponthe[i]).GetComponent<WeaponSounds>().weaponName;
			}
			else
			{
				names[i] = Utilities.LoadObject("Gear/" + weaponthe[i]).name;
			}
		}
		return names;
	}

	public string currentWeaponComment()
	{
		if (!currentWeaponHasUpgrades())
		{
		return WeaponLists[weaponIndex].weaponComments[popupIndex];
		}
		else
		{
			return Utilities.LoadObject("upgrades/" + WeaponLists[weaponIndex].weaponNames[popupIndex]).GetComponent<UpgradeInfo>().weaponComments[currentUpgradeIndex()];
		}
	}

	public string currentWeaponStats()
	{	
		if (!currentWeaponHasUpgrades())
		{
			return WeaponLists[weaponIndex].weaponStats[popupIndex];
		}
		else
		{
			return Utilities.LoadObject("upgrades/" + WeaponLists[weaponIndex].weaponNames[popupIndex]).GetComponent<UpgradeInfo>().weaponStats[currentUpgradeIndex()];
		}
	}



	public int weaponsInThisPage()
	{
		string[] weaponthe =  WeaponLists[weaponIndex].weaponNames;
		int amount = 0;
		foreach (string str in weaponthe)
		{
			if (str == null || str == string.Empty)
			{
				return amount;
			}
			amount++;
		}
		return 10;
	}

	public void SetPopup(string weapon, int index, string weaponName)
	{
		popup = true;
		popupWeapon = weapon;
		popupName = weaponName;
		popupIndex = index;
	}

	public int weaponIndex;

	private float koofScreen = (float)Screen.height / 768f;

	public string popupName;

	public int theMomer
	{
		get
		{
			return Screen.width + Screen.height;
		}
	}

	public void Start()
	{
		biggerLabelThingy.fontSize = theMomer / 125;
		evenBiggerLabelThingy.fontSize = theMomer / 75;
		labelThingy.fontSize = theMomer / 200;
	}

	public void Update()
	{
	}

	public Rect FindRects(string type, int index)
	{
		if (type == "icon")
		{
			switch(index)
			{
				case 0:
				return Utilities.screenScaleRect(0.15f, 0.2f, 0.2f, 0.15f);
				case 1:
				return Utilities.screenScaleRect(0.25f, 0.2f, 0.2f, 0.15f);
				case 2:
				return Utilities.screenScaleRect(0.35f, 0.2f, 0.2f, 0.15f);
				case 3:
				return Utilities.screenScaleRect(0.45f, 0.2f, 0.2f, 0.15f);
				case 4:
				return Utilities.screenScaleRect(0.55f, 0.2f, 0.2f, 0.15f);
				case 5:
				return Utilities.screenScaleRect(0.15f, 0.55f, 0.2f, 0.15f);
				case 6:
				return Utilities.screenScaleRect(0.25f, 0.55f, 0.2f, 0.15f);
				case 7:
				return Utilities.screenScaleRect(0.35f, 0.55f, 0.2f, 0.15f);
				case 8:
				return Utilities.screenScaleRect(0.45f, 0.55f, 0.2f, 0.15f);
				case 9:
				return Utilities.screenScaleRect(0.55f, 0.55f, 0.2f, 0.15f);
			}
		}
		if (type == "set")
		{
			switch(index)
			{
				case 0:
				return Utilities.screenScaleRect(0.25f, 0.4f, 0.08f, 0.075f);
				case 1:
				return Utilities.screenScaleRect(0.35f, 0.4f, 0.08f, 0.075f);
				case 2:
				return Utilities.screenScaleRect(0.45f, 0.4f, 0.08f, 0.075f);
				case 3:
				return Utilities.screenScaleRect(0.55f, 0.4f, 0.08f, 0.075f);
				case 4:
				return Utilities.screenScaleRect(0.65f, 0.4f, 0.08f, 0.075f);
				case 5:
				return Utilities.screenScaleRect(0.25f, 0.75f, 0.08f, 0.075f);
				case 6:
				return Utilities.screenScaleRect(0.35f, 0.75f, 0.08f, 0.075f);
				case 7:
				return Utilities.screenScaleRect(0.45f, 0.75f, 0.08f, 0.075f);
				case 8:
				return Utilities.screenScaleRect(0.55f, 0.75f, 0.08f, 0.075f);
				case 9:
				return Utilities.screenScaleRect(0.65f, 0.75f, 0.08f, 0.075f);
			}
		}
		if (type == "labels")
		{
			switch(index)
			{
				case 0:
				return Utilities.screenScaleRect(0.25f, 0.15f, 0.2f, 0.15f);
				case 1:
				return Utilities.screenScaleRect(0.35f, 0.15f, 0.2f, 0.15f);
				case 2:
				return Utilities.screenScaleRect(0.45f, 0.15f, 0.2f, 0.15f);
				case 3:
				return Utilities.screenScaleRect(0.55f, 0.15f, 0.2f, 0.15f);
				case 4:
				return Utilities.screenScaleRect(0.65f, 0.15f, 0.2f, 0.15f);
				case 5:
				return Utilities.screenScaleRect(0.25f, 0.50f, 0.2f, 0.15f);
				case 6:
				return Utilities.screenScaleRect(0.35f, 0.50f, 0.2f, 0.15f);
				case 7:
				return Utilities.screenScaleRect(0.45f, 0.50f, 0.2f, 0.15f);
				case 8:
				return Utilities.screenScaleRect(0.55f, 0.50f, 0.2f, 0.15f);
				case 9:
				return Utilities.screenScaleRect(0.65f, 0.50f, 0.2f, 0.15f);
			}
		}
		return Utilities.screenScaleRect(0f, 0f, 0f, 0f);
	}

	public void OnGUI()
	{
		Rect position = new Rect(((float)Screen.width - 2048f * (float)Screen.height / 1154f) / 2f, 0f, 2048f * (float)Screen.height / 1154f, Screen.height);
		GUI.DrawTexture(position, catFon, ScaleMode.StretchToFill);
		GUI.DrawTexture(Utilities.screenScaleRect(0.19f, 0.03f, 0.6f, 0.9f), menuBG);
		GUI.DrawTexture(Utilities.screenScaleRect(0.1f, 0.035f, 0.1f, 0.075f), coinsLabel);
		GUI.Label(Utilities.screenScaleRect(0.1475f, 0.06f, 0.2f, 0.15f), "" + Defs.CoinsAmount, biggerLabelThingy);
		if (Debug.isDebugBuild || Application.platform == RuntimePlatform.WindowsEditor)
		{
			if (GUI.Button(Utilities.screenScaleRect(0.8f, 0.8f, 0.12f, 0.1f), string.Empty, blankButton))
			{
				Defs.CoinsAmount += 10000;
			}
		}
		if (!popup)
		{
			for (int i = 0; i < weaponsInThisPage(); i++)
			{
				GUI.DrawTexture(FindRects("icon", i), currentWeaponTextures()[i], ScaleMode.StretchToFill);
			}
			for (int i = 0; i < weaponsInThisPage(); i++)
			{
				GUI.Label(FindRects("labels", i), currentWeaponNames2()[i].Replace("newline", "\n"), labelThingy);
			}
			if (GUI.Button(Utilities.screenScaleRect(0.515f, 0.05f, 0.03f, 0.05f), string.Empty, arrow1))
			{
				BS();
				if (weaponIndex >= WeaponLists.Count - 1)
				{
					weaponIndex = 0;
					return;
				}
				if (weaponIndex < WeaponLists.Count - 1)
				{
					weaponIndex++;
				}
			}
			if (GUI.Button(Utilities.screenScaleRect(0.415f, 0.05f, 0.03f, 0.05f), string.Empty, arrow2))
			{
				BS();
				if (weaponIndex < 1)
				{
					weaponIndex = WeaponLists.Count - 1;
					return;
				}
				weaponIndex--;
			}
			for (int i = 0; i < weaponsInThisPage(); i++)
			{
				if (GUI.Button(FindRects("set", i), string.Empty, setButton))
				{
					BS();
					//CategorySet(catType, currentWeaponNames()[i]);
					//Debug.LogWarning("Sucessfully Set " + catType + " To " + currentWeaponNames()[i]);
					SetPopup(currentWeaponNames()[i], i, currentWeaponNames2()[i]);
				}
			}
			if (GUI.Button(Utilities.screenScaleRect(0.1f, 0.8f, 0.12f, 0.1f), string.Empty, back))
			{
				BS();
				GUIHelper.DrawLoading();
				Application.LoadLevel("ArmoryScene");
			}
		}
		if (popup)
		{
			GUI.DrawTexture(Utilities.screenScaleRect(0.185f, 0.15f, 0.65f, 0.75f), popupBG);
			if (!currentWeaponNeedsBuy || Bought(popupWeapon))
			{
				if (GUI.Button(Utilities.screenScaleRect(0.625f, 0.72f, 0.15f, 0.125f), string.Empty, setButton))
				{
					BS();
					CategorySet(catType, popupWeapon);
					Debug.LogWarning("Sucessfully Set " + catType + " To " + popupWeapon);
					popup = false;
				}
			}
			else
			{
				if (GUI.Button(Utilities.screenScaleRect(0.625f, 0.72f, 0.15f, 0.125f), string.Empty, blankButton))
				{
					BS();
					Buy(currentWeaponPrice, popupWeapon);
				}
			}
			if (GUI.Button(Utilities.screenScaleRect(0.25f, 0.72f, 0.15f, 0.125f), string.Empty, closeButton))
			{
				BS();
				popup = false;
			}
			GUI.Label(Utilities.screenScaleRect(0.45f, 0.2f, 0.5f, 0.5f), popupName, commentLabelThingy);
			GUI.Label(Utilities.screenScaleRect(0.275f, 0.55f, 0.5f, 0.5f), currentWeaponComment(), commentLabelThingy);
			if (currentWeaponNeedsBuy && !Bought(popupWeapon))
			{
				GUI.Label(Utilities.screenScaleRect(0.7f, 0.7545f, 0.5f, 0.5f), "" + currentWeaponPrice, evenBiggerLabelThingy);
				GUI.DrawTexture(Utilities.screenScaleRect(0.658f, 0.7535f, 0.03f, 0.05f), coin);
			}
			GUI.DrawTexture(Utilities.screenScaleRect(0.3f, 0.3f, 0.3f, 0.23f), textureToDraw(), ScaleMode.StretchToFill);
			GUI.Label(Utilities.screenScaleRect(0.635f, 0.4f, 0.3f, 0.3f), currentWeaponStats().Replace("newline", "\n"), commentLabelThingy);
			if (currentWeaponHasUpgrades())
			{
				if (GUI.Button(Utilities.screenScaleRect(0.46f, 0.25f, 0.075f, 0.075f), string.Empty, next))
				{
					BS();
					if (currentUpgradeIndex() >= Utilities.LoadObject("upgrades/" + WeaponLists[weaponIndex].weaponNames[popupIndex]).GetComponent<UpgradeInfo>().weaponNames.Length - 1)
					{
						popupWeapon =  Utilities.LoadObject("Weapons/" + Utilities.LoadObject("upgrades/" + WeaponLists[weaponIndex].weaponNames[popupIndex]).GetComponent<UpgradeInfo>().weaponNames[0]).tag;
						popupName =  Utilities.LoadObject("Weapons/" + Utilities.LoadObject("upgrades/" + WeaponLists[weaponIndex].weaponNames[popupIndex]).GetComponent<UpgradeInfo>().weaponNames[0]).GetComponent<WeaponSounds>().weaponName;
						return;
					}
					if (currentUpgradeIndex() < WeaponLists.Count - 1)
					{
						popupWeapon =  Utilities.LoadObject("Weapons/" + Utilities.LoadObject("upgrades/" + WeaponLists[weaponIndex].weaponNames[popupIndex]).GetComponent<UpgradeInfo>().weaponNames[currentUpgradeIndex() + 1]).tag;
						popupName =  Utilities.LoadObject("Weapons/" + Utilities.LoadObject("upgrades/" + WeaponLists[weaponIndex].weaponNames[popupIndex]).GetComponent<UpgradeInfo>().weaponNames[currentUpgradeIndex() + 1]).GetComponent<WeaponSounds>().weaponName;
					}	
				}
			}
		}
	}
	public static void CategorySet(string catnumber, string weapon)
	{
		PlayerPrefs.SetString(catnumber, weapon);
	} 
	//public int weaponIndex()
	//{
	//	for (int i = 0; i < weapons.Length; i++)
	//	{
	//		if (currentWeapon == weapons[i])
	//		{
	//			return i;
	//		}
	//	}
	//	return 0;
	//}
}
