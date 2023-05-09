using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class MainTarget : MonoBehaviour
{
    private GameObject RightHand;
    private KeyControllers keyControllersrRight;
    private ChangePerspectiveController controller;

    [SerializeField]
    private AudioSource targetLocation;

    private void Start()
    {
        targetLocation = GetComponent<AudioSource>();
        RightHand = GameObject.FindGameObjectWithTag("RightHand");
        keyControllersrRight = RightHand.GetComponent<KeyControllers>();
        controller = GameObject.FindObjectOfType<ChangePerspectiveController>();
    }

    void Update()
    {
        if (keyControllersrRight.getTargetPlaying() == true & !targetLocation.isPlaying & controller.getTargetSoundUserPos())
        {
            //Debug.Log("PLAY TARGET");
            targetLocation.Play();
        }
        else if(!keyControllersrRight.getTargetPlaying())
        {
            //Debug.Log("STOP TARGET");
            targetLocation.Stop();
        }
    }
}
