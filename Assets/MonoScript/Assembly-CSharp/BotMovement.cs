using System;
using System.Collections;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
	public delegate void DelayedCallback();

	private Transform target;

	private float timeToRemoveLive;

	private ZombieCreator _gameController;

	private bool Agression;

	private bool deaded;

	private Player_move_c healthDown;

	private GameObject player;

	private float CurLifeTime;

	private string idleAnim = "Idle";

	private string normWalkAnim = "Norm_Walk";

	private string zombieWalkAnim = "Zombie_Walk";

	private string offAnim = "Zombie_Off";

	private string deathAnim = "Zombie_Dead";

	private string onAnim = "Zombie_On";

	private string attackAnim = "Zombie_Attack";

	private string shootAnim;

	private GameObject _modelChild;

	private Sounds _soundClips;

	private bool falling;

	private UnityEngine.AI.NavMeshAgent _nma;

	private BoxCollider _modelChildCollider;

	public string myCAnim(string a){
        return Defs.CAnim(_modelChild, a);
    }

	private void Awake()
	{
		if (prefs.GetInt("COOP") == 1)
		{
			base.enabled = false;
		}
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			if (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				_modelChild = transform.gameObject;
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
	}

	private void Start()
	{
		_nma = GetComponent<UnityEngine.AI.NavMeshAgent>();
		_modelChildCollider = _modelChild.GetComponent<BoxCollider>();
		shootAnim = offAnim;
		healthDown = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<Player_move_c>();
		player = GameObject.FindGameObjectWithTag("Player");
		_gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ZombieCreator>();
		_gameController.NumOfLiveZombies++;
		_soundClips = _modelChild.GetComponent<Sounds>();
		timeToRemoveLive = _soundClips.timeToHit * Mathf.Pow(0.95f, GlobalGameController.AllLevelsCompleted);
		CurLifeTime = timeToRemoveLive;
		target = null;
		_modelChild.GetComponent<Animation>().Stop();
		Walk();
		_soundClips.attackingSpeed += UnityEngine.Random.Range(0f - _soundClips.attackingSpeedRandomRange[0], _soundClips.attackingSpeedRandomRange[1]);
	}

	public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
	{
		return Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * 57.29578f;
	}

	private void Update()
	{
		if (!deaded)
		{
			if (!(target != null))
			{
				return;
			}
			float num = Vector3.Distance(target.position, base.transform.position);
			Vector3 vector = new Vector3(target.position.x, base.transform.position.y, target.position.z);
			if (num >= _soundClips.attackDistance)
			{
				try {
					_nma.SetDestination(vector);
				} catch {

				}
				_nma.speed = _soundClips.attackingSpeed * Mathf.Pow(1.05f, GlobalGameController.AllLevelsCompleted);
				CurLifeTime = timeToRemoveLive;
				PlayZombieRun();
				return;
			}
			if (_nma.path != null)
			{
				try {
					_nma.ResetPath();
				}catch{}
			}
			CurLifeTime -= Time.deltaTime;
			base.transform.LookAt(vector);
			if (CurLifeTime <= 0f)
			{
				healthDown.hit(_soundClips.damagePerHit, base.transform.GetChild(0).gameObject);
				CurLifeTime = timeToRemoveLive;
				if (PlayerPrefsX.GetBool(PlayerPrefsX.SndSetting, true))
				{
					base.GetComponent<AudioSource>().PlayOneShot(_soundClips.bite);
				}
			}
			if ((bool)_modelChild.GetComponent<Animation>()[myCAnim(attackAnim)])
			{
				_modelChild.GetComponent<Animation>().CrossFade(myCAnim(attackAnim));
			}
			else if ((bool)_modelChild.GetComponent<Animation>()[myCAnim(shootAnim)])
			{
				_modelChild.GetComponent<Animation>().CrossFade(myCAnim(shootAnim));
			}
		}
		else if (falling)
		{
			float num2 = 7f;
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - num2 * Time.deltaTime, base.transform.position.z);
		}
	}

	public void PlayZombieRun()
	{
		if ((bool)_modelChild.GetComponent<Animation>()[myCAnim(zombieWalkAnim)])
		{
			_modelChild.GetComponent<Animation>().CrossFade(myCAnim(zombieWalkAnim));
		}
	}

	public void SetTarget(Transform _target, bool agression)
	{
		Agression = agression;
		if ((bool)_target && target != _target)
		{
			try {
			_nma.ResetPath();
			}catch{}
			if (PlayerPrefsX.GetBool(PlayerPrefsX.SndSetting, true))
			{
				base.GetComponent<AudioSource>().PlayOneShot(_soundClips.voice);
			}
			PlayZombieRun();
		}
		else if (!_target && target != _target)
		{
			try {
			_nma.ResetPath();
			} catch {

			}
			Walk();
		}
		target = _target;
		SpawnMonster component = GetComponent<SpawnMonster>();
		component.ShouldMove = _target == null;
	}

	private void Run()
	{
	}

	private void Stop()
	{
	}

	private void Attack()
	{
	}

	public void Death()
	{
		_nma.enabled = false;
		if (PlayerPrefsX.GetBool(PlayerPrefsX.SndSetting, true))
		{
			base.GetComponent<AudioSource>().PlayOneShot(_soundClips.death);
		}
		float num = _soundClips.death.length;
		_modelChild.GetComponent<Animation>().Stop();
		if ((bool)_modelChild.GetComponent<Animation>()[myCAnim(deathAnim)])
		{
			_modelChild.GetComponent<Animation>().Play(myCAnim(deathAnim));
			num = Mathf.Max(num, _modelChild.GetComponent<Animation>()[myCAnim(deathAnim)].length);
			CodeAfterDelay(_modelChild.GetComponent<Animation>()[myCAnim(deathAnim)].length * 1.25f, StartFall);
		}
		else
		{
			StartFall();
		}
		CodeAfterDelay(num, DestroySelf);
		_modelChild.GetComponent<BoxCollider>().enabled = false;
		deaded = true;
		SpawnMonster component = GetComponent<SpawnMonster>();
		component.ShouldMove = false;
		_gameController.NumOfDeadZombies++;
		GlobalGameController.Score += _soundClips.scorePerKill;
	}

	private void DestroySelf()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void StartFall()
	{
		falling = true;
	}

	private void Walk()
	{
		_modelChild.GetComponent<Animation>().Stop();
		if ((bool)_modelChild.GetComponent<Animation>()[myCAnim(normWalkAnim)])
		{
			_modelChild.GetComponent<Animation>().CrossFade(myCAnim(normWalkAnim));
		}
		else
		{
			_modelChild.GetComponent<Animation>().CrossFade(myCAnim(zombieWalkAnim));
		}
	}

	private void FixedUpdate()
	{
		if ((bool)base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().velocity = Vector3.zero;
			base.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}
	}

	public void CodeAfterDelay(float delay, DelayedCallback callback)
	{
		StartCoroutine(___DelayedCallback(delay, callback));
	}

	private IEnumerator ___DelayedCallback(float time, DelayedCallback callback)
	{
		yield return new WaitForSeconds(time);
		callback();
	}

	private void OnDestroy()
	{
	}
}
