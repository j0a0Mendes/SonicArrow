using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingTarget : MonoBehaviour
{
    public AudioSource audioSource; // the audio source to play
    public bool loop = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // get the audio source component from the game object
    }

    public void ActivateLoop()
    {
        if (!loop)
        {
            loop = true;
            audioSource.Play();
        }
    }

    public void DeactivateLoop()
    {
        if (loop)
        {
            loop = false;
            audioSource.Stop();
        }
    }
}
