using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyControllers : MonoBehaviour
{
    [SerializeField]
    public InputActionProperty targetSoundTrigger;

    [SerializeField]
    public InputActionProperty replayPerspective;

    [SerializeField]
    public InputActionProperty replayTrigger;

    private MainTarget target;

    private ChangePerspectiveController controller;
    

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindObjectOfType<MainTarget>();
        controller = GameObject.FindObjectOfType<ChangePerspectiveController>();
    }

    // Update is called once per frame
    void Update()
    {
        //button A in the right controller
        float buttonATriggered = targetSoundTrigger.action.ReadValue<float>();

        //button X in the left controller
        float buttonXTriggered = replayPerspective.action.ReadValue<float>();

        //button Y in the left controller
        float buttonYTriggered = replayTrigger.action.ReadValue<float>();

        if (target != null)
        {
            if(buttonXTriggered == 1){
                controller.changePerspective();
            }

            if (buttonATriggered == 1)
            {
                //trigger the sound from the target
                target.turnOnTargetSound();
                controller.enableChange();
            }
            else
            {
                target.turnOffTargetSound();
            }

            //if(buttonTriggered == 2){
            //    changeController.changePerspective();
            //}
        }   
    }
}
