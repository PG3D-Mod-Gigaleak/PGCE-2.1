using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ZombieCreator : MonoBehaviour
{
	private GameObject[] _teleports;

	public bool bossShowm;

	public List<SurvivalConfig.Enemy> zombiePrefabs = new List<SurvivalConfig.Enemy>();

	private bool _isMultiplayer;

	private int _numOfLiveZombies;

	private int _numOfDeadZombies;

	private int _numOfDeadZombsSinceLastFast;

	public float curInterval = 10f;

	private GameObject[] _enemyCreationZones;

	private List<SurvivalConfig.Enemy[]> _enemies = new List<SurvivalConfig.Enemy[]>();

	public int NumOfLiveZombies
	{
		get
		{
			return _numOfLiveZombies;
		}
		set
		{
			_numOfLiveZombies = value;
		}
	}

	public int NumOfDeadZombies
	{
		get
		{
			return _numOfDeadZombies;
		}
		set
		{
			if (bossShowm)
			{
				bossShowm = false;
				GameObject[] teleports = _teleports;
				foreach (GameObject gameObject in teleports)
				{
					gameObject.SetActive(true);
				}
				return;
			}
			int num = value - _numOfDeadZombies;
			_numOfDeadZombies = value;
			NumOfLiveZombies -= num;
			_numOfDeadZombsSinceLastFast += num;
			if (_numOfDeadZombsSinceLastFast == 12)
			{
				if (curInterval > 5f)
				{
					curInterval -= 5f;
				}
				_numOfDeadZombsSinceLastFast = 0;
			}
			if (_numOfDeadZombies < GlobalGameController.EnemiesToKill)
			{
				return;
			}
			if (GlobalGameController.currentLevel == GlobalGameController.levelMapping[0])
			{
				GameObject[] teleports2 = _teleports;
				foreach (GameObject gameObject2 in teleports2)
				{
					gameObject2.SetActive(true);
				}
			}
			else
			{
				_createBoss();
			}
		}
	}

	private void Awake()
	{
		if (prefs.GetInt("MultyPlayer") != 1)
		{
			//for (int i = 0; i < Defs.numOfLevels; i++)
			//{
			//	_enemies.Add(Defs.GetEnemiesFromRandomByIndex(i));
			//}
			//string[] array = null;
			//if (!(GlobalGameController.currentLevel != GlobalGameController.levelMapping[0]))
			//{
			//	zombiePrefabs.Add((new SurvivalConfig.Enemy() {name = "Zombie_Default", prefab = Resources.Load<GameObject>("enemies/Enemy1_go"), skin = Resources.Load<GameObject>("enemies/Enemy1_go").GetComponent<MeshRenderer>().material.GetTexture("_MainTex")}));
			//	return;
			//}
			//foreach (SurvivalConfig.Enemy[] enemies in _enemies)
			//{
			//	zombiePrefabs.AddRange(enemies);
			//}
			zombiePrefabs.AddRange(Defs.GetEnemiesFromRandomByIndex(GlobalGameController.previousLevel));
		}
	}

	private void Start()
	{
		if (prefs.GetInt("MultyPlayer") == 1)
		{
			_isMultiplayer = true;
		}
		else
		{
			_isMultiplayer = false;
		}
		_teleports = GameObject.FindGameObjectsWithTag("Portal");
		GameObject[] teleports = _teleports;
		foreach (GameObject gameObject in teleports)
		{
			if (GlobalGameController.AllLevelsCompleted <= 0 || GlobalGameController.currentLevel != GlobalGameController.levelMapping[0])
			{
				gameObject.SetActive(false);
			}
		}
		if (!_isMultiplayer)
		{
			_enemyCreationZones = GameObject.FindGameObjectsWithTag("EnemyCreationZone");
			curInterval = Mathf.Max(1f, curInterval - 2f * (float)GlobalGameController.AllLevelsCompleted);
		}
	}

	public void BeganCreateEnemies()
	{
		StartCoroutine(AddZombies());
	}

	private void Update()
	{
	}

	private IEnumerator AddZombies()
	{
		float halfLLength = 17f;
		float radius = 2.5f;
		do
		{
			int numOfZombsToAdd3 = GlobalGameController.ZombiesInWave;
			numOfZombsToAdd3 = Mathf.Min(numOfZombsToAdd3, GlobalGameController.SimultaneousEnemiesOnLevelConstraint - NumOfLiveZombies);
			numOfZombsToAdd3 = Mathf.Min(numOfZombsToAdd3, GlobalGameController.EnemiesToKill - (NumOfDeadZombies + NumOfLiveZombies));
			for (int i = 0; i < numOfZombsToAdd3; i++)
			{
				int typeOfZomb = Random.Range(0, zombiePrefabs.Count);
				GameObject spawnZone = _enemyCreationZones[Random.Range(0, _enemyCreationZones.Length)];
				Object.Instantiate(position: _createPos(spawnZone), original: zombiePrefabs[typeOfZomb].prefab, rotation: Quaternion.identity).name = zombiePrefabs[typeOfZomb].name;
			}
			yield return new WaitForSeconds(curInterval);
		}
		while (NumOfDeadZombies + NumOfLiveZombies < GlobalGameController.EnemiesToKill);
	}

	private Vector3 _createPos(GameObject spawnZone)
	{
		BoxCollider component = spawnZone.GetComponent<BoxCollider>();
		Vector2 vector = new Vector2(component.size.x * spawnZone.transform.localScale.x, component.size.z * spawnZone.transform.localScale.z);
		Rect rect = new Rect(spawnZone.transform.position.x - vector.x / 2f, spawnZone.transform.position.z - vector.y / 2f, vector.x, vector.y);
		Vector3 result = new Vector3(rect.x + Random.Range(0f, rect.width), (!Defs.levelsWithVarY.Contains((GlobalGameController.currentLevel != GlobalGameController.levelMapping[0]) ? (GlobalGameController.previousLevel + 1) : 0)) ? 0f : spawnZone.transform.position.y, rect.y + Random.Range(0f, rect.height));
		return result;
	}

	private void _createBoss()
	{
		GameObject gameObject = null;
		float num = float.PositiveInfinity;
		GameObject gameObject2 = GameObject.FindGameObjectWithTag("Player");
		if (!gameObject2)
		{
			return;
		}
		GameObject[] enemyCreationZones = _enemyCreationZones;
		foreach (GameObject gameObject3 in enemyCreationZones)
		{
			float num2 = Vector3.Distance(gameObject2.transform.position, gameObject3.transform.position);
			float num3 = Mathf.Abs(gameObject2.transform.position.y - gameObject3.transform.position.y);
			if (num2 > 15f && num2 < num && num3 < 2.5f)
			{
				num = num2;
				gameObject = gameObject3;
			}
		}
		if (!gameObject)
		{
			gameObject = _enemyCreationZones[0];
		}
		Vector3 position = _createPos(gameObject);
		string path = "Boss" + Defs.GetBossFromRandomByIndex(((GlobalGameController.currentLevel != GlobalGameController.levelMapping[0]) ? (GlobalGameController.previousLevel + 1) : 0));
		GameObject original = Resources.Load(Path.Combine("Bosses", path)) as GameObject;
		Object.Instantiate(original, position, Quaternion.identity);
		bossShowm = true;
	}
}
