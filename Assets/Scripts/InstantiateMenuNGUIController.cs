using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class InstantiateMenuNGUIController : Photon.MonoBehaviour {
	public UIGrid spawnGrid;
	private readonly string[] ignoreList = new string[] {
		"Player",
		"spectator",
		"Weapon",
		"Admin",
		"Network"
	};
	private void Start() {
		#if UNITY_EDITOR
		StartSetup();
		#endif
	}
	public void Close() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Destroy(gameObject);
		#if UNITY_EDITOR
		Globals.PlayerMove.showingAdminInput = false;
		#endif
	}
	#if UNITY_EDITOR
	private void StartSetup() {
		// was gonna use threading, but loadall can't be called on any thread besides the main one (rip)
		SetupButtons();
	}
	private void SetAllChildrenToNGUI(Transform t) {
		t.gameObject.layer = LayerMask.NameToLayer("NGUI");
		foreach (Transform child in t) {
			SetAllChildrenToNGUI(child);
		}
	}
	private void TryRemoveComponent(GameObject obj, Type componentType) {
		try {
			Destroy(obj.GetComponent(componentType));
		} catch (Exception e) {
			print(e.Message);
		}
	}
	private void ClearOfAllErrors(GameObject obj) {
		TryRemoveComponent(obj, typeof(BotTrigger));
		TryRemoveComponent(obj, typeof(BotHealth));
		TryRemoveComponent(obj, typeof(HealthItem));
		TryRemoveComponent(obj, typeof(AmmoItem));
		TryRemoveComponent(obj, typeof(BotAI));
		TryRemoveComponent(obj, typeof(BotMovement));
		TryRemoveComponent(obj, typeof(EnderAI));
		TryRemoveComponent(obj, typeof(EnderAIPhoton));
		TryRemoveComponent(obj, typeof(Rocket));
		TryRemoveComponent(obj, typeof(ThrownObject));
		TryRemoveComponent(obj, typeof(Bullet));
		TryRemoveComponent(obj, typeof(ZombiSynchPhoton));
		TryRemoveComponent(obj, typeof(ZombiUpravlenie));
		TryRemoveComponent(obj, typeof(PhotonView));
		TryRemoveComponent(obj, typeof(UnityEngine.AI.NavMeshAgent));
		TryRemoveComponent(obj, typeof(SpawnMonster));
	}
	private void CreateButton(GameObject gameObject) {
		foreach (string toIgnore in ignoreList) {
			if (gameObject.name.Contains(toIgnore)) {
				return;
			}
		}
		GameObject newButton = (GameObject)Instantiate(Resources.Load("InstantiateButton"));
		newButton.transform.parent = spawnGrid.transform;
		spawnGrid.Reposition();
		InstantiateButton instantiateButton = newButton.GetComponent<InstantiateButton>();
		GameObject theInstantiatedOne = (GameObject)Instantiate(gameObject);
		ClearOfAllErrors(theInstantiatedOne);
		theInstantiatedOne.transform.parent = instantiateButton.instantiatePoint.transform;
		newButton.transform.localScale = Vector3.one;
		string unclonedName = theInstantiatedOne.name.Replace("(Clone)", "");
		if (unclonedName == "Boss7" || unclonedName == "Boss12" || unclonedName == "Boss13") {
			theInstantiatedOne.transform.localScale = Vector3.one*0.35f;
		} else if (unclonedName == "Enemy6_go") {
			theInstantiatedOne.transform.localScale = Vector3.one*0.5f;
		}  else {
			theInstantiatedOne.transform.localScale = Vector3.one;
		}
		theInstantiatedOne.transform.localPosition = Vector3.zero;
		SetAllChildrenToNGUI(theInstantiatedOne.transform);
		if (theInstantiatedOne.name.Contains("Boss") || theInstantiatedOne.name.Contains("Enemy")) {
			theInstantiatedOne.transform.GetChild(0).gameObject.GetComponent<Animation>().Play(Defs.CAnim(theInstantiatedOne.transform.GetChild(0).gameObject, "Norm_Walk"));
		}
		if (theInstantiatedOne.name.Contains("Ender")) {
			theInstantiatedOne.GetComponent<Animation>().Play("Walk");
		}
		instantiateButton.GetComponent<ButtonHandler>().Clicked += (object sender, EventArgs args) => {
			PhotonNetwork.Instantiate(AssetDatabase.GetAssetPath(gameObject).Replace("Assets/Resources/", "").Replace(".prefab", ""), Camera.main.transform.position + Camera.main.transform.forward*1.5f, Quaternion.identity, 0);
		};
	}
	private void SetupButtons() {
		foreach (GameObject photonGobj in MiscCache.photonViewGameObjects) {
			CreateButton(photonGobj);
		}
	}
	#endif
}
