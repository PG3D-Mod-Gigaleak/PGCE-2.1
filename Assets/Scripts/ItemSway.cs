using System;
using UnityEngine;

public class ItemSway : MonoBehaviour
{
    [SerializeField] private float smooth;

    [SerializeField] private float multiplier;

    [SerializeField] private bool invertXY;

    [SerializeField] private Vector2 mouseXYMult = new Vector2(1, 1);

    private void Update()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.AngleAxis(Input.GetAxisRaw((invertXY ? "Mouse Y" : "Mouse X")) * multiplier * mouseXYMult.x, Vector3.right) * Quaternion.AngleAxis(Input.GetAxisRaw((invertXY ? "Mouse X" : "Mouse Y")) * multiplier * mouseXYMult.y, Vector3.up), smooth * Time.deltaTime);
    }
}