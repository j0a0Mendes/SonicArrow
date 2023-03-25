using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
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

    //TARGET REGIONS
    [SerializeField]
    GameObject targetFirstRegion;

    //SYSTEM CONTROLLER
    //public int numberOfTurns; //Turn is defined by two states, according to each perspective

    //MODE SELECTION
    [SerializeField]
    public int modeSelected = 0;

    void Start()
    {
        firstPerspective = true;
        changeEnabled = false;
        changePerspectiveTrigger = false;
        bowStringController = GameObject.FindObjectOfType<BowStringController>();
        //initialPosition = xrorigin;
        //targetFirstRegion = GameObject.FindGameObjectWithTag("TargetFirstRegion");

    }

    private void Awake()
    {
        //playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        interactable = midPointGrabObject.GetComponent<XRGrabInteractable>();
    }

    void Update(){
        if (changePerspectiveTrigger && firstPerspective)
        {
            changePerspectiveTrigger = false;
            Vector3 adjustments = new Vector3(-0.7f,-1.399138f,0);
            xrorigin.position = targetFirstRegion.transform.position + adjustments;
            //playerCamera.transform.position = targetFirstRegion.transform.position;
            
            firstPerspective = false;

        }
        else if(changePerspectiveTrigger && !firstPerspective)
        {
            
            changePerspectiveTrigger = false;
            //xrorigin.position = new Vector3(-15.273f, 0.082f, -5.23f);
            xrorigin.position = new Vector3(-41.893f, 0.082f, -4.4f);
            //xrorigin.position = initialPosition.position;
            //playerCamera.transform.position = new Vector3(1.39f,0,-0.22f);


            //ACTIVATE CROSSBOW
            //interactable.enabled = true;
            bowStringController.canShootAgain();

            //CLEAR FIELD (OLD VERSION)
            //GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Arrow");
            //foreach (GameObject obj in allObjects)
            //{
                //Destroy(obj);
            //}

            numberOfShots += 1;
            firstPerspective = true;
            
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
}




