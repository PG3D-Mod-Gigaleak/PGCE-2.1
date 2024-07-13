using UnityEngine;

public class WeaponSounds : MonoBehaviour
{
	[Header("normal stuff")]

	public float swapTime = 1f;

	public bool isSwapIn = false;

	public bool isSwapOut = false;

	public AudioClip shoot;

	public AudioClip reload;

	public AudioClip empty;

	public bool isSerialShooting;

	public GameObject bonusPrefab;

	public int InitialAmmo = 24;

	public int ammoInClip = 12;

	public int maxAmmo = 84;

	public bool isMelee;

	public float range = 3f;

	public int damage = 50;

	public float speedModifier = 1f;

	public GameObject animationObject;

	public int Probability = 1;

	public Vector2 damageRange = new Vector2(-15f, 15f);

	public Vector3 gunPosition = new Vector3(0.35f, -0.25f, 0.6f);

	public Texture preview;

	public int inAppExtensionModifier = 10;

	public float meleeAngle = 45f;

	public float multiplayerDamage = 1f;

	public float meleeAttackTimeModifier = 0.57f;

	[Header("cursed edition stuff")]

	public bool isHeal;

	public float healAmount;

	public bool HealArmor;

	public int maxHealItems;

	public float bloomLow;

	public float bloomHigh;

	public bool isShotgun;

	public int numOfPellets;

	public bool hasAlternateShot;
	public bool hasInspect;

	public WeaponSounds alternateShot;

	public bool hasRecoil;

	public float recoil;

	public bool hasUpgrades;

	public bool isChargeUp;

	public AudioClip chargeUp;

	public AudioClip chargeDown;

	public bool SpeedBoost;

	public float speedBoostAmount;

	public float speedBoostDuration;

	public bool isDouble;

	public int price;

	public string weaponName;

	public bool oldMeleeSystem;

	public bool emptyState;

	public bool throwObject;

	public int throwObjects;
	
	public bool isGrav;
	public bool infiniteAmmo;

	[Header("imported stuff")]

	public Vector2 startZone = new Vector2(3f, 3f);

	public float tekKoof = 3f;

	public float upKoofFire = 0.4f;

	public float maxKoof = 5f;

	public float downKoofFirst = 0.2f;

	public float downKoof = 0.3f;

	public float timeBeforeDownKoof = 0.5f;

	public bool railgun;

	public bool flamethrower;

	public bool bazooka;

	public int rocketNum;

	public float bazookaExplosionRadius = 5f;

	public float bazookaExplosionRadiusSelf = 2.5f;

	public float bazookaImpulseRadius = 6f;

	public float bazookaSpeed = 2f;

	public bool grenadeLauncher;

	private float animLength;

	private float timeFromFire;

	public Vector3 armoryScale = new Vector3(1f, 1f, 1f);

	public Vector3 armoryRotation;

	public Vector3 armoryPosition;

	public CategoryType category;

	public bool isZooming;

	public int scopeNum;

	public float zoomFov;

	public Transform secondBulletSpawn;

	public Color shootLineColor = new Color(1, 1, 0, 1);
	
	public int bulletIndex;

	private void Start()
	{
		if (animationObject != null && animationObject.GetComponent<Animation>()["Shoot"] != null)
		{
			animLength = animationObject.GetComponent<Animation>()["Shoot"].length;
		}
	}

	public void EmptyState()
	{
		if (emptyState && (animationObject.GetComponent<Animation>().IsPlaying("Idle") || !animationObject.GetComponent<Animation>().isPlaying))
		{
			animationObject.GetComponent<Animation>().Play("GunEmptyState");
		}
	}

	private void Update()
	{
		if (timeFromFire > 0f)
		{
			timeFromFire -= 1f * Time.deltaTime;
			tekKoof -= downKoofFirst * Time.deltaTime;
		}
		else
		{
			if (timeFromFire < animLength)
			{
				if (tekKoof > 1f)
				{
					tekKoof -= downKoofFirst * Time.deltaTime * 12;// / animLength;
				}
				if (tekKoof < 1f)
				{
					tekKoof = 1f;
				}
			}
			else
			{
				if (tekKoof > 1f)
				{
					tekKoof -= downKoof * Time.deltaTime * 12;// / animLength;
				}
				if (tekKoof < 1f)
				{
					tekKoof = 1f;
				}
			}
		}
	}

	public void fire()
	{
		timeFromFire = timeBeforeDownKoof;
		tekKoof += upKoofFire + downKoofFirst;
		if (tekKoof > maxKoof + downKoofFirst)
		{
			tekKoof = maxKoof + downKoofFirst;
		}
	}

	public int MaxAmmoWithRespectToInApp
	{
		get
		{
			return maxAmmo;
		}
	}
}
