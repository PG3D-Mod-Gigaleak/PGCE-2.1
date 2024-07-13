using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminInput : MonoBehaviour {
	public Player_move_c myPmc;
	public UIInput textInput;
	public void Close() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Destroy(gameObject);
		#if UNITY_EDITOR
		myPmc.showingAdminInput = false;
		#endif
	}
	public void Send() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		#if UNITY_EDITOR
		myPmc.sendAdmin(textInput.value);
		myPmc.showingAdminInput = false;
		#endif
		Destroy(gameObject);
	}
	public void SendISeeYou() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		#if UNITY_EDITOR
		myPmc.sendAdminISeeYou(textInput.value);
		myPmc.showingAdminInput = false;
		#endif
		Destroy(gameObject);
	}
}
