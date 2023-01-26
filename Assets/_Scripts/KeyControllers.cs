using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyControllers : MonoBehaviour
{
    [SerializeField]
    public InputActionProperty targetSoundTrigger;

    private MainTarget target; 

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindObjectOfType<MainTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        //button A in the right controller
        float buttonTriggered = targetSoundTrigger.action.ReadValue<float>();

        if(target != null)
        {
            if (buttonTriggered == 1)
            {
                //trigger the sound from the target
                target.turnOnTargetSound();
            }
            else
            {
                target.turnOffTargetSound();
            }
        }   
    }
}
