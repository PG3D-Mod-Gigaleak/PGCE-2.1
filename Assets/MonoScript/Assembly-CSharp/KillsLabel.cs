using UnityEngine;

public class KillsLabel : MonoBehaviour
{
	private UILabel _label;

	private InGameGUI _inGameGUI;

	private void Start()
	{
		base.gameObject.SetActive(prefs.GetInt("MultyPlayer", 0) == 1 && prefs.GetInt("COOP", 0) == 0);
		_label = GetComponent<UILabel>();
		_inGameGUI = GameObject.FindGameObjectWithTag("InGameGUI").GetComponent<InGameGUI>();
	}

	private void Update()
	{
		if ((bool)_inGameGUI && (bool)_label)
		{
			base.transform.localScale = new Vector3(22f, 22f, 1f);
			if (_inGameGUI != null)
			{
				_label.text = _inGameGUI.killsToMaxKills();
			}
		}
	}
}
