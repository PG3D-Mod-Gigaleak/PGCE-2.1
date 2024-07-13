using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UghAdminDialog : MonoBehaviour
{
    public UILabel textLabel, textLabel2;
    public AudioClip ac, ac2;
    public AudioSource og;
	public void SetTextAndShow(string text) {
		StartCoroutine(dot(text));
	}
    private IEnumerator PlayNoiseAndWait()
    {
        AudioSource x = gameObject.AddComponent<AudioSource>();
        x.pitch = Random.Range(0.8f, 1.2f);
        x.clip = ac;
        x.Play();
        yield return new WaitForSecondsRealtime(ac.length*x.pitch);
        Destroy(x);
        yield break;
    }
    private IEnumerator dot(string text)
    {
        foreach (char letter in text)
        {
            textLabel.text += letter;
            textLabel2.text += letter;
            if (letter != ' ')
                StartCoroutine(PlayNoiseAndWait());
            yield return new WaitForSeconds(Random.Range(0, 0.1f));
        }
        yield return new WaitForSeconds(1f);
        int oL = textLabel.text.Length;
        for (int i = 0; i < oL+1; i++)
        {
            textLabel.text = textLabel.text.Substring(0, oL-i);
            textLabel2.text = textLabel.text.Substring(0, oL-i);
            og.PlayOneShot(ac2);
            yield return new WaitForSeconds(Random.Range(0, 0.1f));
        }
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        yield break;
    }
}
