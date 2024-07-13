using UnityEngine;

public class AmmoItem : MonoBehaviour
{
	private Player_move_c test;

	private bool isKilled;

	private GameObject player;

	public AudioClip AmmoItemUp;

	private PhotonView photonView;

	private void Start()
	{
		photonView = PhotonView.Get(this);
		if (prefs.GetInt("MultyPlayer") == 1)
		{
			if (GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>() != null && GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().myGun != null)
			{
				test = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().myGun.GetComponent<Player_move_c>();
			}
			if (GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>() != null)
			{
				player = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().myPlayer;
			}
		}
		else
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("PlayerGun");
			if ((bool)gameObject)
			{
				test = gameObject.GetComponent<Player_move_c>();
			}
			player = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().myPlayer;
		}
	}

	[PunRPC]
	private void delBonusPhoton(int id, int idPlayer)
	{
		Debug.Log("get del rpc " + id);
		GameObject[] array = GameObject.FindGameObjectsWithTag("Bonus");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (id != gameObject.GetComponent<PhotonView>().viewID)
			{
				continue;
			}
			GameObject[] array3 = GameObject.FindGameObjectsWithTag("Player");
			GameObject[] array4 = array3;
			foreach (GameObject gameObject2 in array4)
			{
				if (idPlayer == gameObject2.GetComponent<PhotonView>().viewID)
				{
					Debug.Log(string.Concat(gameObject2, " ", gameObject2.transform.Find("GameObject"), " ", gameObject.GetComponent<AmmoItem>(), " ", gameObject.GetComponent<AmmoItem>().AmmoItemUp));
					if (gameObject2 != null)
					{
						gameObject2.transform.Find("GameObject").GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AmmoItem>().AmmoItemUp);
					}
				}
			}
			Object.Destroy(gameObject, 0.3f);
			break;
		}
	}

	private void Update()
	{
		if (isKilled)
		{
			return;
		}
		if (test == null || player == null)
		{
			if (prefs.GetInt("MultyPlayer") == 1)
			{
				if (GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>() != null && GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().myGun != null)
				{
					test = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().myGun.GetComponent<Player_move_c>();
				}
				if (GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>() != null)
				{
					player = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().myPlayer;
				}
			}
			else
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag("PlayerGun");
				if ((bool)gameObject)
				{
					test = gameObject.GetComponent<Player_move_c>();
				}
				player = GameObject.FindGameObjectWithTag("Player");
			}
		}
		if (test == null || player == null || !(Vector3.Distance(base.transform.position, player.transform.position) < 2f) || !test.NeedAmmo())
		{
			return;
		}
		if (!GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().AddAmmo() && !GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().currentWeaponSounds.isHeal)
		{
			GlobalGameController.Score += Defs.ScoreForSurplusAmmo;
		}
		if (GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().currentWeaponSounds.isHeal)
		{
			GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().AddHeal();
		}
		else if (GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().currentWeaponSounds.throwObject)
		{
			GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().AddThrowObject();
		}
		if (PlayerPrefsX.GetBool(PlayerPrefsX.SndSetting, true))
		{
			test.gameObject.GetComponent<AudioSource>().PlayOneShot(AmmoItemUp);
		}
		isKilled = true;
		if (prefs.GetInt("MultyPlayer") == 1)
		{
			photonView.RPC("delBonusPhoton", PhotonTargets.All, GetComponent<PhotonView>().viewID, player.GetComponent<PhotonView>().viewID);
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}
}
