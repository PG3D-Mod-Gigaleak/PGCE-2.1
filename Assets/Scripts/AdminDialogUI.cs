using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminDialogUI : MonoBehaviour {
	public UILabel textLabel;
	public Animation anim;
	public void SetTextAndShow(string text) {
		textLabel.text = text;
		anim.Play("adminfade");
	}
}
