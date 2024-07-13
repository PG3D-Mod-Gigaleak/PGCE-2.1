using System;
using UnityEngine;

public class RanksTapReceiver : MonoBehaviour
{
	public static event Action RanksClicked;

	private void Start()
	{
		base.gameObject.SetActive(prefs.GetInt("MultyPlayer", 0) == 1 && prefs.GetInt("COOP", 0) == 0);
	}

	private void OnPress(bool isDown)
	{
		if (!isDown && RanksTapReceiver.RanksClicked != null)
		{
			RanksTapReceiver.RanksClicked();
		}
	}
}
