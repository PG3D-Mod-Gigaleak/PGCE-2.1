using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AlertNGUI : MonoBehaviour
{
	public UILabel tL;
	public float t;
	private float left = 8f;
	public static void Show(string text, float timeToCrash)
	{
		GameObject j = Resources.Load<GameObject>("Alert");
		j = Instantiate(j);
		AlertNGUI x = j.GetComponent<AlertNGUI>();
		x.left = timeToCrash;
		x.tL.text = text;
		DontDestroyOnLoad(j);
	}
	private void Update()
	{
		t += Time.deltaTime;
		if (t > left)
		{
			UnityEngine.Debug.Log("goodbye cruel world");
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.ExitPlaymode();
			#else
			Process.GetCurrentProcess().Kill();
			#endif
		}
	}
}
