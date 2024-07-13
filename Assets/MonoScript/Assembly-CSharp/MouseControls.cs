using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControls : MonoBehaviour {

    //ublic float mouseSensitivity = 100f;

    //ublic Transform playerBody;

    //loat xRotation = 0f;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
	}
	//
	//// Update is called once per frame
	//void Update () {
   //    bool islocked = Cursor.lockState == CursorLockMode.Locked;
   //    if (!Application.isMobilePlatform)
   //    {
   //    if (islocked)   {

   //    Cursor.visible = false;
   //    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
   //    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

   //    xRotation -= mouseY;
   //    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

   //    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
   //    playerBody.Rotate(Vector3.up * mouseX);
   //    }
   //    }
   //}
}

