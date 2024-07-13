using System;
using System.Collections;
using UnityEngine;

public class WeaponsComparer : IComparer
{
	private static int baseLngth = "Weapon".Length;

	private static string[] multiplayerWeaponsOrd = new string[5]
	{
		//"Weapon1",
		//"Weapon2",
		//"Weapon3",
		//"Weapon9",
		//WeaponManager.GoldenEagleWeaponName,
		//WeaponManager.BerettaWN,
		//WeaponManager.GlockWN,
		//WeaponManager.CombatRifleWeaponName,
		//WeaponManager.MinigunWN,
		//WeaponManager.SpasWeaponName,
		//WeaponManager.FAMASWN,
		//WeaponManager.MagicBowWeaponName,
		//WeaponManager.LaserRifleWN,
		//WeaponManager.LightSwordWN,
		//"Weapon6",
		//"Weapon7",
		//WeaponManager.GoldenAxeWeaponnName,
		//WeaponManager.ChainsawWN,
		//WeaponManager.ScytheWN,
		//WeaponManager.MaceWN,
		//WeaponManager.ShovelWN,
		//WeaponManager.CrossbowWN,
		//WeaponManager.Sword_2_WN,
		//WeaponManager.HammerWN,
		//WeaponManager.StaffWN
		prefs.GetString("cat1"),
		prefs.GetString("cat5"),
		prefs.GetString("cat2"),
		prefs.GetString("cat4"),
		prefs.GetString("cat3")
	};

	public int Compare(object x, object y)
	{
		string name = ((Weapon)x).weaponPrefab.name;
		string name2 = ((Weapon)y).weaponPrefab.name;
		if (prefs.GetInt("MultyPlayer", 0) == 1)
		{
			return Array.IndexOf(multiplayerWeaponsOrd, name2).CompareTo(Array.IndexOf(multiplayerWeaponsOrd, name));
		}
		name = name.Substring(baseLngth);
		name2 = name2.Substring(baseLngth);
		int num = int.Parse(name);
		int num2 = int.Parse(name2);
		return num - num2;
	}
}
