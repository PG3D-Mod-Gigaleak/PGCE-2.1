using UnityEngine;

public class ImpactReceiver : MonoBehaviour
{
	private float mass = 1f;

	private Vector3 impact = Vector3.zero;

	private CharacterController character;
	private FirstPersonControl fpcs;

	private void Start()
	{
		character = GetComponent<CharacterController>();
		fpcs = GetComponent<FirstPersonControl>();
	}

	private void Update()
	{
		if (impact.magnitude > 0.2f)
		{
			character.Move(impact * Time.deltaTime);
		}
		impact = Vector3.Lerp(impact, Vector3.zero, 5f * Time.deltaTime);
	}

	public void AddImpact(Vector3 dir, float force)
	{
		dir.Normalize();
		if (dir.y < 0f)
		{
			dir.y = 0f - dir.y;
		}
		if (gameObject.GetComponent<FirstPersonControl>().playerGameObject.GetComponent<Player_move_c>().isMine && gameObject.GetComponent<FirstPersonControl>().playerGameObject.GetComponent<Player_move_c>().isGravFlipped) {
			dir.y *= -1;
		}
		if (!fpcs)
		{
			fpcs = GetComponent<FirstPersonControl>();
		}
		if (fpcs)
		{
			fpcs.velocity.y = fpcs.jumpSpeed * (Globals.PlayerMove.isGravFlipped ? -1 : 1);;
		}
		impact += dir.normalized * force / mass;
	}
}
