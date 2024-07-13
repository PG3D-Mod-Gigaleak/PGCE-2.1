using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
	public GameObject door;

	public AudioClip open;

	public AudioClip close;

	private float timer;

	[HideInInspector] public bool doorOpened;

	private Animation _doorAnim;

	private AudioSource _source;

	private bool closeOnTimerZero;

	void Start()
	{
		_doorAnim = door.GetComponent<Animation>();
		_source = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (timer > 0f)
		{
			timer -= 1 * Time.deltaTime;
			return;
		}
		if (closeOnTimerZero)
		{
			Close(0f);
			closeOnTimerZero = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			InteractDoor(true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			InteractDoor(false);
		}
	}

	public void InteractDoor(bool open)
	{
		if (doorOpened)
		{
			if (open)
			{
				timer = 2f;
				return;
			}
			closeOnTimerZero = true;
			timer = 2f;
			return;
		}
		if (!open || _doorAnim.IsPlaying("open"))
		{
			return;
		}
		Open(_doorAnim.IsPlaying("close") ? _doorAnim["close"].length - _doorAnim["close"].time : 0f);
	}

	public void Close(float elapse)
	{
		_doorAnim.Play("close");
		_doorAnim["close"].time = elapse;
		_source.PlayOneShot(close);
		if (Defs.isMulti)
		{
			Globals.PlayerMove.PlayCloseOnDoor(_doorAnim.name, _source.name, elapse);
		}
		doorOpened = false;
	}

	public void Open(float elapse)
	{
		_doorAnim.Play("open");
		_doorAnim["open"].time = elapse;
		_source.PlayOneShot(open);
		if (Defs.isMulti)
		{
			Globals.PlayerMove.PlayOpenOnDoor(_doorAnim.name, _source.name, elapse);
		}
		doorOpened = true;
	}
}
