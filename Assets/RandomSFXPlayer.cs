using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomSFXPlayer : MonoBehaviour
{
    public ClipSettings[] clips;

    [System.Serializable]
    public class ClipSettings
    {
        public AudioClip soundClip;

        public float volume = 1f;
    }

    public Vector2 cooldownRandom;

    public Vector2 startingCooldownRandom;

    private float cooldown;

    private AudioSource source;

    void Start()
    {
        cooldown = UnityEngine.Random.Range(startingCooldownRandom.x, startingCooldownRandom.y);
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (cooldown > 0f)
        {
            cooldown -= 1f * Time.deltaTime;
            return;
        }
        ClipSettings randomClip = BetterRandom.RandomElement(clips);
        source.PlayOneShot(randomClip.soundClip, (randomClip.volume == 0f ? 1f : randomClip.volume));
        cooldown = BetterRandom.Range(cooldownRandom.x, cooldownRandom.y);
    }
}
