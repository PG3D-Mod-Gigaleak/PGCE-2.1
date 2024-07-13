using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
	public Transform weaponInstaniationPoint;

	public UILabel nameLabel, tipLabel;

	void Start()
	{
		WeaponSounds randSounds = ((GameObject)WeaponManager.WeaponPrefabs[UnityEngine.Random.Range(0, WeaponManager.WeaponPrefabs.Count)]).GetComponent<WeaponSounds>();
		GameObject weapon = Instantiate(randSounds.bonusPrefab);
		weapon.transform.parent = weaponInstaniationPoint;
		if (weapon.GetComponent<SkinnedMeshRenderer>())
		{
			GameObject newModel = new GameObject("NewModel_" + weapon.name);
			newModel.AddComponent<MeshFilter>().sharedMesh = weapon.GetComponent<SkinnedMeshRenderer>().sharedMesh;
			newModel.AddComponent<MeshRenderer>().materials = weapon.GetComponent<SkinnedMeshRenderer>().materials;
			newModel.transform.parent = weapon.transform;
			newModel.transform.localScale = Vector3.one;
			newModel.transform.localPosition = Vector3.zero;
			newModel.transform.localRotation = Quaternion.Euler(randSounds.armoryRotation);
		}
		weapon.transform.localPosition = Vector3.zero;
		weapon.transform.localRotation = Quaternion.identity;
		weapon.layer = 10;
		foreach (Transform trf in weapon.GetComponentsInChildren<Transform>())
		{
			trf.gameObject.layer = 10;
		}
		nameLabel.text = randSounds.weaponName.Replace("newline", "\n");
		Tips tips = Utilities.GetClass<Tips>("Tips");
		tipLabel.text = tips.tips[UnityEngine.Random.Range(0, tips.tips.Length)];
	}
}
