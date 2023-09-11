using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetPropperSound : MonoBehaviour
{
    [SerializeField]
    public GameObject propperSound;

    private GameObject LeftHand;
    private KeyControllers keyControllersrLeft;

    private GameObject RightHand;
    private KeyControllers keyControllersrRight;

    private AudioSource sound;

    private SessionManager sessionManager;

    void Start()
    {
        LeftHand = GameObject.FindGameObjectWithTag("LeftHand");
        keyControllersrLeft = LeftHand.GetComponent<KeyControllers>();

        RightHand = GameObject.FindGameObjectWithTag("RightHand");
        keyControllersrRight = RightHand.GetComponent<KeyControllers>();

        sound = propperSound.GetComponent<AudioSource>();

        sessionManager = GameObject.FindObjectOfType<SessionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sessionManager.getIsRightHanded())
        {
            if (keyControllersrLeft.getPropperSoundTarget() & !sound.isPlaying)
            {
                sound.Play();

            }
            else if (!keyControllersrLeft.getPropperSoundTarget())
            {
                sound.Stop();
            }
        }
        else
        {
            if (keyControllersrRight.getPropperSoundTarget() & !sound.isPlaying)
            {
                sound.Play();

            }
            else if (!keyControllersrRight.getPropperSoundTarget())
            {
                sound.Stop();
            }
        }
    }
}
