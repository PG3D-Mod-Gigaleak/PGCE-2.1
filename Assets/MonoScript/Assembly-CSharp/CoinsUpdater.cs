using UnityEngine;

public class CoinsUpdater : MonoBehaviour
{
	private UILabel coinsLabel;

	private void Start()
	{
		coinsLabel = GetComponent<UILabel>();
	}

	private void Update()
	{
		coinsLabel.text = string.Empty + Storager.getInt(Defs.Coins, false);
	}
}
