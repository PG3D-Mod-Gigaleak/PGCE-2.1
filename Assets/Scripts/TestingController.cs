using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingController : MonoBehaviour
{
	public void EnterSurvival()
	{
		loading = true;
		PlayerPrefs.SetInt("MultyPlayer", 0);
		PlayerPrefs.SetInt("COOP", 0);
		GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().Reset();
		PlayerPrefs.SetFloat(Defs.CurrentHealthSett, Player_move_c.MaxPlayerHealth);
		PlayerPrefs.SetFloat(Defs.CurrentArmorSett, 0f);
		FlurryPluginWrapper.LogSurvivalModePress();
		Application.LoadLevel("LoadingNoWait");
	}

	public void EnterCOOP()
	{
		loading = true;
		PlayerPrefs.SetString("TypeConnect", "inet");
		PlayerPrefs.SetInt("COOP", 1);
		PlayerPrefs.SetInt("MultyPlayer", 1);
		GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().Reset();
		FlurryPluginWrapper.LogCOOPModePress();
		Application.LoadLevel("NewMapList");
	}

	public void EnterDictionary()
	{
		loading = true;
		Application.LoadLevel("Encyclopedia");
	}

	public void EnterMenu()
	{
		loading = true;
		Application.LoadLevel(Defs.CurrentMainMenuScene);
	}

	private bool loading;

	private void OnGUI()
	{
		if (loading)
		{
			GUIHelper.DrawLoading();
		}
	}
}
