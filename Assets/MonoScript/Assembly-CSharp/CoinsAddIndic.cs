using System.Collections;
using UnityEngine;

public class CoinsAddIndic : MonoBehaviour
{
	private UISprite ind;

	private bool blinking;

	public AudioClip coinsAdded;

	private void Start()
	{
		ind = GetComponent<UISprite>();
	}

	private void OnEnable()
	{
		CoinsMessage.CoinsLabelDisappeared += IndicateCoinsAdd;
	}

	private void OnDisable()
	{
		CoinsMessage.CoinsLabelDisappeared -= IndicateCoinsAdd;
	}

	private void IndicateCoinsAdd()
	{
		if (!blinking)
		{
			StartCoroutine(blink());
		}
		NGUITools.PlaySound(coinsAdded);
	}

	private IEnumerator blink()
	{
		blinking = true;
		for (int i = 0; i < 7; i++)
		{
			ind.spriteName = "coin_frame_ON";
			yield return null;
			yield return new WaitForSeconds(0.1f);
			ind.spriteName = "coin_frame";
			yield return new WaitForSeconds(0.1f);
		}
		blinking = false;
	}
}
