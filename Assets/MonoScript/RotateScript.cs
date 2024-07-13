using UnityEngine;

public class RotateScript : MonoBehaviour
{
	public Vector3 rotationSpeed = new Vector3(0f, 0f, 1f);

	private void Update()
	{
		base.transform.Rotate(rotationSpeed * Time.deltaTime);
	}
}
