using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // audio index 0 = playerShoot, 1 = asteroidGetHit, 2 = asteroidHitPlayer, 3 = ufoShoot;
    private AudioSource audioSource;
    public static AudioManager sharedInstance;

    public AudioClip[] audios;


    private void Awake()
    {
        sharedInstance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int index)
    {
        audioSource.PlayOneShot(audios[index]);
    }
}
