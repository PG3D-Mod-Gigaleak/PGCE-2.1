using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holoville.HOTween;

public class MenuGUI : MonoBehaviour
{
	public static MenuGUI instance;

	public void EnterDeathmatch()
	{
		loading = true;
		prefs.SetInt("MultyPlayer", 1);
		prefs.SetInt("COOP", 0);
		GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>().Reset();
		FlurryPluginWrapper.LogDeathmatchModePress();
		Application.LoadLevel("NewMapList");
	}

	public void EnterArmory()
	{
		ArmoryNGUI.instance.gameObject.SetActive(true);
		gameObject.SetActive(false);
	}

	public void EnterTesting()
	{
		loading = true;
		Application.LoadLevel("BetaTesting");
	}

	public void EnterSettings()
	{
		loading = true;
		Application.LoadLevel("SettingScene");
	}

	public void EnterSkinmaker()
	{
		loading = true;
		prefs.SetInt(Defs.SkinEditorMode, 0);
		FlurryPluginWrapper.LogSkinsMakerModePress();
		FlurryPluginWrapper.LogSkinsMakerEnteredEvent();
		Application.LoadLevel("SkinEditor");
	}

	private bool loading;
	public UIButton optionsBtn, achievementsBtn, profileBtn;

	public void EnterOptions() {
		loading = true;
		Application.LoadLevel("NewOptions");
	}

	public void EnterAchievements() {
		loading = true;
		Application.LoadLevel("AchievementMenu");
	}

	private void OnGUI()
	{
		if (loading)
		{
			GUIHelper.DrawLoading();
		}
	}

	private UIButtonColor.State oldState, achOldState, profileOldState;
	private bool completelyIgnoreStateCHANGES, achCompletelyIgnoreStateCHANGES, profileCompletelyIgnoreStateCHANGES;
	private float nz = 0;

	private void Update() {
		if (oldState != optionsBtn.state && !completelyIgnoreStateCHANGES) {
			oldState = optionsBtn.state;
			if (oldState == UIButtonColor.State.Hover) {
				nz -= 45;
				nz = nz % 360;
				Tweener j = HOTween.To(optionsBtn.transform, .5f, new TweenParms().Prop("localRotation", new Vector3(0, 0, nz)).Ease(EaseType.EaseOutBounce));
			}
			if (oldState == UIButtonColor.State.Pressed) {
				completelyIgnoreStateCHANGES = true;
				Tweener j = HOTween.To(optionsBtn.transform, .5f, new TweenParms().Prop("localRotation", new Vector3(0, 0, -360)).Ease(EaseType.EaseInBack).OnComplete(EnterOptions));
			}
		}
		if (achOldState != achievementsBtn.state && !achCompletelyIgnoreStateCHANGES) {
			achOldState = achievementsBtn.state;
			if (achOldState == UIButtonColor.State.Hover) {
				Tweener j = HOTween.To(achievementsBtn.transform, .5f, new TweenParms().Prop("localRotation", new Vector3(0, 0, -5)).Ease(EaseType.EaseOutBounce));
			}
			if (achOldState == UIButtonColor.State.Normal) {
				Tweener j = HOTween.To(achievementsBtn.transform, .5f, new TweenParms().Prop("localRotation", new Vector3(0, 0, 0)).Ease(EaseType.EaseOutBounce));
			}
			if (achOldState == UIButtonColor.State.Pressed) {
				EnterAchievements();
			}
		}
		if (profileOldState != profileBtn.state && !profileCompletelyIgnoreStateCHANGES) {
			profileOldState = profileBtn.state;
			if (profileOldState == UIButtonColor.State.Hover) {
				Tweener j = HOTween.To(profileBtn.transform, .5f, new TweenParms().Prop("localRotation", new Vector3(0, 0, 5)).Ease(EaseType.EaseOutBounce));
			}
			if (profileOldState == UIButtonColor.State.Normal) {
				Tweener j = HOTween.To(profileBtn.transform, .5f, new TweenParms().Prop("localRotation", new Vector3(0, 0, 0)).Ease(EaseType.EaseOutBounce));
			}
			if (profileOldState == UIButtonColor.State.Pressed) {
				EnterProfile();
			}
		}
	}

	private void EnterProfile()
	{
		loading = true;
		Application.LoadLevel("ProfileShop");
	}

	private void Start() {
		instance = this;
		completelyIgnoreStateCHANGES = false;
		HOTween.Init(true, true, true);
		HOTween.EnableOverwriteManager();
		Achievements.Give("opengame");

		#if UNITY_EDITOR
			Achievements.Give("devteam");
		#endif
	}

	public void ClickTitle()
	{
		GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("themoment"));
	}
}
