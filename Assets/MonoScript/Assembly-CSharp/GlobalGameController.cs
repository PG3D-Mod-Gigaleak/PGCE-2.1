using System.Collections.Generic;
using UnityEngine;

public class GlobalGameController
{
	public static readonly int NumOfLevels;

	private static int _currentLevel;

	private static int _allLevelsCompleted;

	private static int score;

	public static bool showTableMyPlayer;

	public static bool isFullVersion;

	public static Vector3 posMyPlayer;

	public static Quaternion rotMyPlayer;

	public static float healthMyPlayer;

	public static int numOfCompletedLevels;

	public static int totalNumOfCompletedLevels;

	public static int _currentIndexInMapping;

	public static List<int> levelMapping;

	public static int coinsBase;

	public static int coinsBaseAdding;

	public static int levelsToGetCoins;

	public static string AppVersion;

	public static int currentLevel
	{
		get
		{
			return _currentLevel;
		}
		set
		{
			_currentLevel = value;
		}
	}

	public static int AllLevelsCompleted
	{
		get
		{
			return _allLevelsCompleted;
		}
		set
		{
			_allLevelsCompleted = value;
		}
	}

	public static int previousLevel
	{
		get
		{
			if (_currentIndexInMapping > 0)
			{
				return levelMapping[_currentIndexInMapping - 1];
			}
			return 0;
		}
	}

	public static int ZombiesInWave
	{
		get
		{
			return 4;
		}
	}

	public static int EnemiesToKill
	{
		get
		{
			return ((currentLevel != levelMapping[0]) ? 30 : 5) + 30 * AllLevelsCompleted;
		}
	}

	public static int Score
	{
		get
		{
			return score;
		}
		set
		{
			score = value;
		}
	}

	public static int SimultaneousEnemiesOnLevelConstraint
	{
		get
		{
			return 20;
		}
	}

	static GlobalGameController()
	{
		NumOfLevels = Defs.numOfLevels + 1;
		_currentLevel = -1;
		_allLevelsCompleted = 0;
		score = 0;
		showTableMyPlayer = false;
		isFullVersion = true;
		numOfCompletedLevels = 0;
		totalNumOfCompletedLevels = 0;
		_currentIndexInMapping = 0;
		levelMapping = new List<int>();
		coinsBase = 1;
		coinsBaseAdding = 0;
		levelsToGetCoins = 1;
		AppVersion = "3.11";
		for (int i = 0; i < NumOfLevels - 1; i++)
		{
			levelMapping.Add(i);
		}
	}

	private static void Swap(IList<int> list, int indexA, int indexB)
	{
		int value = list[indexA];
		list[indexA] = list[indexB];
		list[indexB] = value;
	}

	public static void reGenerateLevelMapping()
	{
		reGenerateLevelMappingCementeryOrEgyptFirst();
	}

	public static void reGenerateLevelMappingCementeryOrEgyptFirst()
	{
		int num = levelMapping[levelMapping.Count - 1];
		int num2 = 0;
		int num3 = 1;
		if (num == num2)
		{
			int num4 = levelMapping.IndexOf(num3);
			if (num4 != 0)
			{
				Swap(levelMapping, 0, num4);
			}
		}
		else if (num == num3)
		{
			int num5 = levelMapping.IndexOf(num2);
			if (num5 != 0)
			{
				Swap(levelMapping, 0, num5);
			}
		}
		else
		{
			int item = Random.Range(0, 2);
			int num6 = levelMapping.IndexOf(item);
			if (num6 != 0)
			{
				Swap(levelMapping, 0, num6);
			}
		}
		_MixLevelMapping(1);
	}

	public static void reGenerateLevelMapping___Random()
	{
		int num = levelMapping[levelMapping.Count - 1];
		do
		{
			_MixLevelMapping();
		}
		while (num == levelMapping[0]);
	}

	private static void _MixLevelMapping(int startIndex = 0)
	{
		for (int i = 0; i < 100; i++)
		{
			int num = Random.Range(startIndex, levelMapping.Count);
			int num2 = num;
			while (num == num2)
			{
				num2 = Random.Range(startIndex, levelMapping.Count);
			}
			Swap(levelMapping, num, num2);
		}
	}

	public static void decrementLevel()
	{
		if (_currentIndexInMapping > 0)
		{
			_currentIndexInMapping--;
			currentLevel = levelMapping[_currentIndexInMapping];
		}
	}

	public static void setLevelToFirstInMapping()
	{
		_currentIndexInMapping = 0;
		currentLevel = levelMapping[_currentIndexInMapping];
	}

	public static void incrementLevel()
	{
		if (_currentIndexInMapping <= levelMapping.Count - 1)
		{
			_currentIndexInMapping++;
			if (levelMapping.Count >= _currentIndexInMapping + 1)
			{
				currentLevel = levelMapping[_currentIndexInMapping];
			}
		}
	}

	public static void ResetParameters()
	{
		reGenerateLevelMapping();
		currentLevel = ((prefs.GetInt(Defs.TrainingComplSett, 0) != 1) ? 101 : levelMapping[0]);
		AllLevelsCompleted = 0;
		numOfCompletedLevels = -1;
		totalNumOfCompletedLevels = -1;
		_currentIndexInMapping = 0;
	}
}
