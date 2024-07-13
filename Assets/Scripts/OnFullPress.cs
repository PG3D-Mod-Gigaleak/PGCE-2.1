using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFullPress : MonoBehaviour
{
	public List<EventDelegate> onFullPress;

	public OnButtonStateChanged buttonState;

	void Start()
	{
		buttonState.unpressed += ()=> { EventDelegate.Execute(onFullPress); };
	}
}
