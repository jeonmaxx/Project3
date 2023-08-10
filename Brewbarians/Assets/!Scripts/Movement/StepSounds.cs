using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Unity.VisualScripting.Member;

public enum TileKind { None, Dirt, Grass, Stone, Path}
public class StepSounds : MonoBehaviour
{
    public AudioClip[] dirtSounds;
    public AudioClip[] grassSounds;
    public AudioClip[] stoneSounds;
    public AudioClip[] pathSounds;

    private AudioSource audioSource;
    public PlayerMovement movement;

    public TileKind kind;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator PlayRandomSound()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);

        switch (kind)
        {
            case TileKind.Grass:
                audioSource.clip = grassSounds[Random.Range(0, grassSounds.Length)];                
                break;
            case TileKind.Dirt:
                audioSource.clip = dirtSounds[Random.Range(0, dirtSounds.Length)];
                break;
            case TileKind.Stone:
                audioSource.clip = stoneSounds[Random.Range(0, stoneSounds.Length)];
                break;
            case TileKind.Path:
                audioSource.clip = pathSounds[Random.Range(0, pathSounds.Length)];
                break;
        };
        audioSource.Play();
    }

    public void Update()
    {
        if (movement.m_PlayerMovement.x != 0 || movement.m_PlayerMovement.y != 0)
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

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Dirt")
        {
            kind = TileKind.Dirt;
        }
        
        if (collision.name == "Gras")
        {
            kind = TileKind.Grass;
        }
        
        if (collision.name == "Stone")
        {
            kind = TileKind.Stone;
        }
        
        if (collision.name == "Path")
        {
            kind = TileKind.Path;
        }
    }
}
