using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnderAI : MonoBehaviour
{
	public Animation eyes;

	public EnderAIPoint startingPoint;

	public Transform itemParent, objectStack, pointStack;

	public GameObject drink, cake;

	private Animation mAnim;

	private UnityEngine.AI.NavMeshAgent mAgent;

	public EnderAIPoint currentPoint;

	private bool eating, drinking, moving, actioning;

	private string lastPlayedAnimation, lastPlayedEyeAnimation;

	private float blinkTimer;

	public void Blink() { PlayEyeAnimation("Blink"); }

	public void EyesLookAround() { PlayEyeAnimation("LookAround"); }

	public void WideEyes() { PlayEyeAnimation("WideEyes"); }

	public void LookAround() { PlayAnimation("LookAround"); }

	private void Start()
	{
		if (Defs.isMulti)
		{
			Destroy(gameObject);
		}
		mAnim = GetComponent<Animation>();
		mAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		currentPoint = startingPoint;
		moving = true;
		ChangePoint();
	}

	private void Update()
	{
		blinkTimer -= 1 * Time.deltaTime;
		if (blinkTimer <= 0f)
		{
			Blink();
			blinkTimer = UnityEngine.Random.Range(0.3f, 2f);
		}
		if (actioning && !mAnim.isPlaying)
		{
			if (eating)
			{
				Debug.LogError("ate");
				onFinishEat();
				eating = false;
			}
			else if (drinking)
			{
				Debug.LogError("drank");
				onFinishDrink();
				drinking = false;
			}
			OnFinishAction();
		}
		if (moving && !actioning && !eating && !drinking)
		{
			if (mAgent.hasPath && mAgent.remainingDistance <= 1)
			{
				moving = false;
				OnReachCheckpoint(currentPoint);
			}
			if (!mAnim.isPlaying)
			{
				PlayAnimation("Walk");
			}
			mAgent.SetDestination(currentPoint.transform.position);
		}
	}

	public bool CallAction(EnderAIPoint.Action action) 
	{ 
		if (UnityEngine.Random.Range(0f, 1f) <= action.chance)
		{
			base.SendMessage(action.action);
			moving = false;
			return true;
		}
		return false;
	}

	public void OnFinishAction()
	{
		actioning = false;
		moving = true;
		ChangePoint();
	}

	public void ChangePoint()
	{
		int index = UnityEngine.Random.Range(0, currentPoint.connectedPoints.Length);
		EnderAIPoint randomPoint = currentPoint.connectedPoints[index].GetComponent<EnderAIPoint>();
		currentPoint = randomPoint;
	}

	public void OnReachCheckpoint(EnderAIPoint point)
	{
		if (CallAction(point.actionHere))
		{
			actioning = true;
			return;
		}
		moving = true;
		ChangePoint();
	}

	public void EatCake() { Eat(cake); }

	public void DrinkDrink() { Drink(drink); }

	public void Eat(GameObject foodObject)
	{
		PlayAnimation("Eat");
		eating = true;
		transform.LookAt(foodObject.transform.position);
		transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
		Vector3 origPos = foodObject.transform.localPosition;
		Quaternion origRot = foodObject.transform.localRotation;
		onFinishEat += ()=> { foodObject.transform.parent = objectStack; foodObject.transform.localPosition = origPos; foodObject.transform.localRotation = origRot; onFinishEat = null; };
		foodObject.transform.parent = itemParent;
		foodObject.transform.localPosition = Vector3.zero;
		foodObject.transform.localRotation = Quaternion.identity;
	}

	public void Drink(GameObject drinkObject)
	{
		PlayAnimation("Drink");
		drinking = true;
		transform.LookAt(drinkObject.transform.position);
		transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
		Vector3 origPos = drinkObject.transform.localPosition;
		Quaternion origRot = drinkObject.transform.localRotation;
		onFinishDrink += ()=> { drinkObject.transform.parent = objectStack; drinkObject.transform.localPosition = origPos; drinkObject.transform.localRotation = origRot; onFinishDrink = null; };
		drinkObject.transform.parent = itemParent;
		drinkObject.transform.localPosition = Vector3.zero;
		drinkObject.transform.localRotation = Quaternion.identity;
	}

	public void PlayEyeAnimation(string animation)
	{
		if (eyes.IsPlaying(animation))
		{
			return;
		}
		if (animation == lastPlayedEyeAnimation)
		{
			eyes.Play(animation);
			return;
		}
		lastPlayedEyeAnimation = animation;
		eyes.CrossFade(animation, 0.2f);
	}

	public void PlayAnimation(string animation)
	{
		if (animation == lastPlayedAnimation)
		{
			mAnim.Play(animation);
			return;
		}
		lastPlayedAnimation = animation;
		mAnim.CrossFade(animation, 0.2f);
	}

	private System.Action onFinishEat, onFinishDrink;
}
