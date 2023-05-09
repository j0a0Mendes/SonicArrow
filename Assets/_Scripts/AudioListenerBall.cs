using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerBall : MonoBehaviour
{
    private GameObject RightHand;
    private KeyControllers keyControllersrRight;
    private ChangePerspectiveController controller;

    [SerializeField]
    private AudioSource ballSound;

    private void Start()
    {
        ballSound = GetComponent<AudioSource>();
        RightHand = GameObject.FindGameObjectWithTag("RightHand");
        keyControllersrRight = RightHand.GetComponent<KeyControllers>();
        controller = GameObject.FindObjectOfType<ChangePerspectiveController>();
    }

    void Update()
    {
        if (keyControllersrRight.getTargetPlaying() == true & !ballSound.isPlaying & controller.getTargetSoundAimPos())
        {
            //Debug.Log("PLAY BALL");
            ballSound.Play();
        }
        else if (!keyControllersrRight.getTargetPlaying())
        {
            //Debug.Log("STOP BALL");
            ballSound.Stop();
        }
    }
}
