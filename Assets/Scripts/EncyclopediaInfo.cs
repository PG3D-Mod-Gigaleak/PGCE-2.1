using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncyclopediaInfo : MonoBehaviour
{
	[System.Serializable]
	public class Entry
	{
		public string name;

		public string description;

		public string strategy;
	}

	public List<Entry> entries;

	private static EncyclopediaInfo _instance;

	public static EncyclopediaInfo instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Utilities.GetClass<EncyclopediaInfo>("EncyclopediaInfo");
			}
			return _instance;
		}
	}

	public static Entry GetEntry(string enemyName)
	{
		return instance.entries.Find(x => x.name == enemyName);
	}
}
