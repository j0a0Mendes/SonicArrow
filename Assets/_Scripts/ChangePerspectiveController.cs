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
    
    void Start()
    {
        firstPerspective = true;
        changePerspectiveTrigger = false;
        changeEnabled = true;
    }

    private void Awake()
    {
        interactable = midPointGrabObject.GetComponent<XRGrabInteractable>();
    }

    void Update(){
        if (changePerspectiveTrigger & firstPerspective)
        {
            changePerspectiveTrigger = false;
            xrorigin.position = new Vector3(9.5f,-0.3f,-5.5f);
            firstPerspective = false;
        }
        else
        {
            if(changePerspectiveTrigger & !firstPerspective){
                changePerspectiveTrigger = false;
                 xrorigin.position = new Vector3(-15.273f, 0.082f, -5.23f);

                //ACTIVATE BOW
                interactable.enabled = true;

                //CLEAR FIELD
                GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Arrow");
                foreach (GameObject obj in allObjects)
                {
                    Destroy(obj);
                }

                firstPerspective = true;
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
}




