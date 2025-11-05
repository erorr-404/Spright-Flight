using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----- Audio -----")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] public float defaultVolume = 1f;
    
    [Header("----- Audio Clips -----")]
    [SerializeField] public AudioClip background;
    [SerializeField] public AudioClip destroy;
    [SerializeField] public AudioClip meteoriteCollision;
    [SerializeField] public AudioClip restart;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        SFXSource.PlayOneShot(clip, volume);
    }
}
