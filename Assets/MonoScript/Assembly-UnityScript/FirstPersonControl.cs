using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(CharacterController))]
public class FirstPersonControl : MonoBehaviour
{
	public GameObject peekPivot;
	public Joystick moveTouchPad;

	public Joystick rotateTouchPad;

	public Transform cameraPivot;

	public float forwardSpeed;

	public float backwardSpeed;

	public float sidestepSpeed;

	public float jumpSpeed;

	public float inAirMultiplier;

	public Vector2 rotationSpeed;

	public float tiltPositiveYAxis;

	public float tiltNegativeYAxis;

	public float tiltXAxisMinimum;

	public string myIp;

	public GameObject playerGameObject;

	public int typeAnim;

	private Transform thisTransform;

	public GameObject camPlayer;

	private CharacterController character;

	private Vector3 cameraVelocity;

	private Vector3 velocity;

	private bool canJump;

	public bool isMine;

	private Rect fireZone;

	private Rect jumpZone;

	private bool _jumping;

	private bool jump;

	private GameObject _playerGun;

	private float startForwardSpeed;

	private float startBackwardSpeed;

	private float startSidestepSpeed;

	private float timeUpdateAnim;
	
	public Joystick jumpButton;

	public float camSwaySpeed;

	public CamSway camSway;

	// BHOP SHIT
	public float bhopSpeedMult = 1.0f;

	public float initialBhopIncrement = 0.25f;

	public float bhopIncrement = 0.25f;

	public float bhopTimeout = 0.05f;

	public float bhopAllowTimer = 0.0f;

	public float time = 0.0f;

	public float timeAtLastPress = 0.0f;

	public float holdTimeout = 0.2f;

	public bool isHoldingJump = false;
	//

	public FirstPersonControl()
	{
		forwardSpeed = 4f;
		backwardSpeed = 1f;
		sidestepSpeed = 1f;
		jumpSpeed = 4.5f;
		inAirMultiplier = 0.25f;
		rotationSpeed = new Vector2(2f, 1f);
		tiltPositiveYAxis = 0.6f;
		tiltNegativeYAxis = 0.4f;
		tiltXAxisMinimum = 0.1f;
		canJump = true;
	}

	public virtual void setIsMine()
	{
		isMine = true;
	}

	private float lerpup(ref float x, ref float y)
	{
		return Mathf.Lerp(x, y * Globals.PlayerMove.GetSpeedMod() * bhopSpeedMult * (Globals.PlayerMove.crouching ? 0.25f : ConstantMultiplier), 0.05f);
	}

	private float lerpdown(ref float x)
	{
		return Mathf.Lerp(x, 0, 0.05f);
	}

	private const float ConstantMultiplier = 1.35f;

	public virtual void SetSpeedModifier()
	{
		try {
			bool xz = (Globals.PlayerMove.walking || !isGrounded);
			if (!(startForwardSpeed <= 0f))
			{
				forwardSpeed = (xz ? lerpup(ref forwardSpeed, ref startForwardSpeed) : lerpdown(ref forwardSpeed));
			}
			if (!(startBackwardSpeed <= 0f))
			{
				backwardSpeed = (xz ? lerpup(ref backwardSpeed, ref startBackwardSpeed) : lerpdown(ref backwardSpeed));
			}
			if (!(startSidestepSpeed <= 0f))
			{
				sidestepSpeed = (xz ? lerpup(ref sidestepSpeed, ref startSidestepSpeed) : lerpdown(ref sidestepSpeed));
			}
		} catch {

		}
	}

	public virtual void Start()
	{
		if (isMine) {
			Screen.lockCursor = true;
		}
		startForwardSpeed = forwardSpeed;
		startBackwardSpeed = backwardSpeed;
		startSidestepSpeed = sidestepSpeed;
		thisTransform = (Transform)GetComponent(typeof(Transform));
		character = (CharacterController)GetComponent(typeof(CharacterController));
		_playerGun = Globals.PlayerMove.gameObject;
		GameObject gameObject = GameObject.Find("PlayerSpawn");
		if ((bool)gameObject)
		{
			thisTransform.position = gameObject.transform.position;
		}
	}

	public virtual void OnEndGame()
	{
		moveTouchPad.Disable();
		if ((bool)rotateTouchPad)
		{
			rotateTouchPad.Disable();
		}
		enabled = false;
	}

	public virtual void Jumping()
	{
		if (jump)
		{
		}
	}

	[PunRPC]
	public virtual void setIp(string _ip)
	{
		myIp = _ip;
		//Debug.Log("firstPesonControl=" + Network.player.ipAddress + " myIp=" + _ip);
	}

	public virtual Vector2 updateKeyboardControls()
	{
		#if UNITY_EDITOR
		if (Globals.PlayerMove && Globals.PlayerMove.showingAdminInput) {
			return new Vector2(0, 0);
		}
		#endif
		if (Globals.PlayerMove && Globals.PlayerMove.showChat) {
			return new Vector2(0, 0);
		}
		int num = default(int);
		int num2 = default(int);
		if (Input.GetKey("w"))
		{
			num = 1;
		}
		if (Input.GetKey("s"))
		{
			num = -1;
		}
		if (Input.GetKey("a"))
		{
			num2 = -1;
		}
		if (Input.GetKey("d"))
		{
			num2 = 1;
		}
		return new Vector2(num2, num);
	}

	public virtual void updateMouseControls()
	{
	}

	public virtual void Jump()
	{
		if (bhopAllowTimer < bhopTimeout && !isHoldingJump)
		{
			bhopSpeedMult += bhopIncrement;
			bhopIncrement /= 1.15f;
		}
		if (isMine)
		{
			Globals.PlayerMove.CrouchToggle(false);
		}
		jump = true;
		canJump = false;
	}

	public Transform gravGroundCheck;

	public bool isGrounded {
		get {
			if (_playerGun != null && _playerGun.GetComponent<Player_move_c>().isMine) {
				if (_playerGun.GetComponent<Player_move_c>().isMine && _playerGun.GetComponent<Player_move_c>().isGravFlipped) {
					RaycastHit otherHit;
					Debug.DrawRay(transform.position, Vector3.up*Vector3.Distance(transform.position, gravGroundCheck.position), Color.green);
					if (Physics.Raycast(transform.position, Vector3.up, out otherHit, Vector3.Distance(transform.position, gravGroundCheck.position))) {
						Collider other = otherHit.collider;
						if (other.gameObject != gameObject) {
							return true;
						}
					} else {
						return false;
					}
				}
			}
			return character.isGrounded;
		}
	}


	public virtual void Update()
	{
		time += Time.deltaTime;
		bool can = true;
		if (PhotonView.Get(this).isMine)
			setIsMine();
		else
			isMine = false;
		if (isMine && forwardSpeed > 40*ConstantMultiplier)
		{
			Achievements.Give("speed");
		}
		if (!Application.isMobilePlatform && isMine)
		{
			moveTouchPad.position = updateKeyboardControls();
			if (Cursor.lockState == CursorLockMode.Locked)
			{
				rotateTouchPad.position = new Vector2(Input.GetAxis("Mouse X") * 10f, Input.GetAxis("Mouse Y") * 10f);
			}
			#if UNITY_EDITOR
			if (Globals.PlayerMove && Globals.PlayerMove.showingAdminInput) {
				can = false;
			}
			#endif
			if (Globals.PlayerMove && Globals.PlayerMove.inGameGUI.chatInput.isSelected) {
				can = false;
			}
			float ad = 0;
			float offs = 0;
			if (can) {
				if (Input.GetKey(KeyCode.Space))
				{
					jumpButton.jumpPressed = true;
				}
				if (Input.GetKeyDown(KeyCode.Space))
				{
					timeAtLastPress = time;
				}
				if (Input.GetKeyUp(KeyCode.Space))
				{
					jumpButton.jumpPressed = false;
				}
				if (!Globals.PlayerMove.isKilled && Globals.PlayerMove.CurHealth > 0f)
				{
					if (Input.GetKey(KeyCode.E))
					{
						ad -= 20f;
						offs += 0.7226772f;
					}
					if (Input.GetKey(KeyCode.Q))
					{
						ad += 20f;
						offs -= 0.7226772f;
					}
				}
			}
			/*peekPivot.transform.localRotation = Quaternion.Lerp(peekPivot.transform.localRotation, Quaternion.Euler(0, 0, ad), Time.deltaTime * camSwaySpeed);
			cameraPivot.rotation = Quaternion.Euler(cameraPivot.rotation.eulerAngles.x, cameraPivot.rotation.eulerAngles.y, peekPivot.transform.localRotation.eulerAngles.z);
			Globals.PlayerMove.gunCam.transform.rotation = Quaternion.Euler(Globals.PlayerMove.gunCam.transform.rotation.eulerAngles.x, Globals.PlayerMove.gunCam.transform.rotation.eulerAngles.y, peekPivot.transform.localRotation.eulerAngles.z);
			*/
			camSway.value = Mathf.Lerp(camSway.value, (moveTouchPad.position.x > 0 ? -3.5f : moveTouchPad.position.x < 0 ? 3.5f : 0)+ad, Time.deltaTime * camSwaySpeed);
			camSway.offset = Mathf.Lerp(camSway.offset, offs, Time.deltaTime * camSwaySpeed);
		}
		SetSpeedModifier();
		Vector3 motion = thisTransform.TransformDirection(new Vector3(moveTouchPad.position.x, 0f, moveTouchPad.position.y));
		motion.y = 0f;
		motion.Normalize();
		Vector2 vector = new Vector2(Mathf.Abs(moveTouchPad.position.x), Mathf.Abs(moveTouchPad.position.y));
		if (!(vector.y <= vector.x))
		{
			if (!(moveTouchPad.position.y <= 0f))
			{
				motion *= forwardSpeed * vector.y;
			}
			else
			{
				motion *= backwardSpeed * vector.y;
			}
		}
		else
		{
			motion *= sidestepSpeed * vector.x;
		}
		if (time-timeAtLastPress > holdTimeout && Input.GetKey(KeyCode.Space))
		{
			isHoldingJump = true;
		}
		if (!Input.GetKey(KeyCode.Space))
		{
			isHoldingJump = false;
		}
		if (isGrounded)
		{
			canJump = true;
			bhopAllowTimer += Time.deltaTime;
			if (bhopAllowTimer > bhopTimeout)
			{
				bhopSpeedMult = 1.0f;
				bhopIncrement = initialBhopIncrement;
			}
			jump = false;
			Joystick joystick = rotateTouchPad;
			if (canJump && joystick.jumpPressed)
			{
				joystick.jumpPressed = false;
				Jump();
			}
			if (jump)
			{
				velocity.y = jumpSpeed * (_playerGun.GetComponent<Player_move_c>().isGravFlipped ? -1 : 1);
			}
		}
		else
		{
			bhopAllowTimer = 0;
			velocity.y += Physics.gravity.y * Time.deltaTime;
			if (rotateTouchPad.jumpPressed)
			{
				rotateTouchPad.jumpPressed = false;
			}
		}
		motion += velocity;
		motion += Physics.gravity;
		motion *= Time.deltaTime;
		timeUpdateAnim -= Time.deltaTime;
		if (!(timeUpdateAnim >= 0f) && isGrounded)
		{
			timeUpdateAnim = 0.5f;
			if (!(new Vector2(motion.x, motion.z).magnitude <= 0f) && !_playerGun.GetComponent<Player_move_c>().isSwapping())
			{
				if (_playerGun != null)
				{
					_playerGun.SendMessage("WalkAnimation");
				}
			}
			else if (_playerGun != null && !_playerGun.GetComponent<Player_move_c>().isSwapping())
			{
				_playerGun.SendMessage("IdleAnimation");
			}
		}
		if (!character.enabled)
		{
			return;
		}
		character.Move(motion);
		if (isGrounded)
		{
			velocity = Vector3.zero;
		}
		Vector2 vector2 = Vector2.zero;
		if ((bool)rotateTouchPad)
		{
			vector2 = rotateTouchPad.position;
		}
		else
		{
			Vector3 acceleration = Input.acceleration;
			float num = Mathf.Abs(acceleration.x);
			if (!(acceleration.z >= 0f) && !(acceleration.x >= 0f))
			{
				if (!(num < tiltPositiveYAxis))
				{
					vector2.y = (num - tiltPositiveYAxis) / (1f - tiltPositiveYAxis);
				}
				else if (!(num > tiltNegativeYAxis))
				{
					vector2.y = (0f - (tiltNegativeYAxis - num)) / tiltNegativeYAxis;
				}
			}
			if (!(Mathf.Abs(acceleration.y) < tiltXAxisMinimum))
			{
				vector2.x = (0f - (acceleration.y - tiltXAxisMinimum)) / (1f - tiltXAxisMinimum);
			}
		}
		if (!(vector2.magnitude <= 1f))
		{
		}
		float @float = prefs.GetFloat("SensitivitySett", 12f);
		vector2 *= Time.deltaTime * @float;
		thisTransform.Rotate(0f, vector2.x, 0f, Space.World);
		cameraPivot.Rotate(0f - vector2.y * (Globals.PlayerMove.isGravFlipped ? -1 : 1), 0f, 0f);
	}
}
