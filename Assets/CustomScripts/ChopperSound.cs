using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopperSound : MonoBehaviour
{
    private AudioSource audioSource = null;

    private AudioClip chopperSoundClip = null;

    // Use this for initialization
    private void Start()
    {
        this.audioSource = this.gameObject.AddComponent<AudioSource>();
        this.audioSource.playOnAwake = true;
        this.audioSource.loop = true;
        this.audioSource.spatialize = true;
        this.audioSource.spatialBlend = 1.0f;
        this.audioSource.dopplerLevel = 0.0f;
        this.audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        this.audioSource.maxDistance = 20f;
        this.chopperSoundClip = Resources.Load<AudioClip>("ChopperSound");
        this.audioSource.clip = this.chopperSoundClip;
        this.audioSource.Play();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}