using UnityEngine;

public class FlashFire : MonoBehaviour
{
	public GameObject gunFlashObj;

	public GameObject gunFlashObj2;

	public bool has2Flashes
	{
		get
		{
			return GetComponent<WeaponSounds>().isDouble;
		}
	}

	public float timeFireAction = 0.1f;

	private float activeTime;

	private void Start()
	{
		if (gunFlashObj == null || gunFlashObj2 == null)
		{
			return;
		}
		gunFlashObj.SetActive(false);
		if (has2Flashes)
		{
			gunFlashObj2.SetActive(false);
		}
	}

	private void Update()
	{
		if (gunFlashObj == null || gunFlashObj2 == null)
		{
			return;
		}
		if (activeTime > 0f)
		{
			activeTime -= Time.deltaTime;
			if (activeTime <= 0f)
			{
				gunFlashObj.SetActive(false);
				if (has2Flashes)
				{
					gunFlashObj2.SetActive(false);
				}
			}
		}
	}

	public void fire()
	{
		if (gunFlashObj == null || gunFlashObj2 == null)
		{
			return;
		}
		gunFlashObj.SetActive(true);
		activeTime = timeFireAction;
	}

	public void fire(int index)
	{
		if (gunFlashObj == null || gunFlashObj2 == null)
		{
			return;
		}
		if (index == 0)
		{
			gunFlashObj.SetActive(true);
			activeTime = timeFireAction;
			return;
		}
		gunFlashObj2.SetActive(true);
		activeTime = timeFireAction;
	}
}
