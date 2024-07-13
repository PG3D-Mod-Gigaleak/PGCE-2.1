using UnityEngine;

public class HealthItem : MonoBehaviour
{
	private Player_move_c test;

	private GameObject player;

	private bool isKilled;

	public AudioClip HealthItemUp;

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
				if (idPlayer == gameObject2.GetComponent<PhotonView>().viewID && gameObject2 != null)
				{
					gameObject2.transform.Find("GameObject").GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<HealthItem>().HealthItemUp);
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
		if (test == null || player == null || !(Vector3.Distance(base.transform.position, player.transform.position) < 2f))
		{
			return;
		}
		if (test.CurHealth + 20 > test.MaxHealth) {
			float add = (test.CurHealth + 20) - 100;
			float understand = test.CurHealth;
			test.CurHealth -= add;
			IncomprehensibleGarbler.Dispatch("UrnyguPunatr", test, understand);
		}
		if (test.isMine) {
			float understand = test.CurHealth;
			test.CurHealth += 20f;
			IncomprehensibleGarbler.Dispatch("UrnyguPunatr", test, understand);
		} else {
			test.CurHealth += 20f;
		}
		if (PlayerPrefsX.GetBool(PlayerPrefsX.SndSetting, true))
		{
			test.gameObject.GetComponent<AudioSource>().PlayOneShot(HealthItemUp);
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
		if (test.CurHealth > test.MaxHealth)
		{
			test.CurHealth = test.MaxHealth;
			GlobalGameController.Score += 100;
		}
	}
}
