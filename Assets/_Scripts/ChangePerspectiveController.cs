using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePerspectiveController : MonoBehaviour
{

    public bool firstPerspective;

    [SerializeField]
    public Transform xrorigin;

    [SerializeField]
    public GameObject controllerStand;

    private bool changePerspectiveTrigger;

    private bool changeEnabled;
    
    void Start()
    {
        firstPerspective = true;
        changePerspectiveTrigger = false;
        changeEnabled = true;
    }

    void Update(){
        if (changePerspectiveTrigger & firstPerspective)
        {
            changePerspectiveTrigger = false;
            //controllerStand.transform.position = new Vector3(9.5f,-0.3f,-5.5f);
            xrorigin.position = new Vector3(9.5f,-0.3f,-5.5f);
            firstPerspective = false;
        }
        else
        {
            if(changePerspectiveTrigger & !firstPerspective){
                changePerspectiveTrigger = false;
                //controllerStand.transform.position = new Vector3(-9.5f,0.3f,5.5f);
                xrorigin.position = new Vector3(-20.5f,0.3f,5.5f);
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




