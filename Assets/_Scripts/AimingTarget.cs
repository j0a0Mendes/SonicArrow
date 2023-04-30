using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingTarget : MonoBehaviour
{
    public AudioSource audioSource; // the audio source to play
    //public bool loopAudio = false; // flag to indicate whether audio should loop

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // get the audio source component from the game object
        //audioSource.loop = loopAudio; // set loop property based on flag
        //audioSource.Play(); // play the audio source
    }

    public void ActivateLoop()
    {
        //loopAudio = true; // set loop flag to true
        //audioSource.loop = loopAudio; // set loop property based on flag
        audioSource.Play();
    }

    public void DeactivateLoop()
    {
        audioSource.Stop();
        //loopAudio = false; // set loop flag to true
        //audioSource.loop = loopAudio; // set loop property based on flag
    }
}
