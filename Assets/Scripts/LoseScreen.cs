using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
	public void Quit()
	{
		GameObject.FindGameObjectWithTag("GameController").GetComponent<Initializer>().isCancelReConnect = true;
		if (PhotonNetwork.connected)
		{
			PhotonNetwork.Disconnect();
			ConnectGUI.Local();
		}
		else
		{
			LoadConnectScene.loading = null;
			LoadConnectScene.sceneToLoad = Defs.CurrentMainMenuScene;
			Application.LoadLevel("PromScene");
		}
	}

	public static void TheCallToRuleThemAll()
	{
		GameObject.FindGameObjectWithTag("GameController").GetComponent<Initializer>().isCancelReConnect = true;
		if (PhotonNetwork.connected)
		{
			PhotonNetwork.Disconnect();
			ConnectGUI.Local();
		}
		else
		{
			LoadConnectScene.loading = null;
			LoadConnectScene.sceneToLoad = Defs.CurrentMainMenuScene;
			Application.LoadLevel("PromScene");
		}
	}

	[HideInInspector] public GameObject enemyDiedTo;

	public UILabel deathLabel;

	public Transform enemyInstanPoint;

	private void Start()
	{
		Screen.lockCursor = false;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		if (enemyDiedTo != null)
		{
			GameObject objInstan = enemyDiedTo;
			objInstan.name = objInstan.name.Replace("(Clone)", "");
			GameObject obj = Instantiate(objInstan, enemyInstanPoint);
			obj.SetActive(true);
			obj.layer = 10;
			foreach (Transform trf in obj.GetComponentsInChildren<Transform>())
			{
				trf.gameObject.layer = 10;
			}
			obj.GetComponent<Animation>().Play(Defs.CAnim(obj, "Norm_Walk"));
			return;
		}
		deathLabel.text = "you only have yourself to blame...";
		GameObject obj2 = Instantiate(Resources.Load<GameObject>("PlayerModel"), enemyInstanPoint);
		obj2.layer = 10;
		foreach (Transform trf in obj2.GetComponentsInChildren<Transform>())
		{
			trf.gameObject.layer = 10;
		}
		PreviewController.SetTextureRecursivelyFrom(obj2, SkinsManager.currentMultiplayerSkin());
	}
}
