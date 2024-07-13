using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObject : MonoBehaviour
{
	public float speed;

	[HideInInspector] public float multiplayerDamage;

	[HideInInspector] public int damageSender;

	private PhotonView mView;

	void Start()
	{
		mView = GetComponent<PhotonView>();
		mView.viewID = PhotonNetwork.AllocateViewID();
	}

	void OnDestroy()
	{
		PhotonNetwork.UnAllocateViewID(mView.viewID);
	}

	void Update()
	{
		transform.position += (transform.forward) * Time.deltaTime * speed;
	}

	[PunRPC]
	public void DestroyThrownObject()
	{
		Instantiate(Resources.Load<GameObject>("ThrownDestruct/" + name.Replace("(Clone)", "")), transform.position, transform.rotation);
		Destroy(gameObject);
	}

	public void DestroyThrownObjectOffline()
	{
		Instantiate(Resources.Load<GameObject>("ThrownDestruct/" + name.Replace("(Clone)", "")), transform.position, transform.rotation);
		Destroy(gameObject);
	}

	public Vector3 oldForward, oldPosition;

	public void FixedUpdate() {
		Vector3 fwd = oldForward;
		RaycastHit otherHit;
        if (Physics.Raycast(oldPosition, fwd, out otherHit, Vector3.Distance(oldPosition, transform.position))) {
			Collider other = otherHit.collider;
			if (other.gameObject == gameObject)
				return;
			if (other.transform.tag == "HeadCollider" || other.transform.tag == "BodyCollider")
			{
				if (mView.isMine)
				{
					if (damageSender != other.transform.parent.GetComponent<PhotonView>().viewID)
					{
						Globals.PlayerMove.inGameGUI.Hitmark();
						Globals.PlayerMove.MinusLivePlayerManual(damageSender, other.transform.parent.GetComponent<PhotonView>().viewID, multiplayerDamage * (other.transform.tag == "HeadCollider" ? 1.5f : 1f));
					}
				}
			}
			else if (other.transform.tag == "Enemy")
			{
				if (!Defs.isMulti)
				{
					Globals.PlayerMove.inGameGUI.Hitmark();
					other.transform.GetComponent<BotHealth>().adjustHealth(multiplayerDamage, Camera.main.transform);
				}
				else
				{
					ZombiUpravlenie controller = other.transform.GetComponent<ZombiUpravlenie>();
					float health = controller.health;
					health -= multiplayerDamage;
					controller.setHealth(health, true);
					if (health <= 0f)
					{
						GlobalGameController.Score += controller.transform.GetChild(0).GetComponent<Sounds>().scorePerKill;
					}
					WeaponManager.sharedManager.myTable.GetComponent<NetworkStartTable>().score = GlobalGameController.Score;
					WeaponManager.sharedManager.myTable.GetComponent<NetworkStartTable>().synchState();
				}
			}
			if (Defs.isMulti)
			{
				mView.RPC("DestroyThrownObject", PhotonTargets.All);
			}
			else
			{
				DestroyThrownObjectOffline();
			}
		}
		oldForward = transform.forward;
		oldPosition = transform.position;
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag == "HeadCollider" || other.transform.tag == "BodyCollider")
		{
			if (mView.isMine)
			{
				if (damageSender != other.transform.parent.GetComponent<PhotonView>().viewID)
				{
					Globals.PlayerMove.inGameGUI.Hitmark();
					Globals.PlayerMove.MinusLivePlayerManual(damageSender, other.transform.parent.GetComponent<PhotonView>().viewID, multiplayerDamage * (other.transform.tag == "HeadCollider" ? 1.5f : 1f));
				}
			}
		}
		else if (other.transform.tag == "Enemy")
		{
			if (!Defs.isMulti)
			{
				Globals.PlayerMove.inGameGUI.Hitmark();
				other.transform.GetComponent<BotHealth>().adjustHealth(multiplayerDamage, Camera.main.transform);
			}
			else
			{
				ZombiUpravlenie controller = other.transform.GetComponent<ZombiUpravlenie>();
				float health = controller.health;
				health -= multiplayerDamage;
				controller.setHealth(health, true);
				if (health <= 0f)
				{
					GlobalGameController.Score += controller.transform.GetChild(0).GetComponent<Sounds>().scorePerKill;
				}
				WeaponManager.sharedManager.myTable.GetComponent<NetworkStartTable>().score = GlobalGameController.Score;
				WeaponManager.sharedManager.myTable.GetComponent<NetworkStartTable>().synchState();
			}
		}
		if (Defs.isMulti)
		{
			mView.RPC("DestroyThrownObject", PhotonTargets.All);
		}
		else
		{
			DestroyThrownObjectOffline();
		}
	}

	public void SetMultiplayerData(int viewID, float damage)
	{
		damageSender = viewID;
		multiplayerDamage = damage;
	}
}
