using UnityEngine;

public class Weapon
{
	public GameObject weaponPrefab;

	public CategoryType category
	{
		get
		{
			return weaponPrefab.GetComponent<WeaponSounds>().category;
		}
	}

	public int currentAmmoInBackpack;
	private int pcaib;
	public int currentAmmoInClip
	{
		get
		{
			if (weaponPrefab.GetComponent<WeaponSounds>().infiniteAmmo)
			{
				return 5;
			}
			return pcaib;
		}
		set
		{
			pcaib = value;
		}
	}
}
