using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetPropperSound : MonoBehaviour
{
    [SerializeField]
    public GameObject propperSound;

    private GameObject LeftHand;
    private KeyControllers keyControllersrLeft;

    private AudioSource sound;


    void Start()
    {
        LeftHand = GameObject.FindGameObjectWithTag("LeftHand");
        keyControllersrLeft = LeftHand.GetComponent<KeyControllers>();

        sound = propperSound.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(keyControllersrLeft.getPropperSoundTarget() & !sound.isPlaying){
            sound.Play();
            
        }else if(!keyControllersrLeft.getPropperSoundTarget()){
            sound.Stop();
        }
    }
}
