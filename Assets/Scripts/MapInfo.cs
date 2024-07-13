using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
	[System.Serializable]
	public class Map
	{
		public string sceneName, mapName;

		public Texture icon;

		public int backgroundMusic;

		public Quaternion rotation;

		public Vector3 position;

		public bool hasVarY;
		public bool negateAbominatorDamage;
	}

	public static MapInfo Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Resources.Load<GameObject>("MapInfo").GetComponent<MapInfo>();
			}
			return instance;
		}
	}

	public List<Map> CurrentMapsList
	{
		get
		{
			return (prefs.GetInt("COOP", 0) == 1 ? coopMaps : deathmatchMaps);
		}
	}

	private static MapInfo instance;

	public List<Map> deathmatchMaps, coopMaps;
}
