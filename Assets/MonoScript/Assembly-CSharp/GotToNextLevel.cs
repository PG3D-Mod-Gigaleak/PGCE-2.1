using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GotToNextLevel : MonoBehaviour
{
	private Action OnPlayerAddedAct;

	private GameObject _player;

	private Player_move_c _playerMoveC;

	private bool runLoading;

	public Texture levelResult;

	private void Awake()
	{
		OnPlayerAddedAct = _003CAwake_003Em__4;
		Initializer.PlayerAddedEvent += OnPlayerAddedAct;
	}

	private void OnDestroy()
	{
		Initializer.PlayerAddedEvent -= OnPlayerAddedAct;
	}

	private void Update()
	{
		if (!(_player == null) && !(_playerMoveC == null) && !runLoading && Vector3.Distance(base.transform.position, _player.transform.position) < 1.5f)
		{
			PlayerPrefs.SetFloat(Defs.CurrentHealthSett, _playerMoveC.CurHealth);
			PlayerPrefs.SetFloat(Defs.CurrentArmorSett, _playerMoveC.curArmor);
			runLoading = true;
			Debug.Log("end GlobalGameController.currentLevel " + GlobalGameController.currentLevel);
			if (PlayerPrefs.GetInt("FullVersion", 0) == 0 && GlobalGameController.currentLevel == 5)
			{
				GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<Player_move_c>().showGUIUnlockFullVersion = true;
				return;
			}
			AutoFade.LoadLevel("Loading", 2f, 0.5f, Color.white);
			levelResult = Resources.Load(Path.Combine("CoinsIndicationSystem", "level_complete")) as Texture;
		}
	}

	private void OnGUI()
	{
		if (levelResult != null)
		{
			float num = 520f * Defs.Coef;
			float num2 = 129f * Defs.Coef;
			GUI.DrawTexture(new Rect(((float)Screen.width - num) / 2f, ((float)Screen.height - num2) / 2f, num, num2), levelResult);
		}
	}

	[CompilerGenerated]
	private void _003CAwake_003Em__4()
	{
		_player = GameObject.FindGameObjectWithTag("Player");
		_playerMoveC = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<Player_move_c>();
	}
}
