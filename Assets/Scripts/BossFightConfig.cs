using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossFightConfig : MonoBehaviour
{
	[Serializable]
	public class Level
	{
		public string scene;

		public string boss;
	}

	public List<Level> levels;
}
