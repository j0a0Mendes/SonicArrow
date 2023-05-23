using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangePerspectiveController : MonoBehaviour
{

    public bool firstPerspective;

    [SerializeField]
    public Transform xrorigin;

    [SerializeField]
    public GameObject controllerStand;

    [SerializeField]
    public Transform midPointGrabObject;

    XRGrabInteractable interactable;

    private bool changePerspectiveTrigger;

    private bool changeEnabled;

    private GameObject playerCamera;

    private BowStringController bowStringController;

    private int numberOfShots;
    private int points;

    private Transform initialPosition;

    private GameObject rightHand;
    private KeyControllers keyControllersrRight;

    private GameObject LeftHand;
    private KeyControllers keyControllersrLeft;

    //TARGET REGIONS
    [SerializeField]
    GameObject targetFirstRegion;

    //MODE SELECTION
    [SerializeField]
    public int modeSelected = 0;

    private bool changePerspectiveInASec = false;
    private int countChangePerspective = 0;

    //PARAMETER MANAGEMENT 
    private ParameterManager parameterManager;

    private bool changePerspectiveParameter;
    private bool targetSoundParameter;
    private bool spotterTalkingParameter;
    private bool whiteNoiseVerticalParameter;
    private bool hapticOnTargetHoverParameter;

    void Start()
    {
        firstPerspective = true;
        changeEnabled = false;
        changePerspectiveTrigger = false;
        bowStringController = GameObject.FindObjectOfType<BowStringController>();

        rightHand = GameObject.FindGameObjectWithTag("RightHand");
        keyControllersrRight = rightHand.GetComponent<KeyControllers>();

        LeftHand = GameObject.FindGameObjectWithTag("LeftHand");
        keyControllersrLeft = LeftHand.GetComponent<KeyControllers>();

        parameterManager = GameObject.FindObjectOfType<ParameterManager>();

    }

    private void Awake()
    {
        interactable = midPointGrabObject.GetComponent<XRGrabInteractable>();
    }

    void Update() {

        updateParameters();

        if (parameterManager.getChangeOfPerspective())
            {
                if (parameterManager.getChangeOfPerspectiveInstant())
            {
                modeSelected= 0;
            }
            else if(parameterManager.getChangeOfPerspectiveOnReplay()) {
                modeSelected= 1;
            }
        }
        else
        {
            modeSelected = 1;
        }

        bowStringController.setModeSelected(modeSelected);
        
        if (changePerspectiveTrigger && firstPerspective)
        {
            changePerspectiveTrigger = false;
            int wallSystem = keyControllersrLeft.getWallSystem();

            Vector3 adjustments = new Vector3(-0.7f, -1.399138f, 0);        //just to avoid error

            if (wallSystem == 1)
            {
                adjustments = new Vector3(-1.4f, -1.399138f, 0);
            }
            else if (wallSystem == 2)
            {
                adjustments = new Vector3(0, -1.399138f, -1.4f);
            }
            else if (wallSystem == 3)
            {
                adjustments = new Vector3(0.7f, -1.399138f, 0);
                adjustments = new Vector3(1.4f, -1.399138f, 0);

            }
            else
            {
                adjustments = new Vector3(0, -1.399138f, 1.4f);
            }
            
            xrorigin.position = targetFirstRegion.transform.position + adjustments;
            
            firstPerspective = false;

        }
        else if(changePerspectiveTrigger && !firstPerspective)
        {
            
            changePerspectiveTrigger = false;
            
            xrorigin.position = new Vector3(-41.893f, 0.082f, -4.4f);
          
            bowStringController.canShootAgain();

            numberOfShots += 1;
            firstPerspective = true;
            
        }

        if (changePerspectiveInASec)
        {
            countChangePerspective++;
            if (countChangePerspective == 20)
            {
                enableChange();
                changePerspective();
                changePerspectiveInASec = false;
                countChangePerspective = 0;
            }
        }
    }
    public void changePerspective(){
        if(changeEnabled){
            changePerspectiveTrigger = true;
            changeEnabled = false;
        }
    }

    public void enableChange(){
        changeEnabled = true;
    }

    public void addPoints(int roundPoints)
    {
        points += roundPoints;
    }

    public int getPoints()
    {
        return points;
    }

    public int getNumberOfShots()
    {
        return numberOfShots;
    }

    public bool getIsInFirstPerspective()
    {
        return firstPerspective;
    }

    public int getModeSelected()
    {
        return modeSelected;
    }

    public void enableButtonA()
    {
        keyControllersrRight.enableButtonA();
    }

    public void triggerChangePerspectiveInASec()
    {
        changePerspectiveInASec = true;
    }

    public void updateParameters()
    {
        changePerspectiveParameter = parameterManager.getChangeOfPerspective();
        targetSoundParameter = parameterManager.getTargetSound();
        spotterTalkingParameter = parameterManager.getSpotterTalking();
        whiteNoiseVerticalParameter = parameterManager.getWhiteNoiseVerticalAid();
        hapticOnTargetHoverParameter = parameterManager.getHapticOnTargetHover();
    }

    public bool getParameterPerspectiveReplay()
    {
        return parameterManager.getChangeOfPerspectiveOnReplay();
    }

    public bool getParameterTargetSound()
    {
        return targetSoundParameter;
    }

    public bool getParameterSpotterTalking()
    {
        return spotterTalkingParameter;
    }

    public bool getParameterSpotterPoints()
    {
        return parameterManager.getSpotterPointsAid();
    }

    public bool getParameterSpotterQuadrants()
    {
        return parameterManager.getSpotterQuadrantAid();
    }

    public bool getParameterSpotterDirection()
    {
        return parameterManager.getSpotterDirectionAid();
    }

    public bool getParameterWhiteNoiseVerticalAid()
    {
        return whiteNoiseVerticalParameter;
    }

    public bool getHapticOnTargetHover()
    {
        return hapticOnTargetHoverParameter;
    }

    public bool getTargetChangesAtFivePoints()
    {
        return parameterManager.getTargetChangesAtFivePoints();
    }

    public bool getTargetStill()
    {
        return parameterManager.getTargetStill();
    }

    public bool getInFirstPerspective()
    {
        return firstPerspective;
    }

    public void canShootAgain()
    {
        bowStringController.canShootAgain();
    }

    public bool getTargetSound()
    {
        return parameterManager.getTargetSound();
    }

    public bool getTargetSoundUserPos()
    {
        return parameterManager.getTargetSoundUserPos();
    }

    public bool getTargetSoundAimPos()
    {
        return parameterManager.getTargetSoundCrossbowAim();
    }

    public bool getParameterWhiteNoise()
    {
        return parameterManager.getWhiteNoiseVerticalAid();
    }
}




