using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuController : MonoBehaviour {
	public UISlider volumeSlider;
	public UILabel volumeLabel;
	public AudioSource auSr;
	public AudioClip testingAudioCli;
	private int retardedWorkaround = 100;
	private void Start() {
		volumeSlider.value = prefs.GetFloat("setVolm", 1);
	}
	public void Close() {
		Application.LoadLevel(Defs.CurrentMainMenuScene);
	}
	public void VolumeValueCHANGED() {
		if (retardedWorkaround != (int)Mathf.RoundToInt(volumeSlider.value * 100)) {
			EditorOnlyDebug.Log("it's been set to " + (int)Mathf.RoundToInt(volumeSlider.value * 100));
			retardedWorkaround = (int)Mathf.RoundToInt(volumeSlider.value * 100);
		} else {
			EditorOnlyDebug.Log("what it was gonna be was " + (int)Mathf.RoundToInt(volumeSlider.value * 100));
			return;
		}
		if (volumeSlider.value <= 0.0099f) {
			Achievements.Give("silence");
		}
		prefs.SetFloat("setVolm", volumeSlider.value);
		AudioListener.volume = volumeSlider.value;
		auSr.PlayOneShot(testingAudioCli);
		volumeLabel.text = "Volume (" + (int)Mathf.RoundToInt(volumeSlider.value * 100) + "%)";
	}
}
