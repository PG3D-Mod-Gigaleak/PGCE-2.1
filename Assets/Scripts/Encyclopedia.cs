using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Encyclopedia : MonoBehaviour
{
	public static List<GameObject> storedEntities = new List<GameObject>();

	private List<string> unlockedEnemies = new List<string>();

	public UIGrid enemyGridList;

	private GameObject enemyButtonTemplate;

	private GameObject lockedEnemyButtonTemplate;

	public Texture lockedTexure;

	public UILabel enemyNameLabel;

	public UILabel descriptionLabel;

	public Transform enemyInstantiationPoint;

	public Transform scrollThing;

	private List<ParticleSystem> fixedSystems = new List<ParticleSystem>();

	public static List<SurvivalConfig.Enemy> parsedEnemies = new List<SurvivalConfig.Enemy>();

	void Start()
	{
		enemyButtonTemplate = Resources.Load<GameObject>("EncyclopediaEnemyButton");
		lockedEnemyButtonTemplate = Resources.Load<GameObject>("EncyclopediaLockedEnemyButton");
		foreach (SurvivalConfig.Enemy enemy in parsedEnemies)
		{
			if (PlayerPrefs.GetInt(enemy.name + "_EncyclopediaUnlock") == 1)
			{
				CreateButton(enemy, false);
				continue;
			}
			CreateButton(enemy, true);
		}
		enemyGridList.Reposition();
		FixParticles();
	}

	void Update()
	{
		//placeholder until the scrollbar decides to function at all
		scrollThing.transform.localPosition = new Vector3(scrollThing.transform.localPosition.x, scrollThing.transform.localPosition.y + mouseScrollThing, scrollThing.transform.localPosition.z);
	}

	public int mouseScrollThing
	{
		get
		{
			return (Input.GetAxis("Mouse ScrollWheel") == 0 ? 0 : Input.GetAxis("Mouse ScrollWheel") < 0 ? 25 : -25);
		}
	}

	public static bool GetEnemyRuledOut(string name)
	{
		if (name.StartsWith("Enemy"))
		{
			return Resources.Load<GameObject>("enemies/" + name).transform.GetChild(0).GetComponent<EncyclopediaStats>().ruleOut;
		}
		return Resources.Load<GameObject>("bosses/" + name).transform.GetChild(0).GetComponent<EncyclopediaStats>().ruleOut;
	}

	public void FixParticles()
	{
		foreach (ParticleSystem particle in Resources.FindObjectsOfTypeAll<ParticleSystem>())
		{
			if (!particle.gameObject.activeInHierarchy || fixedSystems.Contains(particle))
			{
				continue;
			}
			particle.startSpeed /= 6f;
			particle.startSize /= 10f;
			ParticleSystem.ShapeModule shape = particle.shape;
			shape.radius /= 10f;
			fixedSystems.Add(particle);
		}
	}

	public static void SetTextureRecursivelyFrom(GameObject obj, Texture txt)
	{
		foreach (Transform item in obj.transform)
		{
			if ((bool)item.gameObject.GetComponent<Renderer>() && (bool)item.gameObject.GetComponent<Renderer>().material)
			{
				if (item.gameObject.GetComponent<Renderer>().materials.Length > 1 && item.gameObject.name.Equals("raven_head"))
				{
					Material[] materials = item.gameObject.GetComponent<Renderer>().materials;
					foreach (Material material in materials)
					{
						if (material.name.Equals("raven_eye (Instance)"))
						{
							if (GlobalGameController.currentLevel == 6)
							{
								material.color = new Color(0.32156864f, 0f, 44f / 85f);
							}
						}
						else
						{
							material.mainTexture = txt;
						}
					}
				}
				else
				{
					item.gameObject.GetComponent<Renderer>().material.mainTexture = txt;
				}
			}
			SetTextureRecursivelyFrom(item.gameObject, txt);
		}
	}

	public static void SetTextureRecursivelyFromDontChange(GameObject obj, Texture txt)
	{
		foreach (Transform item in obj.transform)
		{
			if ((bool)item.gameObject.GetComponent<Renderer>() && (bool)item.gameObject.GetComponent<Renderer>().material)
			{
				if (item.gameObject.GetComponent<Renderer>().materials.Length > 1 && item.gameObject.name.Equals("raven_head"))
				{
					Material[] materials = item.gameObject.GetComponent<Renderer>().materials;
					foreach (Material material in materials)
					{
						if (material.name.Equals("raven_eye (Instance)"))
						{
							if (GlobalGameController.currentLevel == 6)
							{
								material.color = new Color(0.32156864f, 0f, 44f / 85f);
							}
						}
						else
						{
							if (item.tag != "donotchange" && item.tag != "ObjectLabel")
							{
								material.mainTexture = txt;
							}
						}
					}
				}
				else
				{
					if (item.tag != "donotchange" && item.tag != "ObjectLabel")
					{
						item.gameObject.GetComponent<Renderer>().material.mainTexture = txt;
					}
				}
			}
			SetTextureRecursivelyFromDontChange(item.gameObject, txt);
		}
	}

	public void Exit()
	{
		LoadConnectScene.loading = null;
		LoadConnectScene.sceneToLoad = Defs.CurrentMainMenuScene;
		Application.LoadLevel("PromScene");
	}

	public GameObject CreateButton(SurvivalConfig.Enemy enemy, bool locked)
	{
		bool boss = enemy.name.StartsWith("Boss");
		if (enemy.prefab == null)
		{
			return null;
		}
		GameObject newButton = (locked ? Instantiate(lockedEnemyButtonTemplate, enemyGridList.transform)  : Instantiate(enemyButtonTemplate, enemyGridList.transform));
		GameObject newEnemy = Instantiate(enemy.prefab.transform.GetChild(0).gameObject, newButton.transform.Find("EnemyInstantiation"));
		newEnemy.transform.localScale = enemy.prefab.transform.localScale;
		newEnemy.layer = 10;
		foreach (Transform trf in newEnemy.GetComponentsInChildren<Transform>())
		{
			trf.gameObject.layer = 10;
		}
		newEnemy.GetComponent<Animation>().Play(Defs.CAnim(newEnemy.gameObject, "Norm_Walk"));
		if (locked)
		{
			SetTextureRecursivelyFrom(newEnemy.gameObject, lockedTexure);
		}
		if (!locked)
		{
			newButton.transform.Find("NameLabel").GetComponent<UILabel>().text = enemy.name.Replace("_", " ").Replace("Default", "");
			newButton.GetComponent<ButtonHandler>().Clicked += (object sender2, EventArgs args2) =>
			{
				SelectEnemy(enemy);
			};
			SetTextureRecursivelyFromDontChange(newEnemy, enemy.skin);
		}
		return newButton;
	}

	public AudioClip btnSound;

	public void SelectEnemy(SurvivalConfig.Enemy enemy)
	{
		bool boss = enemy.name.StartsWith("Boss");
		GetComponent<AudioSource>().PlayOneShot(btnSound);
		try
		{
			Destroy(enemyInstantiationPoint.GetChild(0).gameObject);
		}
		catch
		{
			Debug.Log("nothing there LOL!");
		}
		if (enemy.prefab == null)
		{
			return;
		}
		GameObject newEnemy = Instantiate(enemy.prefab.transform.GetChild(0).gameObject, enemyInstantiationPoint);
		newEnemy.transform.localScale = enemy.prefab.transform.localScale;
		newEnemy.gameObject.layer = 10;
		foreach (Transform trf in newEnemy.gameObject.GetComponentsInChildren<Transform>())
		{
			trf.gameObject.layer = 10;
		}
		newEnemy.GetComponent<Animation>().Play(Defs.CAnim(newEnemy.gameObject, "Norm_Walk"));
		enemyNameLabel.text = enemy.name.Replace("_", " ").Replace("Default", "");
		try
		{
			descriptionLabel.text = (EncyclopediaInfo.GetEntry(enemy.name).description == "" ? "hey you. the person who added this enemy. please set a description you IDIOT...." : EncyclopediaInfo.GetEntry(enemy.name).description);
		}
		catch
		{
			descriptionLabel.text = "there's a thing called encyclopedia entries. use it buddy.";
		}
		SetTextureRecursivelyFromDontChange(newEnemy, enemy.skin);
		FixParticles();
	}
}
