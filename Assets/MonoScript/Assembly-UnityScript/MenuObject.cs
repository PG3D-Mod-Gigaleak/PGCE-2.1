using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class MenuObject : MonoBehaviour
{
	public GameObject highlight;

	public float rotateSpeed;

	public Transform menuObject;

	public virtual void OnMouseOver()
	{
		if (!MenuControl.clicked)
		{
			menuObject.Rotate(Vector3.right * Time.deltaTime * rotateSpeed);
			highlight.SetActive(true);
			float y = transform.position.y;
			Vector3 position = highlight.transform.position;
			float num = (position.y = y);
			Vector3 vector2 = (highlight.transform.position = position);
		}
	}

	public virtual void OnMouseExit()
	{
		if (!MenuControl.clicked)
		{
			highlight.SetActive(false);
			menuObject.eulerAngles = Vector3.zero;
		}
	}

	public virtual IEnumerator OnMouseDown()
	{
		yield break;
		//return new _0024OnMouseDown_0024105(this).GetEnumerator();
	}


}
