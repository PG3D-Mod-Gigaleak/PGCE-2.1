using UnityEngine;

public class NetworkInterpolatedTransform : MonoBehaviour
{
	private Vector3 correctPlayerPos = Vector3.zero;

	private Quaternion correctPlayerRot = Quaternion.identity;

	private Vector3 correctPlayerScale = Vector3.zero;

	private void Awake()
	{
		if (prefs.GetInt("MultyPlayer") != 1 || prefs.GetString("TypeConnect").Equals("inet"))
		{
			base.enabled = false;
		}
	}

	private void Update()
	{
		
	}
}
