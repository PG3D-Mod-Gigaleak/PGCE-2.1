using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorCamera : MonoBehaviour {
	void FixedUpdate () {
		if (Globals.PlayerMove && transform.parent.gameObject != Globals.PlayerMove._weaponManager.myPlayer) {
			gameObject.SetActive(false);
		}
		if (gameObject.GetComponent<AudioListener>()) {
			gameObject.GetComponent<AudioListener>().enabled = false;
		}
		if (gameObject.GetComponent<Camera>()) {
			gameObject.GetComponent<Camera>().orthographicSize = 10f;
		}
		if (Globals.PlayerMove && Globals.PlayerMove.isMine) {
			transform.position = Globals.PlayerMove.transform.position + new Vector3(0, 99, 0);
		} 
	}
}
