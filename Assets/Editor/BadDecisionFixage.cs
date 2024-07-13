using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BadDecisionFixage : Editor
{
	[MenuItem("Bad Decision Fixage/Fix Nix's Goofy M16 Workarounds")]
	public static void FixNixsGoofysharedMaterials()
	{
		foreach (GameObject weapon in Resources.LoadAll<GameObject>("weapons"))
		{
			foreach (MeshRenderer renderer in weapon.GetComponentsInChildren<MeshRenderer>())
			{
				if (renderer == null || renderer.sharedMaterial == null)
				{
					Debug.LogError("null lol");
					continue;
				}
				if (renderer.sharedMaterial.name == "test" || renderer.sharedMaterial.name == "test 1")
				{
					renderer.enabled = false;
				}
			}
		}
	}
}
