using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
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

    private AudioListener centerUserAudioListener;

    [SerializeField]
    public GameObject centerUserPos;

    [SerializeField]
    public GameObject cameraUser;

    private AudioListener listenerUser;

    [SerializeField]
    public int shotsWithoutHit = 0;

    public bool changeTarget;

    public bool isTalking;

    private bool endFlag;

    private SessionManager sessionManager;

    private AudioSource endPhaseSound;

    void Start()
    {
        firstPerspective = true;
        changeEnabled = false;
        changePerspectiveTrigger = false;
        bowStringController = GameObject.FindObjectOfType<BowStringController>();
        bowStringController.canShootAgain();

        rightHand = GameObject.FindGameObjectWithTag("RightHand");
        keyControllersrRight = rightHand.GetComponent<KeyControllers>();

        LeftHand = GameObject.FindGameObjectWithTag("LeftHand");
        keyControllersrLeft = LeftHand.GetComponent<KeyControllers>();

        parameterManager = GameObject.FindObjectOfType<ParameterManager>();

        //AudioListeners
        centerUserAudioListener = centerUserPos.GetComponent<AudioListener>();
        listenerUser = cameraUser.GetComponent<AudioListener>(); // Get the AudioListener component
        sessionManager = GameObject.FindObjectOfType<SessionManager>();

        endPhaseSound = GameObject.Find("EndPhase").GetComponent<AudioSource>();
    }

    public bool getIsTalking()
    {
        return isTalking;
    }

    public void setIsTalking(bool val)
    {
        isTalking = val;
    }

    public void addShotWithoutHit()
    {
        shotsWithoutHit += 1;
        if (shotsWithoutHit >= 5)
        {
            changeTarget = true;
        }
    }

    public bool getChangeTarget()
    {
        if (changeTarget)
        {
            shotsWithoutHit = 0;
            changeTarget = false;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void resetShotWithoutHit()
    {
        shotsWithoutHit = 0;
    }
    private void Awake()
    {
        interactable = midPointGrabObject.GetComponent<XRGrabInteractable>();
    }


    private int conditionChangeCount = 0;

    
    void FixedUpdate() {
        conditionChangeCount++;

        /**if(keyControllersrLeft.getLeftConditionTrigger() == true && keyControllersrRight.getRightConditionTrigger() == true)
        {
            //Debug.Log(conditionChangeCount);
            if(conditionChangeCount >= 25)
            {
                conditionChangeCount = 0;
                parameterManager.selectNextCondition();


                //Debug.Log("Change condition");
            }    
        }*/

        bool endPhaseFlag = sessionManager.getEndPhaseFlag();

        if(endPhaseFlag)
        {
            endFlag = true;
        }

        if(endFlag)
        {
            if(isTalking != true)
            {
                endFlag = false;
                endPhaseSound.Play();
            }
        }

        updateParameters();

        if (parameterManager.getChangeOfPerspective())
            {
                if (parameterManager.getChangeOfPerspectiveInstant())
            {
                modeSelected= 0;
            }
            else if(parameterManager.getChangeOfPerspectiveOnReplay()) {
                modeSelected = 1;
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
            firstPerspective = false;
        }
        else if(changePerspectiveTrigger && !firstPerspective)
        {
            
            changePerspectiveTrigger = false;
            
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

    public void ResetMissedShots()
    {
        shotsWithoutHit = 0;
    }

    public void SwitchAudioListeners(bool enableListenerUser)
    {
        listenerUser.enabled = enableListenerUser;
        centerUserAudioListener.enabled = !enableListenerUser;
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

    public void resetTheSystem()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool getFirstCondition(){
        return parameterManager.getFirstCondition();
    }

    public bool getSecondCondition(){
        return parameterManager.getSecondCondition();
    }

    public bool getThirdCondition(){
        return parameterManager.getThirdCondition();
    }
}




