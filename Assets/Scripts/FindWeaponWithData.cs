using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindWeaponWithData : MonoBehaviour
{
    Dictionary<string, float> speedModifiers;
    void Start()
    {
        speedModifiers = new Dictionary<string, float>();
        foreach (WeaponSounds WS in Resources.LoadAll<WeaponSounds>("weapons"))
        {
            speedModifiers.Add(WS.name, WS.speedModifier);
        }
        Dictionary<string, float> ordered = speedModifiers.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        foreach (KeyValuePair<string, float> x in ordered)
        {
            Debug.Log($"{x.Key}: {x.Value}");
        }
    }
}
