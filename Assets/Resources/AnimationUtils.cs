using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUtils : MonoBehaviour
{
    public GameObject target;

    public void Send(string msg)
    {
        target.SendMessage(msg);
    }
}
