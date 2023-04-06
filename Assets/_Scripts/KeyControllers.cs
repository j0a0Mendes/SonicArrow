using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class KeyControllers : MonoBehaviour
{

    [SerializeField]
    public XRBaseController leftController;

    [SerializeField]
    public XRBaseController rightController;

    [SerializeField]
    public InputActionProperty targetSoundTrigger;  //target sound trigger (B)

    [SerializeField]
    public InputActionProperty replayPerspective; //change to activate replay (X)

    [SerializeField]
    public InputActionProperty replayTrigger;   //future change for the white noise (Y)

    [SerializeField]
    public InputActionProperty changePerspective;   //future change for the white noise (A)

    [SerializeField]
    public InputActionProperty loadCrossbowButton;   //load the crossbow

    [SerializeField]
    public InputActionProperty shootCrossbowButton;   //shoot the crossbow

    //MANAGE WHEN YOU CAN PRESS KEYS
    private bool buttonAEnabled;
    private bool buttonBEnabled;
    private bool buttonXEnabled;
    private bool buttonYEnabled;

    private MainTarget target;

    private ChangePerspectiveController controller;

    private ActionReplay actionReplay;

    private ActionReplayArrow actionReplayArrow;

    private BowStringController bowStringController;

    private bool readyToShoot;

    private bool xButtonEnabled;

    private int modeSelected;
    //private bool startYCount;
    //private int yCount = 0;

    private GameObject rightHand;
    private KeyControllers keyControllersrRight;

    private GameObject LeftHand;
    private KeyControllers keyControllersrLeft;

    //HAPTICS PURPOSES


    public float defaultAmplitude = 1f;
    public float defaultDuration = 1.5f;

    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindObjectOfType<MainTarget>();
        controller = GameObject.FindObjectOfType<ChangePerspectiveController>();
        bowStringController = GameObject.FindObjectOfType<BowStringController>();

        rightHand = GameObject.FindGameObjectWithTag("RightHand");
        keyControllersrRight = rightHand.GetComponent<KeyControllers>();

        LeftHand = GameObject.FindGameObjectWithTag("LeftHand");
        keyControllersrLeft = LeftHand.GetComponent<KeyControllers>();
        //actionReplay = GameObject.FindObjectOfType<ActionReplay>();
        //actionReplayArrow = GameObject.FindObjectOfType<ActionReplayArrow>();

        //REPLAY BUTTON
        xButtonEnabled = true;
        buttonAEnabled = false;
        buttonBEnabled = false;
        buttonYEnabled = false;
        buttonXEnabled = false;

        //HAPTIC PURPOSES
        
        
    }   

    // Update is called once per frame
    void Update()
    {
        //get mode selected. 0 - Instant Sound Output. 1 - Replay Mode
        modeSelected = controller.getModeSelected();

        //button A in the right controller
        float buttonATriggered = changePerspective.action.ReadValue<float>();

        //button B in the right controller
        float buttonBTriggered = targetSoundTrigger.action.ReadValue<float>();

        //button X in the left controller
        float buttonXTriggered = replayPerspective.action.ReadValue<float>();

        //button Y in the left controller
        float buttonYTriggered = replayTrigger.action.ReadValue<float>();

        //button grip from the right controller
        float buttonLoadCrossbow = loadCrossbowButton.action.ReadValue<float>();

        //button shoot from the right controller
        float buttonShootCrossbow = shootCrossbowButton.action.ReadValue<float>();

        if (target != null)
        {

            if(buttonLoadCrossbow == 1 && readyToShoot == false && controller.getIsInFirstPerspective() == true)
            {
                readyToShoot = true;
                bowStringController.prepareCrossBow();
                bowStringController.playPullingString();
                //Debug.Log("CROSSBOW PREPARED");
                SendHaptics();
            }

            if (buttonShootCrossbow == 1 && readyToShoot == true && controller.getIsInFirstPerspective() == true)
            {
                readyToShoot = false;
                bowStringController.shootCrossBow();
                //Debug.Log("CROSSBOW SHOT");
            }

            //Debug.Log("ButtonEnabled: " + buttonAEnabled.ToString());
            //Debug.Log(buttonATriggered);

            count = 0;
            if (buttonATriggered == 1){
                //Debug.Log("A PRESSSSEEEEEEEEEEEEEEEEEEED");
                //Debug.Log("ButtonEnabled: " + buttonXEnabled.ToString() + " " + count.ToString());
                if (buttonAEnabled == true)
                {
                    count += 1;
                    buttonAEnabled = false;
                    //controller.enableChange();
                    //controller.changePerspective();
                    /**if (count % 2 == 0)
                    {
                        //keyControllersrLeft.enableButtonX();
                        keyControllersrLeft.disableButtonX();
                    }
                    else
                    {
                        keyControllersrLeft.enableButtonX();
                        //keyControllersrLeft.disableButtonX();
                    }**/
                    
                }
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
                //WHY?
                //Debug.Log("BUTAO: " + buttonXEnabled.ToString());
                //disableButtonX();
                //Debug.Log("X PRESSED");
                //startYCount = true;
                //actionReplay.triggerReplayMode();

                buttonXEnabled = false;
                actionReplayArrow = GameObject.FindObjectOfType<ActionReplayArrow>();
                actionReplayArrow.triggerReplayMode();

                controller.enableChange();
                controller.changePerspective();
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
        //Debug.Log("MEGA PENISSSSS");
        buttonAEnabled = true;
        //controller.enableChange();
    }

    public void disableButtonA()
    {
        buttonAEnabled = false;
        //Debug.Log("Button A Disabled");
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
        //Debug.Log("ESQUECEEE");
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

    //HAPTIC PURPOSES
    [ContextMenu("Send Haptics")]
    public void SendHaptics()
    {
        //leftController.SendHapticImpulse(defaultAmplitude, defaultDuration);
        rightController.SendHapticImpulse(defaultAmplitude, defaultDuration);
    }

    public void SendHaptics(bool isLeftController, float amplitude, float duration)
    {
        if (isLeftController && leftController != null)
        {
            leftController.SendHapticImpulse(amplitude, duration);
        }
        else if(rightController != null)
        {
            Debug.Log("Vibrating");
            rightController.SendHapticImpulse(amplitude, duration);
        }
    }
}
