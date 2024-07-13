using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakyNGUIPanel : MonoBehaviour
{
    void Update()
    {
        transform.localPosition = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
        transform.localRotation = Quaternion.Euler(0, 0, Random.Range(-2, 2));
    }
}
