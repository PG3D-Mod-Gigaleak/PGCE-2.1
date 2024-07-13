using UnityEngine;

public class InGameGUI : MonoBehaviour
{
	public delegate float GetFloatVAlue();

	public delegate string GetString();

	public GetFloatVAlue health;

	public GetFloatVAlue armor;

	public GetString killsToMaxKills;

	public GetString timeLeft;

	public GameObject elixir;

	public GameObject scoreLabel;

	public GameObject enemiesLabel;

	public GameObject timeLabel;

	public GameObject killsLabel;

	private FirstPersonControl fpc
	{
		get
		{
			return WeaponManager.sharedManager.myPlayer.GetComponent<FirstPersonControl>();
		}
	}

	[HideInInspector] public Player_move_c playerMoveC;

	public GameObject altShot;

	public UIButtonColor shootButton;

	public UIButtonColor jumpButton;

	public UIButtonColor altShootButton;

	public GameObject entryPopup;

	public UILabel ammoLabel;

	public Transform weaponInstantiationPoint;

	private GameObject selectedWeapon;

	private bool jumping
	{
		get
		{
			return jumpButton.state == UIButtonColor.State.Pressed;
		}
	}

	public bool shooting
	{
		get
		{
			#if UNITY_EDITOR
			if (Globals.PlayerMove && Globals.PlayerMove.showingAdminInput)
				return false;
			#endif
			if (Globals.PlayerMove && Globals.PlayerMove.showChat)
				return false;
			return shootButton.state == UIButtonColor.State.Pressed;
		}
	}

	private bool altShooting
	{
		get
		{
			return altShootButton.state == UIButtonColor.State.Pressed;
		}
	}

	public GameObject[] aimSprites;

	public UITexture zoomTex;

	public UITexture healthBar, armorBar;

	public UILabel healthAmount, armorAmount, pingLabel, fpsLabel;

	private bool zoomed;

	public Animation hitmarker;
	public UIInput chatInput;
	public void Zoom(bool onOff, int index = 0)
	{
		zoomTex.gameObject.SetActive(onOff);
		zoomTex.mainTexture = Resources.Load<Texture>("scopes/" + index);
		foreach (GameObject aimSprite in aimSprites)
		{
			aimSprite.SetActive(!onOff);
		}
		zoomed = onOff;
	}

	private void Start()
	{
		if (prefs.GetInt("AddCoins", 0) == 1)
		{
			Invoke("GenerateMiganie", 1f);
			prefs.SetInt("AddCoins", 0);
		}
	}

	public void Hitmark()
	{
		hitmarker.Play("Hitmark");
	}

	public void Headmark()
	{
		hitmarker.Play("Headmarker");
	}

	public void Reload()
	{
		playerMoveC.SendMessage("ReloadPressed");
	}

	public void OpenChat()
	{
		playerMoveC.OpenChat();
	}

	public void SENDcHAT() {
		string chatInputVal = chatInput.value;
		if (string.IsNullOrEmpty(chatInputVal.Trim()))
			return;
		chatInput.isSelected = false;
		playerMoveC.showChat = false;
		chatInput.value = "";
		playerMoveC.SendChat(chatInputVal);
	}

	[Beebyte.Obfuscator.SkipRename]
	private void GenerateMiganie()
	{
		CoinsMessage.FireCoinsAddedEvent();
	}

	public void newEntryPopup(string name)
	{
		entryPopup.GetComponent<Animation>().PlayQueued("UnlockedNewThing");
	}

	public void ChangeWeapon(WeaponSounds sounds)
	{
        if (selectedWeapon != null)
        {
            Destroy(selectedWeapon);
        }

        selectedWeapon = Instantiate(sounds.bonusPrefab, weaponInstantiationPoint).gameObject;
		if (selectedWeapon.GetComponent<SkinnedMeshRenderer>())
		{
			GameObject newModel = new GameObject("NewModel_" + selectedWeapon.name);
			newModel.AddComponent<MeshFilter>().sharedMesh = selectedWeapon.GetComponent<SkinnedMeshRenderer>().sharedMesh;
			newModel.AddComponent<MeshRenderer>().materials = selectedWeapon.GetComponent<SkinnedMeshRenderer>().materials;
			newModel.transform.parent = selectedWeapon.transform;
			newModel.transform.localScale = Vector3.one;
			newModel.transform.localPosition = Vector3.zero;
			newModel.transform.localRotation = Quaternion.Euler(sounds.armoryRotation);
            newModel.layer = 10;
		}
        selectedWeapon.transform.localPosition = sounds.armoryPosition;
		selectedWeapon.transform.localRotation = Quaternion.Euler(sounds.armoryRotation);
		selectedWeapon.transform.localScale = sounds.armoryScale;
        
        selectedWeapon.layer = 10;
        foreach (Transform trf in selectedWeapon.gameObject.GetComponentsInChildren<Transform>())
        {
            trf.gameObject.layer = 10;
        }
	}

	private void Update()
	{
		if (Application.isMobilePlatform)
		{
			fpc.jumpButton.jumpPressed = jumping;
			if (shooting)
			{
				playerMoveC.SendMessage("ShotPressed");
			}
			else if (altShooting)
			{
				playerMoveC.SendMessage("ShotPressed", true);
			}
		}
		if (zoomed)
		{
			return;
		}
		if (PhotonNetwork.connectionState == ConnectionState.Connected && PhotonNetwork.room != null) {
			pingLabel.alpha = 1;
			fpsLabel.alpha = 1;
			pingLabel.text = "Ping (avg): " + PhotonNetwork.GetPing() + " ms";
			fpsLabel.text = "FPS (avg): " + /*(int)(Time.frameCount / Time.time)*/ (int)(1f / Time.unscaledDeltaTime) + " FPS";
		} else {
			pingLabel.alpha = 0;
			fpsLabel.alpha = 0;
		}
		if ((int)(1f / Time.unscaledDeltaTime) < 30f)
		{
			Debug.LogWarning($"FPS hit below 30! FPS count: {(1f / Time.unscaledDeltaTime)}");
		}
		if (prefs.GetInt("ShowFPS", 0) == 0) {
			pingLabel.alpha = 0;
			fpsLabel.alpha = 0;
		}
		aimSprites[0].transform.localPosition = new Vector3(0f, 8f + WeaponManager.sharedManager.currentWeaponSounds.tekKoof * WeaponManager.sharedManager.currentWeaponSounds.startZone.y * 0.5f, 0f);
		aimSprites[1].transform.localPosition = new Vector3(0f, -8f - WeaponManager.sharedManager.currentWeaponSounds.tekKoof * WeaponManager.sharedManager.currentWeaponSounds.startZone.y * 0.5f, 0f);
		aimSprites[3].transform.localPosition = new Vector3(8f + WeaponManager.sharedManager.currentWeaponSounds.tekKoof * WeaponManager.sharedManager.currentWeaponSounds.startZone.y * 0.5f, 0f, 0f);
		aimSprites[2].transform.localPosition = new Vector3(-8f - WeaponManager.sharedManager.currentWeaponSounds.tekKoof * WeaponManager.sharedManager.currentWeaponSounds.startZone.y * 0.5f, 0f, 0f);
		healthBar.fillAmount = (playerMoveC.CurHealth / playerMoveC.MaxHealth);
		armorBar.fillAmount = (playerMoveC.curArmor / playerMoveC.MaxArmor);
		healthAmount.text = Mathf.RoundToInt(playerMoveC.CurHealth) + "";
		armorAmount.text = Mathf.RoundToInt(playerMoveC.curArmor) + "";
	}
}
