using UnityEngine;

public class NetworkInterpolatedTransform : MonoBehaviour
{
	private Vector3 correctPlayerPos = Vector3.zero;

	private Quaternion correctPlayerRot = Quaternion.identity;

	private Vector3 correctPlayerScale = Vector3.zero;

	private void Awake()
	{
		if (PlayerPrefs.GetInt("MultyPlayer") != 1 || PlayerPrefs.GetString("TypeConnect").Equals("inet"))
		{
			base.enabled = false;
		}
	}

	private void Update()
	{
		
	}
}
