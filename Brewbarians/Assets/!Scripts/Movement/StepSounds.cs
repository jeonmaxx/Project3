using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Unity.VisualScripting.Member;

public enum TileKind { Earth, Grass, Stone, Path}
public class StepSounds : MonoBehaviour
{
    public AudioClip[] sounds;

    private AudioSource audioSource;
    public PlayerMovement movement;

    public TileKind kind;

    public bool inGras;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator PlayRandomSound()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        audioSource.clip = sounds[Random.Range(0, sounds.Length - 1)];
        audioSource.Play();
    }

    public void Update()
    {
        if (kind == TileKind.Grass && (movement.m_PlayerMovement.x != 0 || movement.m_PlayerMovement.y != 0))
        {
            StartCoroutine(PlayRandomSound());
            Debug.Log("Grass touched");
        }
        else
        {
            StopCoroutine(PlayRandomSound());
            audioSource.Stop();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Gras")
        {
            kind = TileKind.Grass;
        }
        if(collision.name == "Stone")
        {
            kind = TileKind.Stone;
        }
    }
}
