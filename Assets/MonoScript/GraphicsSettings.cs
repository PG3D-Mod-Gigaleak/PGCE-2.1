using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsSettings : MonoBehaviour
{

public Texture fon;

public GUIStyle back;

public GUIStyle ninteentwennybytenatey;

public GUIStyle onethousandtwennyfourbysixhunrded;

public GUIStyle eighthunfedbysixdungred;

public GUIStyle fullscreenthe;

void OnGUI()
{
	float num = (float)Screen.height / 768f;
	GUI.DrawTexture(new Rect((float)Screen.width / 2f - 683f * num, 0f, 1366f * num, Screen.height), fon);

		if (GUI.Button(Utilities.screenScaleRect(0.235f, 0.5f, 0.12f, 0.12f), string.Empty, ninteentwennybytenatey))
		{
			Screen.SetResolution(1920, 1080, false);
		}

		if (GUI.Button(Utilities.screenScaleRect(0.410f, 0.5f, 0.12f, 0.12f), string.Empty, onethousandtwennyfourbysixhunrded))
		{
			Screen.SetResolution(1024, 600, false);
		}

		if (GUI.Button(Utilities.screenScaleRect(0.410f, 0.7f, 0.12f, 0.12f), string.Empty, fullscreenthe))
		{
			if (Screen.fullScreen)
			{
			Screen.fullScreen = false;
			}
			else
			{
			Screen.fullScreen = true;
			}
		}

		if (GUI.Button(Utilities.screenScaleRect(0.605f, 0.5f, 0.12f, 0.12f), string.Empty, eighthunfedbysixdungred))
		{
			Screen.SetResolution(800, 600, false);
		}

		if (GUI.Button(Utilities.screenScaleRect(0.1f, 0.8f, 0.12f, 0.1f), string.Empty, back))
		{
			GUIHelper.DrawLoading();
			Application.LoadLevel(Defs.CurrentMainMenuScene);
		}


}

}
