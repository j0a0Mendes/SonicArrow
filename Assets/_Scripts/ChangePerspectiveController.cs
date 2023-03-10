using System.Collections;
using System.Collections.Generic;
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

    private int numberOfTurns = 0;
    private int points = 0;


    //TARGET REGIONS
    [SerializeField]
    GameObject targetFirstRegion;

    //SYSTEM CONTROLLER
    //public int numberOfTurns; //Turn is defined by two states, according to each perspective
    
    void Start()
    {
        firstPerspective = true;
        changeEnabled = false;
        changePerspectiveTrigger = false;
        bowStringController = GameObject.FindObjectOfType<BowStringController>();
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
            
            xrorigin.position = targetFirstRegion.transform.position;
            //playerCamera.transform.position = targetFirstRegion.transform.position;
            firstPerspective = false;

        }
        else if(changePerspectiveTrigger && !firstPerspective)
        {
            
            changePerspectiveTrigger = false;
            xrorigin.position = new Vector3(-15.273f, 0.082f, -5.23f);

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

            numberOfTurns += 1;
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

    public int getNumberOfTurns()
    {
        return numberOfTurns;
    }
}




