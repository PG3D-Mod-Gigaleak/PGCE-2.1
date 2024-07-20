using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerDynastyWater : MonoBehaviour
{
	public float speedMultiplier = 1f;
	public float sinResultMultiplier = 1f;
	public float cosResultMultiplier = .35f;
	private float mTimer;
	void Update()
	{
		mTimer += Time.deltaTime * speedMultiplier;

		transform.rotation = Quaternion.Euler(Mathf.Sin(mTimer)*sinResultMultiplier, 0f, Mathf.Cos(mTimer)*cosResultMultiplier);
	}
}
