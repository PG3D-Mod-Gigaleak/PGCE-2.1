using UnityEngine;

public class MobileOnly : MonoBehaviour
{
	void Start()
	{
		if (!Application.isMobilePlatform)
		{
			Destroy(gameObject);
		}
	}
}
