using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] slashClips;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Punch()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        return slashClips[UnityEngine.Random.Range(0, slashClips.Length)];
    }
}
