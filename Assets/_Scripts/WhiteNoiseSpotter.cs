using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteNoiseSpotter : MonoBehaviour
{
    public AudioSource audioSource;
    private GameObject LeftHand;
    private KeyControllers keyControllersrLeft;

    private bool audioPlaying;

    public void Start()
    {
        LeftHand = GameObject.FindGameObjectWithTag("LeftHand");
        keyControllersrLeft = LeftHand.GetComponent<KeyControllers>();
    }

    public void Update()
    {
        if(audioSource.isPlaying)
        {
            audioPlaying = true;
        }
        else
        {
            audioPlaying = false;
        }

        if (keyControllersrLeft.getPlayWN())
        {
            PlayAudio();
        }
        else
        {
            StopAudio();
        }
    }
    public void PlayAudio()
    {
        if (!audioPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopAudio()
    {
        if (audioPlaying)
        {
            audioSource.Stop();
        }
    }

    public void changePitch(float pitch)
    {
        if (audioSource != null)
        {
            audioSource.pitch = pitch;
        }
    }
}
