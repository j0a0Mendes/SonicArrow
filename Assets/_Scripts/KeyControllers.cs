using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyControllers : MonoBehaviour
{
    [SerializeField]
    public InputActionProperty targetSoundTrigger;  //target sound trigger (B)

    [SerializeField]
    public InputActionProperty replayPerspective; //change to activate replay (X)

    [SerializeField]
    public InputActionProperty replayTrigger;   //future change for the white noise (Y)

    [SerializeField]
    public InputActionProperty changePerspective;   //future change for the white noise (A)




    private MainTarget target;

    private ChangePerspectiveController controller;

    private ActionReplay actionReplay;

    private ActionReplayArrow actionReplayArrow;

    private bool xButtonEnabled;
    private bool startYCount;
    private int yCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindObjectOfType<MainTarget>();
        controller = GameObject.FindObjectOfType<ChangePerspectiveController>();
        //actionReplay = GameObject.FindObjectOfType<ActionReplay>();
        //actionReplayArrow = GameObject.FindObjectOfType<ActionReplayArrow>();

        //REPLAY
        xButtonEnabled = true;
}

    // Update is called once per frame
    void Update()
    {
        //button A in the right controller
        float buttonATriggered = changePerspective.action.ReadValue<float>();

        //button B in the right controller
        float buttonBTriggered = targetSoundTrigger.action.ReadValue<float>();

        //button X in the left controller
        float buttonXTriggered = replayPerspective.action.ReadValue<float>();

        //button Y in the left controller
        float buttonYTriggered = replayTrigger.action.ReadValue<float>();

        if (target != null)
        {
            if(buttonATriggered == 1){
                controller.changePerspective();
            }

            if (buttonBTriggered == 1)
            {
                Debug.Log("B PRESSED");
                //trigger the sound from the target
                target.turnOnTargetSound();
                controller.enableChange();
            }
            else
            {
                target.turnOffTargetSound();
            }

            if(buttonXTriggered == 1)
            {
                Debug.Log("X PRESSED");
                if (xButtonEnabled) 
                {
                    xButtonEnabled = false;
                    startYCount = true;
                    //actionReplay.triggerReplayMode();

                    actionReplayArrow = GameObject.FindObjectOfType<ActionReplayArrow>();
                    actionReplayArrow.triggerReplayMode();


                }
                
            }
            
            //if(buttonTriggered == 2){
            //    changeController.changePerspective();
            //}
        }   
    }

    private void FixedUpdate()
    {
        if (startYCount)
        {
            yCount++;
            if(yCount == 100)
            {
                startYCount = false;
                yCount = 0;
                xButtonEnabled = true;

            }
        }
    }
}
