using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource _audi;

    public AudioClip[] bgMusic;
    public AudioClip[] soundFX;

    private void Start()
    {
        _audi = GetComponent<AudioSource>();
    }

    public void PlaySound(int soundClip)
    {
        _audi.PlayOneShot(soundFX[soundClip]);
    }
}
