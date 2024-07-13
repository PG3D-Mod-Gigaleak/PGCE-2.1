using System;
using System.Collections.Generic;
using Prime31;
using Rilisoft;
using UnityEngine;

public sealed class NetworkStartTable : MonoBehaviour
{
	public struct infoClient
	{
		public string ipAddress;

		public string name;

		public string coments;
	}

	public Texture mySkin;

	public Texture plashkaStartMultu;

	public Texture plashkaStartCoop;

	public Texture kubochek;

	public Texture zagolovokWiner;

	public Texture zagolovokStart;

	public List<SurvivalConfig.Enemy> zombiePrefabs = new List<SurvivalConfig.Enemy>();

	private GameObject _playerPrefab;

	public GameObject tempCam;

	public GameObject zombieManagerPrefab;

	private bool _isMultiplayer;

	private int addCoins;

	private int minCoin1 = 2500;

	private int minCoin2 = 7000;

	private int minCoin3 = 8000;

	private bool showMessagFacebook;

	private bool showMessagTiwtter;

	private bool clickButtonFacebook;

	public bool isIwin;

	public List<infoClient> players = new List<infoClient>();

	public GUIStyle back;

	public GUIStyle start;

	public GUIStyle restart;

	public GUIStyle playersWindow;

	public GUIStyle playersWindowFrags;

	public GUIStyle twitterStyle;

	public GUIStyle facebookStyle;

	public GUIStyle labelStyle;

	public GUIStyle messagesStyle;

	public Texture head_players;

	public Texture nicksStyle;

	public Texture killsStyle;

	public Texture scoreTexture;

	private Vector2 scrollPosition = Vector2.zero;

	public GameObject _purchaseActivityIndicator;

	private float koofScreen = (float)Screen.height / 768f;

	public WeaponManager _weaponManager;

	public bool showTable;

	public string nickPobeditelya;

	public bool isShowNickTable;

	public bool runGame = true;

	public GUIStyle zagolovokStyle;

	public GameObject[] zoneCreatePlayer;

	private GameObject _cam;

	public bool showDisconnectFromServer;

	public bool showDisconnectFromMasterServer;

	private float timerShow = -1f;

	public string NamePlayer;

	public int CountKills;

	public int oldCountKills;

	public string[] oldSpisokName;

	public string[] oldCountLilsSpisok;

	public int oldIndexMy;

	private GameObject tc;

	public float score;

	public float scoreOld;

	private PhotonView photonView;

	private bool _canUserUseFacebookComposer;

	private bool _hasPublishPermission;

	private bool _hasPublishActions;

	private string _SocialMessage()
	{
		if (!Storager.hasKey(Defs.COOPScore))
		{
			Storager.setInt(Defs.COOPScore, 0, false);
		}
		int @int = Storager.getInt(Defs.COOPScore, false);
		bool flag = prefs.GetInt("COOP", 0) == 1;
		int int2 = prefs.GetInt("Rating", 0);
		string applicationUrl = Defs.ApplicationUrl;
		if (isIwin)
		{
			return (!flag) ? string.Format("Now I have {0} wons! Try Pixel Gun 3D right now! {1}", int2, applicationUrl) : string.Format("Now I have {0} score! Try Pixel Gun 3D right now! {1}", @int, applicationUrl);
		}
		return (!flag) ? string.Format("I won {0} matches at this time! Try Pixel Gun 3D right now! {1}", int2, applicationUrl) : string.Format("I received {0} scores at this time! Try Pixel Gun 3D right now! {1}", @int, applicationUrl);
	}

	private string _SocialSentSuccess(string SocialName)
	{
		return "Message was sent to " + SocialName;
	}

	private void completionHandler(string error, object result)
	{
		if (error != null)
		{
			Debug.LogError(error);
		}
		else
		{
			Utils.logObject(result);
		}
	}

	private void Awake()
	{
		zombiePrefabs.AddRange(Defs.GetEnemiesFromThisCoopLevel(Application.loadedLevelName));
	}

	public void setScoreFromGlobalGameController()
	{
		score = GlobalGameController.Score;
		synchState();
	}

	[PunRPC]
	private void addPlayer(string _name)
	{
		_weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
		WeaponManager.infoClient item = default(WeaponManager.infoClient);
		item.name = _name;
		_weaponManager.players.Add(item);
	}

	[PunRPC]
	private void RunGame()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			gameObject.GetComponent<NetworkStartTable>().runGame = true;
		}
	}

	[PunRPC]
	private void delPlayer(string _name)
	{
		_weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
		for (int i = 0; i < _weaponManager.players.Count; i++)
		{
			if (_weaponManager.players[i].name.Equals(_name))
			{
				_weaponManager.players.RemoveAt(i);
			}
		}
	}

	public void sendDelMyPlayer()
	{
		photonView.RPC("delPlayer", PhotonTargets.Others, prefs.GetString("NamePlayer", Defs.defaultPlayerName));
	}

	private void playersTable()
	{
		GUIStyle gUIStyle = ((!isShowNickTable) ? start : restart);
		Texture texture = ((prefs.GetInt("COOP", 0) != 1) ? killsStyle : scoreTexture);
		GUI.DrawTexture(new Rect((float)Screen.width / 2f + ((float)playersWindow.normal.background.width / 2f - (float)texture.width * 1.6f) * koofScreen, (float)Screen.height * 0.55f - ((float)playersWindow.normal.background.height + (float)nicksStyle.height * 1.8f) * 0.5f * koofScreen, (float)texture.width * koofScreen, (float)texture.height * koofScreen), texture);
		GUI.DrawTexture(new Rect((float)Screen.width / 2f - (float)playersWindow.normal.background.width / 2f * koofScreen, (float)Screen.height * 0.55f - ((float)playersWindow.normal.background.height + (float)nicksStyle.height * 1.8f) * 0.5f * koofScreen, (float)nicksStyle.width * koofScreen, (float)nicksStyle.height * koofScreen), nicksStyle);
		if ((prefs.GetString("TypeGame").Equals("server") || runGame || prefs.GetString("TypeConnect").Equals("inet")) && (!isShowNickTable || !prefs.GetString("TypeConnect").Equals("local")) && GUI.Button(new Rect((float)Screen.width * 0.9f - (float)gUIStyle.normal.background.width / 2f * (float)Screen.height / 768f, (float)Screen.height * 0.9f - (float)gUIStyle.normal.background.height / 2f * (float)Screen.height / 768f, (float)(gUIStyle.normal.background.width * Screen.height) / 768f, (float)(gUIStyle.normal.background.height * Screen.height) / 768f), string.Empty, gUIStyle))
		{
			isShowNickTable = false;
			CountKills = 0;
			score = 0f;
			GlobalGameController.Score = 0;
			synchState();
			startPlayer();
			return;
		}
		playersWindow.fontSize = Mathf.RoundToInt(30f * koofScreen);
		playersWindowFrags.fontSize = Mathf.RoundToInt(30f * koofScreen);
		playersWindowFrags.alignment = TextAnchor.UpperRight;
		GUILayout.Space((float)Screen.height * 0.55f - (float)playersWindow.normal.background.height * 0.5f * koofScreen);
		GUILayout.BeginHorizontal(GUILayout.Height((float)playersWindow.normal.background.height * koofScreen));
		GUILayout.Space((float)Screen.width * 0.5f - (float)playersWindow.normal.background.width * 0.5f * koofScreen);
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, playersWindow);
		if (showTable)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
			for (int i = 1; i < array.Length; i++)
			{
				GameObject gameObject = array[i];
				int num = i - 1;
				while (num >= 0 && ((prefs.GetInt("COOP", 0) != 1) ? ((float)array[num].GetComponent<NetworkStartTable>().CountKills) : array[num].GetComponent<NetworkStartTable>().score) < ((prefs.GetInt("COOP", 0) != 1) ? ((float)gameObject.GetComponent<NetworkStartTable>().CountKills) : gameObject.GetComponent<NetworkStartTable>().score))
				{
					array[num + 1] = array[num];
					num--;
				}
				array[num + 1] = gameObject;
			}
			if (array.Length > 0)
			{
				GameObject[] array2 = array;
				foreach (GameObject gameObject2 in array2)
				{
					GUILayout.Space(20f * koofScreen);
					GUILayout.BeginHorizontal();
					GUILayout.Space(20f * koofScreen);
					if (gameObject2.Equals(_weaponManager.myTable))
					{
						playersWindow.normal.textColor = new Color(1f, 1f, 0f, 1f);
						playersWindowFrags.normal.textColor = new Color(1f, 1f, 0f, 1f);
					}
					else
					{
						playersWindow.normal.textColor = new Color(0.7843f, 0.7843f, 0.7843f, 1f);
						playersWindowFrags.normal.textColor = new Color(0.7843f, 0.7843f, 0.7843f, 1f);
					}
					GUILayout.Label(gameObject2.GetComponent<NetworkStartTable>().NamePlayer, playersWindow, GUILayout.Width((float)playersWindow.normal.background.width * koofScreen * 0.75f));
					if (prefs.GetInt("COOP", 0) == 1)
					{
						float num2 = gameObject2.GetComponent<NetworkStartTable>().score;
						GUILayout.Label((num2 != -1f) ? num2.ToString() : "0", playersWindowFrags, GUILayout.Width((float)playersWindow.normal.background.width * koofScreen * 0.1f));
					}
					else
					{
						int countKills = gameObject2.GetComponent<NetworkStartTable>().CountKills;
						GUILayout.Label((countKills != -1) ? countKills.ToString() : "0", playersWindowFrags, GUILayout.Width((float)playersWindow.normal.background.width * koofScreen * 0.1f));
					}
					GUILayout.Space(20f * koofScreen);
					GUILayout.EndHorizontal();
				}
			}
			GUILayout.EndScrollView();
			GUILayout.EndHorizontal();
		}
		else
		{
			if (oldSpisokName.Length > 0)
			{
				GUI.DrawTexture(new Rect((float)Screen.width * 0.5f + (float)playersWindow.normal.background.width * 0.7f * koofScreen, (float)Screen.height * 0.55f - (float)playersWindow.normal.background.height * 0.45f * koofScreen, (float)kubochek.width * koofScreen, (float)kubochek.height * koofScreen), kubochek);
				for (int k = 0; k < oldSpisokName.Length; k++)
				{
					if (oldIndexMy == k)
					{
						playersWindow.normal.textColor = new Color(1f, 1f, 0f, 1f);
						playersWindowFrags.normal.textColor = new Color(1f, 1f, 0f, 1f);
					}
					else
					{
						playersWindow.normal.textColor = new Color(0.7843f, 0.7843f, 0.7843f, 1f);
						playersWindowFrags.normal.textColor = new Color(0.7843f, 0.7843f, 0.7843f, 1f);
					}
					GUILayout.Space(20f * koofScreen);
					GUILayout.BeginHorizontal();
					GUILayout.Space(20f * koofScreen);
					string text = oldCountLilsSpisok[k];
					GUILayout.Label(oldSpisokName[k], playersWindow, GUILayout.Width((float)playersWindow.normal.background.width * koofScreen * 0.85f));
					GUILayout.Label((!text.Equals("-1")) ? text.ToString() : "0", playersWindowFrags, GUILayout.Width((float)playersWindow.normal.background.width * koofScreen * 0.1f));
					GUILayout.Space(20f * koofScreen);
					GUILayout.EndHorizontal();
				}
			}
			GUILayout.EndScrollView();
			GUILayout.EndHorizontal();
			addCoins = 0;
			if (prefs.GetInt("COOP", 0) == 1)
			{
				int[] mincoins = Defs.GetDifficultyMinCoins(Defs.GetDifficultyForThisLevel(Application.loadedLevelName));
				int reward = Defs.GetDifficultyReward(Defs.GetDifficultyForThisLevel(Application.loadedLevelName));
				if (int.Parse(oldCountLilsSpisok[0]) >= mincoins[0] && int.Parse(oldCountLilsSpisok[0]) < mincoins[1])
				{
					addCoins = reward / 3;
				}
				if (int.Parse(oldCountLilsSpisok[0]) >= mincoins[1] && int.Parse(oldCountLilsSpisok[0]) < mincoins[2])
				{
					addCoins = reward / 2;
				}
				if (int.Parse(oldCountLilsSpisok[0]) >= mincoins[2])
				{
					addCoins = reward;
				}
			}
			if (prefs.GetInt("CustomGame", 0) == 0 && prefs.GetInt("COOP", 0) != 1)
			{
				addCoins = 1 * 10;
			}
			if (addCoins > 0)
			{
				if (oldIndexMy == 0)
				{
					playersWindowFrags.normal.textColor = new Color(1f, 1f, 0f, 1f);
				}
				else
				{
					playersWindowFrags.normal.textColor = new Color(0.7843f, 0.7843f, 0.7843f, 1f);
				}
				playersWindowFrags.fontSize = Mathf.RoundToInt(23f * koofScreen);
				playersWindowFrags.alignment = TextAnchor.UpperLeft;
			}
		}
		if (isShowNickTable)
		{
			if (addCoins > 0 && oldIndexMy == 0)
			{
				GUI.DrawTexture(new Rect((float)Screen.width / 2f - (float)zagolovokWiner.width / 2f * koofScreen, 0f, (float)zagolovokWiner.width * koofScreen, (float)zagolovokWiner.height * koofScreen), zagolovokWiner);
			}
			else
			{
				zagolovokStyle.fontSize = Mathf.RoundToInt(60f * (float)Screen.height / 768f);
				zagolovokStyle.normal.textColor = new Color(0f, 0f, 0f, 1f);
				GUI.Label(new Rect((float)Screen.width / 2f - ((float)head_players.width / 2f + 4f) * koofScreen, (float)Screen.height * 0.15f - (float)head_players.height / 2f * (float)Screen.height / 768f, (float)head_players.width * koofScreen, (float)head_players.height * koofScreen), string.Empty + nickPobeditelya + " WINS!", zagolovokStyle);
				GUI.Label(new Rect((float)Screen.width / 2f - ((float)head_players.width / 2f - 4f) * koofScreen, (float)Screen.height * 0.15f - (float)head_players.height / 2f * (float)Screen.height / 768f, (float)head_players.width * koofScreen, (float)head_players.height * koofScreen), string.Empty + nickPobeditelya + " WINS!", zagolovokStyle);
				GUI.Label(new Rect((float)Screen.width / 2f - ((float)head_players.width / 2f + 4f) * koofScreen, (float)Screen.height * 0.15f - ((float)head_players.height / 2f + 4f) * (float)Screen.height / 768f, (float)head_players.width * koofScreen, (float)head_players.height * koofScreen), string.Empty + nickPobeditelya + " WINS!", zagolovokStyle);
				GUI.Label(new Rect((float)Screen.width / 2f - ((float)head_players.width / 2f + 4f) * koofScreen, (float)Screen.height * 0.15f - ((float)head_players.height / 2f - 4f) * (float)Screen.height / 768f, (float)head_players.width * koofScreen, (float)head_players.height * koofScreen), string.Empty + nickPobeditelya + " WINS!", zagolovokStyle);
				GUI.Label(new Rect((float)Screen.width / 2f - ((float)head_players.width / 2f - 4f) * koofScreen, (float)Screen.height * 0.15f - ((float)head_players.height / 2f + 4f) * (float)Screen.height / 768f, (float)head_players.width * koofScreen, (float)head_players.height * koofScreen), string.Empty + nickPobeditelya + " WINS!", zagolovokStyle);
				GUI.Label(new Rect((float)Screen.width / 2f - ((float)head_players.width / 2f - 4f) * koofScreen, (float)Screen.height * 0.15f - ((float)head_players.height / 2f - 4f) * (float)Screen.height / 768f, (float)head_players.width * koofScreen, (float)head_players.height * koofScreen), string.Empty + nickPobeditelya + " WINS!", zagolovokStyle);
				GUI.Label(new Rect((float)Screen.width / 2f - (float)head_players.width / 2f * koofScreen, (float)Screen.height * 0.15f - ((float)head_players.height / 2f + 4f) * (float)Screen.height / 768f, (float)head_players.width * koofScreen, (float)head_players.height * koofScreen), string.Empty + nickPobeditelya + " WINS!", zagolovokStyle);
				GUI.Label(new Rect((float)Screen.width / 2f - (float)head_players.width / 2f * koofScreen, (float)Screen.height * 0.15f - ((float)head_players.height / 2f - 4f) * (float)Screen.height / 768f, (float)head_players.width * koofScreen, (float)head_players.height * koofScreen), string.Empty + nickPobeditelya + " WINS!", zagolovokStyle);
				zagolovokStyle.normal.textColor = new Color(1f, 1f, 0f, 1f);
				GUI.Label(new Rect((float)Screen.width / 2f - (float)head_players.width / 2f * koofScreen, (float)Screen.height * 0.15f - (float)head_players.height / 2f * (float)Screen.height / 768f, (float)head_players.width * koofScreen, (float)head_players.height * koofScreen), string.Empty + nickPobeditelya + " WINS!", zagolovokStyle);
			}
			Rect position = new Rect((float)Screen.width * 0.5f + (float)facebookStyle.normal.background.width * 0.05f * koofScreen, (float)Screen.height * 0.923f - (float)Screen.height * 0.0525f, (float)facebookStyle.normal.background.width * koofScreen, (float)facebookStyle.normal.background.height * koofScreen);
			Rect position2 = new Rect((float)Screen.width * 0.5f - (float)facebookStyle.normal.background.width * 1.05f * koofScreen, (float)Screen.height * 0.923f - (float)Screen.height * 0.0525f, (float)facebookStyle.normal.background.width * koofScreen, (float)facebookStyle.normal.background.height * koofScreen);
			if (!Application.isEditor)
			{
				if (GUI.Button(position, string.Empty, twitterStyle))
				{
					FlurryPluginWrapper.LogTwitter();
					InitTwitter();
				}
				if (GUI.Button(position2, string.Empty, facebookStyle))
				{
					FlurryPluginWrapper.LogFacebook();
					InitFacebook();
				}
			}
		}
		else
		{
			if (prefs.GetInt("CustomGame", 0) == 1)
			{
				GUI.DrawTexture(new Rect((float)Screen.width / 2f - (float)head_players.width / 2f * koofScreen, 0f, (float)head_players.width * koofScreen, (float)head_players.height * koofScreen), head_players);
			}
			else
			{
				GUI.DrawTexture(new Rect((float)Screen.width / 2f - (float)head_players.width / 2f * koofScreen, 0f, (float)head_players.width * koofScreen, (float)head_players.height * koofScreen), zagolovokStart);
			}
			if (prefs.GetInt("COOP", 0) == 1)
			{
				GUI.DrawTexture(new Rect((float)Screen.width / 2f - (float)plashkaStartCoop.width * 0.5f * koofScreen, (float)Screen.height - (float)plashkaStartCoop.height * koofScreen, (float)plashkaStartCoop.width * koofScreen, (float)plashkaStartCoop.height * koofScreen), plashkaStartCoop);
			}
			else
			{
				GUI.DrawTexture(new Rect((float)Screen.width / 2f - (float)plashkaStartMultu.width * 0.5f * koofScreen, (float)Screen.height - (float)plashkaStartMultu.height * koofScreen, (float)plashkaStartMultu.width * koofScreen, (float)plashkaStartMultu.height * koofScreen), plashkaStartMultu);
			}
		}
		if (!GUI.Button(new Rect((float)Screen.width * 0.1f - (float)back.active.background.width / 2f * (float)Screen.height / 768f, (float)Screen.height * 0.9f - (float)back.active.background.height / 2f * (float)Screen.height / 768f, (float)(back.active.background.width * Screen.height) / 768f, (float)(back.active.background.height * Screen.height) / 768f), string.Empty, back))
		{
			return;
		}
		if (prefs.GetString("TypeConnect").Equals("local"))
		{
			//sendDelMyPlayer();
			//if (prefs.GetString("TypeGame").Equals("server"))
			//{
			//	PhotonNetwork.Disconnect();
			//	GameObject.FindGameObjectWithTag("NetworkTable").GetComponent<LANBroadcastService>().StopBroadCasting();
			//}
			//else if (Network.connections.Length == 1)
			//{
			//	Debug.Log("Disconnecting: " + Network.connections[0].ipAddress + ":" + Network.connections[0].port);
			//	Network.CloseConnection(Network.connections[0], true);
			//}
			//if (_purchaseActivityIndicator == null)
			//{
			//	Debug.LogWarning("_purchaseActivityIndicator == null");
			//}
			//else
			//{
			//	_purchaseActivityIndicator.SetActive(false);
			//}
			//ConnectGUI.Local();
		}
		else
		{
			sendDelMyPlayer();
			if (_purchaseActivityIndicator == null)
			{
				Debug.LogWarning("_purchaseActivityIndicator == null");
			}
			else
			{
				_purchaseActivityIndicator.SetActive(false);
			}
			prefs.SetInt("ExitGame", 1);
			PhotonNetwork.LeaveRoom();
		}
	}

	[Beebyte.Obfuscator.SkipRename]
	public void startPlayer()
	{
		_playerPrefab = Resources.Load("Player") as GameObject;
		_cam = GameObject.FindGameObjectWithTag("CamTemp");
		_cam.SetActive(false);
		_weaponManager.useCam = null;
		GameObject gameObject = zoneCreatePlayer[UnityEngine.Random.Range(0, zoneCreatePlayer.Length)];
		BoxCollider component = gameObject.GetComponent<BoxCollider>();
		Vector2 vector = new Vector2(component.size.x * gameObject.transform.localScale.x, component.size.z * gameObject.transform.localScale.z);
		Rect rect = new Rect(gameObject.transform.position.x - vector.x / 2f, gameObject.transform.position.z - vector.y / 2f, vector.x, vector.y);
		Vector3 position = new Vector3(rect.x + UnityEngine.Random.Range(0f, rect.width), gameObject.transform.position.y + 2f, rect.y + UnityEngine.Random.Range(0f, rect.height));
		if (prefs.GetInt("StartAfterDisconnect") == 1)
		{
			position = GlobalGameController.posMyPlayer;
		}
		GameObject myPlayer = null;
		if (prefs.GetString("TypeConnect").Equals("inet"))
		{
			myPlayer = PhotonNetwork.Instantiate("Player", position, base.transform.rotation, 0);
			GameObject.FindGameObjectWithTag("GameController").GetComponent<BonusCreator>().BeginCreateBonuses();
		}
		else
		{
			_playerPrefab = Resources.Load("Player") as GameObject;
			//myPlayer = (GameObject)Network.Instantiate(_playerPrefab, position, base.transform.rotation, 0);
		}
		currentCamera = Camera.main;
		_weaponManager.myPlayer = myPlayer;
		GameObject.FindGameObjectWithTag("GameController").GetComponent<Initializer>().SetupObjectThatNeedsPlayer();
		showTable = false;
	}

	public static Camera currentCamera;

	[PunRPC]
	private void setState(string _namePlayer, int _countKills, int _oldCountLills, float _score)
	{
		NamePlayer = _namePlayer;
		CountKills = _countKills;
		oldCountKills = _oldCountLills;
		score = _score;
	}

	public void addZombiManager()
	{
		int num = PhotonNetwork.AllocateViewID();
		photonView.RPC("addZombiManagerRPC", PhotonTargets.All, base.transform.position, base.transform.rotation, num);
	}

	[PunRPC]
	private void addZombiManagerRPC(Vector3 pos, Quaternion rot, int id1)
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(zombieManagerPrefab, pos, rot);
		PhotonView component = gameObject.GetComponent<PhotonView>();
		component.viewID = id1;
	}

	public void addBonus(int _id, int _type, Vector3 _pos, Quaternion rot)
	{
		photonView.RPC("addBonusPhoton", PhotonTargets.Others, _id, _type, _pos, rot);
	}

	[PunRPC]
	public void addBonusPhoton(int _id, int _type, Vector3 _pos, Quaternion rot)
	{
		GameObject.FindGameObjectWithTag("GameController").GetComponent<BonusCreator>().addBonusFromPhotonRPC(_id, _type, _pos, rot);
	}

	[PunRPC]
	private void addBonusPhotonNewClientRPC(int playerId, int _id, int _type, Vector3 _pos, Quaternion rot)
	{
		if (playerId == PhotonNetwork.player.ID)
		{
			Debug.Log("add bonus " + _id + " " + GetComponent<PhotonView>().viewID);
			GameObject.FindGameObjectWithTag("GameController").GetComponent<BonusCreator>().addBonusFromPhotonRPC(_id, _type, _pos, rot);
		}
	}

	[PunRPC]
	private void addZombiManagerNewClientRPC(int playerId, Vector3 pos, Quaternion rot, int id1)
	{
		if (playerId == PhotonNetwork.player.ID)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("ZombiCreator");
			if (!(gameObject != null) || id1 != gameObject.GetComponent<PhotonView>().viewID)
			{
				GameObject gameObject2 = (GameObject)UnityEngine.Object.Instantiate(zombieManagerPrefab, pos, rot);
				PhotonView component = gameObject2.GetComponent<PhotonView>();
				component.viewID = id1;
			}
		}
	}

	public void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		if (prefs.GetInt("COOP", 0) == 1 && (bool)photonView && photonView.isMine && GameObject.FindGameObjectWithTag("ZombiCreator") != null && GameObject.FindGameObjectWithTag("ZombiCreator").GetComponent<PhotonView>().owner.ID == PhotonNetwork.player.ID)
		{
			photonView.RPC("addZombiManagerNewClientRPC", PhotonTargets.Others, player.ID, base.transform.position, base.transform.rotation, GameObject.FindGameObjectWithTag("ZombiCreator").GetComponent<PhotonView>().viewID);
			GameObject[] array = GameObject.FindGameObjectsWithTag("Enemy");
			GameObject[] array2 = array;
			foreach (GameObject gameObject in array2)
			{
				if (!gameObject.GetComponent<ZombiUpravlenie>().deaded)
				{
					photonView.RPC("addZombiNewClientRPC", PhotonTargets.Others, player.ID, gameObject.GetComponent<ZombiUpravlenie>().typeZombInMas, gameObject.transform.position, gameObject.GetComponent<PhotonView>().viewID);
				}
			}
			GameObject[] array3 = GameObject.FindGameObjectsWithTag("Bonus");
			GameObject[] array4 = array3;
			foreach (GameObject gameObject2 in array4)
			{
				photonView.RPC("addBonusPhotonNewClientRPC", PhotonTargets.Others, player.ID, gameObject2.GetComponent<PhotonView>().viewID, gameObject2.GetComponent<SettingBonus>().typeOfMass, gameObject2.transform.position, gameObject2.transform.rotation);
			}
		}
		if ((bool)photonView && photonView.isMine)
		{
			synchState();
		}
	}

	[PunRPC]
	private void addZombiNewClientRPC(int _playerId, int typeOfZomb, Vector3 pos, int _id)
	{
		Debug.Log(string.Empty + GetComponent<PhotonView>().owner.ID + " " + PhotonNetwork.player.ID);
		if (_playerId != PhotonNetwork.player.ID)
		{
			return;
		}
		if (typeOfZomb < 8055)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Enemy");
			GameObject[] array2 = array;
			foreach (GameObject gameObject in array2)
			{
				if (gameObject.GetComponent<PhotonView>().viewID == _id)
				{
					return;
				}
			}
			GameObject gameObject2 = (GameObject)UnityEngine.Object.Instantiate(zombiePrefabs[typeOfZomb].prefab, pos, Quaternion.identity);
			gameObject2.name = zombiePrefabs[typeOfZomb].name;
			PhotonView component = gameObject2.GetComponent<PhotonView>();
			component.viewID = _id;
			return;
		}
		GameObject[] array3 = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] array4 = array3;
		foreach (GameObject gameObject in array4)
		{
			if (gameObject.GetComponent<PhotonView>().viewID == _id)
			{
				return;
			}
		}
		int the = typeOfZomb - 8055;
		GameObject gameObject3 = (GameObject)UnityEngine.Object.Instantiate(Resources.Load<GameObject>("bosses/boss" + the), pos, Quaternion.identity);
		PhotonView component2 = gameObject3.GetComponent<PhotonView>();
		component2.viewID = _id;
	}

	[Beebyte.Obfuscator.SkipRename]
	public void synchState()
	{
		if (prefs.GetInt("COOP", 0) == 0)
		{
			GlobalGameController.Score = CountKills;
		}
		else
		{
			GlobalGameController.Score = Mathf.RoundToInt(score);
		}
		photonView.RPC("setState", PhotonTargets.Others, NamePlayer, CountKills, oldCountKills, score);
		if (prefs.GetInt("COOP", 0) != 1)
		{
		}
	}

	private void InitTwitter()
	{
		Debug.Log("InitTwitter(): init");
		string empty = string.Empty;
		string empty2 = string.Empty;
		if (GlobalGameController.isFullVersion)
		{
			empty = "cuMbTHM8izr9Mb3bIfcTxA";
			empty2 = "mpTLWIku4kIaQq7sTTi91wRLlvAxADhalhlEresnuI";
		}
		else
		{
			empty = "Jb7CwCaMgCQQiMViQRNHw";
			empty2 = "zGVrax4vqgs3CYf04O7glsoRbNT3vhIafte6lfm8w";
		}
		ServiceLocator.TwitterFacade.Init(empty, empty2);
		if (!ServiceLocator.TwitterFacade.IsLoggedIn())
		{
			TwitterLogin();
		}
		else
		{
			TwitterPost();
		}
	}

	private void TwitterLogin()
	{
		TwitterManager.loginSucceededEvent += OnTwitterLogin;
		TwitterManager.loginFailedEvent += OnTwitterLoginFailed;
		ServiceLocator.TwitterFacade.ShowLoginDialog();
	}

	private void OnTwitterLogin(string s)
	{
		TwitterManager.loginSucceededEvent -= OnTwitterLogin;
		TwitterManager.loginFailedEvent -= OnTwitterLoginFailed;
		TwitterPost();
	}

	private void OnTwitterLoginFailed(string _error)
	{
		TwitterManager.loginSucceededEvent -= OnTwitterLogin;
		TwitterManager.loginFailedEvent -= OnTwitterLoginFailed;
	}

	private void TwitterPost()
	{
		TwitterManager.requestDidFinishEvent += OnTwitterPost;
		ServiceLocator.TwitterFacade.PostStatusUpdate(_SocialMessage());
	}

	private void OnTwitterPost(object result)
	{
		if (result != null)
		{
			TwitterManager.requestDidFinishEvent -= OnTwitterPost;
			showMessagTiwtter = true;
			Invoke("hideMessagTwitter", 3f);
		}
	}

	private void OnTwitterPostFailed(string _error)
	{
		TwitterManager.requestDidFinishEvent -= OnTwitterPost;
	}

	[Beebyte.Obfuscator.SkipRename]
	private void hideMessag()
	{
		showMessagFacebook = false;
	}

	[Beebyte.Obfuscator.SkipRename]
	private void hideMessagTwitter()
	{
		showMessagTiwtter = false;
	}

	private void InitFacebook()
	{
		if (!Application.isEditor)
		{
			clickButtonFacebook = true;
			if (!ServiceLocator.FacebookFacade.IsSessionValid())
			{
				Debug.Log("!isSessionValid");
				string[] permissions = new string[1] { "email" };
				ServiceLocator.FacebookFacade.LoginWithReadPermissions(permissions);
			}
			else
			{
				Debug.Log("isSessionValid");
				OnEventFacebookLogin();
			}
		}
	}

	private void InitFacebookEvents()
	{
		if (!Application.isEditor)
		{
			FacebookManager.reauthorizationSucceededEvent += OnEventFacebookLogin;
			FacebookManager.loginFailedEvent += OnEventFacebookLoginFailed;
			FacebookManager.sessionOpenedEvent += OnEventFacebookLogin;
		}
	}

	private void CleanFacebookEvents()
	{
		if (!Application.isEditor)
		{
			FacebookManager.reauthorizationSucceededEvent -= OnEventFacebookLogin;
			FacebookManager.loginFailedEvent -= OnEventFacebookLoginFailed;
			FacebookManager.sessionOpenedEvent -= OnEventFacebookLogin;
		}
	}

	private void OnEventFacebookLogin()
	{
		if (Application.isEditor || !clickButtonFacebook)
		{
			return;
		}
		Debug.Log("OnEventFacebookLogin");
		if (!ServiceLocator.FacebookFacade.IsSessionValid())
		{
			return;
		}
		if (_hasPublishPermission)
		{
			Debug.Log("sendMessag");
			clickButtonFacebook = false;
			showMessagFacebook = true;
			Invoke("hideMessag", 3f);
			Facebook.instance.postMessage(_SocialMessage(), completionHandler);
			return;
		}
		Debug.Log("poluchau permissions");
		string[] permissions = new string[2] { "publish_actions", "publish_stream" };
		ServiceLocator.FacebookFacade.ReauthorizeWithPublishPermissions(permissions, FacebookSessionDefaultAudience.Everyone);
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	_hasPublishPermission = true;
		//	_hasPublishActions = true;
		//}
	}

	private void facebookGraphReqCompl(object result)
	{
		Utils.logObject(result);
	}

	private void facebookSessionOpened()
	{
		if (!Application.isEditor)
		{
			_hasPublishPermission = ServiceLocator.FacebookFacade.GetSessionPermissions().Contains("publish_stream");
			_hasPublishActions = ServiceLocator.FacebookFacade.GetSessionPermissions().Contains("publish_actions");
		}
	}

	private void facebookreauthorizationSucceededEvent()
	{
		if (!Application.isEditor)
		{
			_hasPublishPermission = ServiceLocator.FacebookFacade.GetSessionPermissions().Contains("publish_stream");
			_hasPublishActions = ServiceLocator.FacebookFacade.GetSessionPermissions().Contains("publish_actions");
		}
	}

	private void OnEventFacebookLoginFailed(P31Error error)
	{
		clickButtonFacebook = false;
		Debug.Log("OnEventFacebookLoginFailed=" + error);
	}

	private void Start()
	{
		InitFacebookEvents();
		if (!Application.isEditor)
		{
			ServiceLocator.FacebookFacade.Init();
			FacebookSessionLoginBehavior sessionLoginBehavior = FacebookSessionLoginBehavior.SSO_WITH_FALLBACK;
			ServiceLocator.FacebookFacade.SetSessionLoginBehavior(sessionLoginBehavior);
		}
		if (!Application.isEditor)
		{
			FacebookManager.graphRequestCompletedEvent += facebookGraphReqCompl;
			FacebookManager.sessionOpenedEvent += facebookSessionOpened;
			FacebookManager.reauthorizationSucceededEvent += facebookreauthorizationSucceededEvent;
			_canUserUseFacebookComposer = ServiceLocator.FacebookFacade.CanUserUseFacebookComposer();
		}
		photonView = PhotonView.Get(this);
		// yeah i'm not 100% sure why this says "networkview" but it doesn't matter
		Debug.Log("add NetworkView" + prefs.GetString("TypeGame").Equals("server"));
		if (prefs.GetInt("COOP", 0) == 1 && prefs.GetString("TypeConnect").Equals("inet") && photonView.isMine && prefs.GetString("TypeGame").Equals("server"))
		{
			addZombiManager();
		}
		_purchaseActivityIndicator = StoreKitEventListener.purchaseActivityInd;
		zoneCreatePlayer = GameObject.FindGameObjectsWithTag((prefs.GetInt("COOP", 0) != 1) ? "MultyPlayerCreateZone" : "MultyPlayerCreateZone");
		_weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
		if (prefs.GetInt("MultyPlayer") == 1 && ((prefs.GetString("TypeConnect").Equals("inet") && photonView.isMine)))
		{
			Debug.Log("Start " + GlobalGameController.Score);
			if (prefs.GetInt("StartAfterDisconnect") == 0)
			{
				showTable = true;
			}
			else
			{
				showTable = GlobalGameController.showTableMyPlayer;
				if (!showTable)
				{
					Invoke("startPlayer", 0.1f);
				}
			}
			currentCamera = GameObject.FindGameObjectWithTag("GameController").GetComponent<Initializer>().tc.GetComponent<Camera>();
			tempCam.SetActive(true);
			_weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
			string text = (NamePlayer = _weaponManager.gameObject.GetComponent<FilterBadWorld>().FilterString(prefs.GetString("NamePlayer", Defs.defaultPlayerName)));
			if (prefs.GetString("TypeGame").Equals("server"))
			{
				addPlayer(prefs.GetString("NamePlayer", Defs.defaultPlayerName));
				if (prefs.GetInt("MultyPlayer") == 1)
				{
					photonView.RPC("addPlayer", PhotonTargets.OthersBuffered, text);
				}
			}
			else
			{
				Debug.Log("addPlayer client  " + photonView);
				photonView.RPC("addPlayer", PhotonTargets.AllBuffered, text);
			}
			if (prefs.GetInt("StartAfterDisconnect") == 1)
			{
				CountKills = GlobalGameController.Score;
				score = GlobalGameController.Score;
				Invoke("synchState", 1f);
			}
			else
			{
				CountKills = -1;
				score = -1f;
				Invoke("synchState", 1f);
			}
			synchState();
			mySkin = SkinsManager.currentMultiplayerSkin();
			sendMySkin();
			sendHWID();
		}
		else
		{
			showTable = false;
		}
	}

	[PunRPC]
	private void setMySkin(string str)
	{
		if (base.transform.GetComponent<PhotonView>() == null)
		{
			return;
		}
		byte[] data = Convert.FromBase64String(str);
		Texture2D texture2D = new Texture2D(64, 32);
		texture2D.LoadImage(data);
		texture2D.filterMode = FilterMode.Point;
		texture2D.Apply();
		mySkin = texture2D;
		GameObject[] array = GameObject.FindGameObjectsWithTag("PlayerGun");
		if (array == null)
		{
			return;
		}
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (prefs.GetString("TypeConnect").Equals("inet") && (bool)gameObject && (bool)gameObject.GetComponent<PhotonView>() && gameObject.GetComponent<PhotonView>().owner.Equals(base.transform.GetComponent<PhotonView>().owner))
			{
				gameObject.GetComponent<Player_move_c>().setMyTamble(base.gameObject);
				break;
			}
		}
	}

	public string myHWID = string.Empty;

	[PunRPC]
	private void setMyHWID(string hwid) {
		myHWID = hwid;
	}

	private void sendMySkin()
	{
		Texture2D texture2D = mySkin as Texture2D;
		byte[] inArray = texture2D.EncodeToPNG();
		string text = Convert.ToBase64String(inArray);
		photonView.RPC("setMySkin", PhotonTargets.AllBuffered, text);
	}

	private void sendHWID()
	{
		photonView.RPC("setMyHWID", PhotonTargets.AllBuffered, IncomprehensibleGarbler.GetMacAddress());
	}

	[PunRPC]
	private void ZombiManagerZamenaIdRPC(int _id)
	{
		if (GameObject.FindGameObjectWithTag("ZombiCreator") == null)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(zombieManagerPrefab, base.transform.position, base.transform.rotation);
			PhotonView component = gameObject.GetComponent<PhotonView>();
			component.viewID = _id;
		}
		else
		{
			GameObject.FindGameObjectWithTag("ZombiCreator").GetComponent<PhotonView>().viewID = _id;
		}
	}

	[PunRPC]
	private void ZombiZamenaIdRPC(int _idOld, int _idNew)
	{
		Debug.Log("ZombiZamenaIdRPC  " + _idOld + " " + _idNew);
		GameObject[] array = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (gameObject.GetComponent<PhotonView>().viewID == _idOld)
			{
				gameObject.GetComponent<PhotonView>().viewID = _idNew;
				break;
			}
		}
	}

	private void Update()
	{
		if (prefs.GetInt("MultyPlayer") == 1 && prefs.GetString("TypeConnect").Equals("inet") && photonView.isMine)
		{
			GlobalGameController.showTableMyPlayer = showTable;
		}
		if (prefs.GetString("TypeConnect").Equals("local") && prefs.GetString("TypeGame").Equals("server"))
		{
			LANBroadcastService component = GetComponent<LANBroadcastService>();
			if (component != null)
			{
				component.serverMessage.connectedPlayers = GameObject.FindGameObjectsWithTag("NetworkTable").Length;
			}
		}
		if (prefs.GetString("TypeConnect").Equals("inet") && prefs.GetInt("COOP") == 1 && photonView.isMine)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("ZombiCreator");
			if (gameObject != null && gameObject.GetComponent<PhotonView>().owner == null)
			{
				GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
				int viewID = array[0].GetComponent<PhotonView>().viewID;
				GameObject gameObject2 = array[0];
				GameObject[] array2 = array;
				foreach (GameObject gameObject3 in array2)
				{
					if (gameObject3.GetComponent<PhotonView>().viewID < viewID)
					{
						gameObject2 = gameObject3;
						viewID = gameObject3.GetComponent<PhotonView>().viewID;
					}
				}
				if (gameObject2 == base.gameObject)
				{
					int num = PhotonNetwork.AllocateViewID();
					gameObject.GetComponent<PhotonView>().viewID = num;
					photonView.RPC("ZombiManagerZamenaIdRPC", PhotonTargets.Others, num);
					GameObject[] array3 = GameObject.FindGameObjectsWithTag("Enemy");
					GameObject[] array4 = array3;
					foreach (GameObject gameObject4 in array4)
					{
						int viewID2 = gameObject4.GetComponent<PhotonView>().viewID;
						int num2 = PhotonNetwork.AllocateViewID();
						photonView.RPC("ZombiZamenaIdRPC", PhotonTargets.Others, viewID2, num2);
						gameObject4.GetComponent<PhotonView>().viewID = num2;
					}
					Debug.Log("Set My Upravlenie");
				}
			}
		}
		if (!(timerShow >= 0f))
		{
			return;
		}
		timerShow -= Time.deltaTime;
		if (timerShow < 0f)
		{
			if (_purchaseActivityIndicator == null)
			{
				Debug.LogWarning("_purchaseActivityIndicator == null");
			}
			else
			{
				_purchaseActivityIndicator.SetActive(false);
			}
			ConnectGUI.Local();
		}
	}

	private void OnDisconnectedFromServer(DisconnectCause info)
	{
		Debug.Log("OnDisconnectedFromServer");
		showDisconnectFromServer = true;
		timerShow = 3f;
	}

	private void OnPlayerDisconnected(PhotonPlayer player)
	{
		PhotonNetwork.RemoveRPCs(player);
		PhotonNetwork.DestroyPlayerObjects(player);
		GameObject[] array = GameObject.FindGameObjectsWithTag("PlayerGun");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (!player.ID.Equals(gameObject.GetComponent<Player_move_c>().ID))
			{
				continue;
			}
			GameObject[] array3 = GameObject.FindGameObjectsWithTag("ObjectLabel");
			GameObject[] array4 = array3;
			foreach (GameObject gameObject2 in array4)
			{
				if (gameObject2.GetComponent<NewLabel>().target == gameObject.transform)
				{
					UnityEngine.Object.Destroy(gameObject2);
					break;
				}
			}
			UnityEngine.Object.Destroy(gameObject.transform.parent.transform.gameObject);
		}
	}

	private void OnFailedToConnectToMasterServer(DisconnectCause info)
	{
		Debug.Log("Could not connect to master server: " + info);
		showDisconnectFromMasterServer = true;
		timerShow = 3f;
	}

	public void win(string winner)
	{
		if (!Storager.hasKey(Defs.COOPScore))
		{
			Storager.setInt(Defs.COOPScore, 0, false);
		}
		int @int = Storager.getInt(Defs.COOPScore, false);
		if (GlobalGameController.Score > @int)
		{
			Storager.setInt(Defs.COOPScore, GlobalGameController.Score, false);
		}
		nickPobeditelya = winner;
		GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
		for (int i = 1; i < array.Length; i++)
		{
			GameObject gameObject = array[i];
			int num = i - 1;
			while (num >= 0 && ((prefs.GetInt("COOP", 0) != 1) ? ((float)array[num].GetComponent<NetworkStartTable>().CountKills) : array[num].GetComponent<NetworkStartTable>().score) < ((prefs.GetInt("COOP", 0) != 1) ? ((float)gameObject.GetComponent<NetworkStartTable>().CountKills) : gameObject.GetComponent<NetworkStartTable>().score))
			{
				array[num + 1] = array[num];
				num--;
			}
			array[num + 1] = gameObject;
		}
		oldSpisokName = new string[array.Length];
		oldCountLilsSpisok = new string[array.Length];
		for (int j = 0; j < array.Length; j++)
		{
			if ((bool)_weaponManager && array[j].Equals(_weaponManager.myTable))
			{
				oldIndexMy = j;
				addCoins = 0;
				if (j == 0 && prefs.GetInt("CustomGame", 0) == 0 && prefs.GetInt("COOP", 0) == 1)
				{
					if (array[j].GetComponent<NetworkStartTable>().score >= (float)minCoin1 && array[j].GetComponent<NetworkStartTable>().score < (float)minCoin2)
					{
						addCoins = 1 * 10;
					}
					if (array[j].GetComponent<NetworkStartTable>().score >= (float)minCoin2 && array[j].GetComponent<NetworkStartTable>().score < (float)minCoin3)
					{
						addCoins = 1 * 10;
					}
					if (array[j].GetComponent<NetworkStartTable>().score >= (float)minCoin3)
					{
						addCoins = 1 * 10;
					}
					FlurryPluginWrapper.LogCoinEarned_COOP();
				}
				if (j == 0 && prefs.GetInt("CustomGame", 0) == 0 && prefs.GetInt("COOP", 0) != 1)
				{
					addCoins = 1 * 10;
					FlurryPluginWrapper.LogCoinEarned_Deathmatch();
				}
				if (addCoins > 0)
				{
					if (!Storager.hasKey(Defs.Coins))
					{
						Storager.setInt(Defs.Coins, 0, false);
					}
					int int2 = Storager.getInt(Defs.Coins, false);
					Storager.setInt(Defs.Coins, int2 + addCoins, false);
					prefs.SetInt("AddCoins", 1);
				}
			}
			oldSpisokName[j] = array[j].GetComponent<NetworkStartTable>().NamePlayer;
			if (prefs.GetInt("COOP", 0) == 1)
			{
				oldCountLilsSpisok[j] = ((array[j].GetComponent<NetworkStartTable>().score == -1f) ? (string.Empty + array[j].GetComponent<NetworkStartTable>().scoreOld) : (string.Empty + array[j].GetComponent<NetworkStartTable>().score));
			}
			else
			{
				oldCountLilsSpisok[j] = ((array[j].GetComponent<NetworkStartTable>().CountKills == -1) ? (string.Empty + array[j].GetComponent<NetworkStartTable>().oldCountKills) : (string.Empty + array[j].GetComponent<NetworkStartTable>().CountKills));
			}
		}
		oldCountKills = CountKills;
		scoreOld = score;
		score = -1f;
		GlobalGameController.Score = -1;
		CountKills = -1;
		synchState();
		currentCamera = GameObject.FindGameObjectWithTag("GameController").GetComponent<Initializer>().tc.GetComponent<Camera>();
		if (prefs.GetString("TypeConnect").Equals("local"))
		{
			UnityEngine.Object.DestroyObject(_weaponManager.myPlayer);
		}
		else
		{
			PhotonNetwork.Destroy(_weaponManager.myPlayer);
			if (prefs.GetInt("COOP", 0) == 1)
			{
				GameObject[] array2 = GameObject.FindGameObjectsWithTag("Enemy");
				for (int k = 0; k < array2.Length; k++)
				{
					UnityEngine.Object.Destroy(array2[k]);
				}
			}
		}
		GameObject gameObject2 = GameObject.FindGameObjectWithTag("DamageFrame");
		if (gameObject2 != null)
		{
			UnityEngine.Object.Destroy(gameObject2);
		}
		if (_cam != null)
		{
			_cam.SetActive(true);
		}
		isShowNickTable = true;
	}

	private void end()
	{
		Debug.Log("end");
		if (prefs.GetString("TypeConnect").Equals("local"))
		{
			if (prefs.GetString("TypeGame").Equals("server"))
			{
				PhotonNetwork.Disconnect();
				GameObject.FindGameObjectWithTag("NetworkTable").GetComponent<LANBroadcastService>().StopBroadCasting();
			}
			_purchaseActivityIndicator.SetActive(false);
			ConnectGUI.Local();
		}
		else
		{
			PhotonNetwork.LeaveRoom();
		}
	}

	private void finishTable()
	{
		playersTable();
	}

	private void OnGUI()
	{
		Rect position = new Rect(0f, (float)Screen.height * 0.6f, Screen.width, (float)Screen.height - (float)Screen.height * 4f / 5f);
		messagesStyle.fontSize = Mathf.RoundToInt((float)(30 * Screen.height) / 768f);
		if (showDisconnectFromServer)
		{
			GUI.Label(position, "Server left the game...", messagesStyle);
			GUI.enabled = false;
		}
		if (showDisconnectFromMasterServer)
		{
			GUI.Label(position, "Server left the game...", messagesStyle);
		}
		if (showTable)
		{
			playersTable();
		}
		if (isShowNickTable)
		{
			finishTable();
		}
		if (showMessagFacebook)
		{
			labelStyle.fontSize = Player_move_c.FontSizeForMessages;
			GUI.Label(Player_move_c.SuccessMessageRect(), _SocialSentSuccess("Facebook"), labelStyle);
		}
		if (showMessagTiwtter)
		{
			labelStyle.fontSize = Player_move_c.FontSizeForMessages;
			GUI.Label(Player_move_c.SuccessMessageRect(), _SocialSentSuccess("Twitter"), labelStyle);
		}
		GUI.enabled = true;
	}

	private void OnConnectedToServer()
	{
		Debug.Log("OnConnectedToServer");
	}

	private void OnDestroy()
	{
		CleanFacebookEvents();
	}
}
