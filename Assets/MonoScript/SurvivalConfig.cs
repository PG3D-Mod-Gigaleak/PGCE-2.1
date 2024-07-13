using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SurvivalConfig : MonoBehaviour
{
	[Serializable]
	public class Levels
	{
		public SurvivalConfig.BaseLevel[] levels = new SurvivalConfig.BaseLevel[Defs.numOfLevels];
	}

	
	[Serializable]
	public class Enemy
	{
		public string name;

		public GameObject prefab;

		public Texture skin;
	}

	[Serializable]
	public class BaseLevel
	{
		public LevelSettings[] PossibleLevels;
	}

	[Serializable]
	public class LevelSettings
	{
		public string mySceneName;

		public string[] myEnemies;

		public SurvivalConfig.Enemy[] enemies;

		public int myMusicLevel;

		public string myLoading;

		public int myBoss;
	}

	[SerializeField]
	public Levels levels;
}
