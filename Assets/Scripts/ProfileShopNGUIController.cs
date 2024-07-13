using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class ProfileShopNGUIController : MonoBehaviour {
	public UIInput usernameInput;
	public SkinButton button;
	private WeaponManager _weaponManager;
	public IEnumerator Start() {
		_weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
		string skinB64 = CustomPrefs.CurrentSkin;
		byte[] B64byteData = System.Convert.FromBase64String(skinB64); 
		Texture2D skinTex = new Texture2D(1,1);
		skinTex.LoadImage(B64byteData);
		skinTex.filterMode = FilterMode.Point;
		skinTex.mipMapBias = 0;
		usernameInput.value = prefs.GetString("NamePlayer", "Player");
		button.skin.sharedMaterial = new Material(Shader.Find("Mobile/Diffuse"));
		button.skin.sharedMaterial.SetTexture("_MainTex", skinTex);
		usernameInput.onValidate += Validate;
		yield return null;
		AchievementNGUI.instance.gameObject.SetActive(true);
		// the toggle is weird, so i invert true and false
		catEarsToggle.value = (Storager.getInt("earsOn", false) == 0 ? true : false);
	}
	public char Validate(string text, int charIndex, char addedChar) {
		if ("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz?!^\"',.-_[]{} ".IndexOf(addedChar) == -1) {
			return default(char);
		}
		return addedChar;
	}
	public void OpenSkinSelector() {
		Application.LoadLevel("SkinMenu");
	}
	public void Back() {
		Debug.Log("let me out!!!");
		Application.LoadLevel(Defs.CurrentMainMenuScene);
	}
	public UIToggle catEarsToggle;
	private bool canActuallySet = false;
	public void OnCatEarsToggled() {
		// inverted cause retarded
		if (canActuallySet) {
			#if USES_WEBSOCKET
			if (catEarsToggle.value == (Storager.getInt("earsOn", false) == 0 ? true : false))
				return;
			catEarsToggle.gameObject.SetActive(false);
			handler.networking.WebsocketHandler.CallAction("update_player", (string data) => {
				Dictionary<string, object> resultDictionary = handler.networking.WebsocketHandler.Decrypt(JsonConvert.DeserializeObject<Dictionary<string, object>>(data));
				if ((string)resultDictionary["response"] == "success")
				{
					catEarsToggle.value = !Convert.ToBoolean((string)resultDictionary["catears_set"]);
					if (!catEarsToggle.value) {
						Achievements.Give("catears");
						Storager.setInt("earsOn", 1, false);
					} else {
						Storager.setInt("earsOn", 0, false);
					}
					catEarsToggle.gameObject.SetActive(true);
				}
			}, new Dictionary<string, object>(){
				{"uid", handler.data.UserController.Instance.ID},
				{"ak", handler.data.UserController.Instance.AuthKey},
				{"newcatears", !catEarsToggle.value},
			});
			#else
			if (!catEarsToggle.value) {
				Achievements.Give("catears");
				Storager.setInt("earsOn", 1, false);
			} else {
				Storager.setInt("earsOn", 0, false);
			}
			#endif
		}
		canActuallySet = true;
	}
	public void OnUsernameInputCHANGED() {
		if (usernameInput.value == prefs.GetString("NamePlayer", "Player"))
			return;
		usernameInput.gameObject.SetActive(false);
		#if USES_WEBSOCKET
		handler.networking.WebsocketHandler.CallAction("update_player", (string data) => {
			Dictionary<string, object> resultDictionary = handler.networking.WebsocketHandler.Decrypt(JsonConvert.DeserializeObject<Dictionary<string, object>>(data));
			if ((string)resultDictionary["response"] == "success")
			{
				prefs.SetString("NamePlayer", (string)resultDictionary["name_set"]);
				usernameInput.value = (string)resultDictionary["name_set"];
				usernameInput.gameObject.SetActive(true);
			}
		}, new Dictionary<string, object>(){
			{"uid", handler.data.UserController.Instance.ID},
			{"ak", handler.data.UserController.Instance.AuthKey},
			{"newname", _weaponManager.gameObject.GetComponent<FilterBadWorld>().FilterString(usernameInput.value)},
		});
		#else
		prefs.SetString("NamePlayer", _weaponManager.gameObject.GetComponent<FilterBadWorld>().FilterString(usernameInput.value));
		usernameInput.gameObject.SetActive(true);
		#endif
	}
}
