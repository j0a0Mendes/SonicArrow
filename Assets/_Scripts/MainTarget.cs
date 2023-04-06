using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class MainTarget : MonoBehaviour
{
    private bool sounding = false;

    [SerializeField]
    private AudioSource targetLocation;

    void Update()
    {
        if (sounding & !targetLocation.isPlaying)
        {
            targetLocation.Play();       
        }
    }

    public void turnOnTargetSound()
    {
        sounding= true;
    }
    public void turnOffTargetSound()
    {
        sounding = false;
    }
}
