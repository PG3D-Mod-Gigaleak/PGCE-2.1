using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class GameOverControl : MonoBehaviour
{
	public GameObject explosion;

	public float startingShakeDistance;

	public float decreasePercentage;

	public float shakeSpeed;

	public int numberOfShakes;

	public GameOverControl()
	{
		startingShakeDistance = 0.4f;
		decreasePercentage = 0.5f;
		shakeSpeed = 40f;
		numberOfShakes = 3;
	}

	public virtual IEnumerator Start()
	{
		yield break;
		//return new _0024Start_002462(this).GetEnumerator();
	}

	public virtual IEnumerator CameraShake(Camera cam)
	{
		yield break;
		//return new _0024CameraShake_002480(cam, this).GetEnumerator();
	}


}
