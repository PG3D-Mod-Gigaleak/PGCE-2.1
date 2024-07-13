using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

public class SkinMenu : MonoBehaviour
{
	public GameObject skinBtn;

	public Transform scroll;

	public UIGrid skinGrid;

	public AudioClip btnSound;

	private void PlayBtnSound()
	{
		GetComponent<AudioSource>().PlayOneShot(btnSound);
	}
    
	void Start()
	{
		Skins skins = Resources.Load<Skins>("Skins");
		foreach (Skins.Skin skin in skins.skins)
		{
			CreateSkinButton(skin);
		}
		foreach (Skins.Skin skin in SkinImporter.ImportedSkins())
		{
			CreateSkinButton(skin);
		}
	}

	public void EnterMenu()
	{
		AchievementNGUI.instance.gameObject.SetActive(false);
		Application.LoadLevel("ProfileShop");
	}

	public void SetSkin(Skins.Skin skin)
	{
		#if USES_WEBSOCKET
		handler.networking.WebsocketHandler.CallAction("update_player", (string data) => {
			Dictionary<string, object> resultDictionary = handler.networking.WebsocketHandler.Decrypt(JsonConvert.DeserializeObject<Dictionary<string, object>>(data));
			if ((string)resultDictionary["response"] == "success")
			{
				CustomPrefs.CurrentSkin = (string)resultDictionary["skin_set"];
			}
		}, new Dictionary<string, object>(){
			{"uid", handler.data.UserController.Instance.ID},
			{"ak", handler.data.UserController.Instance.AuthKey},
			{"newskin", Convert.ToBase64String(ImageConversion.EncodeToPNG((Texture2D)skin.skinTexture))},
		});
		#else
		CustomPrefs.CurrentSkin = Convert.ToBase64String(ImageConversion.EncodeToPNG((Texture2D)skin.skinTexture));
		#endif
	}

	public IEnumerator SetBack(Transform transform)
	{
		yield return new WaitForSeconds(0.1f);
		transform.localScale = Vector3.one;
		PlayBtnSound();
	}

	public void CreateSkinButton(Skins.Skin skin)
	{
		SkinButton button = Instantiate(skinBtn).GetComponent<SkinButton>();
		button.gameObject.SetActive(true);
		button.transform.parent = skinGrid.transform;
		button.nameLabel.text = skin.skin;
		button.button.color = skin.color;
		button.nameLabel.color = skin.color;
		button.skin.sharedMaterial = new Material(Shader.Find("Mobile/Diffuse"));
		button.skin.sharedMaterial.SetTexture("_MainTex", skin.skinTexture);
		button.button.GetComponent<ButtonHandler>().Clicked += (object sender2, EventArgs args2)=> { SetSkin(skin);  if (button.transform.localScale == Vector3.one) { button.transform.localScale *= 0.8f; } StartCoroutine(SetBack(button.transform)); };
		button.transform.localScale = Vector3.one;
		skinGrid.Reposition();
	}
}
