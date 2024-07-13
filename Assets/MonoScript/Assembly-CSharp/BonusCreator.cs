using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCreator : MonoBehaviour
{
	public GameObject[] bonusPrefabs;

	public float creationInterval = 10f;

	public float weaponCreationInterval = 30f;

	private Object[] weaponPrefabs;

	private int _lastWeapon = -1;

	private bool _isMultiplayer;

	private ArrayList bonuses = new ArrayList();

	private ArrayList _weapons = new ArrayList();

	public WeaponManager _weaponManager;

	private GameObject[] _bonusCreationZones;

	private ZombieCreator _zombieCreator;

	private ArrayList _weaponsProbDistr = new ArrayList();

	private float _DistrSum()
	{
		float num = 0f;
		foreach (int item in _weaponsProbDistr)
		{
			num += (float)item;
		}
		return num;
	}

	private void Awake()
	{
		if (prefs.GetInt("MultyPlayer") == 1)
		{
			_isMultiplayer = true;
		}
		else
		{
			_isMultiplayer = false;
		}
		if (!_isMultiplayer)
		{
			weaponPrefabs = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().weaponsInGame;
			UnityEngine.Object[] array = WeaponManager.WeaponPrefabs.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				GameObject gameObject = array[i] as GameObject;
				WeaponSounds component = gameObject.GetComponent<WeaponSounds>();
				_weaponsProbDistr.Add(component.Probability);
			}
		}
	}

	private void Start()
	{
		_bonusCreationZones = GameObject.FindGameObjectsWithTag("BonusCreationZone");
		_zombieCreator = base.gameObject.GetComponent<ZombieCreator>();
		_weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
	}

	private void Update()
	{
	}

	public void BeginCreateBonuses()
	{
		StartCoroutine(AddBonus());
		if (prefs.GetInt("MultyPlayer") != 1)
		{
			StartCoroutine(AddWeapon());
		}
	}

	public GameObject GetPrefabWithTag(string tagName)
	{
		UnityEngine.Object[] array = WeaponManager.WeaponPrefabs.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i] as GameObject;
			if (gameObject.CompareTag(tagName))
			{
				return gameObject;
			}
		}
		return null;
	}

	public static GameObject[] GetAllWeapons()
	{
		UnityEngine.Object[] array = WeaponManager.WeaponPrefabs.ToArray();
		List<GameObject> convertedObjects = new List<GameObject>();
		foreach (UnityEngine.Object obj in array)
		{
			convertedObjects.Add(obj as GameObject);
		}
		return convertedObjects.ToArray();
	}

	private IEnumerator AddBonus()
	{
		while (true)
		{
			yield return new WaitForSeconds(creationInterval);
			int enemiesLeft = GlobalGameController.EnemiesToKill - _zombieCreator.NumOfDeadZombies;
			if (enemiesLeft <= 0 && !_zombieCreator.bossShowm)
			{
				break;
			}
			if (prefs.GetInt("MultyPlayer") == 1)
			{
				if (prefs.GetString("TypeConnect").Equals("inet"))
				{
					GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");
					int minID = 10000000;
					GameObject[] array = _players;
					foreach (GameObject _playerTemp in array)
					{
						if (_playerTemp.transform.GetComponent<PhotonView>().viewID < minID)
						{
							minID = _playerTemp.transform.GetComponent<PhotonView>().viewID;
						}
					}
					WeaponManager _weapon = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
					if (!(_weapon.myPlayer != null) || minID != _weapon.myPlayer.GetComponent<PhotonView>().viewID)
					{
						continue;
					}
				}
				GameObject[] _bonuses = GameObject.FindGameObjectsWithTag("Bonus");
				if (_bonuses.Length > 5)
				{
					continue;
				}
			}
			GameObject spawnZone = _bonusCreationZones[Random.Range(0, _bonusCreationZones.Length)];
			BoxCollider spawnZoneCollider = spawnZone.GetComponent<BoxCollider>();
			Vector3 sz = new Vector3(spawnZoneCollider.size.x * spawnZone.transform.localScale.x, spawnZoneCollider.size.y * spawnZone.transform.localScale.y, spawnZoneCollider.size.z * spawnZone.transform.localScale.z);
			Rect zoneRect = new Rect(spawnZone.transform.position.x - sz.x / 2f, spawnZone.transform.position.z - sz.y / 2f, sz.x, sz.y);
			Vector3 pos = new Vector3(zoneRect.x + Random.Range(0f, zoneRect.width), spawnZone.transform.position.y, zoneRect.y + Random.Range(0f, zoneRect.height));
			int type = Random.Range(0, 11);
			if (!_isMultiplayer)
			{
				if (bonuses.Count < 4)
				{
					GameObject newBonus3 = (GameObject)Object.Instantiate(bonusPrefabs[(type >= 9) ? 1u : 0u], pos, Quaternion.identity);
					bonuses.Add(newBonus3);
				}
			}
			else if (prefs.GetString("TypeConnect").Equals("local"))
			{
				GameObject newBonus3 = (GameObject)PhotonNetwork.Instantiate(bonusPrefabs[(type >= 9) ? 1u : 0u].name, pos, Quaternion.identity, 0);
			}
			else
			{
				GameObject newBonus3 = (GameObject)Object.Instantiate(bonusPrefabs[(type >= 9) ? 1u : 0u], pos, Quaternion.identity);
				int _id = PhotonNetwork.AllocateViewID();
				newBonus3.GetComponent<PhotonView>().viewID = _id;
				_weaponManager.myTable.GetComponent<NetworkStartTable>().addBonus(_id, type, pos, Quaternion.identity);
			}
			if (bonuses.Count > 5 && prefs.GetInt("MultyPlayer") != 1)
			{
				Object.Destroy((GameObject)bonuses[0]);
				bonuses.RemoveAt(0);
			}
		}
	}

	public void addBonusFromPhotonRPC(int _id, int _type, Vector3 _pos, Quaternion rot)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(bonusPrefabs[(_type >= 9) ? 1u : 0u], _pos, rot);
		gameObject.GetComponent<PhotonView>().viewID = _id;
		gameObject.GetComponent<SettingBonus>().typeOfMass = _type;
	}

	private IEnumerator AddWeapon()
	{
		while (true)
		{
			yield return new WaitForSeconds(weaponCreationInterval);
			int enemiesLeft = GlobalGameController.EnemiesToKill - _zombieCreator.NumOfDeadZombies;
			if (enemiesLeft <= 0 && !_zombieCreator.bossShowm)
			{
				break;
			}
			GameObject spawnZone = _bonusCreationZones[Random.Range(0, _bonusCreationZones.Length)];
			BoxCollider spawnZoneCollider = spawnZone.GetComponent<BoxCollider>();
			Vector2 sz = new Vector2(spawnZoneCollider.size.x * spawnZone.transform.localScale.x, spawnZoneCollider.size.z * spawnZone.transform.localScale.z);
			Rect zoneRect = new Rect(spawnZone.transform.position.x - sz.x / 2f, spawnZone.transform.position.z - sz.y / 2f, sz.x, sz.y);
			Vector3 pos = new Vector3(zoneRect.x + Random.Range(0f, zoneRect.width), (!Defs.levelsWithVarY.Contains(_curLevel())) ? 0.24f : spawnZone.transform.position.y, zoneRect.y + Random.Range(0f, zoneRect.height));
			float sum = _DistrSum();
			int weaponNumber;
			do
			{
				weaponNumber = 0;
				float val = Random.Range(0f, sum);
				float curSum = 0f;
				for (int i = 0; i < _weaponsProbDistr.Count; i++)
				{
					if (val < curSum + (float)(int)_weaponsProbDistr[i])
					{
						weaponNumber = i;
						break;
					}
					curSum += (float)(int)_weaponsProbDistr[i];
				}
			}
			while (weaponNumber == _lastWeapon || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.PickWeaponName) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.SwordWeaponName) || weaponPrefabs[weaponNumber].name.Equals("Weapon9") || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.CombatRifleWeaponName) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.GoldenEagleWeaponName) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.MagicBowWeaponName) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.SpasWeaponName) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.GoldenAxeWeaponnName) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.ChainsawWN) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.FAMASWN) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.GlockWN) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.ScytheWN) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.ShovelWN) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.Sword_2_WN) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.HammerWN) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.StaffWN) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.LaserRifleWN) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.LightSwordWN) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.BerettaWN) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.MaceWN) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.CrossbowWN) || weaponPrefabs[weaponNumber].name.Equals(WeaponManager.MinigunWN));
			GameObject wp = (GameObject)weaponPrefabs[weaponNumber];
			wp.transform.rotation = Quaternion.identity;
			WeaponSounds ws = wp.GetComponent<WeaponSounds>();
			GameObject bonus = ws.bonusPrefab;
			GameObject newBonus = (GameObject)Object.Instantiate(bonus, pos, Quaternion.identity);
			newBonus.AddComponent<WeaponBonus>();
			WeaponBonus wb = newBonus.GetComponent<WeaponBonus>();
			wb.weaponPrefab = wp;
			float weaponsScale = 1f;
			newBonus.transform.localScale = ((wp.CompareTag("M249MachinegunWeapon") || wp.CompareTag("AK47")) ? new Vector3(1f, 1f, 1f) : ((!wp.CompareTag("Colt45Weapon")) ? new Vector3(weaponsScale, weaponsScale, weaponsScale) : new Vector3(1f, 1f, 1f)));
			_weapons.Add(newBonus);
			if (_weapons.Count > 5)
			{
				Object.Destroy((GameObject)_weapons[0]);
				_weapons.RemoveAt(0);
			}
		}
	}

	private int _curLevel()
	{
		return (prefs.GetInt("MultyPlayer") == 1) ? GlobalGameController.currentLevel : ((GlobalGameController.currentLevel != GlobalGameController.levelMapping[0]) ? (GlobalGameController.previousLevel + 1) : 0);
	}
}
