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

    [SerializeField]
    public GameObject ballPointer;

    //MANAGE WHEN YOU CAN PRESS KEYS
    private bool buttonAEnabled;
    private bool buttonBEnabled;
    private bool buttonXEnabled;
    private bool buttonYEnabled;

    [SerializeField]
    public GameObject centerTarget;

    private MainTarget target;

    private TargetObject targetObject;

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
    public bool vibrate;


    public float defaultAmplitude = 10f;
    public float defaultDuration = 0.1f;

    public int count = 0;

    private AudioListener listener;

    //WHITE NOISE
    public GameObject whiteNoiseSpotter;

    public bool playWhiteNoise;

    public bool playTargetSound;

    // Start is called before the first frame update
    void Start()
    {
        listener = Camera.main.GetComponent<AudioListener>(); // Get the AudioListener component

        target = GameObject.FindObjectOfType<MainTarget>();
        targetObject = GameObject.FindObjectOfType<TargetObject>();
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

        reloadCrossbow();

        //HAPTIC PURPOSES
    }   

    // Update is called once per frame
    void Update()
    {
        if (vibrate)
        {
            SendHaptics();
        }

        if (controller.getTargetSoundAimPos())
        {
            //Debug.Log("AIMPOS");
            //listener.transform.position = targetObject.transform.position;
            listener.transform.position = centerTarget.transform.position;
            //listener.transform.rotation = Quaternion.Euler(0f, 90f, 0f);

            if (controller.getInFirstPerspective())
            {
                listener.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            }
            else
            {
                listener.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            }
        }
        else if (controller.getTargetSoundUserPos())
        {
            //Debug.Log("USERPOS");
            listener.transform.position = Camera.main.transform.position;
        }

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

        //controller.getInFirstPerspective();
        
        if (target != null)
        {
            if (buttonShootCrossbow == 1 && readyToShoot == true && controller.getIsInFirstPerspective() == true)
            {
                if (!bowStringController.stringPullingSound())
                {
                    readyToShoot = false;
                    bowStringController.shootCrossBow();
                }
                //Debug.Log("CROSSBOW SHOT");
            }

            if (buttonATriggered == 1)
            {
                if (controller.getTargetSound()) 
                {
                    if (controller.getInFirstPerspective())
                    {
                        playTargetSound = true;
                    }
                    else
                    {
                        playTargetSound = false;
                    }
                    //Debug.Log("B PRESSED");
                    //Debug.Log(controller.getInFirstPerspective());
                    
                }
            }
            else
            {
                playTargetSound = false;
            }

            //if(buttonXTriggered == 1 && buttonXEnabled == true)
            if(buttonXTriggered == 1)
            {
                //Debug.Log("X PRESSED");
                if (controller.getParameterWhiteNoise())
                {
                    playWhiteNoise = true;
                }

            }
            else
            {
                playWhiteNoise = false;
            }
        }   
    }

    public bool getTargetPlaying()
    {
        return playTargetSound;
    }
    public bool getPlayWN()
    {
        return playWhiteNoise;
    }


    public void reloadCrossbow()
    {
        bowStringController.prepareCrossBow();
        bowStringController.playPullingString();
        //Debug.Log("CROSSBOW PREPARED");
        readyToShootTrue();
        SendHaptics();
    }
    public void readyToShootTrue()
    {
        readyToShoot = true;
    }

    public void enableButtonA()
    {
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
        leftController.SendHapticImpulse(defaultAmplitude, defaultDuration);
        //rightController.SendHapticImpulse(defaultAmplitude, defaultDuration);
    }

    public void SendHaptics(bool isLeftController, float amplitude, float duration)
    {
        if (isLeftController && leftController != null)
        {
            leftController.SendHapticImpulse(amplitude, duration);
            Debug.Log("Vibrating");
            //SendHaptics();
        }
        else if(rightController != null)
        {
            Debug.Log("Vibrating");
            rightController.SendHapticImpulse(amplitude, duration);
        }
    }

    public void SendHaptics(bool isLeftController)
    {
        if (isLeftController && leftController != null)
        {
            Debug.Log("Vibrating");
            leftController.SendHapticImpulse(defaultAmplitude, defaultDuration);
            //SendHaptics();
        }
        else if (rightController != null)
        {
            Debug.Log("Vibrating");
            rightController.SendHapticImpulse(defaultAmplitude, defaultDuration);
        }
    }

    public int getWallSystem()
    {
        return targetObject.getWallSystem();
    }

    public void activateVibrate()
    {
        vibrate = true;
    }
    public void deactivateVibrate()
    {
        vibrate = false;
    }




}
