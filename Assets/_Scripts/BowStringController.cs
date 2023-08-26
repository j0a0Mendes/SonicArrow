using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.GraphicsBuffer;


public class BowStringController : MonoBehaviour
{
    [SerializeField]
    private BowString bowStringRenderer;

    public XRGrabInteractable interactable;

    [SerializeField]
    private Transform midPointGrabObject, midPointVisualObject, midPointParent;

    [SerializeField]
    private float bowStringStretchLimit = 0.3f;

    private Transform interactor;

    private float strength, previousStrength;

    [SerializeField]
    private float stringSoundThreshold = 0.001f;

    [SerializeField]
    private AudioSource audioSource;

    public UnityEvent OnBowPulled;
    public UnityEvent<float> OnBowReleased;

    private bool positioned = false;

    public KeyControllers keyControllersScript;

    //Get keycontrollers respectivelly
    private GameObject rightHandObj;
    private KeyControllers keyControllersrRight;

    private GameObject LeftHand;
    private KeyControllers keyControllersrLeft;

    //public KeyControllers keyControllersRightHand;
    //PERSPECTIVE CHANGE
    private ChangePerspectiveController controller;

    private TimeManager timeManager;

    private SpotterTalkingCheck spotterManager;

    [SerializeField]
    private float zAxisPull;

    //PERSPECTIVE CONTROLLER
    private bool alreadyShot;
    private bool okok;

    private bool theresArrow;

    private bool crossbowed;

    private bool stringPulled;

    private bool canShoot = true;

    //HAPTICS

    public XRController leftHandController;

    //RELOCATE_BOW
    [SerializeField]
    public GameObject rightHand;

    [Range(0, 1)]
    public float intensity;


    float midPointLocalZAbs;

    private int modeSelected;

    private TargetObject targetObject;

    [SerializeField]
    public GameObject centerTarget;

    private void Awake()
    {
        interactable = midPointGrabObject.GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        spotterManager = GameObject.FindObjectOfType<SpotterTalkingCheck>();
        targetObject = GameObject.FindObjectOfType<TargetObject>();
        keyControllersScript = GameObject.FindObjectOfType<KeyControllers>();

        //Get key controllers
        rightHandObj = GameObject.FindGameObjectWithTag("RightHand");
        keyControllersrRight = rightHand.GetComponent<KeyControllers>();

        LeftHand = GameObject.FindGameObjectWithTag("LeftHand");
        keyControllersrLeft = LeftHand.GetComponent<KeyControllers>();

        interactable.selectEntered.AddListener(PrepareBowString);
        interactable.selectExited.AddListener(ResetBowString);
        controller = GameObject.FindObjectOfType<ChangePerspectiveController>();

        timeManager = GameObject.FindObjectOfType<TimeManager>();

        zAxisPull = 1;

        if (!positioned & rightHand != null)
        {
            Transform rightHandTransform = rightHand.transform;
            Vector3 currentPosition = rightHandTransform.position;
            Vector3 crossbowAdjustment = new Vector3(-0.046f, 0.026f, 0.436f);
            Vector3 minorAdjust = new Vector3(0.7f, 0.1f, -0.5f);

            transform.position = currentPosition + crossbowAdjustment + minorAdjust;
            transform.rotation = Quaternion.Euler(357.147f, 3f, 6f);

            positioned = true;
        }

        prepareCrossBow();

    }

    private void Update()
    {
        modeSelected = controller.getModeSelected();

        if (!crossbowed && positioned == true)
        {
            crossbowed = true;
        }

        Vector3 midPointLocalSpace = new Vector3(0, 0, zAxisPull);

        if (canShoot)
        {
            midPointLocalZAbs = Mathf.Abs(midPointLocalSpace.z);

            if (midPointLocalSpace.z < 0 && stringPulled == false && canShoot == true)      //PUULLIINIGGGG
            {
                //Debug.Log("PUULLIINIGGGG");
                stringPulled = true;
                OnBowPulled?.Invoke();

                //timeManager.StartTimer();
            }
            else if (midPointLocalSpace.z == 0 && stringPulled == true && canShoot == true && controller.getIsTalking() == false)     //RELEASEEEED
            {
                //Debug.Log(audioSource.isPlaying);

                timeManager.StopTimer();

                stringPulled = false;
                ResetBowString();

                zAxisPull = 1;

                canShoot = false;
            }

            previousStrength = strength;

            HandleStringPushedBackToStart(midPointLocalSpace);

            HandleStringPulledBackTolimit(midPointLocalZAbs, midPointLocalSpace);

            HandlePullingString(midPointLocalZAbs, midPointLocalSpace);     //PULLING

            bowStringRenderer.CreateString(midPointVisualObject.position);

        }
    }

    private void ResetBowString(SelectExitEventArgs arg0)
    {
        OnBowReleased?.Invoke(strength);
        strength = 0;
        previousStrength = 0;

        interactor = null;
        midPointGrabObject.localPosition = Vector3.zero;
        midPointVisualObject.localPosition = Vector3.zero;
        bowStringRenderer.CreateString(null);
    }

    public bool stringPullingSound()
    {
        return audioSource.isPlaying;
    }

    private void ResetBowString()
    {
        OnBowReleased?.Invoke(strength);
        strength = 0;
        previousStrength = 0;

        interactor = null;
        midPointGrabObject.localPosition = Vector3.zero;
        midPointVisualObject.localPosition = Vector3.zero;
        bowStringRenderer.CreateString(null);



        /*if (modeSelected == 0)
        {
            controller.enableChange();
            controller.changePerspective();
        }*/


        alreadyShot = true;
    }

    private void PrepareBowString(SelectEnterEventArgs arg0)
    {
        interactor = arg0.interactorObject.transform;
        OnBowPulled?.Invoke();
    }



    private void HandleStringPushedBackToStart(Vector3 midPointLocalSpace)
    {
        if (midPointLocalSpace.z >= 0)
        {
            strength = 0;
            midPointVisualObject.localPosition = Vector3.zero;
        }
    }

    private void HandleStringPulledBackTolimit(float midPointLocalZAbs, Vector3 midPointLocalSpace)
    {
        //We specify max pulling limit for the string. We don't allow the string to go any farther than "bowStringStretchLimit"
        if (midPointLocalSpace.z < 0 && midPointLocalZAbs >= bowStringStretchLimit)
        {
            strength = 1;
            midPointVisualObject.localPosition = new Vector3(0, 0, -bowStringStretchLimit);
        }
    }

    private void HandlePullingString(float midPointLocalZAbs, Vector3 midPointLocalSpace)
    {
        //what happens when we are between point 0 and the string pull limit
        if (midPointLocalSpace.z < 0 && midPointLocalZAbs < bowStringStretchLimit)
        {
            strength = Remap(midPointLocalZAbs, 0, bowStringStretchLimit, 0, 1);
            midPointVisualObject.localPosition = new Vector3(0, 0, midPointLocalSpace.z);
        }
    }

    private float Remap(float value, int fromMin, float fromMax, int toMin, int toMax)
    {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }

    //REPLAY MECHANISM

    private List<ActionReplayRecord> actionReplayRecords = new List<ActionReplayRecord>();

    private void FixedUpdate()
    {
        actionReplayRecords.Add(new ActionReplayRecord { position = transform.position, rotation = transform.rotation });
    }

    public void prepareCrossBow()
    {
        zAxisPull = -0.3f;
        centerTarget.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
    }

    public void shootCrossBow()
    {
        centerTarget.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        zAxisPull = 0;
    }

    public void canShootAgain()
    {
        canShoot = true;
        alreadyShot = false;
    }

    public void playPullingString()
    {
        audioSource.Play();
    }

    public void setModeSelected(int mode)
    {
        modeSelected = mode;
    }

    public void changePerspective()
    {
        controller.enableChange();
        controller.changePerspective();
    }

    public bool getAlreadyShot()
    {
        return alreadyShot;
    }

    public void relocateTarget()
    {
        targetObject.relocateTarget();
    }
}
