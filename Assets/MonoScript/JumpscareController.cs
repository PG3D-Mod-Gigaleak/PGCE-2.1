using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareController : MonoBehaviour {

	public GameObject jumpscareObject;

	private GameObject playerCam;

	private GameObject BGM;

	void Update () {
		if (playerCam == null)
		{
			playerCam = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().myPlayer.GetComponent<FirstPersonControl>().camPlayer;
		}
		if (BGM == null)
		{
			BGM = GameObject.FindGameObjectWithTag("BGMusic");
		}
	}
	void OnTriggerEnter()
	{
		playerCam.SetActive(false); BGM.SetActive(false); jumpscareObject.SetActive(true);
	}
}
