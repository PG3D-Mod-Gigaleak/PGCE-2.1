using UnityEngine;

public class EnemiesLabel : MonoBehaviour
{
	private UILabel _label;

	private ZombieCreator _zombieCreator;

	private void Start()
	{
		bool flag = prefs.GetInt("MultyPlayer", 0) == 0;
		base.gameObject.SetActive(flag);
		if (flag)
		{
			if (prefs.GetInt("MultyPlayer", 0) == 0)
			{
				UIRoot uIRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
				float num = (float)uIRoot.manualHeight * ((float)Screen.width / (float)Screen.height);
				InGameGUI component = GameObject.FindGameObjectWithTag("InGameGUI").GetComponent<InGameGUI>();
				GameObject gameObject = component.healthBar.gameObject;
				float num2 = gameObject.transform.localPosition.x + gameObject.transform.localScale.x / 2f;
				float num3 = num - 131f - 128f;
				float num4 = (num3 - num2) / 3f;
				base.transform.localPosition = new Vector3(0f - (num - (num2 + num4 * 2f)), base.transform.localPosition.y, base.transform.localPosition.z);
			}
			_label = GetComponent<UILabel>();
			_zombieCreator = GameObject.FindGameObjectWithTag("GameController").GetComponent<ZombieCreator>();
		}
	}

	private void Update()
	{
		base.transform.localScale = new Vector3(22f, 22f, 1f);
		_label.text = "Enemies\n" + (GlobalGameController.EnemiesToKill - _zombieCreator.NumOfDeadZombies);
	}
}
