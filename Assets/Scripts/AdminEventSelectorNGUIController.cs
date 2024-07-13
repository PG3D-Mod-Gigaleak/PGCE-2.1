using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminEventSelectorNGUIController : MonoBehaviour {
	public void Close() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Destroy(gameObject);
		#if UNITY_EDITOR
		Globals.PlayerMove.showingAdminInput = false;
		#endif
	}
	public void OpenAdmin() {
		#if UNITY_EDITOR
		Close();
		Globals.PlayerMove.showingAdminInput = true;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		GameObject j = Instantiate(Resources.Load<GameObject>("AdminInput"));
		AdminInput the = j.GetComponent<AdminInput>();
		the.myPmc = Globals.PlayerMove;
		#endif
	}
	public void OpenSpawn() {
		#if UNITY_EDITOR
		Close();
		Globals.PlayerMove.showingAdminInput = true;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		GameObject j = Instantiate(Resources.Load<GameObject>("AdminResourceSpawner"));
		#endif
	}
	public void ScaleMeX2() {
		#if UNITY_EDITOR
		Globals.PlayerMove._weaponManager.myPlayer.transform.localScale *= 2;
		#endif
	}
	public void ScaleMeDividedBy2() {
		#if UNITY_EDITOR
		Globals.PlayerMove._weaponManager.myPlayer.transform.localScale /= 2;
		#endif
	}
	public void KillServer() {
		#if UNITY_EDITOR
		Close();
		Globals.PlayerMove.StartCoroutine(Globals.PlayerMove.killServerCl());	
		#endif
	}
	public void FlipEveryone() {
		#if UNITY_EDITOR
		Close();
		Globals.PlayerMove.sendFlipAll();
		#endif
	}
}
