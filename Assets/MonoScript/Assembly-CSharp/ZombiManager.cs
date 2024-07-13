using System.Collections.Generic;
using UnityEngine;

public class ZombiManager : MonoBehaviour
{
	public float timeGame;

	public float nextTimeSynch;

	public float nextAddZombi;

	public List<SurvivalConfig.Enemy> zombiePrefabs = new List<SurvivalConfig.Enemy>();

	private List<string[]> _enemies = new List<string[]>();

	private GameObject[] _enemyCreationZones;

	public bool startGame;

	private float timeSinceLastBossSpawn;

	public float maxTimeGame
	{
		get
		{
			return Defs.GetThisCoopMaxTime(Application.loadedLevelName);
		}
	}

	public PhotonView photonView;

	private void Awake()
	{
		zombiePrefabs.AddRange(Defs.GetEnemiesFromThisCoopLevel(Application.loadedLevelName));
	}

	private void Start()
	{
		nextAddZombi = 5f / Defs.GetThisCoopZombieSpawnMult(Application.loadedLevelName);
		_enemyCreationZones = GameObject.FindGameObjectsWithTag("EnemyCreationZone");
		photonView = PhotonView.Get(this);
	}

	[PunRPC]
	private void synchTime(float _time)
	{
		timeGame = _time;
	}

	private void Update()
	{
		if (timeSinceLastBossSpawn > 0)
		{
			timeSinceLastBossSpawn -= Time.deltaTime;
		}
		float theMomer = timeGame / maxTimeGame * 100f;
		foreach (TimeSurvivalConfig.BossSpawn bossSpawn in Defs.GetThisCoopBossSpawns(Application.loadedLevelName))
		{
			if (theMomer >= bossSpawn.timeSpawn && theMomer < bossSpawn.timeSpawn + 0.1f && timeSinceLastBossSpawn <= 0f)
			{
				addBoss(bossSpawn.bossName);
				timeSinceLastBossSpawn = 1f;
			}
		}
		if (!startGame && GameObject.FindGameObjectsWithTag("Player").Length > 0)
		{
			startGame = true;
			timeGame = 0f;
			nextTimeSynch = 0f;
			nextAddZombi = 0f;
		}
		if (startGame && GameObject.FindGameObjectsWithTag("Player").Length == 0)
		{
			startGame = false;
			timeGame = 0f;
			nextTimeSynch = 0f;
			nextAddZombi = 0f;
		}
		if (!startGame)
		{
			return;
		}
		if (startGame)
		{
			timeGame += Time.deltaTime;
		}
		PhotonView photonView = PhotonView.Get(this);
		if (photonView.isMine && timeGame > nextTimeSynch)
		{
			photonView.RPC("synchTime", PhotonTargets.Others, timeGame);
			nextTimeSynch = timeGame + 3f;
		}
		if (photonView.isMine && timeGame > maxTimeGame)
		{
			startGame = false;
			timeGame = 0f;
			GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
			float num = -100f;
			string text = string.Empty;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].GetComponent<NetworkStartTable>().score > num)
				{
					num = array[i].GetComponent<NetworkStartTable>().score;
					text = array[i].GetComponent<NetworkStartTable>().NamePlayer;
				}
			}
			photonView.RPC("win", PhotonTargets.All, text);
		}
		if (timeGame > nextAddZombi && photonView.isMine && GameObject.FindGameObjectsWithTag("Enemy").Length < Defs.GetMaxEnemiesFromThisCoopLevel(Application.loadedLevelName))
		{
			float num2 = 4f / Defs.GetThisCoopZombieSpawnMult(Application.loadedLevelName);
			if (timeGame > maxTimeGame * 0.4f)
			{
				num2 = 3f / Defs.GetThisCoopZombieSpawnMult(Application.loadedLevelName);
			}
			if (timeGame > maxTimeGame * 0.8f)
			{
				num2 = 2f / Defs.GetThisCoopZombieSpawnMult(Application.loadedLevelName);
			}
			nextAddZombi += num2;
			addZombi();
		}
	}

	[PunRPC]
	private void win(string _winer)
	{
		GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().myTable.GetComponent<NetworkStartTable>().win(_winer);
	}

	private void addZombi()
	{
		GameObject gameObject = _enemyCreationZones[Random.Range(0, _enemyCreationZones.Length)];
		BoxCollider component = gameObject.GetComponent<BoxCollider>();
		Vector2 vector = new Vector2(component.size.x * gameObject.transform.localScale.x, component.size.z * gameObject.transform.localScale.z);
		Rect rect = new Rect(gameObject.transform.position.x - vector.x / 2f, gameObject.transform.position.z - vector.y / 2f, vector.x, vector.y);
		Vector3 vector2 = new Vector3(rect.x + Random.Range(0f, rect.width), (!Defs.levelsWithVarY.Contains(GlobalGameController.currentLevel)) ? 0f : gameObject.transform.position.y, rect.y + Random.Range(0f, rect.height));
		int num = 0;
		float num2 = timeGame / maxTimeGame * 100f;
		if (num2 < 15f)
		{
			num = Random.Range(0, 3);
		}
		if (num2 >= 15f && num2 < 30f)
		{
			num = Random.Range(0, 5);
		}
		if (num2 >= 30f && num2 < 45f)
		{
			num = Random.Range(0, 6);
		}
		if (num2 >= 45f && num2 < 60f)
		{
			num = Random.Range(3, 8);
		}
		if (num2 >= 60f && num2 < 75f)
		{
			num = Random.Range(5, 9);
		}
		if (num2 >= 75f)
		{
			num = Random.Range(5, 10);
		}
		photonView.RPC("addZombiRPC", PhotonTargets.All, num, vector2, PhotonNetwork.AllocateViewID());
	}

	private void addBoss(int boss)
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("BossSpawner");
		BoxCollider component = gameObject.GetComponent<BoxCollider>();
		Vector2 vector = new Vector2(component.size.x * gameObject.transform.localScale.x, component.size.z * gameObject.transform.localScale.z);
		Rect rect = new Rect(gameObject.transform.position.x - vector.x / 2f, gameObject.transform.position.z - vector.y / 2f, vector.x, vector.y);
		Vector3 vector2 = new Vector3(rect.x + Random.Range(0f, rect.width), (!Defs.levelsWithVarY.Contains(GlobalGameController.currentLevel)) ? 0f : gameObject.transform.position.y, rect.y + Random.Range(0f, rect.height));
		photonView.RPC("addBossRPC", PhotonTargets.All, boss, vector2, PhotonNetwork.AllocateViewID());
	}

	[PunRPC]
	private void addBossRPC(int boss, Vector3 pos, int _id)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load<GameObject>("bosses/boss" + boss), pos, Quaternion.identity);
		gameObject.GetComponent<ZombiUpravlenie>().typeZombInMas = 8055 + boss;
		PhotonView component = gameObject.GetComponent<PhotonView>();
		component.viewID = _id;
	}

	[PunRPC]
	private void addZombiRPC(int typeOfZomb, Vector3 pos, int _id)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(zombiePrefabs[typeOfZomb].prefab, pos, Quaternion.identity);
		gameObject.name = zombiePrefabs[typeOfZomb].name;
		gameObject.GetComponent<ZombiUpravlenie>().typeZombInMas = typeOfZomb;
		PhotonView component = gameObject.GetComponent<PhotonView>();
		component.viewID = _id;
	}
}
