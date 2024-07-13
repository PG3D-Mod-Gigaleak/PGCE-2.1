using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOnButtonState : MonoBehaviour
{
	public OnButtonStateChanged buttonState;

	public bool crossfade;

	public float crossfadeSpeed;

	public string normal, hover, pressed, disabled, unhover, unpressed, cancel;

	void Start()
	{
		buttonState.normal += ()=> { PlayAnimation(normal); };
		buttonState.hover += ()=> { PlayAnimation(hover); };
		buttonState.pressed += ()=> { PlayAnimation(pressed); };
		buttonState.disabled += ()=> { PlayAnimation(disabled); };
		buttonState.unpressed += ()=> { PlayAnimation(unpressed); };
		buttonState.unhover += ()=> { PlayAnimation(unhover); };
		buttonState.cancel += ()=> { PlayAnimation(cancel); };
	}

	public void PlayAnimation(string animation)
	{
		if (crossfade)
		{
			GetComponent<Animation>().CrossFade(animation, crossfadeSpeed);
			return;
		}
		GetComponent<Animation>().Play(animation);
	}
}
