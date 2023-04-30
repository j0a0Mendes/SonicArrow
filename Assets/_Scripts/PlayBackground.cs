using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBackground : MonoBehaviour
{
    public AudioSource audioSource; // the audio source to play

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // get the audio source component from the game object
        audioSource.loop = true; // set loop property to true
        audioSource.Play(); // play the audio source
    }
}
