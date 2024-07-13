using System.Collections;
using UnityEngine;

public sealed class DownloadObbExample : MonoBehaviour
{
	private string expPath;

	private string logtxt;

	private bool alreadyLogged;

	private string nextScene = "SceneMenu";

	private bool downloadStarted;

	public Texture2D background;

	public GUISkin mySkin;

	private void log(string t)
	{
		logtxt = logtxt + t + "\n";
		MonoBehaviour.print("MYLOG " + t);
	}

	private void OnGUI()
	{
		GUI.skin = mySkin;
		GUI.DrawTexture(new Rect(0f, 0f, background.width, background.height), background);
			if (true)
			{
				StartCoroutine(loadLevel());
			}
	}

	protected IEnumerator loadLevel()
	{
		Application.LoadLevel(nextScene);
		yield return new WaitForSeconds(0.00001f);
	}
}
