using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnButtonStateChanged : MonoBehaviour
{
	public UIButtonColor color;

	private UIButtonColor.State lastState;

	void Start()
	{
		lastState = color.state;
	}

	void Update()
	{
		if (color.state != lastState)
		{
			OnChange(color.state);
			lastState = color.state;
		}
	}

	private void OnChange(UIButtonColor.State state)
	{
		if (state == UIButtonColor.State.Hover && lastState == UIButtonColor.State.Pressed)
		{
			unpressed();
			return;
		}
		if (state == UIButtonColor.State.Normal && lastState == UIButtonColor.State.Hover)
		{
			unhover();
			return;
		}
		if (state == UIButtonColor.State.Normal && lastState == UIButtonColor.State.Pressed)
		{
			cancel();
			return;
		}
		switch (state) { case UIButtonColor.State.Normal: normal(); break; case UIButtonColor.State.Hover: hover(); break; case UIButtonColor.State.Pressed: pressed(); break; case UIButtonColor.State.Disabled: disabled(); break; }
	}

	public Action normal, hover, pressed, disabled, unhover, unpressed, cancel;
}
