using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;


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

    //PERSPECTIVE CHANGE
    private ChangePerspectiveController controller;

    [SerializeField]
    private float zAxisPull;

    //PERSPECTIVE CONTROLLER
    private bool alreadyShot;

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

    private void Awake()
    {
        interactable = midPointGrabObject.GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        interactable.selectEntered.AddListener(PrepareBowString);
        interactable.selectExited.AddListener(ResetBowString);
        controller = GameObject.FindObjectOfType<ChangePerspectiveController>();

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

    }

    private void Update()
    {

        /**if (!positioned & rightHand != null)
        {
            Transform rightHandTransform = rightHand.transform;
            transform.position = rightHandTransform.position;

            //transform.position = new Vector3(-0.046f, 0.026f, 0.436f);

            positioned = true;
            
        }**/

        if (!crossbowed && positioned == true)
        {
            crossbowed = true;

            //transform.position = new Vector3(170f, -98.53101f, 6f);
            //transform.position = new Vector3(6f, -98.53101f, 170f);
        }

        Vector3 midPointLocalSpace = new Vector3(0, 0, zAxisPull);

        //if (interactor != null)
        //if(zAxisPull <= 0)
        //{

        //convert bow string mid point position to the local space of the MidPoint
        //Vector3 midPointLocalSpace =
        //    midPointParent.InverseTransformPoint(midPointGrabObject.position); // localPosition

        //Vector3 midPointLocalSpace = new Vector3(0, 0, zAxisPull);

        /**Debug.Log("-----------------------");
        Debug.Log(midPointLocalSpace);
        Debug.Log(midPointLocalSpace.z);
        Debug.Log("-----------------------");**/

        //get the offset

        if (canShoot)
        {
            midPointLocalZAbs = Mathf.Abs(midPointLocalSpace.z);

            if (midPointLocalSpace.z < 0 && stringPulled == false && canShoot == true)
            {
                Debug.Log("PUULLIINIGGGG");
                stringPulled = true;
                OnBowPulled?.Invoke();
            }
            else if (midPointLocalSpace.z == 0 && stringPulled == true && canShoot == true)
            {
                Debug.Log("RELEASEEEED");

                stringPulled = false;
                ResetBowString();

                zAxisPull = 1;

                canShoot = false;
            }


            previousStrength = strength;

            HandleStringPushedBackToStart(midPointLocalSpace);

            HandleStringPulledBackTolimit(midPointLocalZAbs, midPointLocalSpace);

            HandlePullingString(midPointLocalZAbs, midPointLocalSpace);

            bowStringRenderer.CreateString(midPointVisualObject.position);
   
        }
    }

    private void ResetBowString(SelectExitEventArgs arg0)
    {
        OnBowReleased?.Invoke(strength);
        strength = 0;
        previousStrength = 0;
        audioSource.pitch = 1;
        audioSource.Stop();

        interactor = null;
        midPointGrabObject.localPosition = Vector3.zero;
        midPointVisualObject.localPosition = Vector3.zero;
        bowStringRenderer.CreateString(null);
    }

    private void ResetBowString()
    {
        OnBowReleased?.Invoke(strength);
        strength = 0;
        previousStrength = 0;
        audioSource.pitch = 1;
        audioSource.Stop();

        interactor = null;
        midPointGrabObject.localPosition = Vector3.zero;
        midPointVisualObject.localPosition = Vector3.zero;
        bowStringRenderer.CreateString(null);



        controller.enableChange();
        controller.changePerspective();


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
            audioSource.pitch = 1;
            audioSource.Stop();
            strength = 0;
            midPointVisualObject.localPosition = Vector3.zero;
        }
    }

    private void HandleStringPulledBackTolimit(float midPointLocalZAbs, Vector3 midPointLocalSpace)
    {
        //We specify max pulling limit for the string. We don't allow the string to go any farther than "bowStringStretchLimit"
        if (midPointLocalSpace.z < 0 && midPointLocalZAbs >= bowStringStretchLimit)
        {
            audioSource.Pause();
            strength = 1;
            //Vector3 direction = midPointParent.TransformDirection(new Vector3(0, 0, midPointLocalSpace.z));
            midPointVisualObject.localPosition = new Vector3(0, 0, -bowStringStretchLimit);
        }
    }

    private void HandlePullingString(float midPointLocalZAbs, Vector3 midPointLocalSpace)
    {
        //what happens when we are between point 0 and the string pull limit
        if (midPointLocalSpace.z < 0 && midPointLocalZAbs < bowStringStretchLimit)
        {
            if (audioSource.isPlaying == false && strength <= 0.01f)
            {
                audioSource.Play();
            }

            strength = Remap(midPointLocalZAbs, 0, bowStringStretchLimit, 0, 1);
            midPointVisualObject.localPosition = new Vector3(0, 0, midPointLocalSpace.z);

            PlayStringPullinSound();
        }
    }

    private float Remap(float value, int fromMin, float fromMax, int toMin, int toMax)
    {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }

    private void PlayStringPullinSound()
    {
        //Check if we have moved the string enought to play the sound unpause it
        if (Mathf.Abs(strength - previousStrength) > stringSoundThreshold)
        {
            if (strength < previousStrength)
            {
                //Play string sound in reverse if we are pusing the string towards the bow
                audioSource.pitch = -1;
            }
            else
            {
                //Play the sound normally
                audioSource.pitch = 1;
            }
            audioSource.UnPause();
        }
        else
        {
            //if we stop moving Pause the sounds
            audioSource.Pause();
        }

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
    }

    public void shootCrossBow()
    {
        zAxisPull = 0;
    }

    

    public void canShootAgain()
    {
        canShoot = true;
    }
}
