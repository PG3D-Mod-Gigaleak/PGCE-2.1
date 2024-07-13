using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementOnCollide : MonoBehaviour
{
	public string achievement;

	void OnTriggerEnter(Collider other)
	{
		if ((other.tag == "Player" && !Defs.isMulti) || (other.tag == "Player" && Defs.isMulti && other.gameObject == Globals.Player))
		{
			Achievements.Give(achievement);
		}
	}
}
