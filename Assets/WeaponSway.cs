using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
	private FirstPersonControl control;

	private Player_move_c pmc;

	private Vector3 cameraOffset
	{
		get
		{
			return new Vector3(WeaponManager.sharedManager.currentWeaponSounds.isDouble ? 0.1f : -0f, 0f, 0f);
		}
	}

	public Vector3 currentPosition { get; set; }

	public Vector3 currentRotation { get; set; }

	public Vector3 sway { get; set; }

	public Camera gunCamera;

	public static WeaponSway instance;

	private bool wasUngrounded;

	public float bobSpeed, bobMultiplier, lerpSpeed;

	public float shake { get; set; }

	private float time;

	private void Start()
	{
		pmc = GetComponentInChildren<Player_move_c>();

		if (!pmc.isMine)
		{
			Destroy(this);
			return;
		}

		instance = this;

		control = GetComponent<FirstPersonControl>();
	}

	private void Update()
	{
		currentRotation = new Vector3(Mathf.Clamp(currentRotation.x, -15, 15), Mathf.Clamp(currentRotation.y, -10, 10), currentRotation.z);
		sway = new Vector3(Mathf.Clamp(sway.x, -15, 15), Mathf.Clamp(sway.y, -10, 10), sway.z);

		shake = Mathf.Lerp(shake, 0f, Time.deltaTime * 10f);
		Vector3 randomShake = new Vector3(Random.Range(0, 2) == 1 ? Random.Range(shake - (shake * 0.25f), shake) : Random.Range(-shake, -shake + (shake * 0.25f)), Random.Range(0, 2) == 1 ? Random.Range(shake - (shake * 0.25f), shake) : Random.Range(-shake, -shake + (shake * 0.25f)), 0f);

		gunCamera.transform.localPosition = Vector3.Slerp(gunCamera.transform.localPosition + randomShake, cameraOffset + currentPosition - new Vector3(Easing.EaseInOut.Sine(time * 0.5f) * bobMultiplier * 0.5f, Easing.EaseInOut.Sine(time) * bobMultiplier) + randomShake, Time.deltaTime * bobSpeed * lerpSpeed);
		gunCamera.transform.localRotation = Quaternion.Slerp(gunCamera.transform.localRotation, Quaternion.Euler(-(currentRotation.x + sway.x), -(currentRotation.y + sway.y), -(Easing.EaseInOut.Sine(time * 0.5f) * bobMultiplier * 0.5f * 100f + (currentRotation.z + sway.z) + (-control.moveTouchPad.position.x * 5f))), Time.deltaTime * bobSpeed * lerpSpeed);
		
		if (control.character.isGrounded)
		{
			currentPosition = Vector3.Slerp(currentPosition, Vector3.zero, Time.deltaTime * bobSpeed * lerpSpeed);
			currentRotation = Vector3.Slerp(currentRotation, Vector3.zero, Time.deltaTime * bobSpeed * lerpSpeed);

			if (wasUngrounded)
			{
				sway += new Vector3(7.5f, 0f, Random.Range(-5f, 5f)); 
				wasUngrounded = false;
			}
		}

		sway = Vector3.Slerp(sway, Vector3.zero, Time.deltaTime * bobSpeed * lerpSpeed);
		
		if (control.character.isGrounded && control.moveTouchPad.position != Vector2.zero)
		{
			time += Time.deltaTime * bobSpeed * (control.moveTouchPad.position.y == 0 ? control.moveTouchPad.position.x : control.moveTouchPad.position.y) * (control.usableVelocity*5).magnitude;
		}
		else if (!control.character.isGrounded)
		{
			currentRotation -= Vector3.right * (Time.deltaTime * 2.5f);
			wasUngrounded = true;
		}
		else
		{
			time += Time.deltaTime * 0.15f;
		}

		sway += new Vector3(control.rotateTouchPad.position.y * 0.035f, control.rotateTouchPad.position.x * 0.015f, 0f);
	}
}
