using System.Collections;
using UnityEngine;

public sealed class AppsMenu : MonoBehaviour
{
	public Texture fon;

	public Texture pixlgun3d;

	public Texture man;

	public Texture androidFon;

	public GUIStyle shooter;

	public GUIStyle skinsmaker;

	private string expPath = string.Empty;

	private string logtxt;

	private bool downloadStarted;

	private bool ApplicationBinarySplitted
	{
		get
		{
			return Defs.AndroidEdition == Defs.RuntimeAndroidEdition.GoogleLite || Defs.AndroidEdition == Defs.RuntimeAndroidEdition.GooglePro;
		}
	}

	private void Update()
	{
		if (Application.platform == RuntimePlatform.Android && Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void SetRandomLevels()
	{
		int[] the = new int[Defs.numOfLevels];
		int index = 0;
		foreach (SurvivalConfig.BaseLevel baseLevel in Defs.m_SurvivalConfig.levels.levels)
		{
			the[index] = UnityEngine.Random.Range(0, baseLevel.PossibleLevels.Length);
			index++;
		}
		Defs.randomScenesThisLoad = the;
	}

	private void Start()
	{
		WeaponSounds.hideArms = prefs.GetInt("hideArms") == 1;
		WeaponManager.WeaponCatNullCheck();
		SetRandomLevels();
		//StartCoroutine(loadLevel());
	}

	private void LoadLoading()
	{
		GlobalGameController.currentLevel = -1;
		Application.LoadLevel("Loading");
	}

	private void log(string t)
	{
		logtxt = logtxt + t + "\n";
		MonoBehaviour.print("MYLOG " + t);
	}

	private void OnGUI()
	{
	}

	protected IEnumerator loadLevel()
	{
		Application.LoadLevel("Loading");
		yield return new WaitForSeconds(0.001f);
	}
}
