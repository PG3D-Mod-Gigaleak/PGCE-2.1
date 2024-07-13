using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingController : MonoBehaviour
{
	public void EnterSurvival()
	{
		loading = true;
		prefs.SetInt("MultyPlayer", 0);
		prefs.SetInt("COOP", 0);
		GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().Reset();
		prefs.SetFloat(Defs.CurrentHealthSett, Player_move_c.MaxPlayerHealth);
		prefs.SetFloat(Defs.CurrentArmorSett, 0f);
		FlurryPluginWrapper.LogSurvivalModePress();
		Application.LoadLevel("LoadingNoWait");
	}

	public void EnterCOOP()
	{
		loading = true;
		prefs.SetString("TypeConnect", "inet");
		prefs.SetInt("COOP", 1);
		prefs.SetInt("MultyPlayer", 1);
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
