using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class InputDemo : MonoBehaviour
{
	private GameObject textObject;

	private string enteredText;

	private char cursorChar;

	private bool acceptInput;

	public InputDemo()
	{
		cursorChar = "-"[0];
	}

	public virtual void Start()
	{
		InitializeText();
	}

	public virtual void InitializeText()
	{
		FlyingText.addRigidbodies = false;
		enteredText = string.Empty;
		acceptInput = true;
		textObject = FlyingText.GetObject("-", new Vector3(-7f, 6f, 0f), Quaternion.identity);
		InvokeRepeating("BlinkCursor", 0.5f, 0.5f);
	}

	public virtual void OnGUI()
	{
		if (acceptInput)
		{
			GUI.Label(new Rect(10f, 10f, 500f, 30f), "Type some text! Hit return when done.");
		}
	}

	public virtual void Update()
	{
		if (!acceptInput)
		{
			return;
		}
		//IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(Input.inputString);
		//while (enumerator.MoveNext())
		//{
		//	//char c = RuntimeServices.UnboxChar(enumerator.Current);
		//	if (c == "\b"[0])
		//	{
		//		if (enteredText.Length > 0)
		//		{
		//			enteredText = enteredText.Substring(0, enteredText.Length - 1);
		//		}
		//	}
		//	else if (c == "\n"[0] || c == "\r"[0])
		//	{
		//		if (enteredText.Length > 0)
		//		{
		//			StartCoroutine(ExplodeText());
		//		}
		//	}
		//	else if (c != "<"[0] && c != ">"[0])
		//	{
		//		enteredText += c;
		//		UnityRuntimeServices.Update(enumerator, c);
		//	}
		//	FlyingText.UpdateObject(textObject, enteredText + cursorChar);
		//}
	}

	public virtual IEnumerator ExplodeText()
	{
		yield break;
		//return new _0024ExplodeText_002493(this).GetEnumerator();
	}

	[Beebyte.Obfuscator.SkipRename]
	public virtual void BlinkCursor()
	{
		if (cursorChar == "-"[0])
		{
			cursorChar = " "[0];
		}
		else
		{
			cursorChar = "-"[0];
		}
		FlyingText.UpdateObject(textObject, enteredText + cursorChar);
	}


}
