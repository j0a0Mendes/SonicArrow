using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class MainTarget : MonoBehaviour
{
    private GameObject RightHand;
    private KeyControllers keyControllersrRight;

    [SerializeField]
    private AudioSource targetLocation;

    private void Start()
    {
        targetLocation = GetComponent<AudioSource>();
        RightHand = GameObject.FindGameObjectWithTag("RightHand");
        keyControllersrRight = RightHand.GetComponent<KeyControllers>();
    }

    void Update()
    {
        if (keyControllersrRight.getTargetPlaying() == true & !targetLocation.isPlaying)
        {
            targetLocation.Play();
        }
        else if(!keyControllersrRight.getTargetPlaying())
        {
            targetLocation.Stop();
        }
    }
}
