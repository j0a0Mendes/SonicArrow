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

    //MANAGE WHEN YOU CAN PRESS KEYS
    private bool buttonAEnabled;
    private bool buttonBEnabled;
    private bool buttonXEnabled;
    private bool buttonYEnabled;

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

        //REPLAY BUTTON
        xButtonEnabled = true;
        buttonAEnabled = false;
        buttonBEnabled = false;
        buttonYEnabled = false;
        buttonXEnabled = false;
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
            if(buttonATriggered == 1 && buttonAEnabled == true){
                Debug.Log("A PRESSED");
                controller.enableChange();
                controller.changePerspective();

            }

            if (buttonBTriggered == 1)
            {
                Debug.Log("B PRESSED");
                Debug.Log(buttonAEnabled);
                //trigger the sound from the target
                target.turnOnTargetSound();
                //controller.enableChange();
            }
            else
            {
                target.turnOffTargetSound();
            }

            if(buttonXTriggered == 1 && buttonXEnabled == true)
            {
                disableButtonX();
                Debug.Log("X PRESSED");
                buttonXEnabled = false;
                //startYCount = true;
                //actionReplay.triggerReplayMode();

                actionReplayArrow = GameObject.FindObjectOfType<ActionReplayArrow>();
                actionReplayArrow.triggerReplayMode();


                
                
            }
            
            //if(buttonTriggered == 2){
            //    changeController.changePerspective();
            //}
        }   
    }

    private void FixedUpdate()
    {
        /**if (startYCount)
        {
            /yCount++;
            if(yCount == 100)
            {
                startYCount = false;
                yCount = 0;
                enableButtonX(); 
        
            }
        }**/
    }

    public void enableButtonA()
    {
        buttonAEnabled= true;
        controller.enableChange();
    }

    public void disableButtonA()
    {
        buttonAEnabled = false;
    }

    public void enableButtonB()
    {
        buttonBEnabled = true;
    }

    public void disableButtonB()
    {
        buttonBEnabled = false;
    }

    public void enableButtonX()
    {
        buttonXEnabled = true;
    }

    public void disableButtonX()
    {
        buttonXEnabled = false;
    }

    public void enableButtonY()
    {
        buttonYEnabled = true;
    }

    public void disableButtonY()
    {
        buttonYEnabled = false;
    }
}
