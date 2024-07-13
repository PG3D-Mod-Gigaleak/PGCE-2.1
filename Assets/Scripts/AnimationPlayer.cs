using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
	public void PlayAnimation(string animation)
	{
		GetComponent<Animation>().Play(animation);
	}
}
