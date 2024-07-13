using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class Initializer : MonoBehaviour
{
	public GameObject _purchaseActivityIndicator;

	public GameObject tc;

	public GameObject tempCam;

	public bool isDisconnect;

	public int countConnectToRoom;

	public float timerShowNotConnectToRoom;

	public Texture reconnectTexture;

	public Texture noConnectRoomTexture;

	public GUIStyle back;

	public bool isCancelReConnect;

	private GameObject _playerPrefab;

	public GameObject networkTablePref;

	private bool _isMultiplayer;

	public GUIStyle messagesStyle;

	public bool isNotConnectRoom;

	private Vector2 scrollPosition = Vector2.zero;

	private List<Vector3> _initPlayerPositions = new List<Vector3>();

	private List<float> _rots = new List<float>();

	private float koofScreen = (float)Screen.height / 768f;

	public WeaponManager _weaponManager;

	public bool showMaxPlayer;

	public bool showDisconnect;

	public float timerShow = -1f;

	public Transform playerPrefab;

	public Texture fonLoadingScene;

	private bool showLoading;

	public static event Action PlayerAddedEvent;

	private void Awake()
	{
		prefs.SetInt("ExitGame", 0);
		GameObject gameObject = null;
		if (prefs.GetInt("MultyPlayer") != 1)
		{
			gameObject = ((GlobalGameController.currentLevel != GlobalGameController.levelMapping[0]) ? (Resources.Load("BackgroundMusic/BackgroundMusic_Level" + (Defs.GetBGMNumberFromRandomByIndex(GlobalGameController.previousLevel))) as GameObject) : (Resources.Load("BackgroundMusic/BackgroundMusic_Level0") as GameObject));
		}
		else
		{
			int musicNum = Globals.CurrentMap.backgroundMusic;
			GlobalGameController.currentLevel = musicNum;
			gameObject = Resources.Load("BackgroundMusic/BackgroundMusic_Level" + musicNum) as GameObject;
		}
		UnityEngine.Object.Instantiate(gameObject);
		if (prefs.GetInt("MultyPlayer") != 1)
		{
			GameObject gameObject2 = GameObject.FindGameObjectWithTag("Configurator");
			CoinConfigurator component = gameObject2.GetComponent<CoinConfigurator>();
			if (component.CoinIsPresent)
			{
				GameObject original = Resources.Load("coin") as GameObject;
				UnityEngine.Object.Instantiate(original, component.pos, Quaternion.Euler(270f, 0f, 0f));
			}
		}
	}

	private void Start()
	{
		_purchaseActivityIndicator = StoreKitEventListener.purchaseActivityInd;
		prefs.SetInt("StartAfterDisconnect", 0);
		PhotonNetwork.isMessageQueueRunning = true;
		if (prefs.GetInt("MultyPlayer") == 1)
		{
			_isMultiplayer = true;
		}
		else
		{
			_isMultiplayer = false;
		}
		_weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
		_weaponManager.players.Clear();
		if (!_isMultiplayer)
		{
			_initPlayerPositions.Add(new Vector3(12f, 1f, 9f));
			_initPlayerPositions.Add(new Vector3(17f, 1f, -15f));
			_initPlayerPositions.Add(new Vector3(-30f, 1f, -35f));
			_initPlayerPositions.Add(new Vector3(0f, 1f, 0f));
			_initPlayerPositions.Add(new Vector3(-33f, 1.2f, -13f));
			_initPlayerPositions.Add(new Vector3(-2.67f, 1f, 2.67f));
			_initPlayerPositions.Add(new Vector3(0f, 1f, 0f));
			_initPlayerPositions.Add(new Vector3(19f, 1f, -0.8f));
			_initPlayerPositions.Add(new Vector3(-28.5f, 1.75f, -3.73f));
			_initPlayerPositions.Add(new Vector3(-2.5f, 1.75f, 0f));
			_rots.Add(0f);
			_rots.Add(0f);
			_rots.Add(270f);
			_rots.Add(0f);
			_rots.Add(180f);
			_rots.Add(0f);
			_rots.Add(0f);
			_rots.Add(270f);
			_rots.Add(270f);
			_rots.Add(270f);
			int @int = Storager.getInt(Defs.EarnedCoins, false);
			if (@int > 0)
			{
				GameObject original = Resources.Load("MessageCoinsObject") as GameObject;
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(original);
			}
			AddPlayer();
			return;
		}
		List<MapInfo.Map> maps = MapInfo.Instance.CurrentMapsList;
		Vector3 position = Vector3.zero;
		Quaternion rotation = Quaternion.identity;
		if (GameObject.Find("InitCam"))
		{
			GameObject ic = GameObject.Find("InitCam");
			position = ic.transform.position;
			rotation = ic.transform.rotation;
		}
		else
		{
			position = Globals.CurrentMap.position;
			rotation = Globals.CurrentMap.rotation;
		}
		tc = UnityEngine.Object.Instantiate(tempCam, position, rotation) as GameObject;

		if (prefs.GetString("TypeConnect").Equals("local"))
		{
			if (prefs.GetString("TypeGame").Equals("client"))
			{
				//bool useNat = !PhotonNetwork.HavePublicAddress();
				//Network.useNat = useNat;
				//Debug.Log(_weaponManager.ServerIp + " " + Network.Connect(_weaponManager.ServerIp, 25002));
			}
			else
			{
				//_weaponManager.myTable = (GameObject)Network.Instantiate(networkTablePref, base.transform.position, base.transform.rotation, 0);
			}
		}
		else
		{
			_weaponManager.myTable = PhotonNetwork.Instantiate("NetworkTable", base.transform.position, base.transform.rotation, 0);
		}
	}

	[PunRPC]
	private void SpawnOnNetwork(Vector3 pos, Quaternion rot, int id1, PhotonPlayer np)
	{
		Transform transform = UnityEngine.Object.Instantiate(networkTablePref, pos, rot).transform;
		PhotonView component = transform.GetComponent<PhotonView>();
		component.viewID = id1;
	}

	private void AddPlayer()
	{
		_playerPrefab = Resources.Load("Player") as GameObject;
		int index = ((GlobalGameController.currentLevel != GlobalGameController.levelMapping[0]) ? GlobalGameController.previousLevel : 0);
		Vector3 position = ((GlobalGameController.currentLevel != GlobalGameController.levelMapping[0]) ? _initPlayerPositions[index] : new Vector3(-0.72f, 1.75f, -13.23f));
		float y = ((GlobalGameController.currentLevel != GlobalGameController.levelMapping[0]) ? _rots[index] : 0f);
		UnityEngine.Object.Instantiate(_playerPrefab, position, Quaternion.Euler(0f, y, 0f));
		Invoke("SetupObjectThatNeedsPlayer", 0.01f);
	}

	[Beebyte.Obfuscator.SkipRename]
	public void SetupObjectThatNeedsPlayer()
	{
		if (prefs.GetInt("MultyPlayer") == 1)
		{
			Initializer.PlayerAddedEvent();
			return;
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("CoinBonus");
		if ((bool)gameObject)
		{
			CoinBonus component = gameObject.GetComponent<CoinBonus>();
			if ((bool)component)
			{
				component.SetPlayer();
			}
		}
		GetComponent<ZombieCreator>().BeganCreateEnemies();
		GetComponent<BonusCreator>().BeginCreateBonuses();
		Initializer.PlayerAddedEvent();
	}

	private void OnGUI()
	{
		float num = (float)Screen.height / 768f;
		if (isDisconnect && timerShowNotConnectToRoom < 0f)
		{
			GUI.DrawTexture(new Rect((float)Screen.width * 0.5f - (float)reconnectTexture.width * num * 0.5f, (float)Screen.height * 0.7f - (float)reconnectTexture.height * 0.5f * num, (float)reconnectTexture.width * num, (float)reconnectTexture.height * num), reconnectTexture);
			if (GUI.Button(new Rect((float)Screen.width * 0.5f - (float)back.active.background.width / 2f * koofScreen, (float)Screen.height * 0.9f - (float)back.active.background.height / 2f * num, (float)back.active.background.width * num, (float)back.active.background.height * num), string.Empty, back))
			{
				isCancelReConnect = true;
				if (PhotonNetwork.connected)
				{
					ConnectGUI.Local();
				}
				else
				{
					LoadConnectScene.loading = null;
					LoadConnectScene.sceneToLoad = "NewMapList";
					Application.LoadLevel("PromScene");
				}
			}
		}
		if (isDisconnect && timerShowNotConnectToRoom > 0f)
		{
			timerShowNotConnectToRoom -= Time.deltaTime;
			if (timerShowNotConnectToRoom > 0f)
			{
				GUI.DrawTexture(new Rect((float)Screen.width * 0.5f - (float)noConnectRoomTexture.width * num * 0.5f, (float)Screen.height * 0.7f - (float)noConnectRoomTexture.height * 0.5f * num, (float)noConnectRoomTexture.width * num, (float)noConnectRoomTexture.height * num), noConnectRoomTexture);
			}
			else if (PhotonNetwork.connected)
			{
				ConnectGUI.Local();
			}
			else
			{
				LoadConnectScene.loading = null;
				LoadConnectScene.sceneToLoad = "NewMapList";
				Application.LoadLevel("PromScene");
			}
		}
		messagesStyle.alignment = TextAnchor.MiddleCenter;
		messagesStyle.fontSize = Mathf.RoundToInt(60f * (float)Screen.height / 768f);
		messagesStyle.normal.textColor = Color.white;
		Rect position = new Rect(0f, (float)Screen.height * 0.15f, Screen.width, (float)Screen.height * 0.2f);
		if (showLoading && prefs.GetInt("MultyPlayer") == 1 && prefs.GetString("TypeConnect").Equals("inet"))
		{
			if (_weaponManager.myTable != null)
			{
				_weaponManager.myTable.GetComponent<NetworkStartTable>().isShowNickTable = false;
				_weaponManager.myTable.GetComponent<NetworkStartTable>().showTable = false;
			}
			Rect position2 = new Rect(((float)Screen.width - 2048f * (float)Screen.height / 1154f) / 2f, 0f, 2048f * (float)Screen.height / 1154f, Screen.height);
			GUI.DrawTexture(position2, fonLoadingScene, ScaleMode.StretchToFill);
		}
		if (showMaxPlayer)
		{
			GUI.Label(position, "Server is full...", messagesStyle);
		}
		position.y = (float)Screen.height / 2f;
		if (showDisconnect)
		{
			GUI.Label(position, "Ops, internet fails...\ncheck the connection.", messagesStyle);
		}
	}

	private void Update()
	{
		if (timerShow > 0f)
		{
			timerShow -= Time.deltaTime;
			Debug.Log("OnLeftRoom (local) init update");
			showLoading = true;
			Invoke("goToConnect", 0.1f);
		}
	}

	private void OnConnectedToServer()
	{
		_weaponManager.myTable = (GameObject)PhotonNetwork.Instantiate("NetworkTable", base.transform.position, base.transform.rotation, 0);
		Debug.Log("OnConnectedToServer");
	}

	private void OnFailedToConnect(DisconnectCause error)
	{
		Debug.Log("Could not connect to server: " + error);
		if (error == DisconnectCause.MaxCcuReached)
		{
			showMaxPlayer = true;
		}
		if (error == DisconnectCause.ExceptionOnConnect)
		{
			showDisconnect = true;
		}
		timerShow = 5f;
		Debug.Log("timerShow=5f;");
		if (!(_weaponManager == null) && !(_weaponManager.myTable == null))
		{
			_weaponManager.myTable.GetComponent<NetworkStartTable>().isShowNickTable = false;
			_weaponManager.myTable.GetComponent<NetworkStartTable>().showTable = false;
		}
	}

	[Beebyte.Obfuscator.SkipRename]
	private void goToConnect()
	{
		ConnectGUI.Local();
	}

	public void OnLeftRoom()
	{
		Debug.Log("OnLeftRoom (local) init");
		if (prefs.GetInt("ExitGame") == 1)
		{
			showLoading = true;
			Invoke("goToConnect", 0.1f);
			if (!(_weaponManager == null) && !(_weaponManager.myTable == null))
			{
				_weaponManager.myTable.GetComponent<NetworkStartTable>().isShowNickTable = false;
				_weaponManager.myTable.GetComponent<NetworkStartTable>().showTable = false;
			}
		}
	}

	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("OnDisconnectedFromPhotoninit");
	}

	private void OnConnectionFail(DisconnectCause cause)
	{
		timerShowNotConnectToRoom = -1f;
		isCancelReConnect = false;
		isNotConnectRoom = false;
		countConnectToRoom = 0;
		prefs.SetString("TypeGame", "client");
		Debug.Log("OnConnectionFail " + GlobalGameController.Score);
		Debug.Log("OnConnectionFail " + cause);
		tc.SetActive(true);
		GameObject[] array = GameObject.FindGameObjectsWithTag("Bonus");
		for (int i = 0; i < array.Length; i++)
		{
			UnityEngine.Object.Destroy(array[i]);
		}
		GameObject[] array2 = GameObject.FindGameObjectsWithTag("Enemy");
		for (int j = 0; j < array2.Length; j++)
		{
			UnityEngine.Object.Destroy(array2[j]);
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("InGameGUI");
		if (gameObject != null)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		GameObject gameObject2 = GameObject.FindGameObjectWithTag("ChatViewer");
		if (gameObject2 != null)
		{
			UnityEngine.Object.Destroy(gameObject2);
		}
		isDisconnect = true;
		Invoke("ConnectToPhoton", 3f);
		_purchaseActivityIndicator.SetActive(true);
	}

	[Beebyte.Obfuscator.SkipRename]
	private void ConnectToPhoton()
	{
		if (!isCancelReConnect)
		{
			Debug.Log("ConnectToPhoton ");
			if (prefs.GetInt("COOP", 0) == 1)
			{
				PhotonNetwork.ConnectUsingSettings("v" + GlobalGameController.AppVersion + "COOP");
			}
			else
			{
				PhotonNetwork.ConnectUsingSettings("v" + GlobalGameController.AppVersion);
			}
		}
	}

	private void OnFailedToConnectToPhoton(object parameters)
	{
		Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + parameters);
		if (!isCancelReConnect)
		{
			Invoke("ConnectToPhoton", 3f);
		}
	}

	public void OnJoinedLobby()
	{
		ConnectToRoom();
	}

	[Beebyte.Obfuscator.SkipRename]
	private void ConnectToRoom()
	{
		Debug.Log("OnJoinedLobby " + prefs.GetString("RoomName"));
		if (!isCancelReConnect)
		{
			PhotonNetwork.JoinRoom(prefs.GetString("RoomName"));
		}
	}

	private void OnPhotonJoinRoomFailed()
	{
		countConnectToRoom++;
		Debug.Log("OnPhotonJoinRoomFailed - init");
		isNotConnectRoom = true;
		if (countConnectToRoom < 6)
		{
			Invoke("ConnectToRoom", 3f);
		}
		else
		{
			timerShowNotConnectToRoom = 3f;
		}
	}

	private void OnJoinedRoom()
	{
		_purchaseActivityIndicator.SetActive(false);
		Debug.Log("OnJoinedRoom - init");
		if (isDisconnect)
		{
			prefs.SetInt("StartAfterDisconnect", 1);
			_weaponManager.myTable = PhotonNetwork.Instantiate("NetworkTable", base.transform.position, base.transform.rotation, 0);
		}
		isDisconnect = false;
	}

	public void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Debug.Log("OnPhotonPlayerConnectedinit: " + player);
	}

	public void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log("OnPlayerDisconnecedinit: " + player);
	}

	public void OnReceivedRoomList()
	{
		Debug.Log("OnReceivedRoomListinit");
	}

	public void OnReceivedRoomListUpdate()
	{
		Debug.Log("OnReceivedRoomListUpdateinit");
	}

	public void OnConnectedToPhoton()
	{
		Debug.Log("OnConnectedToPhotoninit");
	}

	public void OnFailedToConnectToPhoton()
	{
		Debug.Log("OnFailedToConnectToPhotoninit");
	}

	public void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		Debug.Log("OnPhotonInstantiate init" + info.sender);
	}
}
