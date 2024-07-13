using System.Reflection;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	public int rocketNum;

	public float extraSpeed { get; set; }

	public string weaponName = string.Empty;

	public float damage;

	public float radiusDamage;

	public float radiusDamageSelf;

	public float radiusImpulse;

	public float multiplayerDamage;

	public Vector2 damageRange;

	private PhotonView photonView;

	private bool isMulti;

	private bool isInet;

	private bool isMine;

	private bool isCompany;

	private bool isCOOP;

	private WeaponManager _weaponManager;

	private bool isKilled;

	private void Awake()
	{
		isMulti = prefs.GetInt("MultyPlayer") == 1;
		isInet = true;//prefs.GetString("TypeConnect").Equals("inet");
		isCompany = false;//prefs.GetInt("company", 0) == 1;
		isCOOP = prefs.GetInt("COOP", 0) == 1;
	}

	private void Start()
	{
		_weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
		photonView = PhotonView.Get(this);
		if (isMulti)
		{
			isMine = photonView.isMine;
		}
		if (isMulti && isMine)
		{
			photonView.RPC("SetRocketActive", PhotonTargets.AllBuffered, rocketNum, radiusImpulse);
		}
		else if (!isMulti)
		{
			SetRocketActive(rocketNum, radiusImpulse);
		}
		if (!isMulti || isMine)
		{
			Invoke("Remove", 7f);
			return;
		}
		GetComponent<BoxCollider>().enabled = false;
		base.transform.GetComponent<Rigidbody>().isKinematic = true;
	}

	[Obfuscation(Exclude = true)]
	private void Remove()
	{
		if (!isMulti)
		{
			Object.Destroy(base.gameObject);
		}
		else
		{
			PhotonNetwork.Destroy(base.gameObject);
		}
	}

	[PunRPC]
	public void SetRocketActive(int rn, float _radiusImpulse)
	{
		radiusImpulse = _radiusImpulse;
		Instantiate(Resources.Load<GameObject>("rockets/Rocket_" + rn), transform);
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.transform.parent != null)
		{
		}
		if ((!other.gameObject.tag.Equals("Player") || !(other.gameObject == _weaponManager.myPlayer)) && (!(other.transform.parent != null) || !other.transform.parent.gameObject.tag.Equals("Player") || !(other.transform.parent.gameObject == _weaponManager.myPlayer)))
		{
			KillRocket();
		}
	}

	private void KillRocket()
	{
		if (isKilled)
		{
			return;
		}
		isKilled = true;
		if (isMulti)
		{
			if (photonView != null)
			{
				photonView.RPC("Collide", PhotonTargets.All, weaponName);
			}
			else
			{
				Debug.Log("Rocket.KillRocket():    photonView == null");
			}
		}
		else
		{
			Collide(weaponName);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if ((isMulti || (!other.gameObject.tag.Equals("Player") && (!(other.transform.parent != null) || !other.transform.parent.gameObject.tag.Equals("Player")))) && (!isMulti || ((!other.gameObject.tag.Equals("Player") || !(other.gameObject == _weaponManager.myPlayer)) && (!(other.transform.parent != null) || !other.transform.parent.gameObject.tag.Equals("Player") || !(other.transform.parent.gameObject == _weaponManager.myPlayer)))))
		{
			KillRocket();
		}
	}

	[PunRPC]
	private void Collide(string _weaponName)
	{
		BazookaExplosion(_weaponName);
		if (!isMulti)
		{
			Object.Destroy(base.gameObject);
		}
		else if (isMine)
		{
			Invoke("DestroyRocket", 0.1f);
		}
	}

	[Obfuscation(Exclude = true)]
	private void DestroyRocket()
	{
		PhotonNetwork.Destroy(base.gameObject);
	}

	public Vector3 oldPosition;
	public Vector3 oldForward;
	public void FixedUpdate() {
		Vector3 fwd = oldForward;
		RaycastHit otherHit;
        if (Physics.Raycast(oldPosition, fwd, out otherHit, Vector3.Distance(oldPosition, transform.position))) {
			Collider other = otherHit.collider;
			if ((!other.gameObject.tag.Equals("Player") || !(other.gameObject == _weaponManager.myPlayer)) && (!(other.transform.parent != null) || !other.transform.parent.gameObject.tag.Equals("Player") || !(other.transform.parent.gameObject == _weaponManager.myPlayer)) && other.gameObject != gameObject)
			{
				KillRocket();
			}
		}
		oldForward = transform.forward;
		oldPosition = transform.position;
	}

	public void ImDestroyRPC()
	{
		Invoke("ImDestroy", 0.2f);
	}

	[Obfuscation(Exclude = true)]
	public void ImDestroy()
	{
		Object.Destroy(base.gameObject);
	}

	public void BazookaExplosion(string explosionName)
	{
		Vector3 position = base.transform.position;
		string path = ResPath.Combine("Explosions", explosionName);
		GameObject gameObject = Resources.Load(path) as GameObject;
		if (gameObject == null)
		{
			gameObject = Resources.Load(ResPath.Combine("Explosions", "Weapon75")) as GameObject;
		}
		if (gameObject != null)
		{
			GameObject gameObject2 = Object.Instantiate(gameObject, position, Quaternion.identity) as GameObject;
			gameObject2.GetComponent<AudioSource>().enabled = PlayerPrefsX.GetBool(PlayerPrefsX.SndSetting, true);
		}
		if (!isMulti || isMine)
		{
			if (isMulti && _weaponManager.myPlayer == null)
			{
				return;
			}
			GameObject[] array = GameObject.FindGameObjectsWithTag("Enemy");
			GameObject[] array2 = array;
			foreach (GameObject gameObject3 in array2)
			{
				if (!((gameObject3.transform.position - position).magnitude < radiusDamage))
				{
					continue;
				}
				Globals.PlayerMove.inGameGUI.Hitmark();
				if (!isMulti)
				{
					BotHealth component = gameObject3.GetComponent<BotHealth>();
					component.adjustHealth(0f - damage + Random.Range(damageRange.x, damageRange.y), Camera.main.transform);
					continue;
				}
				float health = gameObject3.GetComponent<ZombiUpravlenie>().health;
				if (health > 0f)
				{
					health -= damage + Random.Range(damageRange.x, damageRange.y);
					gameObject3.GetComponent<ZombiUpravlenie>().setHealth(health, true);
					GlobalGameController.Score += 5;
					if (health <= 0f)
					{
						GlobalGameController.Score += gameObject3.transform.GetChild(0).GetComponent<Sounds>().scorePerKill;
					}
					_weaponManager.myTable.GetComponent<NetworkStartTable>().score = GlobalGameController.Score;
					_weaponManager.myTable.GetComponent<NetworkStartTable>().synchState();
				}
			}
			if (isMulti)
			{
				GameObject[] array3 = GameObject.FindGameObjectsWithTag("Player");
				GameObject[] array4 = array3;
				foreach (GameObject gameObject4 in array4)
				{
					bool flag = false;
					flag = gameObject4.GetComponent<PhotonView>().isMine;
					if (((!isCOOP || !flag) && (isCOOP || (!flag && (isCompany) && ((!isCompany))))) || !((gameObject4.transform.position - position).magnitude < ((!flag) ? radiusDamage : radiusDamageSelf)))
					{
						continue;
					}
					float num = 1f;
					bool isHeadShot = false;
					if (flag)
					{
						_weaponManager.lastEnemyHitBy = null;
						float num2 = multiplayerDamage;
						float num3 = num2 - gameObject4.GetComponent<SkinName>().playerMoveC.curArmor;
						if (num3 < 0f)
						{
							gameObject4.GetComponent<SkinName>().playerMoveC.curArmor -= num2;
							num3 = 0f;
						}
						else
						{
							gameObject4.GetComponent<SkinName>().playerMoveC.curArmor = 0f;
						}
						if (gameObject4.GetComponent<SkinName>().playerMoveC.CurHealth > 0f)
						{
							if (gameObject4.GetComponent<SkinName>().playerMoveC.isMine) {
								float understand = gameObject4.GetComponent<SkinName>().playerMoveC.CurHealth;
								gameObject4.GetComponent<SkinName>().playerMoveC.CurHealth -= num3;
								IncomprehensibleGarbler.Dispatch("UrnyguPunatr", gameObject4.GetComponent<SkinName>().playerMoveC, understand);
							} else {
								gameObject4.GetComponent<SkinName>().playerMoveC.CurHealth -= num3;
							}
							if (gameObject4.GetComponent<SkinName>().playerMoveC.CurHealth <= 0f)
							{
								if (isMine && gameObject4.GetComponent<SkinName>().playerMoveC.isMine) {
									gameObject4.GetComponent<SkinName>().playerMoveC.DispatchDie();
									Achievements.Give("suicidebomber");
								}
								gameObject4.GetComponent<SkinName>().playerMoveC.sendImDeath(gameObject4.GetComponent<SkinName>().NickName);
							}
						}
					}
					else
					{
						_weaponManager.myPlayer.GetComponent<SkinName>().playerMoveC.MinusLivePlayer(gameObject4.GetComponent<PhotonView>().viewID, multiplayerDamage * num);
					}
				}
			}
		}
		GameObject gameObject5 = (isMulti ? WeaponManager.sharedManager.myPlayer : GameObject.FindGameObjectWithTag("Player"));
		if (gameObject5 != null)
		{
			ImpactReceiver impactReceiver = gameObject5.GetComponent<ImpactReceiver>();
			if (impactReceiver == null)
			{
				impactReceiver = gameObject5.AddComponent<ImpactReceiver>();
			}
			float num4 = 100f;
			if (radiusImpulse != 0f)
			{
				num4 = (gameObject5.transform.position - position).magnitude / radiusImpulse;
			}
			float num5 = Mathf.Max(0f, 1f - num4);
			float force = 133.4f * num5;
			impactReceiver.AddImpact(gameObject5.transform.position - position, force);
		}
	}
}
