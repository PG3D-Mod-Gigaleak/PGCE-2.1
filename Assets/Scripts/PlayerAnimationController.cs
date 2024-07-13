using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerAnimationController : MonoBehaviour
{
	public int state;

	public Texture2D[] stateTextures;

	public SkinnedMeshRenderer skinModel;

	void Update()
	{
		skinModel.sharedMaterial.mainTexture = stateTextures[(int)state];
	}
}
