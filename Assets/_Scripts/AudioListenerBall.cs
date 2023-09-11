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

    private SessionManager sessionManager;

    private GameObject LeftHand;
    private KeyControllers keyControllersrLeft;

    private void Start()
    {
        ballSound = GetComponent<AudioSource>();
        RightHand = GameObject.FindGameObjectWithTag("RightHand");
        keyControllersrRight = RightHand.GetComponent<KeyControllers>();
        controller = GameObject.FindObjectOfType<ChangePerspectiveController>();
        sessionManager = GameObject.FindObjectOfType<SessionManager>();

        LeftHand = GameObject.FindGameObjectWithTag("LeftHand");
        keyControllersrLeft = LeftHand.GetComponent<KeyControllers>();
    }

    void Update()
    {
        if (sessionManager.getIsRightHanded())
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
        else
        {
            if (keyControllersrLeft.getTargetPlaying() == true & !ballSound.isPlaying & controller.getTargetSoundAimPos())
            {
                //Debug.Log("PLAY BALL");
                ballSound.Play();
            }
            else if (!keyControllersrLeft.getTargetPlaying())
            {
                //Debug.Log("STOP BALL");
                ballSound.Stop();
            }
        }
    }
}
