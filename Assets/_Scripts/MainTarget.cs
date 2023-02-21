using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTarget : MonoBehaviour
{
    private bool sounding = false;

    [SerializeField]
    private AudioSource hitSound;

    [SerializeField]
    private AudioSource targetLocation;

    
    // Update is called once per frame
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            hitSound.Play();    
        }
    }
}
