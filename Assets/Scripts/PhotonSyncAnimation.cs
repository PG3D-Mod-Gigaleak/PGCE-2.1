using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PhotonSyncAnimation : MonoBehaviour
{
    private Animation anim;

	private PhotonView photonView;
	
    private float animationTime;

	public string syncAnim;

	private bool synced;

	private void OnDestroy()
	{
		PhotonNetwork.UnAllocateViewID(photonView.viewID);
	}

    private void Start()
    {
		photonView = GetComponent<PhotonView>();
		if (!PhotonNetwork.connected)
		{
			enabled = false;
		}
		photonView.viewID = PhotonNetwork.AllocateViewID();
        anim = GetComponent<Animation>();
        animationTime = anim[anim.clip.name].normalizedTime;
        photonView.RPC("SyncAnimationTime", PhotonTargets.All, animationTime);
    }

    [PunRPC]
    private void SyncAnimationTime(float time)
    {
		if (synced)
		{
			return;
		}
        animationTime = time;
        anim[syncAnim].normalizedTime = animationTime;
        anim.Play(syncAnim);
		synced = true;
	}
}
