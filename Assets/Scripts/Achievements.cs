using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : ScriptableObject {
	[System.Serializable]
	public struct Achievement {
		public string name, id, description;
		public Texture2D icon;
		public bool hideName, hideDescription, hideIcon, hideAchievement, fakeAchievement;
	}
	public Achievement[] achievements;
	public Achievement GetAchievement(string name) {
		foreach (Achievement achievement in achievements) {
			if (achievement.id == name) {
				return achievement;
			}
		}
		return default(Achievement);
	}

	public static void Give(string achievement)
	{
		AchievementNGUI.instance.GiveAchievement(achievement);
	}
}