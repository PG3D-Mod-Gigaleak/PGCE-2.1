using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeSurvivalConfig : MonoBehaviour
{
	[Serializable]
	public class Maps
	{
		public MapSettings[] mapSettings;
	}

	[Serializable]
	public class MapSettings
	{
		public string MyName;

		public string[] myEnemies = new string[10] { "1", "15", "14", "2", "3", "9", "11", "12", "10", "16" };

		public SurvivalConfig.Enemy[] enemies;

		public string myPath = "enemyskins/coop/";

		public float MaxTime = 300f;

		public float ZombieSpawnMultiplier = 1f;

		public BossSpawn[] bossSpawns;

		public enum Difficulty
		{
			easy = 0,

			normal = 1,

			hard = 2,

			veryHard = 3,

			extreme = 4,

			insane = 5,

			impossible = 6,

			masochist = 7
		};

		public int MaxZombies = 15;

		public Difficulty difficulty;
	}

	[Serializable]
	public class BossSpawn
	{
		public int bossName;

		public float timeSpawn;
	}

	public Maps maps;

	public string[] coopMaps;
}
