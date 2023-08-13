using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundManager : MonoBehaviour
{
    public AudioClip[] clickSounds;
    public AudioClip[] openSounds;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ClickButton()
    {
        audioSource.clip = clickSounds[Random.Range(0, clickSounds.Length)];
        audioSource.Play();
    }

    public void PlayOpenSound()
    {
        audioSource.clip = openSounds[Random.Range(0, openSounds.Length)];
        audioSource.Play();
    }
}
