using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SerializeRocket : Photon.MonoBehaviour
{

	private Vector3 correctPlayerPos = Vector3.zero;

	private Quaternion correctPlayerRot = Quaternion.identity;

	private Vector3 correctPlayerScale = Vector3.zero;

	private void Awake()
	{
		if (prefs.GetInt("MultyPlayer") != 1 || prefs.GetString("TypeConnect").Equals("local"))
		{
			base.enabled = false;
		}
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			Vector3 position = base.transform.localPosition;
			Quaternion rotation = base.transform.localRotation;
			Vector3 scale = base.transform.localScale;
			stream.Serialize(ref position);
			stream.Serialize(ref rotation);
			stream.Serialize(ref scale);
		}
		else
		{
			stream.Serialize(ref correctPlayerPos);
			stream.Serialize(ref correctPlayerRot);
			stream.Serialize(ref correctPlayerScale);
		}
	}

	private void Update()
	{
		base.transform.position = Vector3.Lerp(base.transform.position, correctPlayerPos, Time.deltaTime * 5f);
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, correctPlayerRot, Time.deltaTime * 5f);
		base.transform.localScale = Vector3.Lerp(base.transform.localScale, correctPlayerScale, Time.deltaTime * 5f);
    }
}
