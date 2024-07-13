using UnityEngine;

internal sealed class InAppInstancer : MonoBehaviour
{
	public GameObject inAppGameObjectPrefab;

	public GameObject amazonIapManagerPrefab;

	private bool _amazonIapManagerInitialized;

	private void Start()
	{
		if (!GameObject.FindGameObjectWithTag("InAppGameObject"))
		{
			Object.Instantiate(inAppGameObjectPrefab, Vector3.zero, Quaternion.identity);
		}
		if (amazonIapManagerPrefab == null)
		{
			Debug.LogWarning("amazonIapManager == null");
		}
		else if (!_amazonIapManagerInitialized)
		{
			Object.Instantiate(amazonIapManagerPrefab, Vector3.zero, Quaternion.identity);
			_amazonIapManagerInitialized = true;
		}
	}
}
