using System.Collections.Generic;
using UnityEngine;

public class PersConfigurator : MonoBehaviour
{
	public GameObject body;

	public GameObject gun;

	public GameObject ears;

	private GameObject weapon;

	public string myCAnim(string a){
        return Defs.CAnim(weapon.GetComponent<WeaponSounds>().animationObject, a);
    }

	private void Start()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (GameObject obj in BonusCreator.GetAllWeapons())
		{
			if (obj.name.Replace("(Clone)", "") == prefs.GetString("cat1") || obj.name.Replace("(Clone)", "") == prefs.GetString("cat2") || obj.name.Replace("(Clone)", "") == prefs.GetString("cat3") || obj.name.Replace("(Clone)", "") == prefs.GetString("cat4") || obj.name.Replace("(Clone)", "") == prefs.GetString("cat5"))
			{
				list.Add(obj);
			}
		}
		GameObject original = list[Random.Range(0, list.Count)] as GameObject;
		GameObject gameObject = Object.Instantiate(original) as GameObject;
		gameObject.transform.parent = body.transform;
		weapon = gameObject;
		weapon.transform.localPosition = Vector3.zero;
		weapon.transform.localRotation = Quaternion.identity;
		try {
			string cAnim = myCAnim("Profile");
			if (!string.IsNullOrEmpty(cAnim)) {
				weapon.GetComponent<WeaponSounds>().animationObject.GetComponent<Animation>().Play(cAnim);
			}
		} catch {
			Debug.Log("?????");
		}
		gun = gameObject.GetComponent<WeaponSounds>().bonusPrefab;
		Texture texture = SkinsManager.currentMultiplayerSkin();
		texture.filterMode = FilterMode.Point;
		Player_move_c.SetTextureRecursivelyFrom(base.gameObject, texture, new GameObject[1] { gun });
	}

	private void Update()
	{
		ears.SetActive((Storager.getInt("earsOn", false) == 0 ? false : true));
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
		Touch[] touches = Input.touches;
		foreach (Touch touch in touches)
		{
			RaycastHit hitInfo;
			if (touch.phase == TouchPhase.Began && Physics.Raycast(ray, out hitInfo, 1000f, -5) && hitInfo.collider.gameObject.name.Equals("MainMenu_Pers"))
			{
				prefs.SetInt(Defs.ProfileEnteredFromMenu, 1);
				ConnectGUI.GoToProfile();
				break;
			}
		}
	}
}
