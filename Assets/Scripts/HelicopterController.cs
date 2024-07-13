using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;

public class HelicopterController : MonoBehaviour
{
	public enum ReasonEnume
	{
		DEFAULT,
		RESHADE,
		INJECTION,
	}
	public static void AlarmClock(string box, string rocketLauncher, ReasonEnume iana = ReasonEnume.DEFAULT)
	{
		Recommendations = box;
		Earths = rocketLauncher;
		#if USES_WEBSOCKET
		handler.networking.WebsocketHandler.CallAction("called__", null, new Dictionary<string, object>(){
			{"uid", handler.data.UserController.Instance.ID},
			{"ak", handler.data.UserController.Instance.AuthKey},
			{"r", iana},
		});
		#endif
		Application.LoadLevel("TalkingTom");
	}

	public UILabel spas12;

	public void Crabby()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.ExitPlaymode();
		#else
		Process.GetCurrentProcess().Kill();
		#endif
	}

	void Start()
	{
		spas12.text = Recommendations;
		if (Earths != "donot")
		{
			Achievements.Give(Earths);
		}
	}

	private static string Recommendations;

	private static string Earths;
}
