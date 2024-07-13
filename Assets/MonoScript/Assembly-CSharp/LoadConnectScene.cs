using System.Collections;
using UnityEngine;

public sealed class LoadConnectScene : MonoBehaviour
{
	public static string sceneToLoad = string.Empty;

	public static Texture loading;

	public UITexture loadingTexture;

	private GameObject aInd;

	private void Start()
	{
		//if (loading == null)
		//{
		//	loading = MenuSettings.GetCurrentMenu.loading;
		//}
		Invoke("_loadConnectScene", Time.unscaledDeltaTime);
		//aInd = StoreKitEventListener.purchaseActivityInd;
		//if (aInd == null)
		//{
		//	Debug.LogWarning("aInd == null");
		//}
		//else
		//{
		//	aInd.SetActive(true);
		//}
	}

	void Update()
	{
		if (loading != null)
		{
			loadingTexture.mainTexture = loading;
		}
	}

	private void OnGUI()
	{
		//Rect position = new Rect(((float)Screen.width - 2048f * (float)Screen.height / 1154f) / 2f, 0f, 2048f * (float)Screen.height / 1154f, Screen.height);
		//GUI.DrawTexture(position, loading, ScaleMode.StretchToFill);
	}

	[Beebyte.Obfuscator.SkipRename]
	private void _loadConnectScene()
	{
		StartCoroutine(lcs());
	}

	private IEnumerator lcs()
	{
		yield return Application.LoadLevelAsync(sceneToLoad);
		yield break;
	}

	private void OnDestroy()
	{
		if (aInd != null)
		{
			aInd.SetActive(false);
		}
	}
}
