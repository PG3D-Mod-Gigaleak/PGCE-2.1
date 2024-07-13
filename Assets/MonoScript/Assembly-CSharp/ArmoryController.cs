using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoryController : MonoBehaviour {
	
public Texture2D armoryFon;

public GUIStyle back;

public GUIStyle primary;

public GUIStyle backup;

public GUIStyle melee;

public GUIStyle special;

public GUIStyle premium;

public GUIStyle gear;

public void OnGUI()
{
	GUI.DrawTexture(Utilities.screenScaleRect(0f, 0f, 1f, 1f), armoryFon, ScaleMode.StretchToFill);
	if (GUI.Button(Utilities.screenScaleRect(0.1f, 0.8f, 0.12f, 0.1f), string.Empty, back))
	{
		GUIHelper.DrawLoading();
		Application.LoadLevel(Defs.CurrentMainMenuScene);
	}
	if (GUI.Button(Utilities.screenScaleRect(0.075f, 0.5f, 0.12f, 0.12f), string.Empty, primary))
	{
		Debug.Log("you pressed primary");
		Application.LoadLevel("PrimaryScene");
	}
	if (GUI.Button(Utilities.screenScaleRect(0.235f, 0.5f, 0.12f, 0.12f), string.Empty, backup))
	{
		Debug.Log("you pressed backup!");
		Application.LoadLevel("BackupScene");
	}
	if (GUI.Button(Utilities.screenScaleRect(0.410f, 0.5f, 0.12f, 0.12f), string.Empty, melee))
	{
		Debug.Log("you pressed melee");
		Application.LoadLevel("MeleeScene");
	}
	if (GUI.Button(Utilities.screenScaleRect(0.605f, 0.5f, 0.12f, 0.12f), string.Empty, special))
	{
		Debug.Log("you pressed special!");
		Application.LoadLevel("SpecialScene");
	}
	if (GUI.Button(Utilities.screenScaleRect(0.785f, 0.5f, 0.12f, 0.12f), string.Empty, premium))
	{
		Debug.Log("you pressed premium!");
		Application.LoadLevel("PremiumScene");
	}
	if (GUI.Button(Utilities.screenScaleRect(0.410f, 0.84f, 0.12f, 0.12f), string.Empty, gear))
	{
		Debug.Log("you pressed gear!");
		Application.LoadLevel("GearScene");
	}
}
}
