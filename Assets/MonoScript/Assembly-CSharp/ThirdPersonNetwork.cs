using Photon;
using UnityEngine;

public class ThirdPersonNetwork : Photon.MonoBehaviour
{
	private ThirdPersonCamera cameraScript;

	private ThirdPersonController controllerScript;

	private bool iskilled;

	private bool oldIsKilled;

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
			iskilled = GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().isKilled;
			Vector3 position = base.transform.localPosition;
			Quaternion rotation = base.transform.localRotation;
			Vector3 scale = base.transform.localScale;
			stream.Serialize(ref position);
			stream.Serialize(ref rotation);
			stream.Serialize(ref scale);
			stream.Serialize(ref iskilled);
			GetComponent<SkinName>().playerGameObject.GetComponent<Player_move_c>().isKilled = iskilled;
		}
		else
		{
			stream.Serialize(ref correctPlayerPos);
			stream.Serialize(ref correctPlayerRot);
			stream.Serialize(ref correctPlayerScale);
			oldIsKilled = iskilled;
			stream.Serialize(ref iskilled);
		}
	}

	private void Update()
	{
		if (!base.photonView.isMine)
		{
			if (!oldIsKilled)
			{
				base.transform.position = Vector3.Lerp(base.transform.position, correctPlayerPos, Time.deltaTime * 5f);
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, correctPlayerRot, Time.deltaTime * 5f);
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, correctPlayerScale, Time.deltaTime * 5f);
			}
			else
			{
				base.transform.position = correctPlayerPos;
				base.transform.rotation = correctPlayerRot;
				base.transform.localScale = correctPlayerScale;
			}
		}
	}
}
