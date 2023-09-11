using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class MainTarget : MonoBehaviour
{
    private GameObject RightHand;
    private KeyControllers keyControllersrRight;

    private GameObject LeftHand;
    private KeyControllers keyControllersrLeft;

    private ChangePerspectiveController controller;

    [SerializeField]
    public GameObject ballPointer;
    private WhiteNoiseAssist whiteNoiseAssist;

    [SerializeField]
    private AudioSource targetLocation;

    private SessionManager sessionManager;

    private void Start()
    {
        targetLocation = GetComponent<AudioSource>();

        RightHand = GameObject.FindGameObjectWithTag("RightHand");
        keyControllersrRight = RightHand.GetComponent<KeyControllers>();

        LeftHand = GameObject.FindGameObjectWithTag("LeftHand");
        keyControllersrLeft = LeftHand.GetComponent<KeyControllers>();

        controller = GameObject.FindObjectOfType<ChangePerspectiveController>();
        whiteNoiseAssist = ballPointer.GetComponent<WhiteNoiseAssist>();

        sessionManager = GameObject.FindObjectOfType<SessionManager>();
    }

    void FixedUpdate()
    {

        //targetLocation.pitch = whiteNoiseAssist.getAimPitch();
        if (sessionManager.getIsRightHanded())
        {
            if (keyControllersrRight.getTargetPlaying() == true & !targetLocation.isPlaying & controller.getTargetSoundUserPos())
            {
                //Debug.Log("PLAY TARGET");
                targetLocation.Play();
            }
            else if (!keyControllersrRight.getTargetPlaying())
            {
                //Debug.Log("STOP TARGET");
                targetLocation.Stop();
            }
        }
        else
        {
            if (keyControllersrLeft.getTargetPlaying() == true & !targetLocation.isPlaying & controller.getTargetSoundUserPos())
            {
                //Debug.Log("PLAY TARGET");
                targetLocation.Play();
            }
            else if (!keyControllersrLeft.getTargetPlaying())
            {
                //Debug.Log("STOP TARGET");
                targetLocation.Stop();
            }
        }
    }
}
