using Photon;
using UnityEngine;

public class SerializePosition : Photon.MonoBehaviour
{
	private Vector3 correctPlayerPos = Vector3.zero;

	private Quaternion correctPlayerRot = Quaternion.identity;

    public bool lerp;

    public float lerpSpeed = 5f;

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
			stream.Serialize(ref position);
			stream.Serialize(ref rotation);
		}
		else
		{
			stream.Serialize(ref correctPlayerPos);
			stream.Serialize(ref correctPlayerRot);
		}
	}

	private void Update()
	{
		if (!base.photonView.isMine)
		{
            if (lerp)
            {
                base.transform.position = Vector3.Lerp(base.transform.position, correctPlayerPos, Time.deltaTime * lerpSpeed);
                base.transform.rotation = Quaternion.Lerp(base.transform.rotation, correctPlayerRot, Time.deltaTime * lerpSpeed);
            }
            else
            {
		        base.transform.position = correctPlayerPos;
		        base.transform.rotation = correctPlayerRot;
            }
		}
	}
}
