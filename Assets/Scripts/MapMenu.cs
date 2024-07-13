using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;
using Newtonsoft.Json;
using UnityEngine;

public class MapMenu : MonoBehaviour
{
	private MapInfo _info;

	private GameObject _mapButton;

	public UIGrid grid;
	public UIScrollView scrollView;
	public AudioClip btnSnd;
	public UIPanel selectionPanel, selectedPanel, roomListPanel, customSetupPanel;
	public UITexture loadingBar;
	public float rotationF;
	[Header("Selected Map Panel")]
	public Transform mapNameTransform;
	public Transform playBtnTransform;
	public Transform customBtnTransform;
	public UILabel mapNameLabel;
	public UITexture mapIcon;
	public ButtonHandler play, custom, back;
	private MapInfo.Map selectedMap;
	public AudioSource mapMusicAudioSource;
	[Header("Selecting Map Panel")]
	public ButtonHandler backToMenu;
	public ButtonHandler openRoomList;
	[Header("Room List Panel")]
	public GameObject preset;
	public UIGrid roomListGrid;
	public ButtonHandler backToSelection;
	[Header("Custom Setup Panel")]
	public ButtonHandler backToSelected;
	public ButtonHandler startGame, addKTW, removeKTW, addMP, removeMP;
	public UIInput roomNameInput;
	public UILabel maxPlayersLabel, maxKillsLabel;
	IEnumerator Start()
	{
		while(PhotonNetwork.connected)
		{
			PhotonNetwork.Disconnect();
			yield return null;
		}
		openRoomList.gameObject.SetActive(false);
		loadingBar.gameObject.SetActive(true);
		backToMenu.Clicked += (object sender, System.EventArgs e) => {
			PhotonNetwork.Disconnect();
			Application.LoadLevel(Defs.CurrentMainMenuScene);
		};
		prefs.SetString("TypeConnect", "inet");
		#if USES_WEBSOCKET
		Dictionary<string, object> connectionArgs = new Dictionary<string, object>();
		connectionArgs["uid"] = handler.data.UserController.Instance.ID;
		connectionArgs["ak"] = handler.data.UserController.Instance.AuthKey;
		connectionArgs["coop"] = prefs.GetInt("COOP", 0);
		handler.networking.WebsocketHandler.CallAction("request_connection", (string data) => {
			Dictionary<string, object> resultDictionary = handler.networking.WebsocketHandler.Decrypt(JsonConvert.DeserializeObject<Dictionary<string, object>>(data));
			if ((string)resultDictionary["response"] == "success")
			{
				PhotonNetwork.ConnectUsingSettings((string)resultDictionary["settings"]);
			}
		}, connectionArgs);
		#else
		if (prefs.GetInt("COOP", 0) == 1)
		{
			PhotonNetwork.ConnectUsingSettings("v" + GlobalGameController.AppVersion + "COOP");
		}
		else
		{
			PhotonNetwork.ConnectUsingSettings("v" + GlobalGameController.AppVersion);
		}
		#endif
		yield break;
	}
	public void OnJoinedLobby()
	{
		#if USES_WEBSOCKET
		handler.logger.Log.AddLine("[ConnectGUI::OnJoinedLobby] OnConnectedToPhoton");
		#endif
		RealStart();
	}
	void RealStart()
	{
		loadingBar.gameObject.SetActive(false);
		openRoomList.gameObject.SetActive(true);
		HOTween.Init(true, true, true);
		HOTween.EnableOverwriteManager();
		_info = Resources.Load<GameObject>("MapInfo").GetComponent<MapInfo>();
		_mapButton = Resources.Load<GameObject>("MapButton");
		List<MapInfo.Map> mapInfoList = (prefs.GetInt("COOP", 0) == 1 ? _info.coopMaps : _info.deathmatchMaps);
		foreach (MapInfo.Map map in mapInfoList)
		{
			MapButton mapBtn = Instantiate(_mapButton, grid.transform).GetComponent<MapButton>();
			mapBtn.nameLabel.text = map.mapName;
			mapBtn.mapIcon.mainTexture = map.icon;
			mapBtn.GetComponent<UIDragScrollView>().scrollView = scrollView;
			mapBtn.GetComponent<ButtonHandler>().Clicked += (object sender, System.EventArgs e) => {
				selectedMap = map;
				HandleMapPressed();
			};
		}
		grid.Reposition();
		back.Clicked += HandleHideMap;
		openRoomList.Clicked += (object sender, System.EventArgs e) => {
			UpdateRoomList();
			GetComponent<AudioSource>().PlayOneShot(btnSnd);
			customSetupPanel.alpha = 0;
			roomListPanel.alpha = 1;
			selectionPanel.alpha = 0;
			selectedPanel.alpha = 0;
		};
		backToSelection.Clicked += (object sender, System.EventArgs e) => {
			GetComponent<AudioSource>().PlayOneShot(btnSnd);
			customSetupPanel.alpha = 0;
			roomListPanel.alpha = 0;
			selectionPanel.alpha = 1;
			selectedPanel.alpha = 0;
		};
		custom.Clicked += (object sender, System.EventArgs e) => {
			customSetupPanel.alpha = 1;
			roomListPanel.alpha = 0;
			selectionPanel.alpha = 0;
			selectedPanel.alpha = 0;
		};
		play.Clicked += (object sender, System.EventArgs e) => {
			GetComponent<AudioSource>().PlayOneShot(btnSnd);
			ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
			hashtable["pass"] = string.Empty;
			hashtable["map"] = mapInfoList.IndexOf(selectedMap);
			prefs.SetString("TypeGame", "client");
			prefs.SetInt("CustomGame", 0);
			PhotonNetwork.JoinRandomRoom(hashtable, 0);
		};
		backToSelected.Clicked += (object sender, System.EventArgs e) => {
			GetComponent<AudioSource>().PlayOneShot(btnSnd);
			customSetupPanel.alpha = 0;
			roomListPanel.alpha = 0;
			selectionPanel.alpha = 0;
			selectedPanel.alpha = 1;
		};
		addKTW.Clicked += (object sender, System.EventArgs e) => {
			GetComponent<AudioSource>().PlayOneShot(btnSnd);
			int num3 = int.Parse(killToWin) + 1;
			if (num3 > 50)
			{
				num3 = 50;
			}
			killToWin = num3.ToString();
			maxKillsLabel.text = killToWin;
		};
		removeKTW.Clicked += (object sender, System.EventArgs e) => {
			GetComponent<AudioSource>().PlayOneShot(btnSnd);
			int num3 = int.Parse(killToWin) - 1;
			if (num3 < 10)
			{
				num3 = 10;
			}
			killToWin = num3.ToString();
			maxKillsLabel.text = killToWin;
		};
		addMP.Clicked += (object sender, System.EventArgs e) => {
			GetComponent<AudioSource>().PlayOneShot(btnSnd);
			int num3 = int.Parse(limitsPlayer) + 1;
			if (num3 > 10)
			{
				num3 = 10;
			}
			limitsPlayer = num3.ToString();
			maxPlayersLabel.text = limitsPlayer;
		};
		removeMP.Clicked += (object sender, System.EventArgs e) => {
			GetComponent<AudioSource>().PlayOneShot(btnSnd);
			int num3 = int.Parse(limitsPlayer) - 1;
			if (num3 < 2)
			{
				num3 = 2;
			}
			limitsPlayer = num3.ToString();
			maxPlayersLabel.text = limitsPlayer;
		};
		startGame.Clicked += (object sender, System.EventArgs e) => {
			customRoomName = roomNameInput.value;
			string[] propsToListInLobby = new string[3] { "map", "MaxKill", "pass" };
			ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
			hashtable.Add("map", mapInfoList.IndexOf(selectedMap));
			hashtable.Add("MaxKill", int.Parse(killToWin));
			hashtable.Add("pass", password);
			prefs.SetString("MapName", selectedMap.sceneName);
			prefs.SetString("MaxKill", killToWin);
			string roomName = name;
			goMapName = selectedMap.sceneName;
			mapLoading = (Texture2D)selectedMap.icon;
			RoomOptions roomOptions = new RoomOptions
			{
			    IsVisible = true,
			    IsOpen = true,
			    MaxPlayers = byte.Parse(limitsPlayer),
			    CustomRoomProperties = hashtable,
			    CustomRoomPropertiesForLobby = propsToListInLobby
			};
			PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
		};
	}
	private string killToWin = "10";
	private string limitsPlayer = "4";
	private string password = "";
	private string customRoomName = "Random Server";
	private void OnPhotonRandomJoinFailed()
	{
		password = "";
		customRoomName = "";
		limitsPlayer = "4";
		killToWin = "10";
		List<MapInfo.Map> mapInfoList = _info.CurrentMapsList;
		prefs.SetString("TypeGame", "server");
		string[] propsToListInLobby = new string[3] { "map", "MaxKill", "pass" };
		int selectMapIndex = mapInfoList.IndexOf(selectedMap);
		goMapName = selectedMap.sceneName;
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		hashtable.Add("map", selectMapIndex);
		hashtable.Add("MaxKill", int.Parse(killToWin));
		hashtable.Add("pass", password);
		prefs.SetString("MapName", selectedMap.mapName);
		prefs.SetString("MaxKill", killToWin);
		goMapName = selectedMap.sceneName;
		mapLoading = (Texture2D)selectedMap.icon;	
		string roomName = customRoomName;
		RoomOptions roomOptions = new RoomOptions
		{
		    IsVisible = true,
		    IsOpen = true,
		    MaxPlayers = byte.Parse(limitsPlayer),
		    CustomRoomProperties = hashtable,
		    CustomRoomPropertiesForLobby = propsToListInLobby
		};
		PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
	}
	private void UpdateRoomList()
	{
		RoomInfo[] roomList = PhotonNetwork.GetRoomList();
		List<MapInfo.Map> mapInfoList = _info.CurrentMapsList;
		foreach (Transform p in roomListGrid.transform)
		{
			if (p.gameObject != preset)
			{
				Destroy(p.gameObject);
			}
		}
		foreach (RoomInfo room in roomList)
		{
			GameObject newRoomBtn = Instantiate(preset);
			RoomBtn x = newRoomBtn.GetComponent<RoomBtn>();
			newRoomBtn.transform.SetParent(roomListGrid.transform);
			if (room.Name.Length == 36 && room.Name.IndexOf("-") == 8 && room.Name.LastIndexOf("-") == 23)
			{
				x.roomName.text = "Random Server";
			} else {
				x.roomName.text = room.Name;
			}
			Debug.Log((int)room.CustomProperties["map"]);
			try
			{
				x.mapName.text = mapInfoList[(int)room.CustomProperties["map"]].mapName;
			} catch (System.Exception e)
			{

			}
			x.playerCount.text = room.PlayerCount + "/" + room.MaxPlayers;
			x.mine.Clicked += (object sender, System.EventArgs e) => {
				// completely ignore pass cause it doesnt matter for now
				GetComponent<AudioSource>().PlayOneShot(btnSnd);
				prefs.SetString("MaxKill", room.CustomProperties["MaxKill"].ToString());
				prefs.SetString("MapName", mapInfoList[(int)room.CustomProperties["map"]].sceneName);
				goMapName = mapInfoList[(int)room.CustomProperties["map"]].sceneName;
				mapLoading = (Texture2D)mapInfoList[(int)room.CustomProperties["map"]].icon;	
				PhotonNetwork.JoinRoom(room.Name);
			};
			newRoomBtn.transform.localScale = Vector3.one;
			newRoomBtn.SetActive(true);
		}
		roomListGrid.Reposition();
	}
	private void OnJoinedRoom()
	{
		prefs.SetString("RoomName", PhotonNetwork.room.Name);
		PhotonNetwork.isMessageQueueRunning = false;
		StartCoroutine(MoveToGameScene());
	}
	private void OnCreatedRoom()
	{
		Debug.Log("OnCreatedRoom");
		prefs.SetString("RoomName", PhotonNetwork.room.Name);
		StartCoroutine(MoveToGameScene());
	}
	private string goMapName;
	private Texture2D mapLoading;
	private IEnumerator MoveToGameScene()
	{
		MenuBackgroundMusic.Instance.gameObject.GetComponent<AudioSource>().volume = 1;
		while (PhotonNetwork.room == null)
		{
			yield return 0;
		}
		PhotonNetwork.isMessageQueueRunning = false;
		yield return Resources.UnloadUnusedAssets();
		LoadConnectScene.loading = mapLoading;
		LoadConnectScene.sceneToLoad = goMapName;
		yield return Application.LoadLevelAsync("PromScene");
	}
	void OnReceivedRoomListUpdate()
	{
		UpdateRoomList();
	}
	void HandleHideMap(object sender, System.EventArgs e)
	{
		GetComponent<AudioSource>().PlayOneShot(btnSnd);
		StartCoroutine(ToggleBackgroundMusic(MenuBackgroundMusic.Instance.gameObject.GetComponent<AudioSource>(), true));
		StartCoroutine(ToggleBackgroundMusic(mapMusicAudioSource, false));
		selectionPanel.alpha = 1;
		customSetupPanel.alpha = 0;
		roomListPanel.alpha = 0;
		selectedPanel.alpha = 0;
	}
	IEnumerator ToggleBackgroundMusic(AudioSource aus, bool on)
	{
		if (on)
		{
			for (float i = 0; i < 1+Time.deltaTime; i += Time.deltaTime)
			{
				aus.volume = Mathf.Clamp(Easing.EaseIn.Cubic(i), 0, 1);
				yield return null;
			}
		}
		else
		{
			for (float i = 1; i > 0-Time.deltaTime; i -= Time.deltaTime)
			{
				aus.volume = Mathf.Clamp(Easing.EaseOut.Cubic(i), 0, 1);
				yield return null;
			}
		}
		yield break;
	}
	void HandleMapPressed()
	{
		mapMusicAudioSource.clip = Resources.Load<GameObject>("backgroundmusic/BackgroundMusic_Level" + selectedMap.backgroundMusic).GetComponent<AudioSource>().clip;
		GetComponent<AudioSource>().PlayOneShot(btnSnd);
		mapMusicAudioSource.Play();
		StartCoroutine(ToggleBackgroundMusic(MenuBackgroundMusic.Instance.gameObject.GetComponent<AudioSource>(), false));
		StartCoroutine(ToggleBackgroundMusic(mapMusicAudioSource, true));
		HOTween.From(mapNameTransform, 1f, new TweenParms().Prop("localPosition", new Vector3(-594, 233, 0)).Ease(EaseType.EaseOutCubic));
		HOTween.From(playBtnTransform, 1.2f, new TweenParms().Prop("localPosition", new Vector3(-334, -76, 0)).Ease(EaseType.EaseOutCubic));
		HOTween.From(customBtnTransform, 1.4f, new TweenParms().Prop("localPosition", new Vector3(-334, -187, 0)).Ease(EaseType.EaseOutCubic));
		mapNameLabel.text = selectedMap.mapName;
		mapIcon.mainTexture = selectedMap.icon;
		customSetupPanel.alpha = 0;
		selectionPanel.alpha = 0;
		roomListPanel.alpha = 0;
		selectedPanel.alpha = 1;
	}

	void Update()
	{
		//scroll.localPosition = new Vector3(scroll.localPosition.x, scroll.localPosition.y + mouseScrollThing, scroll.localPosition.z);
		loadingBar.transform.rotation = Quaternion.Euler(new Vector3(0, 0, loadingBar.transform.rotation.eulerAngles.z + rotationF));
	}
}
