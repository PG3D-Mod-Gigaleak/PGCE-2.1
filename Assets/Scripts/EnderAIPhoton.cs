using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnderAIPhoton : Photon.MonoBehaviour
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

	[PunRPC]
	public void EnderBlink() { PlayEyeAnimation("Blink"); }

	public void EyesLookAround() { PlayEyeAnimation("LookAround"); }

	public void WideEyes() { PlayEyeAnimation("WideEyes"); }

	public void LookAround() { PlayAnimation("LookAround"); }

	public void CallRPC(string method, params object[] parameters)
	{
		if (!PhotonNetwork.isMasterClient)
		{
			return;
		}
		photonView.RPC(method, PhotonTargets.All, parameters);
	}

	public void CallRPC(string method)
	{
		if (!PhotonNetwork.isMasterClient)
		{
			return;
		}
		photonView.RPC(method, PhotonTargets.All);
	}


	private void Start()
	{
		if (!Defs.isMulti)
		{
			Destroy(this);
		}
		cake = EnderSpawner.instance.cake;
		drink = EnderSpawner.instance.drink;
		objectStack = EnderSpawner.instance.objectStack;
		pointStack = EnderSpawner.instance.pointStack;
		startingPoint = pointStack.Find("PLAYROOM_CENTER").GetComponent<EnderAIPoint>();
		mAnim = GetComponent<Animation>();
		mAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		currentPoint = startingPoint;
		moving = true;
		CallRPC("EnderOnReachCheckpoint", currentPoint.actionHere.action, false, UnityEngine.Random.Range(0, currentPoint.connectedPoints.Length));
	}

	private void Update()
	{
		if (PhotonNetwork.isMasterClient)
		{
			blinkTimer -= 1 * Time.deltaTime;
			if (blinkTimer <= 0f)
			{
				CallRPC("EnderBlink");
				blinkTimer = UnityEngine.Random.Range(0.3f, 2f);
			}
		}
		if (actioning && !mAnim.isPlaying)
		{
			if (eating)
			{
				onFinishEat();
				eating = false;
			}
			else if (drinking)
			{
				onFinishDrink();
				drinking = false;
			}
			CallRPC("EnderOnFinishAction", UnityEngine.Random.Range(0, currentPoint.connectedPoints.Length));
		}
		if (moving && !actioning && !eating && !drinking)
		{
			if (mAgent.hasPath && mAgent.remainingDistance <= 1)
			{
				moving = false;
				CallRPC("EnderOnReachCheckpoint", currentPoint.actionHere.action, UnityEngine.Random.Range(0f, 1f) <= currentPoint.actionHere.chance, UnityEngine.Random.Range(0, currentPoint.connectedPoints.Length));
			}
			if (!mAnim.isPlaying)
			{
				PlayAnimation("Walk");
			}
			mAgent.SetDestination(currentPoint.transform.position);
		}
	}

	public void CallAction(string action) 
	{ 
		base.SendMessage(action);
		moving = false;
	}

	[PunRPC]
	public void EnderOnFinishAction(int randIndex)
	{
		Debug.LogError("hi");
		actioning = false;
		moving = true;
		ChangePoint(randIndex);
	}

	public void ChangePoint(int randIndex)
	{
		int index = randIndex;
		EnderAIPoint randomPoint = currentPoint.connectedPoints[index].GetComponent<EnderAIPoint>();
		currentPoint = randomPoint;
	}

	[PunRPC]
	public void EnderOnReachCheckpoint(string action, bool actioned, int randIndex)
	{
		if (actioned)
		{
			CallAction(action);
			actioning = true;
			return;
		}
		moving = true;
		ChangePoint(randIndex);
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
