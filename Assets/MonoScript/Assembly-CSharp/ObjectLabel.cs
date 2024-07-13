//using System;
//using UnityEngine;
//
//[RequireComponent(typeof(GUIText))]
//public class ObjectLabel : MonoBehaviour
//{
//	public static Camera currentCamera;
//
//	public Transform target;
//
//	public Vector3 offset = Vector3.up;
//
//	public bool clampToScreen;
//
//	public float clampBorderSize = 0.05f;
//
//	public bool useMainCamera = true;
//
//	public Camera cameraToUse;
//
//	public Camera cam;
//
//	public Vector3 posLabel;
//
//	private Transform thisTransform;
//
//	private Transform camTransform;
//
//	private void Start()
//	{
//		thisTransform = base.transform;
//		cam = currentCamera;
//		camTransform = cam.transform;
//	}
//
//	private void Update()
//	{
//		if (target == null || cam == null)
//		{
//			Debug.Log("target=null");
//			UnityEngine.Object.Destroy(base.gameObject);
//			return;
//		}
//		try
//		{
//			cam = currentCamera;
//			camTransform = cam.transform;
//			if (clampToScreen)
//			{
//				Vector3 vector = camTransform.InverseTransformPoint(target.position);
//				vector.z = Mathf.Max(vector.z, 1f);
//				thisTransform.position = cam.WorldToViewportPoint(camTransform.TransformPoint(vector + offset));
//				thisTransform.position = new Vector3(Mathf.Clamp(thisTransform.position.x, clampBorderSize, 1f - clampBorderSize), Mathf.Clamp(thisTransform.position.y, clampBorderSize, 1f - clampBorderSize), thisTransform.position.z);
//			}
//			else
//			{
//				posLabel = cam.WorldToViewportPoint(target.position + offset);
//				if (posLabel.z >= 0f)
//				{
//					thisTransform.position = posLabel;
//				}
//				else
//				{
//					thisTransform.position = new Vector3(-1000f, -1000f, -1000f);
//				}
//			}
//		}
//		catch (Exception ex)
//		{
//			Debug.Log("Exception in ObjectLabel: " + ex);
//		}
//	}
//}
