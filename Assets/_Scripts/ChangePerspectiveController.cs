using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePerspectiveController : MonoBehaviour
{

    public bool firstPerspective;

    [SerializeField]
    public Transform xrorigin;

    void Start()
    {
        firstPerspective = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Somebody Touched it!!");
        if (firstPerspective)
        {
            xrorigin.position = new Vector3(9.5f,-0.3f,-5.5f);
            firstPerspective = false;
        }
        else
        {
            firstPerspective = true;
        }
        
    }

}




