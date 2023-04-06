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

    }

    private void Awake()
    {
        interactable = midPointGrabObject.GetComponent<XRGrabInteractable>();
    }

    void Update(){

        if (changePerspectiveTrigger && firstPerspective)
        {
            changePerspectiveTrigger = false;
            Vector3 adjustments = new Vector3(-0.7f,-1.399138f,0);
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


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Somebody Touched it!!");
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

}




